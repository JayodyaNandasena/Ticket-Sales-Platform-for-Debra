using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using System.Text.Json;

namespace Debra_WebClient.Pages
{
    public class PartnerProfileModel : PageModel
    {
        private readonly ILogger<PartnerProfileModel> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
		public int PartnerId { get; private set; }
		public Partners Partner { get; private set; }

		[BindProperty]
		public Partners editedPartner { get; set; }
		[BindProperty]
		public string editedUsername { get; set; }
		[BindProperty]
		public string editedPassword { get; set; }

		public PartnerProfileModel(ILogger<PartnerProfileModel> logger, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> OnGet()
        {
            PartnerId = _httpContextAccessor.HttpContext.Session.GetInt32("PartnerId") ?? 0;

            string url = "https://localhost:7102/Partner/ById?id="+PartnerId;
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var reply = await response.Content
                        .ReadFromJsonAsync<OperationResultResponse<Partners>>();

                    if (reply.Status == Status.Success)
                    {
                        Partner = reply.Result;

                        return Page();
                    }
                }
                else
                    throw new Exception(response.StatusCode.ToString());

            }
            return Page();
        }

        public async Task<IActionResult> OnPost()
		{
			// Ensure newPartner is not null
			if (editedPartner == null)
			{
				editedPartner = new Partners();
			}

			// Ensure newPartner.Account is not null
			if (editedPartner.Account == null)
			{
				editedPartner.Account = new PartnerAccounts();
			}

			string url = "https://localhost:7102/Partner";

			var content = new StringContent(JsonSerializer.Serialize(editedPartner), Encoding.UTF8, "application/json");

			using (HttpClient client = new HttpClient())
			{
				HttpResponseMessage response = await client.PostAsync(url, content);

				if (response.IsSuccessStatusCode)
				{
					return RedirectToPage("PartnerProfile");
				}
			}

			throw new Exception($"Failed to update partner");
        }
    }
}
