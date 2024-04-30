using Demo.DAl.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Demo.PL.Models
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "Name Must be Greater Than 4 Character!!")]
        [DisplayName("Employee Name")]
        public string? Name { get; set; }

        [StringLength(20)]
        public string Address { get; set; }
        [Column(TypeName = "money")]
        public double Salary { get; set; } = 1000;
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime HiringDate { get; set; } = DateTime.Now;
        public int DepartmentId { get; set; }
        
        public Department? Department { get; set; }  //Navigation Prop

        public string? ImageUrl { get; set; }

        public IFormFile File { get; set; }

    }
}
