using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Options;

using LMS.Model;

namespace LMS.Controllers
{
    [Route("api/lecturer")]
    public class LecturerController : Controller
    {
        private ILMSDataStore _dbstore;
        public LecturerController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }

        // GET: api/<controller>
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllLecturer());
        }

        // GET api/<controller>/5
        [HttpGet("{lecturerId}")]
        public IActionResult Get(string lecturerId)
        {
            IActionResult result;
            var lecturer = _dbstore.GetLecturer(lecturerId);
            if (lecturer != null)
            {
                result = Ok(lecturer);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody]Lecturer input)
        {
            Lecturer newLecturer = Lecturer.CreateNewLecturerFromBody(input);
            _dbstore.AddLecturer(newLecturer);
            return Ok();
        }

        // PUT api
        [HttpPut("{lecturerId}")]
        public IActionResult Put(string lecturerId, [FromBody]Lecturer input)
        {
            Lecturer newLecturer = Lecturer.CreateNewLecturerFromBody(input);
            _dbstore.EditLecturer(lecturerId, newLecturer);
            return Ok();
        }

        // DELETE api
        [HttpDelete("{lecturerId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(string lecturerId)
        {
            _dbstore.RemoveLecturer(lecturerId);
            return NotFound();
        }

    }
}
