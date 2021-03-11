using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using SchoolLibrary.Client.Domain.Interfaces;
using SchoolLibrary.Client.Domain.Models;
using SchoolLibrary.Client.Domain.Requests;
using SchoolLibrary.Client.Logic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Tests.Controllers
{
    [TestClass]
   public class BookControllerTest
    {
        [TestMethod]
        public async Task HTTPClient_GetAllBooks_ShouldReturnOkAnd()
        {
            //Arrange
            var expectedBook = new List<Books>()
            {
               new Books() {
                Id = 1,
                Title = "pippi",
                Descriptions = "Långstrump",
                ISBN = "1234123412341234",
                PageCount = 220,
                Published = DateTime.Parse("2020-02-20"),
                AuthorId = 1,
                CategoryId = 2
               }
            };

            var json = JsonConvert.SerializeObject(expectedBook);
            string url = "https://localhost:44382/api/Books/GetAll";

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json , Encoding.UTF8, "application/json")
            };

            var mockHttpClientProvider = new Mock<IHttpClientProvider>();
            mockHttpClientProvider.Setup(t => t.GetAsync(It.Is<string>(s => s.StartsWith(url))))
                                              .ReturnsAsync(httpResponse).Verifiable();

            var response = new BookRepository(mockHttpClientProvider.Object);

            // ACT
            var result = await response.GetAllBooksAsync();

            // ASSERT
            var expectedUri = new Uri("https://localhost:44382/api/Books/GetAll");
            result.Should().NotBeNull();
            mockHttpClientProvider.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get  
                 && req.RequestUri == expectedUri);
        }

        [TestMethod]
        public async Task HTTP_GetBookByIdTest_ShouldReturnOkAndReturnGetMethodToCorrecHTTP()
        {
            //Arrange
            var expectedBook = new Books()
            {
                Id = 1,
                Title = "pippi",
                Descriptions = "Långstrump",
                ISBN = "1234123412341234",
                PageCount = 220,
                Published = DateTime.Parse("2020-02-20"),
                AuthorId = 1,
                CategoryId = 2
            };
            var json = JsonConvert.SerializeObject(expectedBook);
            string url = "https://localhost:44382/api/Books/GetById/1";

            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            };
            
            var mockHttpClientProvider = new Mock<IHttpClientProvider>();
            mockHttpClientProvider.Setup(t => t.GetAsync(It.Is<string>(s => s.StartsWith(url))))
                                              .ReturnsAsync(httpResponse).Verifiable(); 

            var response = new BookRepository(mockHttpClientProvider.Object);

            // ACT
            var result = await response.GetBookByIdAsync(1);

            //Assert
          
            var expectedUri = new Uri("https://localhost:44382/api/Books/GetById/1");
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            mockHttpClientProvider.Verify(x => x.GetAsync(It.IsAny<string>()), Times.Once());
            ItExpr.Is<HttpRequestMessage>(req =>
                 req.Method == HttpMethod.Get 
                 && req.RequestUri == expectedUri);
        }



        [TestMethod]
        public async Task HTTPClient_CreateBook_ShouldReturnOkAndReturnPOSTMethodToCorrecHTTP()
        {
            //Arrange
            var createdBook = new CreateBooksRequest()
            {
                Id = 1,
                Title = "pippi",
                Descriptions = "Långstrump",
                PageCount = 220,
                Published = DateTime.Parse("2020-02-20"),
                AuthorName = "Jörgen",
                AuthorLastName = "Brink",
                Category = "Baby"
            };
            var json = JsonConvert.SerializeObject(createdBook);
            var url = "https://localhost:44382/api/Books/Create";
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var httpResponse = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,              
            };

            var mockHttpClientProvider = new Mock<IHttpClientProvider>();
            mockHttpClientProvider.Setup(t => t.PostAsync(url, content))
                                        .ReturnsAsync(httpResponse).Verifiable(); 
            
            var response = new BookRepository(mockHttpClientProvider.Object);

            // ACT
            var result = await response.CreateAsync(createdBook);

            //Assert
            var expectedUri = new Uri("https://localhost:44382/api/Books/Create");
            result.Should().NotBeNull();
            mockHttpClientProvider.Verify(x => x.PostAsync(expectedUri.ToString(), result.Content), Times.Once());
            ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Post 
                 && req.RequestUri == expectedUri);
        }
    }
}
