using Microsoft.AspNetCore.Mvc;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Interlog.HRTool.WebApp.Models.Company;
using Interlog.HRTool.Data.Providers;

namespace Interlog.HRTool.WebApp.Controllers
{
    [Authorize]
    public class CompaniesController : BaseController
    {
        private CompanyProvider _companyProvider;

        public CompaniesController(DatabaseContext context, LanguageProvider languageProvider, LocalizationProvider localizationProvider) : base(context, languageProvider, localizationProvider)
        {
            _companyProvider = new CompanyProvider(context);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            CompanyIndexViewModel viewModel = new CompanyIndexViewModel();

            viewModel.Company = new CompanyViewModel();
            viewModel.Companies = new List<CompanyViewModel>();

            var getAllCompanies = _companyProvider.GetAll();

            foreach (var company in getAllCompanies)
            {
                viewModel.Companies.Add(new CompanyViewModel()
                {
                    Name = company.Name,
                    Id = company.Id,
                    Departments = company.Departments.Count
                });
            }
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CompanyViewModel company)
        {
            if (ModelState.IsValid)
            {
                Company newCompany = new Company()
                {
                    Name = company.Name,
                };

                _companyProvider.Create(newCompany);
                return RedirectToAction(nameof(Index));
            }
            return View(nameof(Index), company);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Company company = _companyProvider.GetById(id);
            if (company == null)
            {
                return NotFound();
            }

            CompanyEditViewModel viewModel = new CompanyEditViewModel();
            viewModel.Name = company.Name;

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyEditViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Company company = _companyProvider.GetById(id);
                if (company == null)
                {
                    return NotFound();
                }

                company.Name = model.Name;
                _companyProvider.Update(company);

                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            Company company = _companyProvider.GetById(id);

            if (company == null)
            {
                return NotFound();
            }

            CompanyDeleteViewModel viewModel = new CompanyDeleteViewModel();
            viewModel.Name = company.Name;

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_companyProvider.Delete(id))
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
