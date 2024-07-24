using Debra_WebClient.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace Debra_WebClient.Pages
{
    public class ViewEventModel : PageModel
    {
        [BindProperty]
        public ReadEvents _event { get; set; }
        public async Task OnGet(int id)
        {
            string url = "https://localhost:7102/Event/ById?Id=" + id;
            
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var reply = await response.Content
                        .ReadFromJsonAsync<OperationResultResponse<ReadEvents>>();

                    if (reply.Status == Status.Success)
                    {
                        _event = reply.Result;
                    }
                }
                else
                    throw new Exception(response.StatusCode.ToString());

            }

        }
    }
}
