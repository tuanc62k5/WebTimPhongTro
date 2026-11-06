using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace DoAn.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {    
        }
        public DbSet<tblMenu> Menus {get; set;}
        public DbSet<tblTaiKhoan> TaiKhoans {get; set;}
        public DbSet<tblKhuVuc> KhuVucs {get; set;}
        public DbSet<tblPhongTro> PhongTros {get; set;}
        public DbSet<tblTinDang> TinDangs {get; set;}
        public DbSet<tblLienHe> LienHes {get; set;}
    }
}