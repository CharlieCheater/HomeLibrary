using HomeLibrary.Infrastructure.Domain.Interfaces;
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
        public async Task<ActionResult> DeleteBook(int id)
        {
            var books = await _context.BooksDAL.DeleteBookAsync(id);
            return Json("success", JsonRequestBehavior.AllowGet);
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