using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokProject.DAL;
using PustokProject.Helpers;
using PustokProject.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PustokProject.Areas.Manage.Controllers
{
    [Area("Manage")]
    public class BookController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public BookController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var data = _context.Books.Include(x=>x.Genre).Include(x=>x.Author).ToList();
            return View(data);
        }

        public IActionResult Create()
        {
            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();


            return View();
        }

        [HttpPost]
        public IActionResult Create(Book book)
        {

            if (!_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author is not found");
            }

            if (!_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre is not found");
            }
            

            CheckCreatingPosterFiles(book);
            CheckCreatingHoverPosterFiles(book);
            CheckImageFiles(book);
            CheckTags(book);
            

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Tags = _context.Tags.ToList();

                return View();
            }

            BookImage bookPosterImage = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "uploads/books", book.PosterFile),
                PosterStatus = true
            };

            BookImage bookHoverPosterImage = new BookImage
            {
                Name = FileManager.Save(_env.WebRootPath, "uploads/books", book.HoverPosterFile),
                PosterStatus = false
            };

            book.BookImages.Add(bookPosterImage);
            book.BookImages.Add(bookHoverPosterImage);

            AddImageFIles(book, book.ImageFiles);

            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    BookTag bookTag = new BookTag
                    {
                        TagId = tagId
                    };

                    book.BookTags.Add(bookTag);
                }
            }

            _context.Books.Add(book);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            Book book = _context.Books.Include(x=>x.BookImages).Include(x=>x.BookTags).FirstOrDefault(x=>x.Id == id);

            if(book == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            ViewBag.Authors = _context.Authors.ToList();
            ViewBag.Genres = _context.Genres.ToList();
            ViewBag.Tags = _context.Tags.ToList();

            book.TagIds = book.BookTags.Select(x => x.TagId).ToList();

            return View(book);
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            Book existBook = _context.Books.Include(x => x.BookImages).Include(x=>x.BookTags).FirstOrDefault(x => x.Id == book.Id);

            if (existBook == null)
            {
                return RedirectToAction("error", "dashboard");
            }

            if (existBook.AuthorId!=book.AuthorId && !_context.Authors.Any(x => x.Id == book.AuthorId))
            {
                ModelState.AddModelError("AuthorId", "Author is not found");
            }

            if (existBook.GenreId!=book.GenreId && !_context.Genres.Any(x => x.Id == book.GenreId))
            {
                ModelState.AddModelError("GenreId", "Genre is not found");
            }

            if (book.PosterFile != null)
            {
                CheckPosterFiles(book);
            }

            CheckImageFiles(book);
            CheckTags(book);

            if (!ModelState.IsValid)
            {
                ViewBag.Authors = _context.Authors.ToList();
                ViewBag.Genres = _context.Genres.ToList();
                ViewBag.Tags = _context.Tags.ToList();



                return View();
            }

            List<string> deletedFiles = new List<string>();

            if(book.PosterFile != null)
            {
                BookImage poster = existBook.BookImages.FirstOrDefault(x=>x.PosterStatus == true);
                deletedFiles.Add(poster.Name);
                poster.Name = FileManager.Save(_env.WebRootPath, "uploads/books", book.PosterFile);
            }

            existBook.BookTags.RemoveAll(x=>!book.TagIds.Contains(x.TagId));

            foreach (var tagId in book.TagIds.Where(x=>existBook.BookTags.Any(bt=>bt.TagId==x)))
            {
                BookTag bookTag = new BookTag
                {
                    TagId = tagId
                };

                existBook.BookTags.Add(bookTag);
            }

            AddImageFIles(existBook, book.ImageFiles);

            existBook.Rate = book.Rate;
            existBook.Name = book.Name;
            existBook.PageSize = book.PageSize;
            existBook.SubDesc = book.SubDesc;
            existBook.Desc = book.Desc;
            existBook.GenreId = book.GenreId;
            existBook.AuthorId = book.AuthorId;
            existBook.CostPrice = book.CostPrice;
            existBook.SalePrice = book.SalePrice;
            existBook.DiscountPercent = book.DiscountPercent;

            _context.SaveChanges();

            FileManager.DeleteAll(_env.WebRootPath, "uploads/books", deletedFiles);

            return RedirectToAction("Index");
        }




        //Private helper methods

        private void CheckPosterFiles(Book book)
        {
                if (book.PosterFile.ContentType != "image/png" && book.PosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("PosterFile", "File format must be image/png or image/jpeg!");
                }

                if (book.PosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("PosterFile", "File size should be less than 2MB!");
                }
        }

        private void CheckHoverPosterFiles(Book book)
        {
                if (book.HoverPosterFile.ContentType != "image/png" && book.HoverPosterFile.ContentType != "image/jpeg")
                {
                    ModelState.AddModelError("HoverPosterFile", "File format must be image/png or image/jpeg!");
                }

                if (book.HoverPosterFile.Length > 2097152)
                {
                    ModelState.AddModelError("HoverPosterFile", "File size should be less than 2MB!");
                }
        }

        private void CheckImageFiles(Book book)
        {
            if (book.ImageFiles != null)
            {
                foreach (var file in book.ImageFiles)
                {
                    if (file.ContentType != "image/png" && file.ContentType != "image/jpeg")
                    {
                        ModelState.AddModelError("ImageFiles", "File format must be image/png or image/jpeg!");
                    }

                    if (file.Length > 2097152)
                    {
                        ModelState.AddModelError("ImageFiles", "File size should be less than 2MB!");
                    }
                }
            }

        }

        private void CheckTags(Book book)
        {
            if (book.TagIds != null)
            {
                foreach (var tagId in book.TagIds)
                {
                    if (!_context.Tags.Any(x => x.Id == tagId))
                    {
                        ModelState.AddModelError("TagIds", "There is no such tag id!");
                        return;
                    }
                }
            }
        }

        private void AddImageFIles(Book book, List<IFormFile> images)
        {
            if (images != null)
            {

                foreach (var file in images)
                {
                    BookImage bookImage = new BookImage
                    {
                        Name = FileManager.Save(_env.WebRootPath, "uploads/books", file),
                        PosterStatus = null
                    };

                    book.BookImages.Add(bookImage);
                }
            }
        }

        private void CheckCreatingPosterFiles(Book book)
        {
            if (book.PosterFile == null)
            {
                ModelState.AddModelError("PosterFile", "PosterFile is required!");
            }
            else
            {
                CheckPosterFiles(book);
            }
        }

        private void CheckCreatingHoverPosterFiles(Book book)
        {
            if (book.HoverPosterFile == null)
            {
                ModelState.AddModelError("HoverPosterFile", "PosterFile is required!");
            }
            else
            {
                CheckHoverPosterFiles(book);
            }
        }
    }
}

