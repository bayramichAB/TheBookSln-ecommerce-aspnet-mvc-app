using Microsoft.AspNetCore.Mvc;
using TheBookshelf.Models;

namespace TheBookshelf.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private ICategoryRepository repository;
        public NavigationMenuViewComponent(ICategoryRepository repo)
        {
            repository = repo;
        }

        public IViewComponentResult Invoke()
        {
            ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.Categories
                .Select(x => x.Name)
                .Distinct()
                .OrderBy(x => x));
        }
    }
}
