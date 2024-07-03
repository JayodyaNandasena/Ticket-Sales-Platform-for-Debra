using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Debra_WebClient.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public PartnerAccounts account { get; set; }
        public void OnGet()
        {
        }
    }
}
