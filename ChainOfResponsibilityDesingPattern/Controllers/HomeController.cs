using ChainOfResponsibilityDesingPattern.ChainOfResponsibility;
using ChainOfResponsibilityDesingPattern.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace ChainOfResponsibilityDesingPattern.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _applicationDbContext;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext applicationDbContext)
        {
            _logger = logger;
            _applicationDbContext = applicationDbContext;
        }

        public async Task<IActionResult> SendEmail()
        {
            var products = await _applicationDbContext.Products.ToListAsync();

            ExcelProcessHandler<List<Product>> excelProcessHandler = new();
            ZipProcessHandler<List<Product>> zipProcessHandler = new();

            // email process handler yazýlmadý
            excelProcessHandler.SetNext(zipProcessHandler)/*.SetNext(emailProcessHandler)*/;

            excelProcessHandler.Handle(products);

            return View(nameof(Index));
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
    }
}
