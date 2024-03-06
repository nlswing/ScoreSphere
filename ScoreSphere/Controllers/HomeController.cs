using Microsoft.AspNetCore.Mvc;
using ScoreSphere.Models;



public class HomeController : Controller
{

    private readonly ScoreSphereDbContext _context;

    public HomeController(ScoreSphereDbContext context)
    {
        _context = context;
    }
    public IActionResult Index()
    {
        var currentUserId = HttpContext.Session.GetInt32("user_id");
        if (currentUserId != null){
            User viewerUser = _context.Users.Find(currentUserId);
            ViewBag.currentUser = viewerUser;
        }
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}

