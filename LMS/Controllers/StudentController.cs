using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Model;
namespace LMS.Controllers
{
    [Route("api/student")]
    public class StudentController : Controller
    {
        private ILMSDataStore _dbstore;
        public StudentController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _dbstore.GetAllStudent();
            //sonResult resultJSObj = new JsonResult(result);
            return Ok(result);
        }

        [HttpGet("{StudentId}")]
        public IActionResult Get(string StudentId)
        {
            IActionResult result;
            var student = _dbstore.GetStudent(StudentId);
            if (student != null)
            {
                result = Ok(student);
            }
            else
            {
                result = NotFound();
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody]Student student)
        {
            Student newStudent = Student.CreateNewStudentFromBody(student);
            _dbstore.AddStudent(newStudent);
            //_dbstore.Save();
           
            return Ok();
        }

        [HttpPut("{StudentId}")]
        public IActionResult Put(string StudentId, [FromBody]Student input)
        {
            Student newStudent = Student.CreateNewStudentFromBody(input);
            _dbstore.EditStudent(StudentId, newStudent);
            return Ok();
        }

        [HttpDelete("{StudentId}")]
        //[Authorize(Roles = "admin")]
        public IActionResult Delete(string StudentId)
        {
            _dbstore.RemoveStudent(StudentId);
            return NotFound();
        }
    }
}
