using Demo.BAL.Interfaces;
using Demo.DAl.Context;
using Demo.DAl.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Repository
{
    public class EmployeeRepository :GenericRepository<Employee> ,IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;   // Dependancy Injection

        public EmployeeRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        
        public Employee GetEagerLoadingById(int? id) 
        {
       
           return _context.Employees.Include(x=>x.Department).FirstOrDefault(x=>x.Id==id);
        }

        public IEnumerable<Employee> Search(string? name)
        {
            var employees = _context.Employees.Where(emp => emp.Name.Trim().ToLower().Contains(name.Trim().ToLower()));
            return employees;
        }
        
        //public int Add(Employee employee)
        //{
        //   _context.Employees.Add(employee);
        //   return _context.SaveChanges();
        //}

        //public int Delete(Employee employee)
        //{
        //    _context.Employees.Remove(employee);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Employee> GetAll()
        //{
        //   return _context.Employees.ToList();
        //}

        //public Employee GetById(int? Id)
        //=> _context.Employees.SingleOrDefault(x => x.Id == Id);

        //public int Update(Employee employee)
        //{
        //    _context.Employees.Update(employee);
        //    return _context.SaveChanges();
        //}
    }
}
