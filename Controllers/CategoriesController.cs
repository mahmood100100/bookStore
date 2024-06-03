using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace bookStore.Controllers
{
    public class CategoriesController : Controller
    {
        ApplicationDbContext context;
        public CategoriesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var categories = context.Categories.ToList();
            return View(categories);
        }
        [HttpGet]
        public IActionResult Create()
        {
            var categories = context.Categories.ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Create(CategotyVM categotyVM)
        {
            if(!ModelState.IsValid)
            {
                return View("Create" , categotyVM);
            }
            var category = new Category()
            {
                Name = categotyVM.Name,
            };
            try
            {
                context.Add(category);
                context.SaveChanges();
                return RedirectToAction("Index");
            } catch {
                ModelState.AddModelError("Name", "Category is already exist");
                return View("Create");
            }
            
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var categoty = context.Categories.Find(id);
            if(categoty is null)
            {
                return NotFound();
            }
            var categotyVM = new CategotyVM()
            {
                Id = id,
                Name = categoty.Name,
            };
            return View("Create" , categotyVM);
        }
        [HttpPost]
        public IActionResult Edit(CategotyVM categotyVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", categotyVM);
            }

            var category = context.Categories.Find(categotyVM.Id);
            if(category is null)
            {
                return NotFound();
            }
            category.Name = categotyVM.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
          var category = context.Categories.Find(id);
          if (category is null)
          {
            return NotFound();
          }

            var categoryVM = new CategotyVM()
            {
                Id = id,
                Name = category.Name,
                CreatedAt = category.CreatedAt,
                UpdatedAt = category.CreatedAt,
            };
           return View("Details" , categoryVM);
        }

        public IActionResult Delete(int id)
        {
            var category = context.Categories.Find(id);
            if (category is null)
            {
                return NotFound();
            }

            context.Categories.Remove(category);
            context.SaveChanges();
            return Ok();
        }

        public IActionResult CheckName(string name)
        {
            var isExist = context.Categories.Any(category => category.Name == name);
            return Json(!isExist);
        }
    }
}