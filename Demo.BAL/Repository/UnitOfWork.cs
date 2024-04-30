using Demo.BAL.Interfaces;
using Demo.DAl.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IEmployeeRepository employeeRepository { get ; set; }
        public IDepartmentRepository departmentRepository { get ; set ; }

        public UnitOfWork(ApplicationDbContext context)
        {
            employeeRepository = new EmployeeRepository(context);
            departmentRepository = new DepartmentRepository(context);
            _context = context;
        }
        public int commit()
        {
            return _context.SaveChanges();
        }
    }
}
