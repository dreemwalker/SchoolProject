using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SchoolClient.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
namespace SchoolClient.Pages
{
    public class EditTeacherModel : PageModel
    {
        public Teacher teacher { get; set; }
        private readonly IHttpClientFactory _clientFactory;

       

        public bool GetBranchesError { get; private set; }
        public EditTeacherModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            teacher = new Teacher();

        }
        public   async Task OnGet(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
        "http://localhost:3776/api/teachers/"+id);


            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
             IEnumerable<Teacher>  t = await response.Content
                     .ReadAsAsync<IEnumerable<Teacher>>();
                teacher = t.FirstOrDefault();
            }
            else
            {
                
            }
        }
        public async Task<IActionResult> OnPost(int id, string name, string position)
        {
            Teacher teacher = new Teacher();
            teacher.Id = id;
            teacher.Fullname = name;
            teacher.Position = position;



            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(teacher), Encoding.UTF8, "application/json");
            var response = await client.PutAsync("http://localhost:3776/api/teachers/"+id, content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Teachers");
            }
            else
            {
                return RedirectToPage("Error");
            }

        }
    }
}