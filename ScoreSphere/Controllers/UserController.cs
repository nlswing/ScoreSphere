using Microsoft.AspNetCore.Mvc;
using ScoreSphere.Models;

public class UserController : Controller
{
    private readonly ScoreSphereDbContext _context;

    public UserController(ScoreSphereDbContext context)
    {
        _context = context;
    }


    [Route("/signup")]
    [HttpGet]
    public IActionResult New()
    {
        return View();
    }

    public string Feedback = string.Empty;

    [Route("/users")]
    [HttpPost]
    public RedirectResult Create(User user)
    {
        bool email_Exists = false;
        string Feedback = string.Empty;
        foreach (User u in _context.Users)
        {
            if (u.Email == user.Email)
            {
                email_Exists = true;
                break;
            }
        }
        if (email_Exists)
        {
            TempData["MESSAGE"] = "Email already assigned to account.";
            return new RedirectResult("/signup");
        }
        else if (user.CheckValidPassword() == false)
        {
            TempData["MESSAGE"] = "Invalid Password: Requirements -> (1 Special Character, 1 Uppercase, 1 Number)";
            return new RedirectResult("/signup");
        }
        else
        {
            user.Photo = "https://creativeandcultural.files.wordpress.com/2018/04/default-profile-picture.png?w=256";
            user.Points = 0;
            user.Achievements = string.Empty;
            user.MatchesAttended = 0;
            _context.Users.Add(user);
            _context.SaveChanges();

            HttpContext.Session.SetInt32("user_id", user.Id);
            return new RedirectResult("api/Matches");
        }
    }

}

