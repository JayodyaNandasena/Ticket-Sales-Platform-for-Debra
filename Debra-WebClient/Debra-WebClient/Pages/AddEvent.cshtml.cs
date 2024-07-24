using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class AddEventModel : PageModel
    {
        public string MinDate { get; set; }

        private readonly ILogger<AddEventModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty]
        public CreateEvents newEvent { get; set; }
        [BindProperty]
        public string MusicianName { get; set; }
        [BindProperty]
        public string BandName { get; set; }

        //[BindProperty]
        //public IFormFile EventImage { get; set; }
        //[BindProperty]
        //public IFormFile MusicianImage { get; set; }
        //[BindProperty]
        //public IFormFile BandImage { get; set; }

        public AddEventModel(ILogger<AddEventModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> OnGet()
        {
            int? partnerId = _httpContextAccessor.HttpContext.Session.GetInt32("PartnerId");

            if (partnerId == null)
            {
                return RedirectToPage("SignIn");
            }

            MinDate = DateTime.Today.ToString("yyyy-MM-dd");
            return Page();
            
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {
            if (newEvent == null)
            {
                newEvent = new CreateEvents();
            }

            if (newEvent.Musicians == null)
            {
                newEvent.Musicians = new List<Musicians>();
            }

            if (newEvent.Bands == null)
            {
                newEvent.Bands = new List<Bands>();
            }

            if (newEvent.Tickets == null)
            {
                newEvent.Tickets = new CreateTickets();
            }

            newEvent.Musicians.Add(
                new Musicians(
                    MusicianName,
                    ""
                )
            );

            newEvent.Bands.Add(
                new Bands(
                    BandName,
                    ""
                )
            );

            newEvent.Image = "";

            newEvent.PartnerId = _httpContextAccessor.HttpContext.Session.GetInt32("PartnerId") ?? 0;

            string url = "https://localhost:7102/Event";

            var content = new StringContent(JsonSerializer.Serialize(newEvent), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = await client.PostAsync(url, content);

                    if (response.IsSuccessStatusCode)
                    {
                        TempData["successMessage"] = "Event Added Succssfully!";
                        return RedirectToPage("Index");
                    }
                    else
                    {
                        // Log the response status code, reason, etc.
                        // Example: _logger.LogError($"Failed to create event. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}");
                        // Handle the failure scenario appropriately (e.g., return a specific error page or message)
                    }
                }
                catch (Exception ex)
                {
                    // Log the exception details
                    // Example: _logger.LogError($"Failed to create event. Exception: {ex.Message}");
                    // Handle the exception (e.g., return a specific error page or message)
                }
            }

            // If all else fails, return a general error response
            throw new Exception($"Failed to create event");
        }

        public string? GetBase64Image(IFormFile image)
        {
            if (image == null)
                return null;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();
                return Convert.ToBase64String(bytes);
            }
        }

    }


}
