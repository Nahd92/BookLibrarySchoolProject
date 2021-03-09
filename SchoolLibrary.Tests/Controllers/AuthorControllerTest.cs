using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using SchoolLibrary.Controllers;
using SchoolLibrary.Domain.Interfaces;
using SchoolLibrary.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SchoolLibrary.Tests.Controllers
{
    [TestClass]
    public class AuthorControllerTest
    {

        private IEnumerable<Author> emptyAuthors = null;



        [TestMethod]
        public async Task TestGetAllAuthors_WithEmptyAuthor_ShouldReturnNotFound()
        {
            //Arrange
            var mockService = new Mock<IRepositoryWrapper>();
            mockService.Setup(x => x.Author.GetAuthorsAsync()).Returns(Task.Run(() => emptyAuthors));
            var controller = new AuthorController(mockService.Object);

            //Act
            var response = await controller.GetAllAuthors();
            //Assert

            response.Should().BeOfType<NotFoundResult>();
        }

        [TestMethod]
        public async Task TestGetAuthorByIdAsync_WithExistingId_ShouldReturnAnAuthor()
        {
            
        }

    }
}
