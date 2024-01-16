namespace TheBookshelf.Models
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }
        void SaveCategory(Category category);
        Category DeleteCategory(long? categoryId);
    }
}
