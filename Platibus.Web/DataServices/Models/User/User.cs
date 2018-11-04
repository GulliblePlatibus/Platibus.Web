using Newtonsoft.Json;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.DataServices.Models.User
{

    public class User : IUser
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}