namespace todo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CongViec",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TieuDe = c.String(),
                        MoTa = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayKetThuc = c.DateTime(),
                        TrangThai = c.Boolean(nullable: false),
                        LoaiCongViecID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.LoaiCongViec", t => t.LoaiCongViecID, cascadeDelete: true)
                .Index(t => t.LoaiCongViecID);
            
            CreateTable(
                "dbo.LoaiCongViec",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TenLoai = c.String(),
                        MucDoKhanCap = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CongViec", "LoaiCongViecID", "dbo.LoaiCongViec");
            DropIndex("dbo.CongViec", new[] { "LoaiCongViecID" });
            DropTable("dbo.LoaiCongViec");
            DropTable("dbo.CongViec");
        }
    }
}
