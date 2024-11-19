using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectForIdentity.Models;
using ProjectForIdentity.ViewModels;

namespace ProjectForIdentity.Controllers
{
    public class UsersController : Controller
    {
        private UserManager<AppUser> _userManager;

        public UsersController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View(_userManager.Users);
        }
        #region Create

        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FullName = model.FullName
                };
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }
                foreach (IdentityError err in result.Errors)
                {
                    ModelState.AddModelError("", err.Description);
                }
            }
            return View(model);
        }
        #endregion
        #region Edit

        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) { return RedirectToAction("Index"); }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                return View(new EditViewModel
                {
                    Email = user.Email,
                    FullName = user.FullName,
                    Id = id
                });
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditViewModel model, string id)
        {
            if (model.Id != id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user != null)
                {
                    user.Email = model.Email;
                    user.FullName = model.FullName;

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded && !string.IsNullOrEmpty(model.Password))
                    {
                        await _userManager.RemovePasswordAsync(user);
                        await _userManager.AddPasswordAsync(user, model.Password);
                    }
                    if (result.Succeeded) { return RedirectToAction(nameof(Index)); }

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            return View(model);
        }
        #endregion
        #region Delete
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.DeleteAsync(user);
            }
            else { return NotFound(); }

            return RedirectToAction(nameof(Index));
        }
        #endregion
        #region Detail
        public async Task<IActionResult> Details(string id)
        {
            var user =  await _userManager.FindByIdAsync(id); 
            return View(user);
        }
        #endregion
    }
}
