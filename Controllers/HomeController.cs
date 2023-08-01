using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebTimer.Models;
using WebTimer.Models.ViewModels;
using WebTimer.Services.Records.Interfaces;

namespace WebTimer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IRecordService recordService;

        public HomeController(ILogger<HomeController> logger, IRecordService recordService)
        {
            _logger = logger;
            this.recordService = recordService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> InsertRecord([FromBody] HomeViewModel homeViewModel)
        {
            var date = await recordService.InsertRecord(homeViewModel.Status, User);

            return Ok(date.ToString("hh:mm:ss"));
        }
    }
}