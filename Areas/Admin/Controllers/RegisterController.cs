using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RegisterController : Controller
    {
        private readonly DataContext _context;

        public RegisterController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(tblTaiKhoan model)
        {
            if (string.IsNullOrWhiteSpace(model.TK_TenDangNhap) ||
                string.IsNullOrWhiteSpace(model.TK_MatKhau) ||
                string.IsNullOrWhiteSpace(model.TK_HoTen))
            {
                ViewBag.Error = "Vui lòng nhập đầy đủ tên đăng nhập, mật khẩu và họ tên.";
                return View(model);
            }

            if (!Regex.IsMatch(model.TK_TenDangNhap, @"^[a-zA-Z0-9_]+$"))
            {
                ViewBag.Error = "Tên đăng nhập chỉ được chứa chữ không dấu, số và dấu gạch dưới (_).";
                return View(model);
            }

            bool exists = await _context.TaiKhoans.AnyAsync(x => x.TK_TenDangNhap == model.TK_TenDangNhap);

            if (exists)
            {
                ViewBag.Error = "Tên đăng nhập đã tồn tại.";
                return View(model);
            }

            model.TK_VaiTro = "KhachThue";
            model.TK_TrangThai = true;
            model.TK_NgayTao = DateTime.Now;

            _context.TaiKhoans.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Đăng ký tài khoản thành công.";
            return RedirectToAction("Index");
        }
    }
}