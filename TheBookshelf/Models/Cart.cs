namespace TheBookshelf.Models
{
    public class Cart
    {
        public string? CartId { get; set; }
        public List<CartLine> Lines { get; set; } = new List<CartLine>();

        public virtual void AddItem(Book book,int quantity)
        {
            CartLine? line = Lines.Where(p => p.Book.BookID == book.BookID).FirstOrDefault();
            if (line == null)
            {
                Lines.Add(new CartLine
                {
                    Book = book,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }                               

        public virtual void RemoveLine(Book book) => Lines.RemoveAll(l=>l.Book.BookID==book.BookID);

        public decimal ComputeTotalValue() => Lines.Sum(e => e.Book.Price * e.Quantity);

        public virtual void Clear() => Lines.Clear();


    }
    
}
