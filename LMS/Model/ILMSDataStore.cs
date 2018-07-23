using System;
using System.Collections.Generic;


namespace LMS.Model
{
    public interface ILMSDataStore
    {
        //User API
        UserLS GetUserByName(string UserName);
        void AddUser(UserLS userLS);
        int ValidateLoginFromDB(UserLS userLS);
        //bool UserEmailCheck(User user);
        //void AddEmailAndPassword(User user);

        //Course API
        IEnumerable<Course> GetAllCourses();
        Course GetCourse(string courseId);
        void AddCourse(Course course);
        void EditCourse(string courseId, Course course);
        void RemoveCourse(string courseId);

        //Student API
        IEnumerable<Student> GetAllStudent();
        Student GetStudent(string StudentId);
        void AddStudent(Student student);
        void EditStudent(string StudentId, Student student);
        void RemoveStudent(string StudentId);

        //Lecturer API
        IEnumerable<Lecturer> GetAllLecturer();
        Lecturer GetLecturer(string LecturerId);
        void AddLecturer(Lecturer lecturer);
        void EditLecturer(string LecturerId, Lecturer lecturer);
        void RemoveLecturer(string LecturerId);

        //Enrollment API
        IEnumerable<Enrolment> GetAllEnrolments();
        void AddEnrolment(string studentId, string courseId);
        void RemoveEnrolment(string studentId, string courseId);

        //Study API
        void AddStudy(string StudentId, string AssignmentId);
        void UpdateStudy(string StudentId, string AssignmentId, Study study);
        void RemoveStudy(string StudentId, string AssignmentId);

        //Assignment API
        IEnumerable<Assignment> GetAllAssignments();
        Assignment GetAssignment(string AssignmentId);
        void AddAssignment(Assignment assignment);
        void EditAssignment(string AssignmentId, Assignment assignment);
        void RemoveAssignment(string AssignmentId);

        bool Save();
    }
}
