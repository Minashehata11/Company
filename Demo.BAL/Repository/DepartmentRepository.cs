using Demo.BAL.Interfaces;
using Demo.DAl.Context;
using Demo.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Repository
{
    public class DepartmentRepository :GenericRepository<Department> , IDepartmentRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartmentRepository(ApplicationDbContext context):base(context) 
        {
            _context = context;
        }
        //public int Add(Department department)
        //{
        //    _context.Add(department);
        //    return _context.SaveChanges();
        //}

        //public int Delete(Department department)
        //{
        //    _context.Remove(department);
        //    return _context.SaveChanges();
        //}

        //public int DeleteById(int? id)
        //{
        //   var dept= _context.Departments.FirstOrDefault(x => x.Id == id);
        //     _context.Remove(dept);
        //    return _context.SaveChanges();
        //}

        //public IEnumerable<Department> GetAll()
        //{
        //    return _context.Departments.ToList();
           
        //}

        //public Department GetById(int? Id)
        //{
        // return   _context.Departments.FirstOrDefault(x => x.Id == Id);
        //}

        //public int Update(Department department)
        //{
        //    _context.Update(department);
        //    return _context.SaveChanges();
        //}
    }
}
