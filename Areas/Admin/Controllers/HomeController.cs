using Microsoft.AspNetCore.Mvc;
using DoAn.Utilities;

namespace DoAn.Areas.Admin.controller;

[Area("Admin")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        if (!Functions.IsLogin(HttpContext))
            return RedirectToAction("Index", "Login", new { area = "Admin" });

        return View();
    }

    public IActionResult Logout()
    {
        Functions.Logout(HttpContext);

        return RedirectToAction("Index", "Login", new { area = "Admin" });
    }
}