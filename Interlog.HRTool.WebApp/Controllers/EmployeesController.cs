using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Interlog.HRTool.WebApp.Models.Employee;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Interlog.HRTool.Data.Providers;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;


namespace Interlog.HRTool.WebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext _context;
        private EmployeeProvider _employeeProvider;
        private DepartmentProvider _departmentProvider;
        private ProfileProvider _profileProvider;

        public EmployeesController(DatabaseContext context)
        {
            _context = context;
            _employeeProvider = new EmployeeProvider(context);
            _departmentProvider = new DepartmentProvider(context);
            _profileProvider = new ProfileProvider(context);

        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Employees.Include(e => e.Department);
            return View(await databaseContext.ToListAsync());
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {

            if (ModelState.IsValid)
            {
                Employee dbEmployee = _employeeProvider.GetByUsername(model.Username);

                if (dbEmployee != null)
                {
                    if (dbEmployee.Password == Shared.Providers.CryptographyProvider.EncodeToBase64(model.Password))
                    {
                        if (dbEmployee.IsActive == true)
                        {
                            string authenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme;

                            // Generate Claims from DbEntity
                            List<Claim> claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, dbEmployee.Email),
                                new Claim(ClaimTypes.NameIdentifier, dbEmployee.Username.ToString())
                            };

                            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                                    claims, authenticationScheme);

                            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(
                                    claimsIdentity);

                            var authProperties = new AuthenticationProperties
                            {
                                AllowRefresh = true,
                                // Refreshing the authentication session should be allowed.
                                //ExpiresUtc = DateTimeOffset.UtcNow.AddHours(8),
                                // The time at which the authentication ticket expires. A 
                                // value set here overrides the ExpireTimeSpan option of 
                                // CookieAuthenticationOptions set with AddCookie.
                                IsPersistent = true,
                                // Whether the authentication session is persisted across 
                                // multiple requests. Required when setting the 
                                // ExpireTimeSpan option of CookieAuthenticationOptions 
                                // set with AddCookie. Also required when setting 
                                // ExpiresUtc.
                                IssuedUtc = DateTimeOffset.UtcNow,
                                // The time at which the authentication ticket was issued.
                                //RedirectUri = "~/Account/Login"
                                // The full path or absolute URI to be used as an http 
                                // redirect response value.
                            };

                            await this.HttpContext.SignInAsync(
                                authenticationScheme,
                                claimsPrincipal,
                                authProperties);

                            return LocalRedirect("~/Home/Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Account inactive");
                            return View(model);
                        }
                    }
                }

                ModelState.AddModelError(string.Empty, "Invalid fields!");
            }

            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectPermanent("~/Home/Index");
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Employee employee = _employeeProvider.GetById(id);
            if (employee == null)
            {
                return NotFound();
            }
            EmployeeViewModel model = new EmployeeViewModel(employee.Id, employee.FirstName, employee.LastName, employee.DepartmentId);
            ViewData[nameof(EmployeeViewModel.DepartmentId)] = new SelectList(_departmentProvider.GetAll(), nameof(Data.Models.Department.Id), nameof(Data.Models.Department.Name), employee.DepartmentId);
            ViewData[nameof(EmployeeViewModel.ProfileIds)] = new MultiSelectList(_profileProvider.GetAll(), nameof(Data.Models.Profile.Id), nameof(Data.Models.Profile.Name), employee.Profiles.Select(x => x.Id));
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Employee employee = _employeeProvider.GetById(id);
                    if (employee == null)
                    {
                        return NotFound();
                    }
                    employee.DepartmentId = model.DepartmentId;
                    _employeeProvider.Update(employee);
                    _employeeProvider.UpdateProfiles(model.Id, model.ProfileIds);

                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!_employeeProvider.EmployeeExists(model.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData[nameof(EmployeeViewModel.DepartmentId)] = new SelectList(_departmentProvider.GetAll(), nameof(Data.Models.Department.Id), nameof(Data.Models.Department.Name), model.DepartmentId);
            ViewData[nameof(EmployeeViewModel.ProfileIds)] = new MultiSelectList(_profileProvider.GetAll(), nameof(Data.Models.Profile.Id), nameof(Data.Models.Profile.Name), model.ProfileIds);
            return View(model);
        }
    }
}
