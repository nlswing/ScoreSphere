using Microsoft.EntityFrameworkCore;
using ScoreSphere.Models;


public class ScoreSphereService : IScoreSphereService
{
    private readonly ScoreSphereDbContext _context;
 
    public ScoreSphereService(ScoreSphereDbContext context)
    {
        _context = context;
    }
 // GetMatchesAsync method uses LINQ to join both Teams and Matches table using the Team1 and Team2 foreign keys and then
 // returns the list of MatchViewModel class objects.
    public async Task<IEnumerable<Match>> GetMatchesAsync()
    {
        var query = from m in _context.Matches
            join t1 in _context.Teams on m.Team1Id equals t1.Id
            join t2 in _context.Teams on m.Team2Id equals t2.Id
            select new Match()
            {
                Id = m.Id,
                Team1Id = t1.Id,
                Team2Id = t2.Id,
                Team1Name = t1.Name,
                Team2Name = t2.Name,
                Team1Logo = t1.Logo,
                Team2Logo = t2.Logo,
                Team1Goals = m.Team1Goals ?? 0,
                Team2Goals = m.Team2Goals ?? 0
            };
        return await query.ToListAsync();
    }

    // UpdateMatchAsync method first queries particular match information from the database and then it will add 1 goal in either Team1 or Team 2
    // depending on the TeamId value passes to it from the front end. If the admin adds a goal in Team1
    // then the Team1 goals will += 1/
 
    public async Task UpdateMatchAsync(MatchUpdateModel model)
    {
        var match = _context.Matches.FirstOrDefault(x => x.Id == model.MatchId);
        if (match != null)
        {
            if (model.TeamId == match.Team1Id)
            {
                match.Team1Goals = (match.Team1Goals ?? 0) + 1;
            }
 
            if (model.TeamId == match.Team2Id)
            {
                match.Team2Goals = (match.Team2Goals ?? 0) + 1;
            }
 
            _context.Matches.Update(match);
            await _context.SaveChangesAsync(); 
        }
    }
}