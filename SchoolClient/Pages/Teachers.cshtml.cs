using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolClient.Models;
using System.Net.Http;
namespace SchoolClient.Pages
{
    public class TeachersModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Teacher> teachers { get; set; }

        public bool GetBranchesError { get; private set; }
        public TeachersModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            teachers = new List<Teacher>();

        }
        public async Task<IActionResult> OnGetDelete(int id)
        {

            /*  var request = new HttpRequestMessage(HttpMethod.Delete,
              "http://localhost:3776/api/teachers/{id}");*/

          var  TeachersBaseUrl="http://localhost:3776/api/teachers/";

                        var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync(new Uri(TeachersBaseUrl + id));



            if (response.IsSuccessStatusCode)
            {
               
               return RedirectToPage("Teachers");
            }
            else
            {
                return RedirectToPage("Error");
            }
           
        }
        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:3776/api/teachers");


            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                teachers = await response.Content
                    .ReadAsAsync<IEnumerable<Teacher>>();
            }
            else
            {

            }
        }
    }
}