using System;
namespace LMS.Model
{
    public class Study
    {

        public static Study CreateNewStudyFromBody(Study studyFromPost)
        {
            Study newCourse = new Study();
            //Mapping value in here
            //Ignore the ID
            newCourse.AssignmentGrade = studyFromPost.AssignmentGrade;
            //newCourse.CourseId = studyFromPost.CourseId;
            //newCourse.LecturerId = courseFromPost.LecturerId;
            //newCourse.CourseCode = courseFromPost.CourseCode;
            return newCourse;
        }

        //public int Id { get; set; }

        public string StudentId { get; set; }
        public string AssignmentId { get; set; }

        public decimal AssignmentGrade { get; set; }

        // for foreign key
        public Student Student { get; set; }
        public Assignment Assignment { get; set; }

        // constructor
        public Study()
        {
        }
    }
}
