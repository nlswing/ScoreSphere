using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreSphere.Models;

namespace ScoreSphere.Controllers;

[ApiController]
// [Authorize]
public class MatchController : ControllerBase
{
    private readonly IScoreSphereService _scoresphereService;
    private bool LoopInProgress = false;
    private Timer _updateTimer;

    private readonly ScoreSphereDbContext _context;

    public MatchController(IScoreSphereService scoresphereService, ScoreSphereDbContext context)
    {
        _scoresphereService = scoresphereService;
        _context = context;
    }




    [HttpGet]
    [Route("api/Matches")]
    public async Task<IEnumerable<Match>> GetMatchesAsync()
    {
        return await _scoresphereService.GetMatchesAsync();
    }


    [HttpPut]
    [Route("api/Matches")]
    public async Task UpdateMatchAsync(MatchUpdateModel model)
    {
        await _scoresphereService.UpdateMatchAsync(model);
    }


    //match updating
    private bool IsFixtureLive(Match match, DateTime currentTime)
    {
        DateTime fixtureStartTime = match.Date ?? DateTime.MinValue;
        return fixtureStartTime < currentTime && fixtureStartTime.AddMinutes(90) > currentTime;
    }

    [HttpGet("api/UpdateLiveFixtures")]
    public void UpdateLiveFixtures()
    {
        if (_updateTimer == null)
        {
            // Set up the timer to call UpdateLiveFixturesAsync every minute
            _updateTimer = new Timer(UpdateLiveFixturesAsync, null, 0, 60 * 1000);
        }
    }

    private void UpdateLiveFixturesAsync(object? state)
    {
        _ = UpdateLiveFixturesAsync();
    }
    [Route("api/[controller]/[action]")]
    public async Task UpdateLiveFixturesAsync()
    {

        Console.WriteLine("Running after a minute!");
        Task<IEnumerable<Match>> allMatchesTask = _scoresphereService.GetMatchesAsync();
        // Wait for the asynchronous operation to complete and get the matches
        IEnumerable<Match> allMatches = await allMatchesTask;
        // going through all matches to see if it's live
        DateTime currentTime = DateTime.Now;
        foreach (Match match in allMatches)
        {
            if (match.UserId == null && IsFixtureLive(match, currentTime))
            {
                // Fixture is live, randomize if team1 or team2 has scored
                Random random = new Random();
                if (random.Next(2) == 0)
                {
                    match.Team1Goals += 1;
                }
                else
                {
                    match.Team2Goals += 1;
                }
                _context.Matches.Update(match);
                Console.WriteLine($"Goal scored in match {match.Id}: {match.Team1Name} {match.Team1Goals} - {match.Team2Goals} {match.Team2Name}");
            }
        }
        await _context.SaveChangesAsync();
    }
}
