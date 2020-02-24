using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCRegistrationForm.Models
{
    public class StudentModel
    {
        public int StudentID { get; set; }
        public String StudentName { get; set; }
        public String EmailID { get; set; }
        public int Marks { get; set; }
    }
}