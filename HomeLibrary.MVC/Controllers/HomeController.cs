using HomeLibrary.Infrastructure.Domain.Interfaces;
using HomeLibrary.Infrastructure.Models;
using HomeLibrary.MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public async Task<ActionResult> GetBooks(int page, string search)
        {
            var books = await _context.BooksDAL.GetDetailedBooksAsync(search, page - 1);
            return Json(books, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DeleteBook(int id)
        {
            var books = await _context.BooksDAL.DeleteBookAsync(id);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddBook()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AddBook(AddBookViewModel viewModel)
        {
            var book = new Book
            {
                Author = viewModel.Author,
                Title = viewModel.Title,
                Description = viewModel.Description,
                PublicationYear = viewModel.PublicationYear,
                Publisher = viewModel.Publisher,
                TableContents = viewModel.TableContents,
            };
            var books = await _context.BooksDAL.InsertBookAsync(book);
            return Json("success", JsonRequestBehavior.AllowGet);
        }
        public ActionResult BookDetails()
        {
            return View();
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