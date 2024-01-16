using Microsoft.EntityFrameworkCore;
using TheBookshelf.Models.ViewModels;

namespace TheBookshelf.Models
{
    public class EFBookRepository :IBookRepository
    {
        private StoreDbContext context;
        public EFBookRepository(StoreDbContext ctx) 
        {
            context = ctx;
        }
        public IQueryable<Book> Books => context.Books.Include(b=>b.Category);

        public void SaveBook(Book? b)
        {

            if (b?.BookID == null)
            {
                context.Books.Add(b!);
            }
            else
            {
                Book? db = context.Books.FirstOrDefault(x => x.BookID == b.BookID);
                if (db != null)
                {
                    db.Name = b.Name;
                    db.Price = b.Price;
                    db.Available = b.Available;
                    db.Pages = b.Pages;
                    db.Date = b.Date;
                    db.Description = b.Description;
                    db.img = b.img;

                    db.Category = b.Category;
                    db.CategoryID = b.CategoryID;
                    db.Author = b.Author;
                    db.AuthorID = b.AuthorID;
                }
            }
            context.SaveChanges();
        }

        public Book DeleteBook(long? bookID)
        {
            Book? db = context.Books.FirstOrDefault(b=>b.BookID==bookID);

            if (db!=null)
            {
                context.Books.Remove(db);
                context.SaveChanges();
            }
            return db!;
        }

        public DropdownsViewModel GetDropdownsViewModel()
        {
            var dropdowns = new DropdownsViewModel()
            {
                Authors=context.Authors.OrderBy(a=>a.Name).ToList(),
                Categories=context.Categories.OrderBy(c=>c.Name).ToList()
            };
            return dropdowns;
        }
    }
}
