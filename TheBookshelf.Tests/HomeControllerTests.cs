using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheBookshelf.Controllers;
using TheBookshelf.Models;
using TheBookshelf.Models.ViewModels;
using Xunit;

namespace TheBookshelf.Tests
{
    public class HomeControllerTests
    {
        [Fact]
        public void Can_Use_Repository()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.SetupGet(m => m.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1"},
                new Book{BookID=2,Name="P2"},
            }).AsQueryable<Book>());

            HomeController controller = new HomeController(mock.Object);

            // Act
            BooksListViewModel result = controller.Index(null,null)?.ViewData.Model as BooksListViewModel ?? new();

            //Assert
            Book[] prodArray = result.Books.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P1", prodArray[0].Name);
            Assert.Equal("P2", prodArray[1].Name);
        }

        [Fact]
        public void Can_Paginate()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1"},
                new Book{BookID=2,Name="P2"},
                new Book{BookID=3,Name="P3"},
                new Book{BookID=4,Name="P4"},
                new Book{BookID=5,Name="P5"},
            }).AsQueryable<Book>());

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Act
            BooksListViewModel result = controller.Index(null,null, 2)?.ViewData.Model as BooksListViewModel ?? new();

            //Assert
            Book[] prodArray = result.Books.ToArray();
            Assert.True(prodArray.Length == 2);
            Assert.Equal("P4", prodArray[0].Name);
            Assert.Equal("P5", prodArray[1].Name);
        }

        [Fact]
        public void Can_Send_Pagination_View_Model()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1"},
                new Book{BookID=2,Name="P2"},
                new Book{BookID=3,Name="P3"},
                new Book{BookID=4,Name="P4"},
                new Book{BookID=5,Name="P5"},
            }).AsQueryable<Book>());

            HomeController controller = new HomeController(mock.Object) { PageSize = 3 };

            //Act

            BooksListViewModel result = controller.Index(null,null, 2)?.ViewData.Model as BooksListViewModel ?? new();

            //Assert
            PagingInfo pagingInfo = result.PagingInfo;
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }

        [Fact]
        public void Can_Filter_Books()
        {
            //Arrange
            //create the mock repository
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(p => p.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1",Category=new Category{ Name="Cat1"} },
                new Book{BookID=2,Name="P2",Category=new Category{ Name="Cat2"}},
                new Book{BookID=3,Name="P3",Category=new Category { Name = "Cat1" }},
                new Book{BookID=4,Name="P4",Category=new Category { Name = "Cat2" }},
                new Book{BookID=5,Name="P5",Category=new Category { Name = "Cat3" }},
            }).AsQueryable<Book>());

            //Arrange create a Controller and make the page size items 3
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Action
            Book[] result = (controller.Index("Cat2",null, 1)?.ViewData.Model as BooksListViewModel ?? new()).Books.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category!.Name == "Cat2");
            Assert.True(result[1].Name == "P4" && result[1]!.Category!.Name == "Cat2");
        }

        [Fact]
        public void Generate_Category_Specific_Book_Count()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1",Category=new Category{ Name="Cat1"}},
                new Book{BookID=2,Name="P2",Category=new Category{ Name="Cat2"}},
                new Book{BookID=3,Name="P3",Category=new Category{ Name="Cat1"}},
                new Book{BookID=4,Name="P4",Category=new Category{ Name="Cat2"}},
                new Book{BookID=5,Name="P5",Category=new Category{ Name="Cat3"}}
            }).AsQueryable);

            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            Func<ViewResult, BooksListViewModel?> GetModel = result => result?.ViewData?.Model as BooksListViewModel;

            //Act
            int? res1 = GetModel(controller.Index("Cat1", null))?.PagingInfo.TotalItems;
            int? res2 = GetModel(controller.Index("Cat2", null))?.PagingInfo.TotalItems;
            int? res3 = GetModel(controller.Index("Cat3", null))?.PagingInfo.TotalItems;
            int? resAll = GetModel(controller.Index(null, null))?.PagingInfo.TotalItems;

            //Assert
            Assert.Equal(2, res1);
            Assert.Equal(2, res2);
            Assert.Equal(1, res3);
            Assert.Equal(5, resAll);
        }

        [Fact]
        public void Can_Filter_Books_By_Name()
        {
            Mock<IBookRepository> mock= new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1",Category=new Category{ Name="Cat1"},Author=new Author{ Name="A1"} },
                new Book{BookID=2,Name="P2",Category=new Category{ Name="Cat2"},Author=new Author{ Name="A2"}},
                new Book{BookID=3,Name="P1",Category=new Category { Name = "Cat3" },Author=new Author{ Name="A3"}},
                new Book{BookID=4,Name="P2",Category=new Category { Name = "Cat4" },Author=new Author{ Name="A4"}},
                new Book{BookID=5,Name="P3",Category=new Category { Name = "Cat5" },Author=new Author{ Name="A5"}}
            }).AsQueryable<Book>());

            //Arrange create a Controller and make the page size items 3
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Action
            Book[] result = (controller.Index(null,"P2", 1)?.ViewData.Model as BooksListViewModel ?? new()).Books.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category!.Name == "Cat2" && result[0].Author!.Name=="A2");
            Assert.True(result[1].Name == "P2" && result[1]!.Category!.Name == "Cat4" && result[1].Author!.Name == "A4");
        }

        [Fact]
        public void Can_Filter_Books_By_Name_Of_Author()
        {
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book[]
            {
                new Book{BookID=1,Name="P1",Category=new Category{ Name="Cat1"},Author=new Author{ Name="A1"} },
                new Book{BookID=2,Name="P2",Category=new Category{ Name="Cat2"},Author=new Author{ Name="A2"}},
                new Book{BookID=3,Name="P3",Category=new Category { Name = "Cat3" },Author=new Author{ Name="A1"}},
                new Book{BookID=4,Name="P4",Category=new Category { Name = "Cat4" },Author=new Author{ Name="A2"}},
                new Book{BookID=5,Name="P5",Category=new Category { Name = "Cat5" },Author=new Author{ Name="A3"}}
            }).AsQueryable<Book>());

            //Arrange create a Controller and make the page size items 3
            HomeController controller = new HomeController(mock.Object);
            controller.PageSize = 3;

            //Action
            Book[] result = (controller.Index(null, "A2", 1)?.ViewData.Model as BooksListViewModel ?? new()).Books.ToArray();

            //Assert
            Assert.Equal(2, result.Length);
            Assert.True(result[0].Name == "P2" && result[0].Category!.Name == "Cat2" && result[0].Author!.Name == "A2");
            Assert.True(result[1].Name == "P4" && result[1]!.Category!.Name == "Cat4" && result[1].Author!.Name == "A2");
        }

        [Fact]
        public void Can_Use_Details()
        {
            //Arrange
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(b => b.Books).Returns((new Book[] 
            {
                new Book{BookID=1,Name="B1"},
                new Book{BookID=2,Name="B2"},
                new Book{BookID=3,Name="B3"},
            }).AsQueryable<Book>());

            HomeController target= new HomeController(mock.Object);

            //Act BooksListViewModel result = controller.Index(null,null)?.ViewData.Model as BooksListViewModel ?? new();
            Book? book1=target.Details(1).ViewData.Model as Book;
            Book? book2 = target.Details(2).ViewData.Model as Book;
            Book? book3 = target.Details(3).ViewData.Model as Book;

            //Assert
            Assert.Equal(1,book1?.BookID);
            Assert.Equal(2, book2?.BookID);
            Assert.Equal(3, book3?.BookID);
        }
    }
}
