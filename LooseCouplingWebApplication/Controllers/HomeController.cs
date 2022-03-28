using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LooseCouplingWebApplication.Models;
using LooseCouplingWebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace LooseCouplingWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IService<UserData> _userService;
    private readonly IService<VehicleData> _vehicleService;
    
    public HomeController(ILogger<HomeController> logger, IService<UserData> userService, IService<VehicleData> vehicleService)
    {
        _logger = logger;
        _userService = userService;
        _vehicleService = vehicleService;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }
    
    public IActionResult ReadUser()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult ReadUser(IFormFile formFile)
    {
        if (formFile.Length == 0)  
            return Content("file not selected");

        var userData = _userService.Read(formFile);

        return View(userData);
    }
    
    [HttpGet]
    public IActionResult RegisterUser()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult RegisterUser(RegistrationUserViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index));
        }
        
        _userService.Save(new UserData()
        {
            Email = viewModel.Email,
            Password = viewModel.Password,
            Adress = viewModel.Adress,
            Adress2 = viewModel.Adress2,
            City = viewModel.City,
            State = viewModel.State,
            Zip = viewModel.Zip
        });
        
        return View(nameof(Index));
    }
    
    
    public IActionResult ReadVehicle()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult ReadVehicle(IFormFile formFile)
    {
        if (formFile.Length == 0)  
            return Content("file not selected");

        var vehicleData = _vehicleService.Read(formFile);

        return View(vehicleData);
    }
    
    [HttpGet]
    public IActionResult RegisterVehicle()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult RegisterVehicle(RegistrationVehicleViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(nameof(Index));
        }
        
        _vehicleService.Save(new VehicleData()
        {
            IdentificationNumber = viewModel.IdentificationNumber,
            Manufacturer = viewModel.Manufacturer,
            Model = viewModel.Model,
            Price = viewModel.Price,
        });
        
        return View(nameof(Index));
    }
    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}