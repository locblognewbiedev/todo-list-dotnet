//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace todo.Models
//{
//    public class CongViec
//    {
//        public int ID { get; set; } // ID tự tăng
//        public string TieuDe { get; set; } // Tiêu đề công việc
//        public string MoTa { get; set; } // Mô tả công việc
//        public DateTime NgayBatDau { get; set; } // Ngày bắt đầu
//        public DateTime? NgayKetThuc { get; set; } // Ngày kết thúc (nullable)
//        public bool TrangThai { get; set; } // Trạng thái công việc (0: chưa hoàn thành, 1: đã hoàn thành)

//        // Khóa ngoại
//        public int LoaiCongViecID { get; set; } // Liên kết đến bảng LoaiCongViec
//        public LoaiCongViec LoaiCongViec { get; set; }
//    }
//}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;



namespace todo.Models
{
    [Table("CongViec")]
    public class CongViec
    {
        // ID tự động tăng
        public int ID { get; set; }

        // Tiêu đề công việc
        public string TieuDe { get; set; }

        // Mô tả công việc
        public string MoTa { get; set; }

        // Ngày bắt đầu công việc
        public DateTime NgayBatDau { get; set; }

        // Ngày kết thúc công việc (nullable vì công việc có thể chưa có ngày kết thúc)
        public DateTime? NgayKetThuc { get; set; }

        // Trạng thái công việc: 0 là chưa hoàn thành, 1 là đã hoàn thành
        public bool TrangThai { get; set; }

        // Khóa ngoại (Liên kết đến bảng LoaiCongViec)
        public int LoaiCongViecID { get; set; }

        // Quan hệ với bảng LoaiCongViec
        public LoaiCongViec LoaiCongViec { get; set; }
    }
}
