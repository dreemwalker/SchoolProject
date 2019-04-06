using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
using SchoolProjectAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace SchoolProjectAPI.Controllers
{
    [Route("api/[controller]")]
    public class TeachersController : Controller
    {
        SchoolDBContext db;
        public TeachersController(SchoolDBContext context)
        {
            this.db = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            var teacherList = db.Teachers.ToList();
            return teacherList;

        }
        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var teacher = db.Teachers;
            if (teacher == null)
                return NotFound();
            return new ObjectResult(teacher);

        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }

            db.Teachers.Add(teacher);
            db.SaveChanges();
            return Ok(teacher);
        }


        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]Teacher teacher)
        {
            if (teacher == null)
            {
                return BadRequest();
            }
            if (!db.Teachers.Any(x => x.Id == teacher.Id))
            {
                return NotFound();
            }

            db.Update(teacher);
            db.SaveChanges();
            return Ok(teacher);
        }


        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Teacher teacher = db.Teachers.FirstOrDefault(x => x.Id == id);
            if (teacher == null)
            {
                return NotFound();
            }
            db.Teachers.Remove(teacher);
            db.SaveChanges();
            return Ok(teacher);
        }
    }
}
