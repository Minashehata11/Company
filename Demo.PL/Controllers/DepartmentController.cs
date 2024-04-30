using Microsoft.AspNetCore.Mvc;
using Demo.BAL.Interfaces;
using Demo.DAl.Entities;
using NToastNotify;

namespace Demo.PL.Controllers;

public class DepartmentController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    // private readonly IDepartmentRepository _department;
    private readonly ILogger<DepartmentController> logger;
    private readonly IToastNotification toast;

    public DepartmentController(IUnitOfWork unitOfWork,ILogger<DepartmentController> logger, IToastNotification toast)
    {
        
        _unitOfWork = unitOfWork;
        this.logger = logger;
        this.toast = toast;
    }
    public IActionResult Index()
    {
        var departments = _unitOfWork.departmentRepository.GetAll();
        return View(departments);
    }

    [HttpGet]
    public IActionResult Create()
    {

        return View(new Department());
    }
    [HttpPost]
    public IActionResult Create(Department department)
    {
        if (ModelState.IsValid)
        {
            _unitOfWork.departmentRepository.Add(department);
            _unitOfWork.commit();
            toast.AddSuccessToastMessage(" Department Created Successfully");
            return RedirectToAction(nameof(Index));

        }
        else
            return View(department);

    }

    public IActionResult Details(int? id)
    {
        try
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _unitOfWork.departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return RedirectToAction("Error", "Home");

        }
        
        
        
    }
    [HttpGet]
    public IActionResult Edit(int? id)
    {
        try
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _unitOfWork.departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return RedirectToAction("Error", "Home");

        }



    }
  
    public IActionResult Edit(Department department)
    {
        if(ModelState.IsValid)
        {
            _unitOfWork.departmentRepository.Update(department);
            _unitOfWork.commit();
            toast.AddSuccessToastMessage(" Department updated Successfully");


            return RedirectToAction(nameof(Index));
        }
        else
            return View(department);


    }
    [HttpGet]
    public IActionResult Delete(int? id)
    {
        try
        {
            if (id == null)
            {
                return BadRequest();
            }
            var department = _unitOfWork.departmentRepository.GetById(id);

            if (department == null)
            {
                return NotFound();
            }
            return View(department);

        }
        catch (Exception ex)
        {
            logger.LogError(ex.Message);
            return RedirectToAction("Error", "Home");

        }



    }
    [HttpPost]
    public IActionResult Delete(Department department)
    {
        _unitOfWork.departmentRepository.Delete(department);
        _unitOfWork.commit();
        toast.AddWarningToastMessage(" Department Deleted");

        return RedirectToAction(nameof(Index));
    }

}
