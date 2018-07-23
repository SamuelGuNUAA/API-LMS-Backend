using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using LMS.Model;

namespace LMS.Controllers
{
    [Route("api/course")]
    public class CourseController : Controller
    {
        private ILMSDataStore _dbstore;
        public CourseController(ILMSDataStore dbstore)
        {
            _dbstore = dbstore;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_dbstore.GetAllCourses());
        }


        [HttpGet("{courseId}")]
        public IActionResult Get(string courseId)
        {
            IActionResult result;
            var course = _dbstore.GetCourse(courseId);
            if(course != null) {
                result = Ok(course);
            } else {
                result = NotFound();
            }
            return result;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult Post([FromBody]Course input)
        {
            Course newCourse = Course.CreateNewCourseFromBody(input);
            _dbstore.AddCourse(newCourse);
            return Ok();
        }

        [HttpPut("{courseId}")]
        public IActionResult Put(string courseID,[FromBody]Course input)
        {
            Course newCourse = Course.CreateNewCourseFromBody(input);
            _dbstore.EditCourse(courseID,newCourse);
            return Ok();
        }

        [HttpDelete("{courseId}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(string courseId)
        {
            _dbstore.RemoveCourse(courseId);
            return NotFound();
        }
    }
}
