using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class TicketsModel : PageModel
    {

        [BindProperty]
        public BuyTickets details { get; set; }
        public List<EventTitles> eventDetails { get; set; } = new List<EventTitles>();


        public async Task<IActionResult> OnGet()
        {
            string url = "https://localhost:7102/Event/Titles";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var reply = await response.Content.ReadFromJsonAsync<List<EventTitles>>();

                    foreach (var item in reply)
                    {
                        eventDetails.Add(item);
                    }
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync()
        {
            string url = "https://localhost:7102/Ticket";
            var content = new StringContent(JsonSerializer.Serialize(details), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Tickets Purchased Successfully!";
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        throw new Exception($"Failed to purchase tickets. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Failed to purchase tickets. Exception: {ex.Message}");
                }
            }
        }
    }
}
