namespace TheBookshelf.Models
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private StoreDbContext context;
        public EFCategoryRepository(StoreDbContext ctx)
        {                                             
            context = ctx;
        }
        public IQueryable<Category> Categories => context.Categories;

        public Category DeleteCategory(long? categoryId)
        {
            Category? category = context.Categories.FirstOrDefault(c=>c.CategoryID==categoryId);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
            }
            return category!;
        }

        public void SaveCategory(Category category)
        {
            if (category.CategoryID==null)
            {
                context.Add(category);
            }
            else 
            {
                Category? categoryDb = context.Categories.FirstOrDefault(c=>c.CategoryID==category.CategoryID);
                if (categoryDb != null)
                {
                    categoryDb.Name=category.Name;
                }
            }
            context.SaveChanges();
        }
    }
}
