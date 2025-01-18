using System.Data.Entity;
using todo.Models;

namespace todo.Data
{
    public class DBContext : DbContext
    {
        // Constructor nhận chuỗi kết nối từ Web.config
        public DBContext() : base("name=DefaultConnection")
        {
        }

        // Các DbSet tương ứng với các bảng trong cơ sở dữ liệu
        public DbSet<CongViec> CongViecs { get; set; }
        public DbSet<LoaiCongViec> LoaiCongViecs { get; set; }

        // Cấu hình thêm nếu cần thiết
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Chỉ định tên bảng cho các entity
            modelBuilder.Entity<CongViec>().ToTable("CongViec"); // Chỉ định bảng CongViec
            modelBuilder.Entity<LoaiCongViec>().ToTable("LoaiCongViec"); // Chỉ định bảng LoaiCongViec

            // Cấu hình quan hệ giữa CongViec và LoaiCongViec
            modelBuilder.Entity<CongViec>()
                .HasRequired(c => c.LoaiCongViec)
                .WithMany(l => l.CongViecs)
                .HasForeignKey(c => c.LoaiCongViecID)
                .WillCascadeOnDelete(true);
        }
    }
}
