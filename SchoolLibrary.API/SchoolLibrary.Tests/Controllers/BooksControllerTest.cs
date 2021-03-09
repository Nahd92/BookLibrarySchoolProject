using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Controllers;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using SchoolLibrary.Domain.Models.ModelBooks;
using SchoolLibrary.Extensions;
using SchoolLibrary.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SchoolLibrary.Tests.Controllers
{
    [TestClass]
    public class BooksControllerTest
    {
        private IEnumerable<IBooks> emptyList = null;

         private IEnumerable<IBooks> expectedBooks = new List<IBooks>(2)
            {
                new IBooks() { Id = 1, Title = "Lord of the rings", Descriptions = "Long", ISBN = "12345678912-1" },
                new IBooks() { Id = 2, Title = "Pippi", Descriptions = "short", ISBN = "12344678912-5" }
            };

        private IBooks createBook = new IBooks()
        {
            Id = 1, Title = "Lord of the rings", Descriptions = "Long",
            Published = DateTime.Parse("2020-02-20"), PageCount = 400, ISBN = "123456789121"
        };

        private CreateBookRequest createBookByRequest = new CreateBookRequest()
                {
                    Id = 1, Title = "Lord of the rings", Descriptions = "Long", 
                    Published = DateTime.Parse("2020-02-20"), PageCount = 400, Category = "Adventure", AuthorName = "J.R.R", AuthorLastName = "Tolkien"
                };


        private readonly Mock<IRepositoryWrapper> mockService;
        private readonly BooksController bookController;

        public BooksControllerTest()
        {
            mockService = new Mock<IRepositoryWrapper>();
            bookController = new BooksController(mockService.Object);
        }



        [TestMethod]
        public async Task TestGetAllBooks_WithEmptyBook_ShouldReturnNotFound()
        {
            //Arrange          
            mockService.Setup(x => x.Book.GetAllBooksAsync()).Returns(Task.Run(() => emptyList));
            //Act
            var response = await bookController.GetAll();
            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetBooksById_WithNullInInput_ShouldReturnBadRequest()
        {
            //Arrange 
            mockService.Setup(x => x.Book.GetBookByIdAsync(It.IsAny<int>())).ReturnsAsync(() => null);
            //Act
            var response = await bookController.GetById(null);
            //Assert
            response.Should().BeOfType<BadRequestResult>();
        }

        [TestMethod]
        public async Task TestGetBooksById_WithCorrectInput_ShouldReturnStatusCodeOk()
        {
            //Arrange s
            mockService.Setup(x => x.Book.GetBookByIdAsync(It.IsAny<int>())).Returns(Task.Run(() => new IBooks()
            {
                Id = 1,
                Title = "Lord of the rings",
                Descriptions = "Long",
                Published = DateTime.Parse("2020-02-20"),
                PageCount = 400
            }));
            //Act
            var response = await bookController.GetById(1);
            //Assert
            response.Should().BeOfType<JsonResult<BookResponse>>();
        }


        [TestMethod]
        public async Task TestGetBooksById_WithCorrectInput_ShouldReturnBookWithSameTitle()
        {
            //Arrange s
            mockService.Setup(x => x.Book.GetBookByIdAsync(It.IsAny<int>())).Returns(Task.Run(() => createBook));

            //Act
            var response = await bookController.GetById(1);
            //Assert
            var result = response.Should().BeOfType<JsonResult<BookResponse>>().Subject;
            var book = result.Content.Should().BeAssignableTo<BookResponse>().Subject;
            book.Title.Should().Be("Lord of the rings");
        }




        [TestMethod]
        public async Task TestGetAllBooks_ReturnsAListWithTwoBooks()
        {
            //Arrange
            mockService.Setup(x => x.Book.GetAllBooksAsync()).Returns(Task.Run(() => expectedBooks));
            //Act
            var response = await bookController.GetAll();
            //Assert

            var result = response.Should().BeOfType<JsonResult<IEnumerable<IBooks>>>().Subject;
            var book = result.Content.Should().BeAssignableTo<IEnumerable<IBooks>>().Subject;
            book.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task TestCreateBook_ReturnsCreatedStatusCode()
        {
            //Arrange
            mockService.Setup(x => x.Book.CreateAsync(It.IsAny<IBooks>())).ReturnsAsync(true);
            mockService.Setup(y => y.Author.CreateAsync(It.IsAny<Author>())).ReturnsAsync(() => new Author
            {
                Id = 1,
                FirstName = "J.R.R",
                LastName = "Tolkien"
            });
            mockService.Setup(e => e.Category.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(() => new Category 
            {
             Id = 1,
             Name = "Adventure"
            });


            //Act
            var response = await bookController.Create(createBookByRequest);


            //Assert
            mockService.Verify(x => x.Author.CreateAsync(It.IsAny<Author>()), Times.Once());
            mockService.Verify(x => x.Category.GetCategoryByName(It.IsAny<string>()), Times.Once());
            response.Should().BeOfType<JsonResult<BookResponse>>();
        }

        [TestMethod]
        public async Task TestCreateBook_ReturnsCorrectAuthor()
        {
            //Arrange
            mockService.Setup(x => x.Book.CreateAsync(It.IsAny<IBooks>())).ReturnsAsync(true);
            mockService.Setup(y => y.Author.CreateAsync(It.IsAny<Author>())).ReturnsAsync(() => new Author
            {
                Id = 1,
                FirstName = "J.R.R",
                LastName = "Tolkien"
            });
            mockService.Setup(e => e.Category.GetCategoryByName(It.IsAny<string>())).ReturnsAsync(() => new Category
            {
                Id = 1,
                Name = "Adventure"
            });

            //Act
            var response = await bookController.Create(createBookByRequest);

            //Assert    
            mockService.Verify(x => x.Author.CreateAsync(It.IsAny<Author>()), Times.Once());
            mockService.Verify(x => x.Book.CreateAsync(It.IsAny<IBooks>()), Times.Once());
            mockService.Verify(x => x.Category.GetCategoryByName(It.IsAny<string>()), Times.Once());
            var result = response.Should().BeOfType<JsonResult<BookResponse>>().Subject;
            var bookResponseResult = result.Content.Should().BeAssignableTo<BookResponse>().Subject;
            bookResponseResult.Author.Should().Be("J.R.R Tolkien");
        }


        [TestMethod]
        public async Task TestDeleteBook_ReturnsSuccessfulAndNoContent()
        {
            //Arrange
            const int deletedId = 1;
            mockService.Setup(x => x.Book.DeleteAsync(deletedId)).ReturnsAsync(true);
            //Act
            var response = await bookController.Delete(deletedId);
            //Assert
            response.Should().BeOfType<OkResult>();
        }




        [TestMethod]
        public async Task TestDeleteBook_ReturnsNotFound()
        {
            //Arrange
            const int deletedId = 3;
            mockService.Setup(x => x.Book.DeleteAsync(deletedId)).ReturnsAsync(false);
            //Act
            var response = await bookController.Delete(deletedId);
            //Assert
            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestDeleteBook_ReturnsBadRequest()
        {
            //Arrange
            mockService.Setup(x => x.Book.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            //Act
            var response = await bookController.Delete(null);
            //Assert
            response.Should().BeOfType<BadRequestResult>();
        }
    }
}
