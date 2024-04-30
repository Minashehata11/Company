using Demo.DAl.Entities;
using Demo.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Patterns;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace Demo.PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class RolesController : Controller
    {
        
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public RolesController(RoleManager<ApplicationRole> roleManager,UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            
          var roles= await _roleManager.Roles.ToListAsync();
           return View(roles);
        }
        public async Task<IActionResult> Create()
        {

            return View( new ApplicationRole()  );
        }
        [HttpPost]
        public async Task<IActionResult> Create( ApplicationRole applicationRole)
        {
            if(ModelState.IsValid)
            {
               var result= await _roleManager.CreateAsync(applicationRole);
               
                if(result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);
            }
            return View(new ApplicationRole());
        }


        public async Task<IActionResult> Delete(string id)
        {
                var role= await _roleManager.FindByIdAsync(id);
                var result = await _roleManager.DeleteAsync(role);

                if (result.Succeeded)
                    return RedirectToAction("Index");
                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error.Description);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AddOrRemoveRoleToUser(string id)
        {
            if (id == null)
                return NotFound();
            var role=await _roleManager.FindByIdAsync(id);
            if(role == null)
                return NotFound();
          //  ViewBag.roleId = id;
            var users= await _userManager.Users.ToListAsync();
            List<UserInRoleViewModel> RolesViewUsers =new List<UserInRoleViewModel>();    
            foreach(var user in users)
            {
                UserInRoleViewModel usersInRoleViews = new UserInRoleViewModel()
                {
                    UserId = user.Id,
                    UserName = user.UserName,
                };
                if ( await _userManager.IsInRoleAsync(user,role.Name))
                    usersInRoleViews.IsSelected=true;
                else
                    usersInRoleViews.IsSelected=false;

               RolesViewUsers.Add(usersInRoleViews);
               
            }
            return View(RolesViewUsers);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrRemoveRoleToUser(string id, List<UserInRoleViewModel> userInRoleViewModel)
        {
            if (id == null)
                return NotFound();
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            if (ModelState.IsValid)
            {
                foreach(var user in userInRoleViewModel)
                {
                    var appUser = await _userManager.FindByIdAsync(user.UserId);
                    if(appUser != null)
                    {
                        if (user.IsSelected && !(await _userManager.IsInRoleAsync(appUser, role.Name)))
                            await _userManager.AddToRoleAsync(appUser, role.Name);
                        else if(!user.IsSelected && await _userManager.IsInRoleAsync(appUser, role.Name))
                             await _userManager.RemoveFromRoleAsync(appUser, role.Name);

                    }
                    
                }

               return RedirectToAction(nameof(Index));
            }
            return View(userInRoleViewModel);
        }

    }
}
