using System.ComponentModel.DataAnnotations;

namespace TheBookshelf.Models
{
    public class Author
    {
        public long? AuthorID { get; set; }
        [Required(ErrorMessage ="Please enter a Author name")]
        public string Name { get; set; } = String.Empty;
        [Required(ErrorMessage = "Please enter a Author's bio")]
        public string Biography { get; set; } = String.Empty;
        IEnumerable<Book>? Books { get; set; }
       
    }
}
