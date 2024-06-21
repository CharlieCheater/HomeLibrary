using HomeLibrary.DAL.Domain.Interfaces;
using HomeLibrary.DAL.Models;
using HomeLibrary.MVC.Globals;
using HomeLibrary.MVC.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace HomeLibrary.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDbContext _context;
        public HomeController(IDbContext context)
        {
            _context = context;
        }
        public ActionResult Index()
        {
            return View();
        }
        public async Task<ActionResult> Book(int id)
        {
            var book = await _context.BooksDAL.GetBookByIdAsync(id);
            BookViewModel vm = new BookViewModel
            {
                Author = book.Author,
                Description = book.Description,
                Id = id,
                PublicationYear = book.PublicationYear,
                Publisher = book.Publisher,
                TableContents = book.TableContents,
                Title = book.Title
            };
            return View(vm);
        }
        public async Task<ActionResult> GetBooks(string search)
        {
            var books = await _context.BooksDAL.GetDetailedBooksAsync(search);
            return Json(books, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DeleteBook(int id)
        {
            var deleted = await _context.BooksDAL.DeleteBookAsync(id);

            if (deleted)
                return Json(new { Text = "OK" }, JsonRequestBehavior.AllowGet);

            Response.StatusCode = 400;
            return Json(new { Text = "Не получилось удалить" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddBook(AddBookViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var book = new Book
            {
                Author = viewModel.Author,
                Title = viewModel.Title,
                Description = viewModel.Description,
                PublicationYear = viewModel.PublicationYear,
                Publisher = viewModel.Publisher,
                TableContents = viewModel.TableContents,
            };
            var result = await _context.BooksDAL.InsertBookAsync(book);

            if(!result)
            {
                ModelState.AddModelError("all", ErrorMessages.ExceptionOnInsertData);
                return View(viewModel);
            }
            return RedirectToAction(nameof(Book), new { id = book.Id });
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}