using ScoreSphere.Models;


public interface IScoreSphereService
{
    Task<IEnumerable<Match>> GetMatchesAsync();
    Task UpdateMatchAsync(MatchUpdateModel model);
}