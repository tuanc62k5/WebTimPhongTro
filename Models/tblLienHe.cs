using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("tblLienHe")]
    public class tblLienHe
    {
        [Key]
        public int LH_ID { get; set; }
        public int TD_ID { get; set; }
        public int TK_NguoiGui { get; set; }
        public int TK_NguoiNhan { get; set; }
        public string LH_NoiDung { get; set; } = "";
        public string LH_DienThoai { get; set; } = "";
        public string LH_TrangThai { get; set; } = "";
        public DateTime LH_NgayGui { get; set; }
        public DateTime? LH_NgayCapNhat {get; set;}

        public tblTinDang? TinDang { get; set; }
        public tblTaiKhoan? NguoiGui { get; set; }
        public tblTaiKhoan? NguoiNhan { get; set; }
    }
}