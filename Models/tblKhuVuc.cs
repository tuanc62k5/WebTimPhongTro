using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DoAn.Models
{
    [Table("tblKhuVuc")]
    public class tblKhuVuc
    {
        [Key]
        public int KV_ID { get; set; }
        public string KV_TenKhuVuc { get; set; } = "";
        public string KV_TinhThanh { get; set; } = "";
        public string KV_PhuongXa { get; set; } = "";
        public string KV_DiaChiChiTiet { get; set; } = "";
        public DateTime KV_NgayTao { get; set; }
    }
}