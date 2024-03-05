using Microsoft.AspNetCore.Mvc;



public class HomeController : Controller
{
    public IActionResult Index()
    {
        Console.WriteLine($"ViewBag: {ViewBag.Id}");
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

}

