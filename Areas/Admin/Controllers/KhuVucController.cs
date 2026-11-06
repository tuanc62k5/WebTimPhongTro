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
        public IActionResult Index()
        {
            var kvList = _context.KhuVucs.OrderBy(kv => kv.KV_ID).ToList();
            return View(kvList);
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
    }
}