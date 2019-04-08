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
    public class EditStudentModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;

        public IEnumerable<Class> classes { get; set; }
        public Student student;
        public bool GetBranchesError { get; private set; }
        public EditStudentModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            classes = new List<Class>();
            student = new Student();
        }
        public async Task OnGet(int id)
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
            var request2 = new HttpRequestMessage(HttpMethod.Get,
        "http://localhost:3776/api/students/" + id);


            var client2 = _clientFactory.CreateClient();

            var response2 = await client2.SendAsync(request2);

            if (response2.IsSuccessStatusCode)
            {
                IEnumerable<Student> t = await response2.Content
                        .ReadAsAsync<IEnumerable<Student>>();
                student = t.FirstOrDefault();
            }
        }
        public async Task<IActionResult> OnPost(int id, string name, string gender,int classid)
        {
            Student student = new Student();
            student.Id = id;
            student.Fullname = name;
            student.Gender = gender;
            student.ClassId = classid;



            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:3776/api/students/" + id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Students");
            }
            else
            {
                return RedirectToPage("Error");
            }

        }
    }
}