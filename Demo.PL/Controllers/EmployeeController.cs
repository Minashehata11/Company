using AutoMapper;
using Demo.BAL.Interfaces;
using Demo.DAl.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using NToastNotify;
using System.Reflection.Metadata;

namespace Demo.PL.Controllers
{
    public class EmployeeController : Controller
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IToastNotification toastNotification;
        private readonly IMapper _mapper;

        public EmployeeController(IUnitOfWork unitOfWork,IToastNotification toastNotification,IMapper mapper)
        {
           
            _unitOfWork = unitOfWork;
            this.toastNotification = toastNotification;
            _mapper = mapper;
        }
        public IActionResult Index( string SearchValue="")
        {
            IEnumerable<Employee> employees;
            IEnumerable<EmployeeViewModel> employeeViews;


            if (string.IsNullOrEmpty(SearchValue))
                employees = _unitOfWork.employeeRepository.GetAll();
               
            else
                employees = _unitOfWork.employeeRepository.Search(SearchValue);

              employeeViews = _mapper.Map<IEnumerable<EmployeeViewModel>>(employees);

            return View(employeeViews);
        }

        
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return BadRequest("Not Found");
            }
            else
            {
                 var employee = _unitOfWork.employeeRepository.GetEagerLoadingById(id);
                if(employee == null)
                {
                    return NotFound();
                }
                 var empView=_mapper.Map<EmployeeViewModel>(employee);
              return View(empView);
            }
            
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.departmets=_unitOfWork.departmentRepository.GetAll();
            return View( new EmployeeViewModel() ) ;

        }


        [HttpPost]
        public IActionResult Create(EmployeeViewModel employeeViewModel)
        {
          //  ModelState["Department"].ValidationState=ModelValidationState.Valid;
            if (ModelState.IsValid)
            {
                //Manual Map Dto

                //Employee emp = new Employee
                //{
                //    Name= employee.Name,
                //    Address= employee.Address,
                //    DepartmentId= employee.DepartmentId,
                //    Email = employee.Email,
                //    Salary = employee.Salary,
                //    HiringDate = employee.HiringDate,
                //    IsActive= employee.IsActive
                //};

                //Auto Mapper
                var emp =_mapper.Map<Employee>(employeeViewModel);
                emp.ImageUrl = DocumnetSetting.UploadFile(employeeViewModel.File, "Images");
                _unitOfWork.employeeRepository.Add(emp);
                _unitOfWork.commit();
                toastNotification.AddSuccessToastMessage("Added Sucessfully");
                return RedirectToAction("Index");
                
            }
            ViewBag.departmets = _unitOfWork.departmentRepository.GetAll();
            return View(employeeViewModel);

        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ViewBag.departmets = _unitOfWork.departmentRepository.GetAll();
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }
                var employee = _unitOfWork.employeeRepository.GetById(id);

                if (employee == null)
                {
                    return NotFound();
                }
                var empView=_mapper.Map<EmployeeViewModel>(employee);    
                return View(empView);

            }
            catch (Exception ex)
            {
               
                return RedirectToAction("Error", "Home");

            }



        }
        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employeeView)
        {
            //ModelState["Department"].ValidationState = ModelValidationState.Valid;
            if (ModelState.IsValid)
            {

                var employee = _mapper.Map<Employee>(employeeView);
                employee.ImageUrl = DocumnetSetting.UploadFile(employeeView.File, "Images");


                _unitOfWork.employeeRepository.Update(employee);
                _unitOfWork.commit();
                toastNotification.AddSuccessToastMessage("Updated Sucessfully");

                return RedirectToAction(nameof(Index));
            }
            else
           ViewBag.departmets = _unitOfWork.departmentRepository.GetAll();
            return View(employeeView);


        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {


            if (id == null)
            {
                return BadRequest();
            }
            var employee = _unitOfWork.employeeRepository.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            _unitOfWork.employeeRepository.Delete(employee); 

            _unitOfWork.commit();
            toastNotification.AddWarningToastMessage("Data Was Deleted");



            return RedirectToAction(nameof(Index));

        }
      
    }
}
