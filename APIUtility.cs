using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CarModelManagement.BLL.Helper
{
    public class APIUtility
    {
        public HttpResponseMessage GetApi(string Path, string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Url = request.GetAsync(Path);
            Url.Wait();
            var result = Url.Result;
            return result;
        }

        public HttpResponseMessage DeleteApi(string Path, string token)
        {
            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Url = request.DeleteAsync(Path);
            Url.Wait();
            var result = Url.Result;
            return result;
        }

        #region Call API [Comman] for POST
        public HttpResponseMessage PostApi(string Path, dynamic obj, string token)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };

            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Url = request.PostAsync(Path, byteContent);
            Url.Wait();
            var result = Url.Result;
            return result;
        }
        #endregion

        #region Call API [Comman] for PUT
        public HttpResponseMessage PutApi(string Path, dynamic obj, string token)
        {
            var myContent = JsonConvert.SerializeObject(obj);
            var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClientHandler clientHandler = new HttpClientHandler();
            clientHandler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; };
            var request = new HttpClient(clientHandler);
            request.BaseAddress = new Uri(Path);
            request.DefaultRequestHeaders.Clear();
            request.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            request.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var Url = request.PutAsync(Path, byteContent);
            Url.Wait();
            var result = Url.Result;
            return result;
        }
        #endregion

    }
}
