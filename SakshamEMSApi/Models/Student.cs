using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SakshamEMSApi.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        public string Name { get; set; }
        public string StudentNo { get; set; }
        public string Branch { get; set; }
        public string Year { get; set; }
        public string ContactNumber { get; set; }
        public string SportsInterested { get; set; }
        public string Hosteler { get; set; }
    }
}