using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Interlog.HRTool.WebApp.Models.Company;
using Interlog.HRTool.Data.Providers;

namespace Interlog.HRTool.WebApp.Controllers
{
    [Authorize]
    public class CompaniesController : Controller
    {
        private readonly DatabaseContext _context;
        private CompanyProvider _companyProvider;

        public CompaniesController(DatabaseContext context)
        {
            _context = context;
            _companyProvider = new CompanyProvider(context);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return _context.Companies != null ? 
                          View(await _context.Companies.ToListAsync()) :
                          Problem("Entity set 'DatabaseContext.Company'  is null.");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
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
            return View(company);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Company company = _companyProvider.GetById(id);
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CompanyViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }


            if (ModelState.IsValid)
            {
                try
                {
                    Company company = _companyProvider.GetById(id);
                    if (company == null)
                    {
                        return NotFound();
                    }
                    company.Name = model.Name;
                    _companyProvider.Update(company);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_companyProvider.CompanyExists(model.Id))
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

            return View(company);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(!_companyProvider.Delete(id))
            {
                TempData["Error"] = "Não é possivel apagar a empresa";
            }
            else
            {
                TempData["Success"] = "Empresa apagada com sucesso";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
