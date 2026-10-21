using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        private readonly DataContext _context;

        public LoginController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string tenDangNhap, string matKhau)
        {
            var user = await _context.TaiKhoans.FirstOrDefaultAsync(x =>
                x.TK_TenDangNhap == tenDangNhap && x.TK_MatKhau == matKhau && x.TK_TrangThai);

            if (user == null)
            {
                ViewBag.Error = "Tên đăng nhập hoặc mật khẩu không đúng.";
                return View();
            }

            HttpContext.Session.SetInt32("TK_ID", user.TK_ID);
            HttpContext.Session.SetString("TK_HoTen", user.TK_HoTen);
            HttpContext.Session.SetString("TK_VaiTro", user.TK_VaiTro);

            if (user.TK_VaiTro == "Admin")
                return RedirectToAction("Index", "Home", new { area = "Admin" });

            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}