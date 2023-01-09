﻿using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BlogProject.Models;
using BlogProject.Repositories;

namespace BlogProject.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IPostsRepository _postsRepository; 

    public HomeController(ILogger<HomeController> logger, IPostsRepository postsRepository)
    {
        _logger = logger;
        _postsRepository = postsRepository;
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