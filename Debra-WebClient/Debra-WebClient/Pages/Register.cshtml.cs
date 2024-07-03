using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public Partners newPartner { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            string url = "https://localhost:7102/Partner";

            newPartner.RegisteredDate = DateTime.Now;

            var content = new StringContent(JsonSerializer.Serialize(newPartner),
                                                Encoding.UTF8, "application/json");
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    string responseData = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseData); // Log the response content to console
                    System.Diagnostics.Debug.WriteLine(responseData);
                    //Console.log(responseData);
                    return RedirectToPage("SignIn");
                }
                else
                {
                    throw new Exception($"Failed to create partner: {response.ReasonPhrase}");
                }
            }
        }
    }
}
