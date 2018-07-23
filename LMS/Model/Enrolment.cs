using System;
namespace LMS.Model
{
    public class Enrolment
    {
        public string StudentId { get; set; }
        public string CourseId { get; set; }

        public Student Student { get; set; }
        public Course Course { get; set; }

        public decimal CourseGrade { get; set; }
    }
}
