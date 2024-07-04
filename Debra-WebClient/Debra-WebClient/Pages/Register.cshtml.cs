using Debra_API.DTOs;
using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Principal;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Partners newPartner { get; set; }
        [BindProperty]
        public string username { get; set; }
        [BindProperty]
        public string password { get; set; }



        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            // Ensure newPartner is not null
            if (newPartner == null)
            {
                newPartner = new Partners();
            }

            // Ensure newPartner.Account is not null
            if (newPartner.Account == null)
            {
                newPartner.Account = new PartnerAccounts();
            }

            string url = "https://localhost:7102/Partner";

            newPartner.Id = 0;
            newPartner.RegisteredDate = DateTime.UtcNow;

            var content = new StringContent(JsonSerializer.Serialize(newPartner), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("SignIn");
                }
            }
            
            throw new Exception($"Failed to create partner");

        }


    }
}
