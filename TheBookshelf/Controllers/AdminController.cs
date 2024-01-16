using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TheBookshelf.Models;

namespace TheBookshelf.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
    public class AdminController : Controller
    {
        private IBookRepository repository;

        public AdminController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Books() => View(repository.Books);

        public ViewResult Edit(int bookId)
        {
            var dropdownsData = repository.GetDropdownsViewModel();
            ViewBag.Authors = new SelectList(dropdownsData?.Authors, "AuthorID", "Name");
            ViewBag.Categories = new SelectList(dropdownsData?.Categories, "CategoryID", "Name");

            return View(repository.Books.FirstOrDefault(b => b.BookID == bookId));
        }

        [HttpPost]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                repository.SaveBook(book);
                TempData["message"] = $"{book.Name} has been saved";
                return RedirectToAction("Books");
            }
            else
            {
                var dropdownsData = repository.GetDropdownsViewModel();
                ViewBag.Authors = new SelectList(dropdownsData?.Authors, "AuthorID", "Name");
                ViewBag.Categories = new SelectList(dropdownsData?.Categories, "CategoryID", "Name");
                return View(book);
            }
        }

        public ViewResult Create()
        {
            var dropdownsData = repository.GetDropdownsViewModel();
            ViewBag.Authors = new SelectList(dropdownsData?.Authors, "AuthorID", "Name");
            ViewBag.Categories = new SelectList(dropdownsData?.Categories, "CategoryID", "Name");
            return View("Edit",new Book());
        } 

        public IActionResult Delete(long? bookID)
        {
            Book deletedBook=repository.DeleteBook(bookID);
            if (deletedBook != null)
            {
                TempData["message"] = $"{deletedBook.Name} was deleted";
            }
            return RedirectToAction("Books");
        }
    }
}
