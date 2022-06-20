using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.DAL;
using PustokProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PustokProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class GenreController : Controller
    {

        private readonly AppDbContext _context;

        public GenreController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var data = _context.Genres.Include(x=>x.Books).ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(_context.Genres.Any(x=>x.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "This genre is already exist!");
                return View();
            }

            _context.Genres.Add(genre);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {

            Genre genre = _context.Genres.FirstOrDefault(x=>x.Id == id);

            if(genre == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            return View(genre);
        }

        [HttpPost]
        public IActionResult Edit(int id, Genre genre)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if(_context.Genres.Any(x=>x.Id!=id && x.Name == genre.Name))
            {
                ModelState.AddModelError("Name", "This genre is already exist");
                return View();
            }

            Genre existGenre = _context.Genres.FirstOrDefault(x=>x.Id == genre.Id);

            if(existGenre == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            existGenre.Name = genre.Name;

            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            Genre existGenre = _context.Genres.FirstOrDefault(x => x.Id == id);

            if(existGenre == null)
            {
                return NotFound();
            }

            _context.Genres.Remove(existGenre);
            _context.SaveChanges();

            return Ok();
        }
    }
}

