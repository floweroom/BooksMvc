using BooksMvc.Models;
using BooksMvc.Repository;
using DbBooks;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BooksMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly BookRepository _bookRepository;
        public HomeController(ILogger<HomeController> logger, BookRepository bookrepository)
        {
            _logger = logger;
            _bookRepository = bookrepository;
        }

        public IActionResult Index()

        {
            BookDto bookDto = new BookDto();
            return View(bookDto);
        }
        [HttpPost]
        public IActionResult Add([FromBody]BookDto bookDto)
        {
           _bookRepository.Add(bookDto);

            return View("Index", bookDto);
                        

        }
        [HttpGet]
        public IActionResult Get()
        {
            _bookRepository.GetAll();
            return View();
        }
        [HttpDelete]
        public IActionResult Del ([FromRoute]int id)
        {
            _bookRepository.Delete(id);
            return View("Index", id);
        }
        [HttpGet]
        public IActionResult GetId([FromQuery]int id)
        {
            _bookRepository.GetId(id);
            return View("Index", id);
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