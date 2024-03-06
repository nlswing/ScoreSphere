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

    [Route("admin/NewMatch")]
    [HttpGet]
    public IActionResult NewMatch()
    {
        var currentUserId = HttpContext.Session.GetInt32("user_id");

        // Filter matches by the current user ID
        ViewBag.Matches = _context.Matches.Where(m => m.UserId == currentUserId).ToList();

        return View();
    }
    [Route("/NewMatch")]
    [HttpPost]

    public IActionResult Create(Match match, string team1name, string team2name, IFormFile team1logoFile, IFormFile team2logoFile, DateTime matchdate, TimeSpan matchtime)
    {
        var currentUserId = HttpContext.Session.GetInt32("user_id");

        // Combine date and time to create a DateTime object
        DateTime matchDateTime = matchdate.Date + matchtime;

        // Convert matchDateTime to UTC if it's not already in UTC
        if (matchDateTime.Kind != DateTimeKind.Utc)
        {
            matchDateTime = matchDateTime.ToUniversalTime();
        }


        var team1 = new Team
        {
            Name = team1name,
            Logo = UploadLogo(team1logoFile)
        };


        var team2 = new Team
        {
            Name = team2name,
            Logo = UploadLogo(team2logoFile)
        };


        _context.Teams.Add(team1);
        _context.Teams.Add(team2);
        _context.SaveChanges();


        match.Date = matchdate;
        match.Team1Id = team1.Id;
        match.Team2Id = team2.Id;
        match.Team1Goals = 0;
        match.Team2Goals = 0;
        match.Team1Logo = team1.Logo;
        match.Team2Logo = team2.Logo;
        match.Date = matchDateTime;
        match.UserId = currentUserId;


        _context.Matches.Add(match);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

    private string UploadLogo(IFormFile logoFile)
    {
        if (logoFile != null && logoFile.Length > 0)
        {
            // Process the uploaded file (save to disk, database, etc.)
            // For simplicity, save it to wwwroot/images folder with a unique filename
            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(logoFile.FileName)}";
            var logoPath = $"wwwroot/images/{uniqueFileName}";

            using (var stream = new FileStream(logoPath, FileMode.Create))
            {
                logoFile.CopyTo(stream);
            }

            // Return only the relative path
            return $"{uniqueFileName}";
        }

        return null;
    }

    [Route("admin/EditMatch/{id}")]
    [HttpGet]
    public IActionResult EditMatch(int id)
    {
        // Fetch the match details based on the id
        var match = _context.Matches.FirstOrDefault(m => m.Id == id);

        if (match == null || match.UserId != HttpContext.Session.GetInt32("user_id"))
        {
            // Handle unauthorized access or non-existing match
            return RedirectToAction("Index");
        }

        return View(match);
    }

    [Route("admin/DeleteMatch")]
    [HttpPost]
    public IActionResult DeleteMatch(int matchId)
    {
        var match = _context.Matches.FirstOrDefault(m => m.Id == matchId);

        if (match == null || match.UserId != HttpContext.Session.GetInt32("user_id"))
        {
            // Handle unauthorized access or non-existing match
            return RedirectToAction("Index");
        }

        _context.Matches.Remove(match);
        _context.SaveChanges();

        return RedirectToAction("Index");
    }

}