using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using SchoolClient.Models;
using Newtonsoft.Json;
using System.Text;
namespace SchoolClient.Pages
{
    public class AddStudentModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Class> classes { get; set; }

        public bool GetBranchesError { get; private set; }
        public AddStudentModel(IHttpClientFactory clientFactory)
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
        public async Task<IActionResult> OnPost(string name, string gender, int classid)
        {
            Student student = new Student();
            student.Fullname = name;
            student.Gender = gender;
            student.ClassId = classid;

            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:3776/api/students", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Students");
            }
            else
            {
                return null;
            }

        }
    }
}