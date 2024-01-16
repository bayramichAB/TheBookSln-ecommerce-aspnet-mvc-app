namespace TheBookshelf.Models
{
    public class CartLine
    {
        public int CartLineID { get; set; }
        public Book Book { get; set; } = new();
        public int Quantity { get; set; }

    }
}
