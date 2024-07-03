using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

namespace Debra_WebClient.Pages
{
    public class AddEventModel : PageModel
    {
        [BindProperty]
        public CreateEvents newEvent { get; set; }
        public void OnGet()
        {
        }
        public IActionResult OnPostTickets()
        {
            
            return Page();
        }
    }
}
