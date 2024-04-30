using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.DAl.Entities
{
    public class ApplicationUser:IdentityUser
    {
        public bool? IsActive { get; set; }

        public Admin? Admin { get; set; }
    }
}
