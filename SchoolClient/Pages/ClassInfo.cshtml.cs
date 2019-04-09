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
    public class ClassInfoModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        public Student perfect { get; set; }
        public Class cl { get; set; }

        public bool GetBranchesError { get; private set; }
        public ClassInfoModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
            cl = new Class();
            perfect = new Student();

        }
        public async Task OnGet(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get,
        "http://localhost:3776/api/Classes/" + id);


            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                IEnumerable<Class> t = await response.Content
                        .ReadAsAsync<IEnumerable<Class>>();
                cl = t.FirstOrDefault();
               // if (cl.Perfects.Count != 0 )
               // perfect = cl.Perfects.FirstOrDefault().Student;

            }
            else
            {

            }
        }
    }
}