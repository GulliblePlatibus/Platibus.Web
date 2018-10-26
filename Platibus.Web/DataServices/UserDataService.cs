using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using Platibus.Web.ConfigHelpers;
using Platibus.Web.DataServices.Models.User;
using Response = Platibus.Web.Documents.Response;

namespace Platibus.Web.DataServices
{
    public interface IUserDataService
    {
        Task<Response> CreateUser(User user);
        Task<User> GetUserById(Guid id);
        Task Login();
    }
    
    public class UserDataService : BaseDataService, IUserDataService
    {
        public UserDataService(IOptions<BackendServerUrlConfiguration> config) : base(config)
        {
        }

        public async Task<Response> CreateUser(User user)
        {
            var baseurl = _serverUrl + "/api/users"; //<-- Endpoint on backend!!!

            var response = await PostAsync<User>(baseurl, user);

            if (!response.IsSuccessStatusCode)
            {
                if (response.Content != null)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();
                    return Response.Unsuccesfull(errorMsg);
                }
                return Response.Unsuccesfull(response.ReasonPhrase);
            }
            
            return Response.Succes();
        }

        public async Task<User> GetUserById(Guid id)
        {
            var baseurl = _serverUrl + "/"; //<-- specific url here!

            var result = await GetAsync(baseurl);

            return await TryReadAsync<User>(result);
        }

        public async Task Login()
        {
            //
            var disco = await DiscoveryClient.GetAsync("https://localhost:5001");

            if (disco.IsError)
            {
                Console.WriteLine(disco.Error);
                return;
            }
            
            //Request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "ro.client", "secret");
            var tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("Ulsan", "1234", "Platibus.Backend");

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            Console.WriteLine(tokenResponse.Json);

            var client = new HttpClient();
            
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("https://localhost:5010/api/users");

            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine("Sucks to be you" + ": " + response.StatusCode);   
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                Console.WriteLine(JArray.Parse(content));
            }
        }
    }
}