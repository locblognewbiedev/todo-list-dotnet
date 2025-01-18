using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace todo.Models
{
    [Table("LoaiCongViec")]
    public class LoaiCongViec
    {
        // ID của loại công việc (Khóa chính)
        public int ID { get; set; }

        // Tên loại công việc
        public string TenLoai { get; set; }

        // Mức độ khẩn cấp của công việc
        public string MucDoKhanCap { get; set; }

        // Quan hệ với bảng CongViec (1 loại công việc có thể có nhiều công việc)
        public ICollection<CongViec> CongViecs { get; set; }
    }
}
