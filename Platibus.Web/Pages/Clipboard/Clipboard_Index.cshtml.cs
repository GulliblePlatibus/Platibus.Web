using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Platibus.Web.Helpers;

namespace Platibus.Web.Pages.Clipboard
{
    [Authorize(Roles = nameof(UserRoles.Admin))]
    public class Clipboard_IndexModel : PageModel
    {
        public void OnGet()
        {

        }
    }
}