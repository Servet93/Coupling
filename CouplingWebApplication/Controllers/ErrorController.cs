using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CouplingWebApplication.Models;
using CouplingWebApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CouplingWebApplication.Controllers;

public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;
    private readonly UserService _userService = new UserService();
    
    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }
}