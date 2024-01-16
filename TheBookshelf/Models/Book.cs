using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TheBookshelf.Models
{
    public class Book
    {
        public long? BookID { get; set; }
        [Required(ErrorMessage = "Please enter a product name")]
        public string Name { get; set; } = String.Empty;

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Please enter a positive price")]
        [Column(TypeName = "decimal(8,2)")]
        public decimal Price { get; set; }
        public bool Available { get; set; } = false;
        [Required(ErrorMessage = "Please enter a page number")]
        public string Pages { get; set; } = String.Empty;
        [Required(ErrorMessage = "Please enter a data")]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please enter a description")]
        public string Description { get; set; } = String.Empty;
        [Required]
        public string img { get; set;} = String.Empty;

        [Required(ErrorMessage = "Please specify a Category")]
        public long? CategoryID { get; set; }
        
        public Category? Category { get; set; }


        [Required(ErrorMessage = "Please specify a Author")]
        public long? AuthorID { get; set; }
        
        public Author? Author { get; set; }
    }
}
