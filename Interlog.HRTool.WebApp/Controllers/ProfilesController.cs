using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Interlog.HRTool.Data.Contexts;
using Interlog.HRTool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Interlog.HRTool.Data.Providers;
using Interlog.HRTool.WebApp.Models.Profile;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Interlog.HRTool.WebApp.Controllers
{
    [Authorize]
    public class ProfilesController : BaseController
    {
        private ProfileProvider _profileProvider;
        
        public ProfilesController(DatabaseContext context, LanguageProvider languageProvider, LocalizationProvider localizationProvider) : base(context, languageProvider, localizationProvider)
        {
            _profileProvider = new ProfileProvider(context);   
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
              return View(_profileProvider.GetAll());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                Profile profile = new Profile()
                {
                    Name = model.Name,
                };

                _profileProvider.Create(profile);
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Profile profile = _profileProvider.GetById(id);
            if (profile == null)
            {
                return NotFound();
            }
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProfileViewModel model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Profile profile = _profileProvider.GetById(id);
                    if (profile == null)
                    {
                        return NotFound();
                    }
                    profile.Name = model.Name;
                    _profileProvider.Update(profile);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_profileProvider.ProfileExists(model.Id))
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
            Profile profile = _profileProvider.GetById(id);

            if (profile == null)
            {
                return NotFound();
            }

            return View(profile);

            
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (!_profileProvider.Delete(id))
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
