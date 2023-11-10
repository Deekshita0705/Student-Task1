using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;


namespace Student_Task1.Models
{
    public class Student
    {
        public int Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string BranchName { get; set; }

        [Required]
        public string YearName { get; set; }
        public int BranchId { get; set; }
        public SelectList BranchList { get; set; }
        public int YearId { get; set; }
        public SelectList YearList { get; set; }
    }
}
