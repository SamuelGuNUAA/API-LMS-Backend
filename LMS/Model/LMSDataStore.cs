using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LMS.Model
{
    public class LMSDataStore : ILMSDataStore
    {
        private LMSDBContext _ctx;

        public LMSDataStore(LMSDBContext ctx)
        {
            _ctx = ctx;
        }

        // For Course API 
        public IEnumerable<Course> GetAllCourses()
        {
            var result = _ctx.Courses
                             /*.Include(s => s.Enrollments).ThenInclude(cw => cw.Student)*/
                             .OrderBy(course => course.CourseId).ToList();
            return result;
        }

        public Course GetCourse(string courseId) {
            return _ctx.Courses.Find(courseId);
        }


        public void AddCourse(Course course)
        {
            _ctx.Courses.Add(course);
            Save();
        }

        public void EditCourse(string courseId, Course course) {
            Course courseToEdit = _ctx.Courses.Find(courseId);
            courseToEdit.Name = course.Name;
            courseToEdit.Introduction = course.Introduction;
            //TODO: Replace the rest Attrubte
            Save();
        }

        public void RemoveCourse(string courseId)
        {
            Course courseToRemove = _ctx.Courses.Find(courseId);
            if (courseToRemove != null)
            {
                _ctx.Courses.Remove(courseToRemove);
            }
            Save();
        }
        //End of Course API


        // For Student API 
        public IEnumerable<Student> GetAllStudent()
        {
            var result = _ctx.Students
                             .Include(u => u.UserLS)
                             .Include(s => s.Enrolments).ThenInclude(cw => cw.Course)
                             .OrderBy(student => student.StudentId).ToList();

            return result;
        }

        /*
        public IEnumerable<Student> GetStudent(string StudentId)
        {
            var result = _ctx.Students
                .Include(s => s.Enrolments).Where(s => s.StudentId == StudentId);
                //.OrderBy(student => student.StudentId).ToList();
            return result;
        }
        */
        public Student GetStudent(string StudentId)
        {
            Student result = _ctx.Students
                                 .Include(u => u.UserLS)
                                 .Include(s => s.Enrolments).Where(s => s.StudentId == StudentId).First();
                               //.OrderBy(student => student.StudentId).ToList();
            return result;
        }

        public void AddStudent(Student student)
        {
            _ctx.Students.Add(student);
            Save();
        }

        public void EditStudent(string StudentId, Student student)
        {
            Student studentToEdit = _ctx.Students.Find(StudentId);
            studentToEdit.GPA = student.GPA;
            //TODO: Replace the rest Attrubte
            Save();
        }

        public void RemoveStudent(string StudentId)
        {
            Student studentToRemove = _ctx.Students.Find(StudentId);
            if (studentToRemove != null)
            {
                _ctx.Students.Remove(studentToRemove);
            }
            Save();
        }
        //End of Student API


        // For Lecturer API
        public IEnumerable<Lecturer> GetAllLecturer()
        {
            var result = _ctx.Lecturers
                             /*.Include(s => s.Enrolments).ThenInclude(cw => cw.Course)*/
                             .OrderBy(lecturer => lecturer.LecturerId).ToList();
            return result;
        }

        public Lecturer GetLecturer(string LecturerId)
        {
            return _ctx.Lecturers.Find(LecturerId);
        }

        public void AddLecturer(Lecturer lecturer)
        {
            _ctx.Lecturers.Add(lecturer);
            Save();
        }

        public void EditLecturer(string LecturerId, Lecturer lecturer)
        {
            Lecturer lecturerToEdit = _ctx.Lecturers.Find(LecturerId);
            lecturerToEdit.Salary = lecturer.Salary;
            //TODO: Replace the rest Attrubte
            Save();
        }

        public void RemoveLecturer(string LecturerId)
        {
            Lecturer lecturerToRemove = _ctx.Lecturers.Find(LecturerId);
            if (lecturerToRemove != null)
            {
                _ctx.Lecturers.Remove(lecturerToRemove);
            }
            Save();
        }
        //End of Lecturer API


        // For Enrolment API 
        public IEnumerable<Enrolment> GetAllEnrolments()
        {
            var result = _ctx.Enrolments
                             .OrderBy(e => e.StudentId).ToList();
            return result;
        }

        public void AddEnrolment(string studentId, string courseId) {
            Student student = _ctx.Students.Find(studentId);

            Course course = _ctx.Courses.Find(courseId);

            var newEnrol = new Enrolment { StudentId = studentId, CourseId = courseId };
            _ctx.Enrolments.Add(newEnrol);
            Save();
        }

        public void RemoveEnrolment(string studentID, string courseID)
        {
            var enrol = _ctx.Enrolments.Find(courseID, studentID);
            if (enrol != null)
            {
                _ctx.Enrolments.Remove(enrol);
            }
            Save();
        }
        //End of Enrolment API


        // For User API
        public UserLS GetUserByName(string UserName)
        {
            return _ctx.UserLSs
                       .Where(s => s.UserName == UserName)
                       .First();
        }

        public void AddUser(UserLS userLS)
        {
            _ctx.UserLSs.Add(userLS);
            Save();
        }

        //Login
        public int ValidateLoginFromDB(UserLS userLS)
        {
            var   UserSearchInDB = _ctx.UserLSs
                                 .Where(s => s.UserName == userLS.UserName)
                                 .First();
            //Not found
            if(UserSearchInDB == null)
                return 0;
            //User password validation
            if (String.Compare(UserSearchInDB.Password, userLS.Password) == 0)
            {
                if (UserSearchInDB.IsAdmin)
                    return 5;   //Admin
                else
                    return 2;   //General user
            }
            //Password error
            return 1;
        }
/*
        //Register
        public bool UserEmailCheck(User user)
        {
            bool Result = false;
            User NewUser = _ctx.Users.Find(user.Email);
            if (NewUser != null)
            {
                Result = true;
            }
            return Result;
        }

        public void AddEmailAndPassword(User user)
        {
            User NewUser = new User();
            NewUser.Email = user.Email;
            NewUser.Password = user.Password;
            _ctx.Users.Add(NewUser);

        }
        //End of User API
*/

        //For Assignment API
        public IEnumerable<Assignment> GetAllAssignments()
        {
            var result = _ctx.Assignments
                .Include(c => c.CourseId)
                .Include(s => s.Studies).ThenInclude(s => s.Student)
                .OrderBy(a => a.AssignmentId).ToList();
            return result;
        }

        public Assignment GetAssignment(string assignmentId)
        {
            return _ctx.Assignments.Find(assignmentId);
        }

        public void AddAssignment(Assignment assignment)
        {
            _ctx.Assignments.Add(assignment);
            Save();
        }

        public void EditAssignment(string assignmentId, Assignment assignment)
        {
            Assignment assignmentToEdit = _ctx.Assignments.Find(assignmentId);
            assignmentToEdit.AssignmentName = assignment.AssignmentName;
            Save();
        }

        public void RemoveAssignment(string assignmentId)
        {
            var assignment = _ctx.Assignments.Find(assignmentId);
            if (assignment != null)
            {
                _ctx.Assignments.Remove(assignment);
            }
            Save();
        }
        //End of Assignment API


        //For Study API
        public void AddStudy(string studentId, string assgnmentId)
        {
            Student student = _ctx.Students.Find(studentId);
            Assignment assignment = _ctx.Assignments.Find(assgnmentId);

            var newStudy = new Study { StudentId = studentId, AssignmentId = assgnmentId };
            _ctx.Studies.Add(newStudy);
            Save();
        }

        public void UpdateStudy(string studentId, string assignemntId, Study study)
        {
            Study GradeToEdit = _ctx.Studies.Find(assignemntId, studentId);
            GradeToEdit.AssignmentGrade = study.AssignmentGrade;
            //TODO: Replace the rest Attrubte
            Save();
        }
        public void RemoveStudy(string assignmentId, string studentId)
        {
            var study = _ctx.Studies.Find(assignmentId, studentId);
            if (study != null)
            {
                _ctx.Studies.Remove(study);
            }
            Save();
        }
        //End of Study API


        public bool Save()
        {
            //True for success , False should throw exception
            return (_ctx.SaveChanges() >= 0);
        }

    }
}
