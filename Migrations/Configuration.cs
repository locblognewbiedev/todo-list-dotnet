

using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using Bogus; // Thêm Bogus vào
using todo.Models;

namespace todo.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<todo.Data.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(todo.Data.DBContext context)
        {
            // Xóa dữ liệu cũ nếu cần thiết
            if (context.LoaiCongViecs.Any()) context.LoaiCongViecs.RemoveRange(context.LoaiCongViecs);
            if (context.CongViecs.Any()) context.CongViecs.RemoveRange(context.CongViecs);

            // Tạo Faker instance cho bảng LoaiCongViecs
            var loaiCongViecFaker = new Faker<LoaiCongViec>()
                .RuleFor(l => l.TenLoai, f => f.Lorem.Word())  // Tạo tên loại công việc ngẫu nhiên
                .RuleFor(l => l.MucDoKhanCap, f => f.PickRandom(new[] { "Cao", "Trung bình", "Thấp" }));  // Mức độ khẩn cấp ngẫu nhiên

            // Tạo 5 loại công việc giả
            var loaiCongViecs = loaiCongViecFaker.Generate(5);  // Tạo 5 loại công việc ngẫu nhiên

            // Thêm các loại công việc vào database, kiểm tra nếu chưa tồn tại
            foreach (var loaiCongViec in loaiCongViecs)
            {
                if (!context.LoaiCongViecs.Any(l => l.TenLoai == loaiCongViec.TenLoai))
                {
                    context.LoaiCongViecs.Add(loaiCongViec);
                }
            }

            // Lưu các loại công việc vào DB để có ID
            context.SaveChanges();

            var loaiCongViecIds = context.LoaiCongViecs.Select(l => l.ID).ToList();
            // Tạo Faker instance cho bảng CongViecs
            var congViecFaker = new Faker<CongViec>()
                .RuleFor(c => c.TieuDe, f => f.Lorem.Sentence())  // Tạo tiêu đề công việc ngẫu nhiên
                .RuleFor(c => c.MoTa, f => f.Lorem.Paragraph())   // Tạo mô tả công việc ngẫu nhiên
                .RuleFor(c => c.NgayBatDau, f => f.Date.Past(1))  // Ngày bắt đầu trong quá khứ
                .RuleFor(c => c.TrangThai, f => f.Random.Bool())  // Trạng thái công việc ngẫu nhiên (true/false)
                .RuleFor(c => c.LoaiCongViecID, f => f.PickRandom(loaiCongViecIds)); // Liên kết với loại công việc (ID từ 1 đến 5)

            // Tạo 10 công việc giả
            var congViecs = congViecFaker.Generate(10);  // Tạo 10 công việc ngẫu nhiên

            // Thêm các công việc vào database, kiểm tra nếu chưa tồn tại
            foreach (var congViec in congViecs)
            {
                if (!context.CongViecs.Any(c => c.TieuDe == congViec.TieuDe))
                {
                    context.CongViecs.Add(congViec);
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            context.SaveChanges();
        }
    }
}

