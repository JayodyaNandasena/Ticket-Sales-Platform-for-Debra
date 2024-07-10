using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class AddEventModel : PageModel
    {
        private readonly ILogger<AddEventModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        [BindProperty]
        public CreateEvents newEvent { get; set; }
        [BindProperty]
        public string MusicianName { get; set; }
        [BindProperty]
        public string BandName { get; set; }

        [BindProperty]
        public IFormFile EventImage { get; set; }
        [BindProperty]
        public IFormFile MusicianImage { get; set; }
        [BindProperty]
        public IFormFile BandImage { get; set; }

        public AddEventModel(ILogger<AddEventModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPostCreateAsync()
        {

            // Ensure newPartner is not null
            if (newEvent == null)
            {
                newEvent = new CreateEvents();
            }

            // Ensure newPartner.Account is not null
            if (newEvent.Musicians == null)
            {
                newEvent.Musicians = [];
            }
            // Ensure newPartner.Account is not null
            if (newEvent.Bands == null)
            {
                newEvent.Bands = [];
            }
            // Ensure newPartner.Account is not null
            if (newEvent.Tickets == null)
            {
                newEvent.Tickets = new CreateTickets();
            }

            string url = "https://localhost:7102/Event";

            newEvent.Musicians.Add(
                new Musicians(
                        MusicianName,
                        ConvertToBase64Async(MusicianImage)
                    )
                );

            newEvent.Bands.Add(
                new Bands(
                        BandName,
                        ConvertToBase64Async(BandImage)
                    )
                );
            newEvent.ImageBase64 = await ConvertToBase64Async(EventImage);

            newEvent.PartnerId = _httpContextAccessor.HttpContext.Session.GetInt32("PartnerId") ?? 0;

            var content = new StringContent(JsonSerializer.Serialize(newEvent), Encoding.UTF8, "application/json");

            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.PostAsync(url, content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToPage("SignIn");
                }
            }

            throw new Exception($"Failed to create event");
        }

        public static async Task<string> ConvertToBase64Async(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] fileBytes = memoryStream.ToArray();
                return Convert.ToBase64String(fileBytes);
            }
        }
    }

    
}
