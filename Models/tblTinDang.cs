using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("tblTinDang")]
    public class tblTinDang
    {
        [Key]
        public int TD_ID { get; set; }
        public int PT_ID { get; set; }
        public string TD_TieuDe { get; set; } = "";
        public string TD_NoiDung { get; set; } = "";
        public int TD_LuotXem { get; set; }
        public string TD_TrangThai { get; set; } = "";
        public string TD_LyDoTuChoi { get; set; } = "";
        public DateTime TD_NgayDang { get; set; }
        public DateTime? TD_NgayCapNhat {get; set;}

        public tblPhongTro? PhongTro {get; set;}
    }
}