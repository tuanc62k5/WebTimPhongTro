using DoAn.Models;
using DoAn.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace DoAn.Areas.Admin.Components
{
    [ViewComponent(Name = "AdminMenu")]
    public class AdminMenuComponent : ViewComponent
    {
        private readonly DBContext _context;
        public AdminMenuComponent(DBContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var menu = await _context.AdminMenus.Where(m => m.IsActive == true)
            .OrderBy(m => m.ItemOrder).ToListAsync();
            return View(menu);
        }
        
    }
}