using TheBookshelf.Models.ViewModels;

namespace TheBookshelf.Models
{
    public interface IBookRepository 
    {
        IQueryable<Book> Books { get; }
        void SaveBook(Book b);
        Book DeleteBook(long? bookID);
        DropdownsViewModel GetDropdownsViewModel();
    }
}
