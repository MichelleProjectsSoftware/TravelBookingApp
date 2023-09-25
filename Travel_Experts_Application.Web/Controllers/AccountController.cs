using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Travel_Experts_Application.Web.ViewModel;

namespace Travel_Experts_Application.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> rolemanager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = rolemanager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel registerVM)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }

            IdentityUser user = new IdentityUser
            {
                UserName = registerVM.UserName,
            };

            IdentityResult result = await _userManager.CreateAsync(user, registerVM.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("Login", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel loginVM, string returnUrl)
        {
            var result = await _signInManager.PasswordSignInAsync(loginVM.UserName, loginVM.Password, loginVM.RememberMe, false);

            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    return LocalRedirect(returnUrl);
                }
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt");
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////

        // GET: AccountController
        public async Task<ActionResult> Index()
        {
            IList<IdentityUser> userList = _userManager.Users.ToList();

            return View(userList);
        }

        // GET: AccountController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AccountController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditUserRoles(string userId)
        {
            ViewData["UserId"] = userId;

            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewData["NotFound"] = $"The user with ID = {userId} was not found";
                // new name: not found
                return View("NotFound");
            }

            // Materialize the roles into a list
            List<IdentityRole> roles = await _roleManager.Roles.ToListAsync();

            IList<UserRolesViewModel> userRolesVMList = new List<UserRolesViewModel>();

            foreach (var role in roles)
            {
                UserRolesViewModel userRoleVM = new UserRolesViewModel()
                {
                    RoleId = role.Id,
                    RoleName = role.Name
                };

                // Materialize the user roles into a list
                List<string> userRoles = (List<string>)await _userManager.GetRolesAsync(user);

                if (userRoles.Contains(role.Name))
                {
                    userRoleVM.IsRoleSelected = true;
                }
                else
                {
                    userRoleVM.IsRoleSelected = false;
                }

                userRolesVMList.Add(userRoleVM);
            }

            return View(userRolesVMList);

        }

            


        [HttpGet]
        // GET: AccountController/Edit/5
        public async Task<ActionResult> EditUser(string id)
        {
            IdentityUser existingUser = await _userManager.FindByIdAsync(id);

            if (existingUser == null)
            {
                ViewData["NotFound"] = $"The user with ID = {id} was not found";
                // new name: not found
                return View("NotFound");
            }

            // many to many of users to roles

            IList<string> userRoles = await _userManager.GetRolesAsync(existingUser);

            EditUserViewModel editUserVM = new EditUserViewModel
            {
                Id = existingUser.Id,
                UserName = existingUser.UserName,
                Roles = userRoles
            };

            return View(editUserVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: AccountController/Edit/5
        public async Task<ActionResult> EditUser(EditUserViewModel editUserVM)
        {
            if (!ModelState.IsValid)
            {
                return View(editUserVM);
            }
            IdentityUser existingUser = await _userManager.FindByIdAsync(editUserVM.Id);

            if (existingUser == null)
            {
                ViewData["NotFound"] = $"The user with ID = {editUserVM.Id} was not found";
                // new name: not found
                return View("NotFound");
            }

            existingUser.UserName = editUserVM.UserName;

            // many to many of users to roles

            IdentityResult result = await _userManager.UpdateAsync(existingUser);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Account");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View();
        }

        // POST: AccountController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUserRoles(IList<UserRolesViewModel> userRolesList, string userId)
        {

            IdentityUser user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewData["NotFound"] = $"The user with ID = {userId} was not found";
                // new name: not found
                return View("NotFound");
            }

            IList<string> roles = await _userManager.GetRolesAsync(user);

            IdentityResult result = await _userManager.RemoveFromRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Error removing user from existing roles");
                return View();
            }

            // link query
            result = await _userManager.AddToRolesAsync(user, userRolesList.Where(x => x.IsRoleSelected).Select(y => y.RoleName));

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Error adding user to selected roles");
                return View();
            }

            return RedirectToAction("EditUser", "Account", new { id = user.Id });
        }

        // GET: AccountController/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AccountController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(string id)
        {
            try
            {
                IdentityUser user = await _userManager.FindByIdAsync(id);

                if (user == null)
                {
                    ViewData["NotFound"] = $"The user with ID = {id} was not found";
                    // new name: not found
                    return View("NotFound");
                }

                IdentityResult result = await _userManager.DeleteAsync(user);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        return View();
                    }
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return View();
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult AccessDenied()
        {
            return View();
        }






    }
}
