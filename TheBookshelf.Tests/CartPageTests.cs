using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Routing;
using Moq;
using System.Linq;
using System.Text;
using System.Text.Json;
using TheBookshelf.Models;
using TheBookshelf.Pages;
using Xunit;

namespace TheBookshelf.Tests
{
    public class CartPageTests
    {
        [Fact]
        public void Can_Load_Cart()
        {
            //Arrange
            Book b1 = new Book { BookID = 1, Name = "B1" };
            Book b2 = new Book { BookID = 2, Name = "B2" };
            Mock<IBookRepository>mockRepo= new Mock<IBookRepository>();
            mockRepo.Setup(m => m.Books).Returns((new Book[] {b1,b2 }).AsQueryable<Book>());

            Cart testCart= new Cart();
            testCart.AddItem(b1,2);
            testCart.AddItem(b2, 1);

            //Action
            CartModel cartModel = new CartModel(mockRepo.Object, testCart);
            cartModel.OnGet("myUrl");

            //Assert
            Assert.Equal(2,cartModel.Cart?.Lines.Count());
            Assert.Equal("myUrl",cartModel.ReturnUrl);

        }

        [Fact]
        public void Can_Update_Cart()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="B1"}
            }).AsQueryable<Book>());

            Cart? cart = new Cart();

            //Action
            CartModel cartModel = new CartModel(mock.Object, cart);
            cartModel.OnPost(1,"myUrl");
        }
    }
}
