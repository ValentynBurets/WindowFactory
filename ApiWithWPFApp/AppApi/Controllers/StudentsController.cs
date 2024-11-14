using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using CodeFirstDatabaseModel;

namespace AppApi.Controllers
{
    public class StudentsController : Controller
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
            TariffViewModel tariff = _mapper.Map<Tariff, TariffViewModel>(_repo.GetItemById(id));
            if (tariff != null)
            {
                return Request.CreateResponse(HttpStatusCode.OK, tariff);
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, "Tariff with id " + id + " not found");
            }
        }

        public HttpResponseMessage Post([System.Web.Http.FromBody] TariffViewModel tariff)
        {
            try
            {
                _repo.AddTariff(_mapper.Map<TariffViewModel, Tariff>(tariff));
                var message = Request.CreateResponse(HttpStatusCode.Created, tariff);
                message.Headers.Location = new Uri(Request.RequestUri + tariff.TariffID.ToString());
                return message;
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, e);
            }
        }
    }
}