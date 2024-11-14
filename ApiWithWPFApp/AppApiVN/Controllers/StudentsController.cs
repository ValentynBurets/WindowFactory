using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodeFirstDatabaseModel;

namespace AppApiVN
{
    public class StudentsController : ApiController
    {
        private DataManager manager = new DataManager();
        public HttpResponseMessage Get()
        {
            IEnumerable<Student> students = manager.GetStudent();
            if (students != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, students);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NoContent, "No users in database");
            }
        }

        public HttpResponseMessage Get(int id)
        {
            Student student = manager.GetStudent(id);
            if (student != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, student);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tariff with id " + id + " not found");
            }
        }

        public HttpResponseMessage Post([System.Web.Http.FromBody] Student student)
        {
            try
            {
                manager.AddStudent(student);
                var message = Request.CreateResponse(HttpStatusCode.Created, student);
                message.Headers.Location = new Uri(Request.RequestUri + student.StudentId.ToString());
                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            try
            {
                bool ok =  manager.DeleteStudent(id);
                if (ok is false)
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Student with such id not found");
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}