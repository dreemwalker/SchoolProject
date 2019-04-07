using System;
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
    public class StudentsController : Controller
    {
        SchoolDBContext db;
        public StudentsController(SchoolDBContext context)
        {
            this.db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Student> Get()
        {
          var studentList = db.Students.Include(c => c.Class);
            return studentList;

        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var student = db.Students.Include(c => c.Class).Where(x=>x.Id==id);
            if (student == null)
                return NotFound();
            return new ObjectResult(student);
           
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }

            db.Students.Add(student);
            db.SaveChanges();
            return Ok(student);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Student student)
        {
            if (student == null)
            {
                return BadRequest();
            }
            if (!db.Students.Any(x => x.Id == student.Id))
            {
                return NotFound();
            }

            db.Update(student);
            db.SaveChanges();
            return Ok(student);
        }

        // DELETE api/students/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Student student = db.Students.FirstOrDefault(x => x.Id == id);
            if (student == null)
            {
                return NotFound();
            }
            db.Students.Remove(student);
            db.SaveChanges();
            return Ok(student);
        }
    }
}
