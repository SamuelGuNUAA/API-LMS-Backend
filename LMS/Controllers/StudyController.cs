using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LMS.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    [Route("api/study")]
    public class StudyController : Controller
    {

        private ILMSDataStore _dbstore;
        public StudyController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]Study value)
        {
            _dbstore.AddStudy(value.StudentId, value.AssignmentId);
        }

        [HttpPut("{AssignmentGrade}")]
        public void Pus([FromBody]Study value)
        {
            _dbstore.UpdateStudy(value.StudentId, value.AssignmentId, value);
        }

        [HttpDelete]
        public IActionResult Delete([FromBody]Study value)
        {
            _dbstore.RemoveStudy(value.StudentId, value.AssignmentId);
            return NotFound();
        }
    }
}
