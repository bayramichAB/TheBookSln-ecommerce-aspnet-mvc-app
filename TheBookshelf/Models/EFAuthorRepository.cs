namespace TheBookshelf.Models
{
    public class EFAuthorRepository : IAuthorRepository
    {
        private StoreDbContext context;
        public EFAuthorRepository(StoreDbContext ctx)
        {
            context = ctx;
        }

        public IQueryable<Author> Authors => context.Authors;

        public Author DeleteAuthor(long? authorId)
        {
            Author? author = context.Authors.FirstOrDefault(a=>a.AuthorID==authorId);

            if (author != null) 
            {
                context.Authors.Remove(author);
                context.SaveChanges();
            }
            return author!;

        }

        public void SaveAuthor(Author author)
        {
            if (author.AuthorID == null)
            {
                context.Authors.Add(author);
            }
            else 
            {
                Author? authorDb=context.Authors.FirstOrDefault(a=>a.AuthorID==author.AuthorID);

                if (authorDb != null) 
                {
                    authorDb.Name= author.Name;
                    authorDb.Biography= author.Biography;
                }
            }
            context.SaveChanges();
        }
    }
}
