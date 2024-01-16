using Microsoft.AspNetCore.Mvc;
using TheBookshelf.Models;

namespace TheBookshelf.Controllers
{
    public class CategoryController :Controller
    {
        private ICategoryRepository repository;
        public CategoryController(ICategoryRepository repo)
        {
            repository = repo;
        }

        public ViewResult Categories() => View(repository.Categories);

        public ViewResult Edit(int categoryId) => View(repository.Categories.FirstOrDefault(c=>c.CategoryID== categoryId));

        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                repository.SaveCategory(category);
                TempData["message"] = $"{category.Name} has been saved";
                return RedirectToAction("Categories");
            }
            return View(category);
        }

        public ViewResult Create() => View("Edit",new Category());

        public IActionResult Delete(long? categoryId)
        {
            Category category=repository.DeleteCategory(categoryId);
            if (category!=null)
            {
                TempData["message"] = $"{category.Name} was deleted";
            }
            return RedirectToAction("Categories");
        }
    }
}
