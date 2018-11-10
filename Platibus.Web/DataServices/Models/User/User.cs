using Newtonsoft.Json;
using Platibus.Web.Acquaintance.IDataServices;
using System;

namespace Platibus.Web.DataServices.Models.User
{

    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AccesLevel { get; set; }
        public Guid Id { get; set; }
    }
}