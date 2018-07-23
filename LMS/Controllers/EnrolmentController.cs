using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LMS.Model;
using LMS.Model.DTO;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    [Route("api/enrolment")]
    public class EnrolmentController : Controller
    {
        private ILMSDataStore _dbstore;
        public EnrolmentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllEnrolments());
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]EnrolmentDTO value)
        {
            //Console.WriteLine("bbb");
            _dbstore.AddEnrolment(value.StudentId,value.CourseId);
        }

        // DELETE api/value
        [HttpDelete]
        public void Delete([FromBody]EnrolmentDTO value)
        {
            _dbstore.RemoveEnrolment(value.StudentId, value.CourseId);
        }
    }
}
