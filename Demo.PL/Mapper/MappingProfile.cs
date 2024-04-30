using AutoMapper;
using Demo.DAl.Entities;
using Demo.PL.Models;

namespace Demo.PL.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            //CreateMap<Employee, EmployeeViewModel>();
            //CreateMap<EmployeeViewModel, Employee>();

            CreateMap<EmployeeViewModel, Employee>().ReverseMap();    

        }
    }
}
