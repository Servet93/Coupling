using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CouplingWebApplication.Models;
using CouplingWebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CouplingWebApplication.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserService _userService = new UserService();
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
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
    public IActionResult RegisterUser(RegistrationViewModel viewModel)
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

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
    }
}