using Demo.DAl.Context;
using Demo.DAl.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NToastNotify;
using NuGet.Versioning;
using System.Reflection.Metadata.Ecma335;

namespace Demo.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class UsersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IToastNotification _toastNotification;

        public UsersController(UserManager<ApplicationUser> userManager,IToastNotification toastNotification)
        {
             _userManager = userManager;
             _toastNotification = toastNotification;
        }
        public async Task<IActionResult>  Index(string SearchValue= "")
        {
            List<ApplicationUser> users;
            if (string.IsNullOrEmpty(SearchValue))
            {
                users = await _userManager.Users.ToListAsync();
            }
            else
            {
                users = await _userManager.Users.Where(user => user.Email.Trim().ToLower().Contains(SearchValue.Trim().ToLower())).ToListAsync();
            }
            return View(users);
        }

        public async Task<IActionResult> Details(string Id,string ActionName ="Details")
        {
            if (Id is  null)
                return NotFound();
            var user= await _userManager.Users.FirstOrDefaultAsync(user => user.Id == Id);
            if(user == null) return NotFound();

			return View(ActionName, user);
		}
		[HttpGet]
		public async Task<IActionResult> Edit(string Id)
		{
            return await Details(Id, "Edit");

		}

		[HttpPost]
		public async Task<IActionResult> Edit(string Id, ApplicationUser applicationUser)
		{
            if (applicationUser.Id != Id)
                return NotFound();
            if (ModelState.IsValid) { 
            var user = _userManager.Users.FirstOrDefault(user => user.Id == Id);

            user.UserName= applicationUser.UserName;
            user.NormalizedUserName= applicationUser.UserName.ToUpper();

            var result= await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    _toastNotification.AddInfoToastMessage("User Was Updated");

                    return RedirectToAction(nameof(Index));
                }
                    
                foreach (var item in result.Errors)
                    ModelState.AddModelError("",item.Description);
           }
            return View(applicationUser);
		}


        
        public async Task<IActionResult> Delete(string Id)
        {
           
            
           var user = _userManager.Users.FirstOrDefault(user => user.Id == Id);
                if(user == null) return NotFound();


            var result = await _userManager.DeleteAsync(user);
                if (result.Succeeded)
            {
                _toastNotification.AddSuccessToastMessage("User Was Deleted");
                return RedirectToAction(nameof(Index));

            }
                    
                foreach (var item in result.Errors)
                    ModelState.AddModelError("", item.Description);

            return RedirectToAction(nameof(Index));
        }
    }
}
