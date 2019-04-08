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
    public class AddTeacherModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;


        public bool GetBranchesError { get; private set; }


        public AddTeacherModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
           

        }
        public void OnGet()
        {
        }
        public async Task OnPost(string name,string position)
        {
            Teacher teacher = new Teacher();
            teacher.Fullname = name;
            teacher.Position = position;
          


            var client = _clientFactory.CreateClient();
            var content = new StringContent(JsonConvert.SerializeObject(teacher), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("http://localhost:3776/api/teachers",content);

            if (response.IsSuccessStatusCode)
            {
               
            }
            else
            {

            }

        }
   
    }
}