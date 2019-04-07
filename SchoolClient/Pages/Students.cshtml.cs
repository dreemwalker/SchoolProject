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
    public class StudentsModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Student> students { get; set; }

        public bool GetBranchesError { get; private set; }
        public StudentsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            students = new List<Student>();
            
        }
        public async Task OnGet()
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
            "http://localhost:3776/api/students");
       

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                students = await response.Content
                    .ReadAsAsync<IEnumerable<Student>>();
            }
            else
            {
               
            }
        }
    }
}