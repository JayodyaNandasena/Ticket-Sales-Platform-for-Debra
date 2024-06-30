using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Debra_WebClient.Pages
{
    public class AddEventModel : PageModel
    {
        [BindProperty]
        public Events newEvent { get; set; }
        public void OnGet()
        {
        }
    }
}
