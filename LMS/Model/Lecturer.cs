using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LMS.Model
{
    public class Lecturer
    {
        
        public static Lecturer CreateNewLecturerFromBody(Lecturer lecturerFromBody)
        {
            Lecturer newLecturer = new Lecturer();
            //Mapping value in here
            //Ignore the ID
            newLecturer.LecturerId = lecturerFromBody.LecturerId;
            newLecturer.Salary = lecturerFromBody.Salary;
            newLecturer.Introduction = lecturerFromBody.Introduction;
            return newLecturer;
        }
        

        //public int Id { get; set; }
        public string LecturerId { get; set; }
        public decimal Salary { get; set; }
        public string Introduction { get; set; }

        //public string UserId { get; set; }
        public UserLS UserLS { get; set; }

        public ICollection<Course> courses { get; set; }

        public Lecturer()
        {
        }
    }
}
