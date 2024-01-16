using System.Linq;
using TheBookshelf.Models;
using Xunit;

namespace TheBookshelf.Tests
{
    public class CartTests
    {
        [Fact]
        public void Can_Add_New_Lines()
        {
            //Arrange
            Book b1 = new Book { BookID = 1, Name = "P1" };
            Book b2 = new Book { BookID = 2, Name = "P2" };

            //Arrange
            Cart cart = new Cart();

            //Act
            cart.AddItem(b1,1);
            cart.AddItem(b2,1);
            CartLine[] results = cart.Lines.ToArray();

            //Assert
            Assert.Equal(2,results.Length);
            Assert.Equal(b1, results[0].Book);
            Assert.Equal(b2, results[1].Book);
        }

        [Fact]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            //Arrange
            Book b1 = new Book { BookID = 1, Name = "B1" };
            Book b2 = new Book { BookID = 2, Name = "B2" };

            Cart cart = new Cart();

            //Act
            cart.AddItem(b1,1);
            cart.AddItem(b2,1);
            cart.AddItem(b1,10);
            CartLine[] results = (cart.Lines ?? new()).OrderBy(c => c.Book.BookID).ToArray();

            //Assert
            Assert.Equal(2,results.Length);
            Assert.Equal(11, results[0].Quantity);
            Assert.Equal(1, results[1].Quantity);
        }

        [Fact]
        public void Can_Remove_Line()
        {
            //Arrange
            Book b1 = new Book { BookID = 1, Name = "B1" };
            Book b2 = new Book { BookID = 2, Name = "B2" };
            Book b3 = new Book { BookID = 3, Name = "B3" };

            Cart cart = new Cart();

            cart.AddItem(b1,1);
            cart.AddItem(b2, 3);
            cart.AddItem(b3, 5);
            cart.AddItem(b2, 1);

            //Act
            cart.RemoveLine(b2);

            //Assert
            Assert.Empty(cart.Lines.Where(c=>c.Book==b2));
            Assert.Equal(2,cart.Lines.Count());
        }

        [Fact]
        public void Calculate_Cart_Total()
        {
            // Arrange
            Book p1 = new Book { BookID = 1, Name = "B1", Price = 100M };
            Book p2 = new Book { BookID = 2, Name = "B2", Price = 50M };

            // Arrange
            Cart cart = new Cart();

            // Act
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);
            cart.AddItem(p1, 3);
            decimal result = cart.ComputeTotalValue();

            // Assert
            Assert.Equal(450M, result);
        }

        [Fact]
        public void Can_Clear_Contents()
        {
            //Arrange 
            Book p1 = new Book { BookID = 1, Name = "B1", Price = 100M };
            Book p2 = new Book { BookID = 2, Name = "B2", Price = 50M };
           
            Cart target = new Cart();

            target.AddItem(p1, 1);
            target.AddItem(p2, 1);

            // Act
            target.Clear();

            // Assert
            Assert.Empty(target.Lines);
        }
    }
}
