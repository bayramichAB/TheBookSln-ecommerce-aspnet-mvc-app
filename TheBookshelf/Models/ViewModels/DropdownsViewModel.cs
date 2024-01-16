namespace TheBookshelf.Models.ViewModels
{
    public class DropdownsViewModel
    {
        public DropdownsViewModel()
        {
            Authors = new List<Author>();
            Categories = new List<Category>();
        }

        public List<Author>? Authors { get; set; }
        public List<Category>? Categories { get; set; }
    }
}
