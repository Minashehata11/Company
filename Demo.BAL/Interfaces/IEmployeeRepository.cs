using Demo.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Interfaces
{
    public interface IEmployeeRepository:IGenericRepository<Employee>
    {
        public Employee GetEagerLoadingById(int? id);
        public IEnumerable<Employee> Search(string? name);
        //public IEnumerable<Employee> GetAll();
        //public Employee GetById(int? Id);

        //public int Add(Employee employee);

        //public int Delete(Employee employee);

        //public int Update(Employee employee);
    }
}
