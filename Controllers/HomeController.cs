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

        public async Task<IActionResult> Index()
        {
            HomeViewModel viewModel = new HomeViewModel();

            var timesByCategotries = await recordService.GetRecordsAndTimesToday(User);

            viewModel.TimesByCategories.Add("Trabalho", timesByCategotries[1]);
            viewModel.TimesByCategories.Add("Projetos", timesByCategotries[2]);
            viewModel.TimesByCategories.Add("Estudos", timesByCategotries[3]);
            viewModel.TimesByCategories.Add("Pessoal", timesByCategotries[4]);
            viewModel.TimesByCategories.Add("Entretenimento", timesByCategotries[5]);

            viewModel.OpenRecord = recordService.GetOpen(User);

            return View(viewModel);
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
            if (homeViewModel == null)
                return BadRequest("Nenhuma atividade selecionada!");

            var date = await recordService.InsertRecord(homeViewModel.Status, User);

            var openRecord = recordService.GetOpen(User);

            return Ok(date.ToString("hh:mm:ss") + $"|{openRecord?.RecordId}");
        }
    }
}