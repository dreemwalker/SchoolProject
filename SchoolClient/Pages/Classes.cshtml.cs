using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using SchoolClient.Models;
namespace SchoolClient.Pages
{
    public class ClassesModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Class> classes { get; set; }

        public bool GetBranchesError { get; private set; }
        public ClassesModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            classes = new List<Class>();

        }
        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:3776/api/classes");


            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                classes = await response.Content
                    .ReadAsAsync<IEnumerable<Class>>();
            }
            else
            {

            }
        }
    }
}