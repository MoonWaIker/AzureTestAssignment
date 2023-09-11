using System.Diagnostics;
using AzureTestAssignment.Models;
using Microsoft.AspNetCore.Mvc;

namespace AzureTestAssignment.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _logger.LogDebug($"{nameof(HomeController)} was created");
        }

        public IActionResult Index()
        {
            _logger.LogDebug($"Redirected to {nameof(Index)}");
            return View();
        }

        public IActionResult Privacy()
        {
            _logger.LogDebug($"Redirected to {nameof(Privacy)}");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}