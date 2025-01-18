using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace todo.Models.DTOs
{
    public class LoaiCongViecDTO
    {
        public int ID { get; set; }
        public string TenLoai { get; set; }
        public string MucDoKhanCap { get; set; }
        //public ICollection<CongViec> CongViecs { get; set; }
    }
}