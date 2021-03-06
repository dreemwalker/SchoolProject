﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SchoolProjectAPI.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SchoolProjectAPI.Controllers
{
    [Route("api/[controller]")]
    public class ClassesController : Controller
    {


        SchoolDBContext db;
        public ClassesController(SchoolDBContext context)
        {
            this.db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Class> Get()
        {
            var ClassList = db.Classes.Include(s => s.Students)
                .Include(p => p.Perfects)
                .Include(ct => ct.ClassTeacher).ThenInclude(t => t.TeacherClass);
            return ClassList.ToList();

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IEnumerable<Class> Get(int id)
        {
            var ClassList = db.Classes.Include(s => s.Students)
                .Include(p => p.Perfects)
                .Include(ct => ct.ClassTeacher).ThenInclude(t => t.TeacherClass).Where(x => x.Id == id);
            return ClassList.ToList();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
