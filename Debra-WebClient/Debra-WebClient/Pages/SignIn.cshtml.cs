using Debra_WebClient.Model;
using Microsoft.AspNetCore.Http;
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
					var reply = await response.Content
                        .ReadFromJsonAsync<OperationResultResponse<Partners>>();

                    if (reply.Status == Status.Success)
                    {
                        Partners loggedPartner = reply.Result;
                        HttpContext.Session.SetInt32("PartnerId", loggedPartner.Id);
						TempData["successMessage"] = "Sign in Success!";
						return RedirectToPage("Index");
                    }
                }
            }
            // Display error toast message
            TempData["ErrorMessage"] = "Invalid credentials or server error.";

            // Redirect to login page 
            return RedirectToPage("SignIn");

        }
    }
}
