using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TheBookshelf.Models;
using TheBookshelf.Models.ViewModels;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Xml.Linq;

namespace TheBookshelf.Controllers
{
    public class HomeController : Controller
    {
        private IBookRepository repository;
        public int PageSize = 4;

        public HomeController(IBookRepository repo)
        {
            repository = repo;
        }

        public ViewResult Index(string? category, string? search, int productPage = 1)
        {
           
            if ( search!=null)
            {       
                search = search.Replace(" ","");
                return View(new BooksListViewModel
                {
                    Books = repository.Books.Where(b => b.Name.Replace(" ", "").Contains(search) || b.Author!.Name.Replace(" ", "").Contains(search)).Include(a => a.Author),

                    PagingInfo = new PagingInfo
                    {
                        CurrentPage = productPage,
                        ItemsPerPage = PageSize,
                        TotalItems = repository.Books.Where(b => b.Name.Replace(" ", "").Contains(search) || b.Author!.Name.Replace(" ", "").Contains(search)).Include(a => a.Author).Count()
                    }
                });
            }

            return View(new BooksListViewModel
            {
                Books = repository.Books.Where(b => category == null || b.Category!.Name == category).Include(a => a.Author)
                .OrderBy(p => p.BookID)
                .Skip((productPage - 1) * PageSize)
                .Take(PageSize),

                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ?
                    repository.Books.Count() :
                    repository.Books.Where(e => e.Category!.Name == category).Count()
                },

                CurrentCategory = category
            });

        }

        public ViewResult Details(int bookId)=> View(repository.Books.Include(a=>a.Author).FirstOrDefault(b => b.BookID == bookId));
    }
}
