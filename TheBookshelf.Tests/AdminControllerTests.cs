using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBookshelf.Controllers;
using TheBookshelf.Models;
using Xunit;

namespace TheBookshelf.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_Contains_All_Books()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="B1"},
                new Book{BookID=1,Name="B2"},
                new Book{BookID=1,Name="B3"},
            }).AsQueryable<Book>());

            AdminController target=new AdminController(mock.Object);

            //Act
            Book[]? result = GetViewModel<IEnumerable<Book>>(target.Books())?.ToArray();

            //Assert
            Assert.Equal(3,result?.Length);
            Assert.Equal("B1", result?[0].Name);
            Assert.Equal("B2", result?[1].Name);
            Assert.Equal("B3", result?[2].Name);
        }

        [Fact]
        public void Can_Edit_Product()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book[] 
            {
                new Book{BookID=1,Name="B1"},
                new Book{BookID=2,Name="B2"},
                new Book{BookID=3,Name="B3"},
            }).AsQueryable<Book>());

            AdminController target=new AdminController(mock.Object);

            //Act
            Book? b1=GetViewModel<Book>(target.Edit(1));
            Book? b2 = GetViewModel<Book>(target.Edit(2));
            Book? b3 = GetViewModel<Book>(target.Edit(3));

            //Assert
            Assert.Equal(1,b1?.BookID);
            Assert.Equal(2, b2?.BookID);
            Assert.Equal(3, b3?.BookID);
        }

        [Fact]
        public void Can_Edit_Nonexistent_Book()
        {
            //Arrange
            Mock<IBookRepository>mock=new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book [] 
            {
                new Book{BookID=1,Name="B1"},
                new Book{BookID=2,Name="B2"},
                new Book{BookID=3,Name="B3"},
            }).AsQueryable<Book>());

            AdminController target = new AdminController(mock.Object);

            //Act
            Book? result=GetViewModel<Book>(target.Edit(4));

            //Assert
            Assert.Null(result);
        }

        [Fact]
        public void Can_Save_Valid_Changes()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            Mock<ITempDataDictionary> tempData = new Mock<ITempDataDictionary>();

            AdminController target = new AdminController(mock.Object)
            {
                TempData = tempData.Object
            };

            Book book = new Book { Name = "Test" };

            //Act
            IActionResult result = target.Edit(book);

            //Assert
            mock.Verify(v=>v.SaveBook(book));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Books", (result as RedirectToActionResult)?.ActionName);
        }

        [Fact]
        public void Can_Save_Invalid_Changes()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();

            AdminController target = new AdminController(mock.Object);

            Book book = new Book { Name = "Test" };

            target.ModelState.AddModelError("error","error");

            //Act
            IActionResult result = target.Edit(book);

            //Assert
            mock.Verify(v=>v.SaveBook(It.IsAny<Book>()),Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Can_Delete_Valid_Products()
        {
            //Arrange
            Book b = new Book { BookID = 2, Name = "Test" };

            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {  
                new Book {BookID = 1, Name = "B1"},
                b,                          
                new Book {BookID = 3, Name = "B3"}
            }.AsQueryable<Book>());

            AdminController target = new AdminController(mock.Object);

            //Act 
            target.Delete(b.BookID);

            // Assert 
            mock.Verify(m => m.DeleteBook(b.BookID));
        }


        private T? GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
