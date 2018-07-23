using System;
using System.Collections.Generic;



namespace LMS.Model
         
{
    public class Assignment
    {
        //public int Id { get; set; }
        public string AssignmentId { get; set; }
        public string AssignmentName { get; set; }

        //To Course
        public string CourseId { get; set; }
        public Course course { get; set; }

        //To Study
        public ICollection<Study> Studies { get; set; }

        public Assignment()
        {
        }
    
    }
}