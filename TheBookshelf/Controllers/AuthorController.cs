using Microsoft.AspNetCore.Mvc;
using TheBookshelf.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace TheBookshelf.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AuthorController  :Controller
    {
        private IAuthorRepository repository;

        public AuthorController(IAuthorRepository repo)
        {
            repository = repo;
        }

        public ViewResult Authors() => View(repository.Authors);

        public ViewResult Edit(int authorId) => View(repository.Authors.FirstOrDefault(a=>a.AuthorID==authorId));

        [HttpPost]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
                repository.SaveAuthor(author);
                TempData["message"] = $"{author.Name} has been saved";
                return RedirectToAction("Authors");
            }
            return View(author);
        }

        public ViewResult Create() => View("Edit",new Author());

        public IActionResult Delete(long? authorId)
        {
            Author author = repository.DeleteAuthor(authorId);
            if (authorId!=null)
            {
                TempData["message"] = $"{author.Name} was deleted";
            }
            return RedirectToAction("Authors");
        }

        public ViewResult AuthorDetails(int authorId) => View(repository.Authors.FirstOrDefault(a=>a.AuthorID==authorId));

    }
}
