using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Entities
{
    public class ApplicationRole:IdentityRole
    {
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}
