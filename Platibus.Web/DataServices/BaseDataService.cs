using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Platibus.Web.ConfigHelpers;

namespace Platibus.Web.DataServices
{
    public class BaseDataService
    {
        protected readonly IOptions<BackendServerUrlConfiguration> _config;
        protected string _serverUrl;
        
        public BaseDataService(IOptions<BackendServerUrlConfiguration> config)
        {
            _config = config;
            _serverUrl = config.Value.BackendServerUrl;
        }

        public BaseDataService(IOptions<BackendServerUrlConfiguration> config, string serverUrl)
        {
            _config = config;
            _serverUrl = serverUrl;
        }

       

        protected async Task<HttpResponseMessage> PostAsync<T>(string baseurl, T entity, bool isAuth = true)
        {
            HttpClient client;


            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }
            
            //Set the client to accept json in body
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var cont = JsonConvert.SerializeObject(entity, Formatting.Indented);
            var httpContent = new StringContent(JsonConvert.SerializeObject(entity, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = null;

            try {
                httpResponse = await client.PostAsync(baseurl, httpContent);
            }
            catch(Exception ex) // TODO : Implement socket exception
            {

            }

            return httpResponse;
            
        }

        protected async Task<HttpResponseMessage> PostAsync(string baseurl, bool isAuth = true)
        {
            HttpClient client;

            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }
            
            //Set the client to accept json in body
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(null, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            return await client.PostAsync(baseurl, httpContent);
        }
        
        protected async Task<HttpResponseMessage> PostManyAsync<T>(string baseurl, T entity, bool isAuth = true)
        {
            HttpClient client;


            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }
            
            //Set the client to accept json in body
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var cont = JsonConvert.SerializeObject(entity, Formatting.Indented);
            var httpContent = new StringContent(JsonConvert.SerializeObject(entity, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            HttpResponseMessage httpResponse = null;

            try {
                httpResponse = await client.PostAsync(baseurl, httpContent);
            }
            catch(Exception ex) // TODO : Implement socket exception
            {

            }

            return httpResponse;
            
        }

       

        protected async Task<HttpResponseMessage> PutAsync<T>(string baseurl, T entity, bool isAuth = true) 
        {
            HttpClient client;

            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }

            var httpContent = new StringContent(JsonConvert.SerializeObject(entity, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            return await client.PutAsync(baseurl, httpContent);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string baseurl)
        {
            HttpClient client;

            client = new HttpClient();

            return await client.DeleteAsync(baseurl); 
        }

        protected async Task<HttpResponseMessage> GetAsync(string baseurl, bool isAuth = true)
        {
            HttpClient client;

            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }

            return await client.GetAsync(baseurl);
        }

        protected async Task<T> TryReadAsync<T>(HttpResponseMessage response) where T : class
        {
            if (response.Content == null)
            {
                return null; // <-- Dont eat the error here!
            }

            var content = await response.Content.ReadAsStringAsync();

            Console.WriteLine(content);
            return JsonConvert.DeserializeObject<T>(content);
        }

        protected async Task<IEnumerable<T>> GetListAsync<T>(HttpResponseMessage responseMessage) where T : class
        {
            if (responseMessage.Content == null)
            {
                return null; // <-- Dont eat the error here!
            }

            var content = await responseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(content);
        }

        protected async Task<HttpResponseMessage> DeleteAsync(string baseUrl,  bool isAuth = true)
        {
            HttpClient client;

            if (isAuth)
            {
                client = GetAuthorizedHttpClient();
            }
            else
            {
                client = new HttpClient();
            }
            
            //Set the client to accept json in body
            client.DefaultRequestHeaders
                .Accept
                .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(null, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            return await client.DeleteAsync(baseUrl);
        }


        
        private HttpClient GetAuthorizedHttpClient()
        {
            // Do some authorization stuff to the httpClient. This could be adding custom auth headers or maybe setting bearer token.
            return new HttpClient();
        }
        
        
    }
}