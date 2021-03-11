using SchoolLibrary.Client.Domain.Interfaces;
using System.Net.Http;
using System.Threading.Tasks;

namespace SchoolLibrary.Client.Logic
{
    public class HttpClientProvider : IHttpClientProvider
    {
        private readonly HttpClient HttpClient;
        public HttpClientProvider()
        {
            HttpClient = new HttpClient();
        }

        public Task<HttpResponseMessage> DeleteAsync(string requestUri) => 
            HttpClient.DeleteAsync(requestUri);
           
        public Task<HttpResponseMessage> GetAsync(string requestUri) => 
            HttpClient.GetAsync(requestUri);
     
        public Task<HttpResponseMessage> PostAsync(string requestUri, HttpContent content) => 
            HttpClient.PostAsync(requestUri, content);
        
        public Task<HttpResponseMessage> PutAsync(string requestUri, HttpContent content) => 
            HttpClient.PutAsync(requestUri, content);
           
    }
}
