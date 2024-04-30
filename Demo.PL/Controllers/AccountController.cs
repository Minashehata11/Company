using Demo.BAL.Interfaces;
using Demo.DAl.Context;
using Demo.DAl.Entities;
using Demo.PL.Helper;
using Demo.PL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NToastNotify;
using System.Security.Policy;

namespace Demo.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IToastNotification _toast;
		private readonly ApplicationDbContext _contex;
		

		public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ILogger<AccountController> logger, IToastNotification toast,ApplicationDbContext contex)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _toast = toast;
			_contex = contex;
			
		}
        public async Task<IActionResult> SignUp()
        {
            return View(new SignUpViewModel());
        }
		[HttpPost]
		public async Task<IActionResult> SignUp(SignUpViewModel data)
		{
			if (ModelState.IsValid)
			{
				// Create a new ApplicationUser with data from the view model
				var user = new ApplicationUser()
				{
					Email = data.Email,
					IsActive = true,
					UserName = data.Email.Split("@")[0],
					EmailConfirmed = true
				};

				// Create the user using UserManager
				var result = await _userManager.CreateAsync(user, data.Password);

				if (result.Succeeded)
				{
					// Create a new Admin object with user data and foreign key reference
					var admin = new Admin()
					{
						Address = data.Address,
						SSN = data.SSN,
						// Set UserId to the newly created user's Id
						UserId = user.Id
					};

					// Add the admin object to the context for saving
					_contex.Admins.Add(admin);

					// Save all changes (user and admin) in one transaction
					try
					{
						await _contex.SaveChangesAsync();
						_toast.AddSuccessToastMessage("Register Has Confirmed");
						return RedirectToAction("SignIn");
					}
					catch (DbUpdateException ex)
					{
						// Handle potential database saving errors
						ModelState.AddModelError(string.Empty, "An error occurred during registration. Please try again.");
						// Log the exception details for further investigation
						_logger.LogError(ex, "Error saving user and admin data");
						return View(data);
					}
				}
				else
				{
					// Handle user creation errors (e.g., add error messages to ModelState)
					foreach (var item in result.Errors)
					{
						ModelState.AddModelError(string.Empty, item.Description);
					}

					return View(data);
				}
			}

			return View(data);
		}
		public async Task<IActionResult> SignIn()
        {
            return View(new SignInViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel data)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(data.Email);
                if (user == null)
                    ModelState.AddModelError("", "No Account");
                if (user != null && await _userManager.CheckPasswordAsync(user, data.Password))
                {
                    var result = await _signInManager.PasswordSignInAsync(user, data.Password, data.Remeberme, true);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    _logger.LogInformation(result.ToString());
                    _logger.LogError(result.ToString());
                }
            }
            return View(data);
        }

       
        public async Task<IActionResult> SignOut()
        {
           await _signInManager.SignOutAsync();
           return RedirectToAction("SignIn");
        }

        public async Task<IActionResult> ForgetPassword()
        {
            return View(new ForgetPasswordViewModel());
        }

        //public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel data)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = await _userManager.FindByEmailAsync(data.Email);
        //        if (user == null)
        //            ModelState.AddModelError("", "No Account");
        //        if (user != null)
        //        {
        //            var token= await _userManager.GeneratePasswordResetTokenAsync(user);
        //            var ResetPasswordlink = Url.Action("ResetPassword", "Account", new {Email= user.Email,Token=token},Request.Scheme);
        //        var email = new Email()
        //        {
        //            Body = ResetPasswordlink,
        //            To= user.Email,
        //            Title="Reset Password"
        //        };
        //        }
        //    }
        

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}