using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace SakshamEMSApi.Models
{
    public class Student
    {
        public int StudentID { get; set; }
        [Required(ErrorMessage="Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage ="Student Number is required")]
        [RegularExpression(@"^([1][4-7]\d{5}[Dd]{0,1})",ErrorMessage ="Invalid Student Number")]
        public string StudentNo { get; set; }
        [Required(ErrorMessage = "Branch is required")]
        public string Branch { get; set; }
        [Required(ErrorMessage = "Year is required")]
        public string Year { get; set; }
        [Required(ErrorMessage = "Contact number is required")]
        [RegularExpression(@"([6-9][0-9]{9})", ErrorMessage = "Invalid phone number")]
        public string ContactNumber { get; set; }
        [Required(ErrorMessage = "Select atleast one")]
        public string SportsInterested { get; set; }
        [Required(ErrorMessage = "Choose one")]
        public string Hosteler { get; set; }
        public string Gender { get; set; }
    }
}