using bookStore.Data;
using bookStore.Models;
using bookStore.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Collections.Generic;

namespace bookStore.Controllers
{
    public class AuthersController : Controller
    {
        public readonly ApplicationDbContext context;

        public AuthersController(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var authers = context.Authers.ToList();
            var authersVM = authers.Select(auther => new AutherVM
            {
                Id = auther.Id,
                Name = auther.Name,
                CreatedAt = auther.CreatedAt,
                UpdatedAt = auther.UpdatedAt,
            }).ToList();

            return View("Index", authersVM);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View("Form", new AutherFormVM());
        }

        [HttpPost]
        public IActionResult Create(AutherFormVM autherFormVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", autherFormVM);
            }

            var auther = new Auther
            {
                Name = autherFormVM.Name,
            };

            try
            {
                context.Authers.Add(auther);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("Name", "Auther already exists");
                return View("Form", autherFormVM);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var auther = context.Authers.Find(id);
            if (auther == null)
            {
                return NotFound();
            }

            var autherFormVM = new AutherFormVM
            {
                Id = auther.Id,
                Name = auther.Name,
            };

            return View("Form", autherFormVM);
        }

        [HttpPost]
        public IActionResult Edit(AutherFormVM autherFormVM)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", autherFormVM);
            }

            var auther = context.Authers.Find(autherFormVM.Id);
            if (auther == null)
            {
                return NotFound();
            }

            auther.Name = autherFormVM.Name;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Details(int id)
        {
            var auther = context.Authers.Find(id);
            if (auther == null)
            {
                return NotFound();
            }

            var autherVM = new AutherVM
            {
                Id = auther.Id,
                Name = auther.Name,
                CreatedAt = auther.CreatedAt,
                UpdatedAt = auther.UpdatedAt,
            };

            return View("Details", autherVM);
        }

        public IActionResult Delete(int id)
        {
            var auther = context.Authers.Find(id);
            if (auther == null)
            {
                return NotFound();
            }

            context.Authers.Remove(auther);
            context.SaveChanges();
            return Ok();
        }

        public IActionResult CheckName(string name)
        {
            var isExist = context.Authers.Any(auther => auther.Name == name);
            return Json(!isExist);
        }
    }
}
