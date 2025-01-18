using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace todo.Models.DTOs
{
    public class CongViecDTO
    {
        public int ID { get; set; }
        public string TieuDe { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime? NgayKetThuc { get; set; }
        public bool TrangThai { get; set; }
        public int LoaiCongViecID { get; set; }
        public string LoaiCongViecTen { get; set; } // Tên loại công việc, nếu cần trả thêm thông tin này
    }
}
