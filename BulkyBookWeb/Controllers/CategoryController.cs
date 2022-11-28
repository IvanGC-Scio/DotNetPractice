using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDBContext _dbContext;
        public CategoryController(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategories = _dbContext.Categories.ToList();
            return View(objCategories);
        }
        //GET
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Create(Category newCat)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Add(newCat);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(newCat);
        }
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult EditCategory(Category cat)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Categories.Update(cat);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(cat);
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = _dbContext.Categories.Find(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Delete(Category cat)
        {
            _dbContext.Categories.Remove(cat);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
