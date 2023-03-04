using BooksMvc.Models;
using BooksMvc.Repository.IInterfaces;
using DbBooks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BooksMvc.Controllers
{
    public class BookController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookRepository _bookRepository;
        private readonly DbBook _dbBook;
        public BookController(ILogger<HomeController> logger, IBookRepository bookrepository, DbBook dbBook)
        {
            _logger = logger;
            _bookRepository = bookrepository;
            _dbBook = dbBook;
        }
        public async Task<IActionResult> Index()
        {
            BookDto bookDto = new BookDto();
            _logger.LogInformation("Стартовая страница");
            var book = await _bookRepository.GetAll();
            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Add()
        {

            return View(new AddBook());
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Add(AddBook addBook)
        {
            await _bookRepository.Add(addBook);
            _logger.LogInformation("Добавили книжечку");

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var Books = await _bookRepository.GetAll();
            _logger.LogInformation("Показали все результаты");
            return View(Books);
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> DelBook(int id)

        {

            var book = await _bookRepository.GetId(id);

            if (book == null)
            {
                return NotFound();
            }
            Delete delete = new Delete()
            {
                Id = id,
            };

            return View("DelBook", delete);



        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Del(int id)
        {
            if (id != null)
            {
                await _bookRepository.Delete(id);
                _logger.LogInformation("Удалили книжку");
                return RedirectToAction("Index");
            }
            return NotFound();
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> UpdateBook(int id)
        {
            if (id != null)
            {
                Book book = await _dbBook.Books.FirstOrDefaultAsync(x => x.Id == id);
                if (book != null)
                {
                    Update update = new Update()
                    {
                        Name = book.Name,
                        Author = book.Author,
                        Pages = book.Pages,
                    };
                    ViewBag.Id = book.Id;
                    return View(update);
                }


            }
            return NotFound();
        }


        [Authorize]
        [HttpPost]
        public IActionResult UpdateBook(int Id, Update update)

        {
            if (update != null)

            {
                _bookRepository.Update(Id, update);


            }
            _logger.LogInformation("Поменяли книжку");
            return RedirectToAction("Index");

        }
        //[HttpGet]
        //public IActionResult GetId([FromQuery]int id)
        //{
        //  //  _bookRepository.GetId(id);
        //  var Book = _bookRepository.GetId(id);
        //    _logger.LogInformation("Нашли по Id");
        //    return View(Book);
        //}

        [HttpGet]
        public IActionResult FindBook()
        {
            return View(new FindBookDto());
        }

        [HttpPost]
        public IActionResult FindBook(FindBookDto findBookDto)
        {
            var Books = _bookRepository.Find(findBookDto);
            return View("FindResults", Books);
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
    

