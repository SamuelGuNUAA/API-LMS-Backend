using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace LMS.Model
{
    public class Course
    {
        public static Course CreateNewCourseFromBody(Course courseFromPost)
        {
            Course newCourse = new Course();
            //Mapping value in here
            //Ignore the ID
            newCourse.Name = courseFromPost.Name;
            newCourse.CourseId = courseFromPost.CourseId;
            newCourse.Introduction = courseFromPost.Introduction;
            //newCourse.CourseCode = courseFromPost.CourseCode;
            return newCourse;
        }

        /*
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        */
        //public string Id { get; set; }
        public string Name { get; set; }
        public string CourseId { get; set; }
        public string Introduction { get; set; }

        public string LecturerId { get; set; }
        public Lecturer Lecturer { get; set; }

        //[JsonProperty("Enrolments", NullValueHandling = NullValueHandling.Ignore)]
        [JsonProperty("Enrolments")]
        public ICollection<Enrolment> Enrolments { get; set; }
        public ICollection<Assignment> Assignments { get; set; }
    }
}
