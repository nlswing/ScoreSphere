using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScoreSphere.Models;

namespace ScoreSphere.Controllers;

[ApiController]
// [Authorize]
public class MatchController : ControllerBase
{
    private readonly IScoreSphereService _scoresphereService;  
 
    public MatchController(IScoreSphereService scoresphereService)
    {
        _scoresphereService = scoresphereService; 
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

}