using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("tblPhongTro")]
    public class tblPhongTro
    {
        [Key]
        public int PT_ID { get; set; }
        public int TK_ID {get; set;}
        public int KV_ID { get; set; }
        public string PT_HinhAnh {get; set;} = "";
        public string PT_TenPhong {get; set;} = "";
        public string PT_LoaiPhong {get; set;} = "";
        public decimal PT_DienTich {get; set;}
        public decimal PT_GiaThue {get; set;}
        public decimal PT_TienCoc {get; set;}
        public int PT_SucChua {get; set;}
        public string PT_DiaChiCuThe { get; set; } = "";
        public string PT_MoTa { get; set; } = "";
        public decimal? PT_TienDien { get; set; }
        public decimal? PT_TienNuoc { get; set; }
        public string PT_TienIch {get; set;} = "";
        public string PT_TrangThai {get; set;} = "";
        public DateTime PT_NgayTao { get; set; }
        public DateTime? PT_NgayCapNhat {get; set;}

        public tblTaiKhoan? TaiKhoan {get; set;}
        public tblKhuVuc? KhuVuc {get; set;}
    }
}