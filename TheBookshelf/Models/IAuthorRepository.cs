namespace TheBookshelf.Models
{
    public interface IAuthorRepository
    {
        IQueryable<Author> Authors { get; }
        void SaveAuthor(Author author);
        Author DeleteAuthor(long? authorId);

    }
}
