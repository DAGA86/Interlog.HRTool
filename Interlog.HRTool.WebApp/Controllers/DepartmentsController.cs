using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Interlog.HRTool.Data.Providers;
using Interlog.HRTool.WebApp.Models.Department;

namespace Interlog.HRTool.WebApp.Controllers
{
    [Authorize]
    public class DepartmentsController : BaseController
    {
        private DepartmentProvider _departmentProvider;
        private CompanyProvider _companyProvider;
        private EmployeeProvider _employeeProvider;

        public DepartmentsController(DatabaseContext context, LanguageProvider languageProvider, LocalizationProvider localizationProvider) : base(context, languageProvider, localizationProvider)
        {
            _departmentProvider = new DepartmentProvider(context);
            _companyProvider = new CompanyProvider(context);
            _employeeProvider = new EmployeeProvider(context);

        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            DepartmentIndexViewModel viewModel = new DepartmentIndexViewModel();

            viewModel.Department = new DepartmentViewModel();
            viewModel.Departments = new List<DepartmentViewModel>();

            var getAllDepartments = _departmentProvider.GetAll();

            foreach (var department in getAllDepartments)
            {
                viewModel.Departments.Add(new DepartmentViewModel()
                {
                    Name = department.Name,
                    Id = department.Id,
                    CompanyId = department.CompanyId,
                    CompanyName = department.Company.Name
                });
            }
            viewModel.Companies = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name));
            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DepartmentViewModel department)
        {
            if (ModelState.IsValid)
            {
                Department newDepartment = new Department()
                {
                    Name = department.Name,
                    CompanyId = department.CompanyId,

                };

                _departmentProvider.Create(newDepartment);

                return RedirectToAction(nameof(Index));
            }
            department.Companies = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), department.CompanyId);
            return View(nameof(Index), department);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Department department = _departmentProvider.GetById(id);
            if (department == null)
            {
                return NotFound();
            }

            DepartmentEditViewModel viewModel = new DepartmentEditViewModel();
            viewModel.CompanyId = department.Id;
            viewModel.Name = department.Name;

            viewModel.Companies = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), department.CompanyId);
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DepartmentEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Department department = _departmentProvider.GetById(id);

                if (department == null)
                {
                    return NotFound();
                }
                department.CompanyId = model.CompanyId;
                department.Name = model.Name;
                _departmentProvider.Update(department);

                return RedirectToAction(nameof(Index));
            }
            model.Companies = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), model.CompanyId);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Department department = _departmentProvider.GetById(id);

            if (department == null)
            {
                return NotFound();
            }

            DepartmentDeleteViewModel viewModel = new DepartmentDeleteViewModel();
            viewModel.Name = department.Name;
            viewModel.CompanyName = department.Company.Name;

            viewModel.Companies = new SelectList(_companyProvider.GetAll(), nameof(Data.Models.Company.Id), nameof(Data.Models.Company.Name), viewModel.CompanyId);
            return View(viewModel);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_departmentProvider.Delete(id))
            {
                TempData["Error"] = "";
            }
            else
            {
                TempData["Success"] = "";
            }

            return RedirectToAction(nameof(Index));
        }


    }
}
