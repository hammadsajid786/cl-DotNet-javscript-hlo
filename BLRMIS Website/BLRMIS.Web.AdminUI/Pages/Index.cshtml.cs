using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BLRMIS.Web.AdminUI.Pages
{
    public class IndexModel : PageModel
    {
        public void OnGet()
        {
            //if (!User.Identity.IsAuthenticated)
            //{
            //   Response.Redirect("/Login?ReturnUrl=/");
            //}
        }
    }
}
