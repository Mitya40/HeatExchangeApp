
using HeatExchangeApp.Models;
using HeatExchangeApp.Models.Additional_Classes;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;
using System.Security.Claims;

namespace WebTeploobmenApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DataBaseContext _context;

        public HomeController(ILogger<HomeController> logger, DataBaseContext context)
        {
            _logger = logger;
            _context = context;
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var data = _context.InitData.FirstOrDefault(x => x.Id == id && (x.IdUser == GetUserId() || x.IdUser == null));
            if (data != null)
            {
                _context.InitData.Remove(data);
                _context.SaveChanges();

            }

            return RedirectToAction("Index");
        }
        public IActionResult Index()
        {
            var datainput = _context.InitData.Where(u => u.IdUser == null || u.IdUser == GetUserId()).ToList();
            return View(datainput);
        }

        [HttpGet]
        public IActionResult Results(int id)
        {
            var variant = _context.InitData.FirstOrDefault(x => x.Id == id);
            var viewModel = new HomeViewModel();
            if (variant != null)
            {
                viewModel.LayerHeight = variant.LayerHeight;
                viewModel.InitTempMaterial = variant.InitTempMaterial;
                viewModel.InitTempGas = variant.InitTempGas;
                viewModel.GasSpeed = variant.GasSpeed;
                viewModel.AvgGasHeatCapacity = variant.AvgGasHeatCapacity;
                viewModel.MaterialRate = variant.MaterialRate;
                viewModel.MaterialHeatCapacity = variant.MaterialHeatCapacity;
                viewModel.VolCoeffHeatTransfer = variant.VolCoeffHeatTransfer;
                viewModel.MachineDiameter = variant.MachineDiameter;
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Results(Calculations calculations, string action)
        {
            var results = calculations.CalculateResults();
            var viewModel = new HomeViewModel
            {
                Results = results,
                LayerHeight = calculations.LayerHeight,
                InitTempMaterial = calculations.InitTempMaterial,
                InitTempGas = calculations.InitTempGas,
                GasSpeed = calculations.GasSpeed,
                AvgGasHeatCapacity = calculations.AvgGasHeatCapacity,
                MaterialRate = calculations.MaterialRate,
                MaterialHeatCapacity = calculations.MaterialHeatCapacity,
                VolCoeffHeatTransfer = calculations.VolCoeffHeatTransfer,
                MachineDiameter = calculations.MachineDiameter
            };

            if (action == "add")
            {
                _context.InitData.Add(new InitData
                {
                    IdUser = GetUserId(),
                    LayerHeight = calculations.LayerHeight,
                    InitTempMaterial = calculations.InitTempMaterial,
                    InitTempGas = calculations.InitTempGas,
                    GasSpeed = calculations.GasSpeed,
                    AvgGasHeatCapacity = calculations.AvgGasHeatCapacity,
                    MaterialRate = calculations.MaterialRate,
                    MaterialHeatCapacity = calculations.MaterialHeatCapacity,
                    VolCoeffHeatTransfer = calculations.VolCoeffHeatTransfer,
                    MachineDiameter = calculations.MachineDiameter
                });
                _context.SaveChanges(); // Сохраняем только при нажатии "Добавить"
            }
            return View(viewModel);
        }

        private int? GetUserId()
        {
            var idUserString = User.FindFirst("id_user")?.Value;
            int? idUser = idUserString == null ? null : int.Parse(idUserString);
            return idUser;

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
