using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Interfaces
{
    public interface IUnitOfWork
    {
        public IEmployeeRepository employeeRepository { get; set; }

        public IDepartmentRepository departmentRepository { get; set; }

       

        public int commit();

    }
}
