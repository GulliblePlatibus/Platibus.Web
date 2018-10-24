using System;
using System.Data.Common;
using System.Diagnostics;
using System.Net.Http;
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
            
            var httpContent = new StringContent(JsonConvert.SerializeObject(entity, Formatting.Indented), System.Text.Encoding.UTF8, "application/json");

            return await client.PostAsync(baseurl, httpContent);
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

            return JsonConvert.DeserializeObject<T>(content);
        }

        private HttpClient GetAuthorizedHttpClient()
        {
            // Do some authorization stuff to the httpClient. This could be adding custom auth headers or maybe setting bearer token.
            return new HttpClient();
        }
        
        
    }
}