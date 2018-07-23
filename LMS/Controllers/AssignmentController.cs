using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using LMS.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LMS.Controllers
{
    [Route("api/assignment")]
    public class AssignmentController : Controller
    {

        private ILMSDataStore _dbstore;
        public AssignmentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllAssignments());
        }

        // GET api/<controller>/5
        [HttpGet("{assignmentId}")]
        public IActionResult Get(string assignmentId)
        {
            IActionResult result;
            var assignment = _dbstore.GetAssignment(assignmentId);
            if (assignment != null)
            {
                result = Ok(assignment);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Assignment input)
        {
            _dbstore.AddAssignment(input);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult Put(string AssignmentId, [FromBody]Assignment input)
        {
            _dbstore.EditAssignment(AssignmentId, input);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string AssignmentId)
        {
            _dbstore.RemoveAssignment(AssignmentId);
            return NotFound();
        }

    }
}
