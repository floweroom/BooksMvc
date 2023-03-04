using BooksMvc.Models;
using DbBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using BooksMvc.Repository.IInterfaces;

namespace BooksMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly DbBook _dbBook;
        public HomeController(ILogger<HomeController> logger, IBookRepository bookrepository, DbBook dbBook)
        {
            _logger = logger;
            _bookRepository = bookrepository;
          
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Timer = DateTime.Now.AddHours(10);
            return View();
        }

        public IActionResult GetTime()
        {
            return Ok(DateTime.Now);
        }

       
    }
}