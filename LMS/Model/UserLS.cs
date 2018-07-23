using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Model
{
    public class UserLS
    {
        public static UserLS CreateNewUserLSFromBody(UserLS userLSFromBody)
        {
            UserLS newUserLS = new UserLS();
            newUserLS.StudentId = userLSFromBody.StudentId;

            return newUserLS;
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public bool   IsAdmin { get; set; }

        public string StudentId { get; set; }
        public string LecturerId { get; set; }

        public Student Student { get; set; }
        public Lecturer Lecturer { get; set; }

    }
}
