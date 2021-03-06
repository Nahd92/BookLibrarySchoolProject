using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolLibrary.Contracts.Request;
using SchoolLibrary.Contracts.Response;
using SchoolLibrary.Controllers;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models.ModelBooks;
using SchoolLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            Published = DateTime.Parse("2020-02-20"), PageCount = 400
        };

        private CreateBookRequest createBookByRequest = new CreateBookRequest()
                {
                    Id = 1, Title = "Lord of the rings", Descriptions = "Long", 
                    Published = DateTime.Parse("2020-02-20"), PageCount = 400 
                };
            



        [TestMethod]
        public async Task TestGetAllBooks_WithEmptyBook_ShouldReturnNotFound()
        {
            //Arrange          
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.GetAllBooksAsync()).Returns(Task.Run(() => emptyList));
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.GetAll();
            //Assert
            var result = response.Should().BeOfType<HttpStatusCodeResult>().Which;
            result.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public async Task TestGetBooksById_WithNullInInput_ShouldReturnBadRequest()
        {
            //Arrange 
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.GetBookByIdAsync(It.IsAny<int>())).Returns(() => null);
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.GetById(null);
            //Assert
            var result = response.Should().BeOfType<HttpStatusCodeResult>().Which;
            result.StatusCode.Should().Be(400);
        }

        [TestMethod]
        public async Task TestGetBooksById_WithCorrectInput_ShouldReturnStatusCodeOk()
        {
            //Arrange s
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.GetBookByIdAsync(It.IsAny<int>())).Returns(Task.Run(() => new IBooks()
            {
                Id = 1,
                Title = "Lord of the rings",
                Descriptions = "Long",
                Published = DateTime.Parse("2020-02-20"),
                PageCount = 400
            }));

            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.GetById(1);
            //Assert
            var result = response.Should().BeOfType<JsonHttpStatusResult>().Which;
            result.StatusCode.Should().Be(200);
        }

        [TestMethod]
        public async Task TestGetAllBooks_ReturnsAListWithTwoBooks()
        {
            //Arrange
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.GetAllBooksAsync()).Returns(Task.Run(() => expectedBooks));
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.GetAll();
            //Assert

            var result = response.Should().BeOfType<JsonHttpStatusResult>().Subject;
            var book = result.Data.Should().BeAssignableTo<IEnumerable<IBooks>>().Subject;
            book.Count().Should().Be(2);
        }

        [TestMethod]
        public async Task TestCreateBook_ReturnsCreatedStatusCode()
        {
            //Arrange
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.CreateAsync(createBook)).ReturnsAsync(true);
            var controller = new BooksController(mockService.Object);
            //Act

            var response = await controller.Create(createBookByRequest);
            //Assert

            var result = response.Should().BeOfType<JsonHttpStatusResult>().Which;
            result.StatusCode.Should().Be(201);
        }

        [TestMethod]
        public async Task TestCreateBook_ReturnsCorrectId()
        {
            //Arrange
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.CreateAsync(createBook)).ReturnsAsync(true);
            var controller = new BooksController(mockService.Object);
            //Act

            var response = await controller.Create(createBookByRequest);
            //Assert
            
            var result = response.Should().BeOfType<JsonHttpStatusResult>().Subject;
            var book = result.Data.Should().BeAssignableTo<BookResponse>().Subject;
            book.Id.Should().Be(1);
        }


        [TestMethod]
        public async Task TestDeleteBook_ReturnsSuccessfulAndNoContent()
        {
            //Arrange
            const int deletedId = 1;
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.DeleteAsync(deletedId)).ReturnsAsync(true);
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.Delete(deletedId);
            //Assert
            var result = response.Should().BeOfType<HttpStatusCodeResult>().Which;
            result.StatusCode.Should().Be(204);
        }




        [TestMethod]
        public async Task TestDeleteBook_ReturnsNotFound()
        {
            //Arrange
            const int deletedId = 3;
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.DeleteAsync(deletedId)).ReturnsAsync(false);
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.Delete(deletedId);
            //Assert
            var result = response.Should().BeOfType<HttpNotFoundResult>().Which;
            result.StatusCode.Should().Be(404);
        }

        [TestMethod]
        public async Task TestDeleteBook_ReturnsBadRequest()
        {
            //Arrange
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Book.DeleteAsync(It.IsAny<int>())).ReturnsAsync(false);
            var controller = new BooksController(mockService.Object);
            //Act
            var response = await controller.Delete(null);
            //Assert
            var result = response.Should().BeOfType<HttpStatusCodeResult>().Which;
            result.StatusCode.Should().Be(400);
        }
    }
}
