using Platibus.Web.DataServices;

namespace Platibus.Web.Registry
{
    public class WebRegistry : StructureMap.Registry
    {
        public WebRegistry()
        {
            For<IUserDataService>().Use<UserDataService>();
        }
    }
}