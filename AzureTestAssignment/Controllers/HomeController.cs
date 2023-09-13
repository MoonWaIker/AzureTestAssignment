using System.Diagnostics;
using AzureTestAssignment.Models;
using AzureTestAssignment.Services.Interfaces;
using AzureTestAssignment.Services.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AzureTestAssignment.Controllers
{
    public class HomeController : Controller
    {
        private const string IndexRoute = "/";
        private const string PrivacyRoute = "/Privacy";
        private readonly IBusinessLogic businessLogic;

        public HomeController(IBusinessLogic businessLogic)
        {
            this.businessLogic = businessLogic;
        }

        [HttpGet(IndexRoute)]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IFormFile file, string email)
        {
            try
            {
                UploadRequest request = new() { File = file, Email = email };
                businessLogic.ProcessRequest(request);

                // Set a success message in TempData
                TempData["SuccessMessage"] = "File uploaded successfully.";
            }
            catch (Exception ex)
            {
                // Set the exception message in TempData
                TempData["ErrorMessage"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }

        [Route(PrivacyRoute)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}