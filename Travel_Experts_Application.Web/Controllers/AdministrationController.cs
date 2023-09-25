using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Travel_Experts_Application.Web.ViewModel;

namespace Travel_Experts_Application.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdministrationController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdministrationController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public ActionResult Index()
        {
            IList<IdentityRole> roleList = _roleManager.Roles.ToList();

            IList<RoleListViewModel> roleListVM = new List<RoleListViewModel>();

            foreach (var role in roleList)
            {
                RoleListViewModel roleVM = new RoleListViewModel()
                {
                    Id = role.Id,
                    RoleName = role.Name,
                };

                roleListVM.Add(roleVM);

            }

            return View(roleListVM);
        }

        // GET: AdministrationController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: AdministrationController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AdministrationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(CreateRoleViewModel createRoleVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View();
                }

                IdentityRole role = new IdentityRole
                {
                    Name = createRoleVM.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // GET: AdministrationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: AdministrationController/Edit/5
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

        // GET: AdministrationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: AdministrationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
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
    }
}
