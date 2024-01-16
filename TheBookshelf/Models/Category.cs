using System.ComponentModel.DataAnnotations;

namespace TheBookshelf.Models
{
    public class Category
    {
        public long? CategoryID { get; set; }
        [Required(ErrorMessage = "Please enter a Category name")]
        public string Name { get; set; } = String.Empty;
        IEnumerable<Book>? Books { get; set; }
    }
}
