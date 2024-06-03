using bookStore.ViewModel;
using bookStore.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using bookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace bookStore.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public BooksController(ApplicationDbContext context , IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.context = context;
        }

        public IActionResult Index()
        {
            var books = context.Books.Include(book => book.Auther).ToList();
            var categories = context.Categories.ToList();
            var bookCategories = context.BookCategories.ToList();

            var booksVM = books.Select(book => new BookVM
            {
                Id = book.Id,
                Name = book.Name,
                Description = book.Description,
                AutherName = book.Auther.Name,
                Publisher = book.Publisher,
                PublishDate = book.PublishDate,
                imageUrl = book.ImageUrl??"NotProvided",
                CategoriesNames = bookCategories
                    .Where(bookCategoty => bookCategoty.bookId == book.Id)
                    .Select(bookCategoty => categories.FirstOrDefault(category =>
                    category.Id == bookCategoty.categoryId)?.Name ?? "Unknown")
                    .ToList()
            }).ToList();


            return View("Index", booksVM);
        }


        [HttpGet]
        public IActionResult Create()
        {
            var authers = context.Authers.OrderBy(auther => auther.Name).ToList();
            var authersList = authers.Select(a => new SelectListItem
            {
                Value = a.Id.ToString(),
                Text = a.Name
            }).ToList();

            var categories = context.Categories.OrderBy(category => category.Name).ToList();
            var categoriesList = categories.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();

            var viewModel = new BookFormVM
            {
                Authers = authersList,
                Categories = categoriesList
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(BookFormVM bookFormVM)
        {
            string fileName = null!;
            if (bookFormVM.ImageUrl != null)
            {
                fileName = Path.GetFileName(bookFormVM.ImageUrl.FileName);
                var path = Path.Combine($"{webHostEnvironment.WebRootPath}/Images/BooksImages", fileName);
                var stream = System.IO.File.Create(path);
                bookFormVM.ImageUrl.CopyTo(stream);
            }

            var book = new Book
            {
                Name = bookFormVM.Name,
                AutherId = bookFormVM.AutherId,
                Publisher = bookFormVM.Publisher,
                PublishDate = bookFormVM.PublishDate,
                Description = bookFormVM.Description,
                ImageUrl = fileName,
                Categories = bookFormVM.SelectedCategories.Select(id => new BookCategory
                {
                    categoryId = id,
                }).ToList()
            };

            context.Books.Add(book);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var book = context.Books.Find(id);
            if (book == null)
            {
                return NotFound();
            }

            var path = Path.Combine(webHostEnvironment.WebRootPath, "Images/BooksImages" , book.ImageUrl);
            if(Path.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            context.Books.Remove(book);
            context.SaveChanges();
            return Ok();
        }

    }
}
