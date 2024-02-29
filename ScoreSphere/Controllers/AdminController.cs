using Microsoft.AspNetCore.Mvc;
namespace ScoreSphere.Controllers;

public class AdminController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
