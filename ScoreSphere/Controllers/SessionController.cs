using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ScoreSphere.Models;

public class SessionsController : Controller
{
    private readonly IHubContext<RealTimeHub> _hubContext;
    private readonly ScoreSphereDbContext _context;

    public SessionsController(IHubContext<RealTimeHub> hubContext, ScoreSphereDbContext context)
    {
        _hubContext = hubContext;
        _context = context;
    }
    
    [Route("/Sessions")]
    [HttpPost]
    public IActionResult Create(string email, string password)
    {
        
        User? user = _context.Users.FirstOrDefault(u => u.Email == email && u.Password == password);

        if (user != null)
        {
            HttpContext.Session.SetInt32("user_id", user.Id);

            // Send a real-time update to all clients
            _hubContext.Clients.All.SendAsync("ReceiveRealTimeUpdate", $"User {user.Id} logged in.");

            return new RedirectResult("/");
        }
        else
        {
            return new RedirectResult("/Sessions/New");
        }
    }
}
