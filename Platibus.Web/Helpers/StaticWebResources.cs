using System;
using Platibus.Web.Acquaintance.IDataServices;

namespace Platibus.Web.Helpers
{
    public class StaticWebResources : IStaticWebResources
    {
        private readonly IUserDataService _userDataService;
        public Guid SubjectId { get; set; }


        public StaticWebResources()
        {
            
        }
        
        
       
    }
    
    
   
}