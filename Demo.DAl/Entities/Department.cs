using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Entities
{
    public class Department:BaseEntity
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Name is Required")]
        [MinLength(4,ErrorMessage ="Name Must be Greater Than 4 Character!!")]
        [DisplayName("Department Name")]
        public string? Name { get; set; }
        
        public int Code { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
