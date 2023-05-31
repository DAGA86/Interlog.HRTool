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

namespace Interlog.HRTool.WebApp.Controllers
{
    public class EmployeesController : Controller
    {
        private readonly DatabaseContext _context;
        private EmployeeProvider _employeeProvider;

        public EmployeesController(DatabaseContext context)
        {
            _context = context;
            _employeeProvider = new EmployeeProvider(context);

        }

        // GET: Employees
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Employee.Include(e => e.Department);
            return View(await databaseContext.ToListAsync());
        }

        // GET
        public Employee? GetByUsername(string username)
        {
            return _employeeProvider.GetByUsername(username);
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


        // GET: Employees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employees/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,UserName,Email,Password,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,UserName,Email,Password,DepartmentId")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
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
            ViewData["DepartmentId"] = new SelectList(_context.Department, "Id", "Name", employee.DepartmentId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Employee == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .Include(e => e.Department)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Employee == null)
            {
                return Problem("Entity set 'DatabaseContext.Employee'  is null.");
            }
            var employee = await _context.Employee.FindAsync(id);
            if (employee != null)
            {
                _context.Employee.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(int id)
        {
            return (_context.Employee?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
