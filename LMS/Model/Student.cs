using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace LMS.Model
{
    public class Student
    {
        // None = nothing // Identity ++  //Computed + annd -
        //[Key]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)] // None = nothing // Identity ++  //Computed + annd -


        public static Student CreateNewStudentFromBody(Student studentFromBody)
        {
            Student newStudent = new Student();
            //Mapping value in here
            //Ignore the ID
            newStudent.StudentId = studentFromBody.StudentId;
            newStudent.GPA = studentFromBody.GPA;
            //newStudent.Name = studentFromBody.Name;
            //newStudent.StudentDetail = studentFromBody.StudentDetail;
            return newStudent;
        }


        public string StudentId { get; set; }
        public decimal GPA { get; set; }

        // foreign keys
        //public string UserId { get; set; }
        public UserLS UserLS { get; set; }

        public ICollection<Enrolment> Enrolments { get; set; }
        public ICollection<Study> Studies { get; set; }

        
        // Constructor
        public Student()
        {
        }
    }
}
