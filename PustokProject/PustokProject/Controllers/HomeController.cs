using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.DAL;
using PustokProject.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PustokProject.Controllers
{
    public class HomeController : Controller
    {
        // GET: /<controller>/
        private readonly AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            HomeViewModel homeVM = new HomeViewModel
            {
                Sliders = _context.Sliders.OrderBy(x => x.Order).ToList(),
                DiscountedBooks = _context.Books
                                .Include(x => x.BookImages).Include(x => x.Author)
                                .Where(x => x.DiscountPercent > 0).Take(20).ToList(),
                FeaturedBooks = _context.Books
                                .Include(x => x.BookImages).Include(x => x.Author)
                                .Where(x => x.IsFeatured).Take(20).ToList(),
                NewBooks = _context.Books
                                .Include(x => x.BookImages).Include(x => x.Author)
                                .Where(x => x.IsNew).Take(20).ToList()
            };

            return View(homeVM);
        }

    }
}

