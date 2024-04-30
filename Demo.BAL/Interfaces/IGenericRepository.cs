using Demo.DAl.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BAL.Interfaces
{
    public interface IGenericRepository<T> where T : BaseEntity
    {
        public IEnumerable<T> GetAll();
        public T GetById(int? Id);

        public void Add(T employee);

        public void Delete(T employee);

        public void Update(T employee);
    }
}
