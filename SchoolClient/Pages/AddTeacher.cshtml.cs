using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SchoolClient.Pages
{
    public class AddTeacherModel : PageModel
    {
        public void OnGet()
        {
        }
        public void OnPost(string name,string position)
        {
            object client = null;
        }
   
    }
}