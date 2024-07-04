using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class SignInModel : PageModel
    {
        [BindProperty]
        public PartnerAccounts account { get; set; }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Ensure account is not null
            if (account == null)
            {
                account = new PartnerAccounts();
            }

            string url = "https://localhost:7102/Partner/ValidateLogin";


            var content = new StringContent(JsonSerializer.Serialize(account), 
                Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("Index");
                }
            }

            throw new Exception($"Failed to create partner");

        }
    }
}
