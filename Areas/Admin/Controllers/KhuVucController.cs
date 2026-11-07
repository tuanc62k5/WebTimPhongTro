using DoAn.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace DoAn.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KhuVucController : Controller
    {
        private readonly DataContext _context;

        public KhuVucController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? tinhThanh, string? phuongXa, string? keyword)
        {
            var khuVucs = await _context.KhuVucs.AsNoTracking()
            .Select(x => new { x.KV_TinhThanh, x.KV_PhuongXa }).ToListAsync();

            ViewBag.TinhThanhs = khuVucs.Select(x => x.KV_TinhThanh)
            .Distinct().OrderBy(x => x).ToList();

            ViewBag.PhuongXaData = khuVucs.Distinct()
            .OrderBy(x => x.KV_PhuongXa).ToList();

            var query = _context.KhuVucs.AsQueryable();

            if (!string.IsNullOrWhiteSpace(tinhThanh))
            {
                query = query.Where(x => x.KV_TinhThanh == tinhThanh);
            }

            if (!string.IsNullOrWhiteSpace(phuongXa))
            {
                query = query.Where(x => x.KV_PhuongXa == phuongXa);
            }

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(x => x.KV_TenKhuVuc.Contains(keyword) ||
                x.KV_TinhThanh.Contains(keyword) || x.KV_PhuongXa.Contains(keyword));
            }

            var kvList = await query.OrderBy(x => x.KV_ID).ToListAsync();

            ViewBag.TinhThanh = tinhThanh;
            ViewBag.PhuongXa = phuongXa;
            ViewBag.Keyword = keyword;

            return View(kvList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(tblKhuVuc model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.KV_NgayTao = DateTime.Now;

            _context.KhuVucs.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thêm khu vực thành công.";

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var khuVuc = await _context.KhuVucs.FindAsync(id);

            if (khuVuc == null)
            {
                return NotFound();
            }

            return View(khuVuc);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, tblKhuVuc model)
        {
            if (id != model.KV_ID)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var khuVuc = await _context.KhuVucs.FindAsync(id);

            if (khuVuc == null)
            {
                return NotFound();
            }

            khuVuc.KV_TenKhuVuc = model.KV_TenKhuVuc;
            khuVuc.KV_TinhThanh = model.KV_TinhThanh;
            khuVuc.KV_PhuongXa = model.KV_PhuongXa;
            khuVuc.KV_DiaChiChiTiet = model.KV_DiaChiChiTiet;

            await _context.SaveChangesAsync();

            TempData["Success"] = "Cập nhật khu vực thành công.";

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            var kv = _context.KhuVucs.Find(id);
            if (kv == null)
                return NotFound();
            return View(kv);
        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            var delKV = _context.KhuVucs.Find(id);
            if (delKV == null)
                return NotFound();
            _context.KhuVucs.Remove(delKV);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}