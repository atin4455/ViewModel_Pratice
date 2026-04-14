using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ViewModel_Pratice.Models;

namespace ViewModel_Pratice.Controllers
{
    public class HomeController : Controller
    {
        // 為了示範方便，用靜態欄位模擬資料庫中的一筆使用者資料。
        private static UserEntity _fakeUser = new()
        {
            Id = 1,
            Name = "王小明",
            Email = "ming@example.com",
            IsAdmin = false,
        };

        [HttpGet]
        public IActionResult BindingDemo()
        {
            ViewBag.CurrentUser = _fakeUser;
            return View();
        }

        [HttpPost]
        public IActionResult UpdateBad(UserEntity model)
        {
            // 危險示範：直接綁定整個 Entity。
            // 若攻擊者額外送出 IsAdmin=true，這裡也會被更新。
            _fakeUser = model;

            TempData["BadResult"] = $"[危險綁定] 已更新：Name={_fakeUser.Name}, Email={_fakeUser.Email}, IsAdmin={_fakeUser.IsAdmin}";
            return RedirectToAction(nameof(BindingDemo));
        }

        [HttpPost]
        public IActionResult UpdateGood(EditProfileViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.CurrentUser = _fakeUser;
                return View("BindingDemo");
            }

            // 安全示範：只把允許編輯的欄位映射回 Entity。
            // 即使有人偷送 IsAdmin，也不在 vm 內，因此不會被改。
            _fakeUser.Name = vm.Name;
            _fakeUser.Email = vm.Email;

            TempData["GoodResult"] = $"[安全綁定] 已更新：Name={_fakeUser.Name}, Email={_fakeUser.Email}, IsAdmin={_fakeUser.IsAdmin}";
            return RedirectToAction(nameof(BindingDemo));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
