using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("tblTaiKhoan")]
    public class tblTaiKhoan
    {
        [Key]
        public int TK_ID { get; set; }
        public string TK_TenDangNhap { get; set; } = "";
        public string TK_MatKhau { get; set; } = "";
        public string TK_HoTen { get; set; } = "";
        public string? TK_Email { get; set; }
        public string? TK_SoDienThoai { get; set; }
        public string TK_VaiTro { get; set; } = "NhanVien";
        public bool TK_TrangThai { get; set; } = true;
        public DateTime TK_NgayTao { get; set; }
    }
}