using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QLHocSinh_LT.Models;
using QLHocSinh_LT.Models.ViewModels.IU;

namespace QLHocSinh_LT.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        //GET : Admin
        public IActionResult Index() 
            => View(_userManager.Users.ToList());

        //GET : Admin/Details/1
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //GET : Admin/Create/1
        public IActionResult Create() 
            => View();

        //POST : Admin/Create/1
        [HttpPost]
        public async Task<IActionResult> Create(RegisterViewModel model)
        {
            //Lớp 1: kiểm tra biểu mẫu
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Username, Email = model.Email };
                //Lớp 2: thử thêm tài khoản
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        //GET : Admin/Edit/1
        public async Task<IActionResult> Edit(string id)
        {
            //Tìm user theo id
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            //Chuyển User thành EditUserViewModel để hiển thị
            var model = new EditUserViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };

            return View(model);
        }

        //POST : Admin/Edit/1
        [HttpPost]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            //Lớp 1: kiểm tra biểu mẫu
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(model.Id);
                if (user == null)
                {
                    return NotFound();
                }

                user.UserName = model.UserName;
                user.Email = model.Email;

                //Lớp 2: thử cập nhật thông tin tài khoản
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //Lớp 3: Kiểm tra có cần cập nhật mật khẩu không
                    if (!string.IsNullOrEmpty(model.NewPassword))
                    {
                        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                        //Lớp 4: Thử cập nhật mật khẩu
                        var passwordChangeResult = await _userManager.ResetPasswordAsync(user, token, model.NewPassword);
                        if (!passwordChangeResult.Succeeded)
                        {
                            foreach (var error in passwordChangeResult.Errors)
                            {
                                ModelState.AddModelError(string.Empty, error.Description);
                            }
                            return View(model);
                        }
                    }
                    return RedirectToAction("Index");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(model);
        }

        //GET : Admin/Delete/1
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        //POST : Admin/Delete/1
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirm(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            //Lớp : thử xoá tài khoản
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("Index");
            }
            ModelState.AddModelError(string.Empty, "Failed to delete the user");
            return View("Index", _userManager.Users.ToList());
        }
    }

}
