using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Debra_WebClient.Pages
{
    public class IndexModel : PageModel
    {
        public List<ReadEvents> allEvents = new List<ReadEvents>();
        public List<ReadEvents> events = new List<ReadEvents>();

        public async Task<IActionResult> OnGet()
        {
            // Run both tasks in parallel
            var allEventsTask = getAllUpcomingEvents();
            var eventsTask = get3UpcomingEvents();

            // Await both tasks to complete
            await Task.WhenAll(allEventsTask, eventsTask);

            return Page();
        }

        public async Task getAllUpcomingEvents()
        {
            string url = "https://localhost:7102/Event";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    allEvents = await response.Content.ReadFromJsonAsync<List<ReadEvents>>();
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }

        public async Task get3UpcomingEvents()
        {
            string url = "https://localhost:7102/Event/Upcoming";
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    events = await response.Content.ReadFromJsonAsync<List<ReadEvents>>();
                }
                else
                {
                    throw new Exception(response.StatusCode.ToString());
                }
            }
        }
    }
}
