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
        //string message = "";
        public bool GetBranchesError { get; private set; }
        public StudentsModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            students = new List<Student>();
           
        }
   
        public async Task<IActionResult> OnGetDelete(int id)
        {

            var StudentsBaseUrl = "http://localhost:3776/api/students/";

            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync(new Uri(StudentsBaseUrl + id));



            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("Students");
                
            }
           
            else
            {
                return RedirectToPage("Error");
            }
            //  return RedirectToPage("Teachers");
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