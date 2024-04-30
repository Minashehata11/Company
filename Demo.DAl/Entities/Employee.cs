using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Entities
{
    public class Employee:BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4, ErrorMessage = "Name Must be Greater Than 4 Character!!")]
        [DisplayName("Employee Name")]
        public string? Name { get; set; }

        [StringLength(20)]
        public string Address { get; set; }
        [Column(TypeName = "money")]
        public double Salary { get; set; }
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public DateTime HiringDate { get; set; }=DateTime.Now;

        public Department Department { get; set; }  //Navigation Prop

        public int DepartmentId { get; set; }

        public string ImageUrl { get; set; }


    }
}
