using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Interlog.HRTool.Data.Providers;
using Interlog.HRTool.WebApp.Models.Employee;
using Interlog.HRTool.WebApp.Models.Department;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Interlog.HRTool.WebApp.Controllers
{
    [Authorize]
    public class DepartmentsController : BaseController
    {
        private DepartmentProvider _departmentProvider;
        private CompanyProvider _companyProvider;
        private EmployeeProvider _employeeProvider;

        public DepartmentsController(DatabaseContext context) : base(context)
        {
            _departmentProvider = new DepartmentProvider(context);
            _companyProvider = new CompanyProvider(context);
            _employeeProvider = new EmployeeProvider(context);

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(_departmentProvider.GetAll());
        }

        
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name));
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel model)
        {
            if (ModelState.IsValid)
            {
                Department department = new Department()
                {
                    Name = model.Name,
                    CompanyId = model.CompanyId
                };
                _departmentProvider.Create(department);
                
                return RedirectToAction(nameof(Index));
            }
            ViewData[nameof(DepartmentViewModel.CompanyId)] = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), model.CompanyId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Department department = _departmentProvider.GetById(id);
            if (department == null)
            {
                return NotFound();
            }            
            ViewData[nameof(DepartmentViewModel.CompanyId)] = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), department.CompanyId);
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Department department = _departmentProvider.GetById(id);

                    if (department == null)
                    {
                        return NotFound();
                    }
                    department.CompanyId = model.CompanyId;
                    department.Name = model.Name;
                    _departmentProvider.Update(department);
                                        
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_departmentProvider.DepartmentExists(model.Id))
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
            ViewData[nameof(DepartmentViewModel.CompanyId)] = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), model.CompanyId);
            return View(model);
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null || _context.Departments == null)
        //    {
        //        return NotFound();
        //    }

        //    var department = await _context.Departments
        //        .Include(d => d.Company)
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (department == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(department);
        //}

        
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    if (_context.Departments == null)
        //    {
        //        return Problem("Entity set 'DatabaseContext.Department'  is null.");
        //    }
        //    var department = await _context.Departments.FindAsync(id);
        //    if (department != null)
        //    {
        //        _context.Departments.Remove(department);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        
    }
}
