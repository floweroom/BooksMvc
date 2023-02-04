using BooksMvc.Models;
using BooksMvc.Repository;
using DbBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;


namespace BooksMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookRepository _bookRepository;
        private readonly DbBook _dbBook;
        public HomeController(ILogger<HomeController> logger, BookRepository bookrepository, DbBook dbBook)
        {
            _logger = logger;
            _bookRepository = bookrepository;
          
        }

        public async Task<IActionResult> Index()

        {
            BookDto bookDto = new BookDto();
            _logger.LogInformation("Стартовая страница");
            var book = await _bookRepository.GetAll();
            return View(book);
        }

       
    }
}