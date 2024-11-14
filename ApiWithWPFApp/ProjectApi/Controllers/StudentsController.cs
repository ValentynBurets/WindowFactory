using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CodeFirstDatabaseModel;

namespace ProjectApi.Controllers
{
    public class StudentsController : Controller
    {
        private DataManager manager = new DataManager();
        public ActionResult<Student> Get()
        {
            IEnumerable<Student> students = manager.GetStudent();
            if (students != null)
            {
                return Ok(students);
            }
            else
            {
                return NoContent();
            }
        }
        [HttpGet]
        public ActionResult<Student> Get(int id)
        {
            try
            {
                Student student = manager.GetStudent(id); 
                if (student != null)
                {
                    return Ok(student);
                }
                else
                {
                    return NotFound( "Student with id " + id + " not found");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
        }
        [HttpPost]
        public ActionResult<Student> Post([System.Web.Http.FromBody] Student student)
        {
            try
            {
                manager.AddStudent(student);
                var uri = new Uri("api/student/"+ student.StudentId.ToString());
                return Created(uri, student);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }
}
