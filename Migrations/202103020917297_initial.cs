namespace ECommerce.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ChiTietDonHangHuys",
                c => new
                    {
                        MaChiTietDonHangHuy = c.Int(nullable: false, identity: true),
                        MaSanPham = c.Int(),
                        MaHuyDon = c.Int(),
                        SoLuong = c.Int(nullable: false),
                        TinhTrang = c.String(),
                    })
                .PrimaryKey(t => t.MaChiTietDonHangHuy)
                .ForeignKey("dbo.HuyDonHangs", t => t.MaHuyDon)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .Index(t => t.MaSanPham)
                .Index(t => t.MaHuyDon);
            
            CreateTable(
                "dbo.HuyDonHangs",
                c => new
                    {
                        MaHuyDon = c.Int(nullable: false, identity: true),
                        MaDonHang = c.String(),
                        MaKH = c.Int(),
                        TongTien = c.Int(nullable: false),
                        NgayXacNhan = c.DateTime(nullable: false),
                        TinhTrang = c.String(),
                    })
                .PrimaryKey(t => t.MaHuyDon)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .Index(t => t.MaKH);
            
            CreateTable(
                "dbo.KhachHangs",
                c => new
                    {
                        MaKH = c.Int(nullable: false, identity: true),
                        TenKH = c.String(),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        DiemTichLuy = c.Int(nullable: false),
                        Email = c.String(),
                        NgaySinh = c.String(),
                        UserName = c.String(nullable: false, maxLength: 25),
                        Password = c.String(nullable: false, maxLength: 30),
                        MaQuyen = c.Int(),
                        MaLoaiKH = c.Int(),
                    })
                .PrimaryKey(t => t.MaKH)
                .ForeignKey("dbo.LoaiKhachHangs", t => t.MaLoaiKH)
                .ForeignKey("dbo.Quyens", t => t.MaQuyen)
                .Index(t => t.MaQuyen)
                .Index(t => t.MaLoaiKH);
            
            CreateTable(
                "dbo.GioHangs",
                c => new
                    {
                        MaGioHang = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        MaSanPham = c.Int(),
                        VoucherID = c.Int(),
                        TongTien = c.Int(nullable: false),
                        NgayDatHang = c.String(),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaGioHang)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .ForeignKey("dbo.Vouchers", t => t.VoucherID)
                .Index(t => t.MaKH)
                .Index(t => t.MaSanPham)
                .Index(t => t.VoucherID);
            
            CreateTable(
                "dbo.SanPhams",
                c => new
                    {
                        MaSanPham = c.Int(nullable: false, identity: true),
                        TenSanPham = c.String(),
                        GiaBan = c.Int(nullable: false),
                        MoTa = c.String(),
                        TomTat = c.String(),
                        Hinh = c.String(),
                        TinhTrang = c.String(),
                        SoLuong = c.Int(nullable: false),
                        MaDanhMuc = c.Int(),
                        MaThuongHieu = c.Int(),
                    })
                .PrimaryKey(t => t.MaSanPham)
                .ForeignKey("dbo.DanhMucs", t => t.MaDanhMuc)
                .ForeignKey("dbo.ThuongHieux", t => t.MaThuongHieu)
                .Index(t => t.MaDanhMuc)
                .Index(t => t.MaThuongHieu);
            
            CreateTable(
                "dbo.ChiTietTruyCaps",
                c => new
                    {
                        MaChiTietTruyCap = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        MaSanPham = c.Int(),
                        NgayTruyCap = c.String(),
                    })
                .PrimaryKey(t => t.MaChiTietTruyCap)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .Index(t => t.MaKH)
                .Index(t => t.MaSanPham);
            
            CreateTable(
                "dbo.DanhMucs",
                c => new
                    {
                        MaDanhMuc = c.Int(nullable: false, identity: true),
                        TenDanhMuc = c.String(),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaDanhMuc);
            
            CreateTable(
                "dbo.ThuongHieux",
                c => new
                    {
                        MaThuongHieu = c.Int(nullable: false, identity: true),
                        TenThuongHieu = c.String(),
                        SoLuong = c.Int(nullable: false),
                        MaDanhMuc = c.Int(),
                    })
                .PrimaryKey(t => t.MaThuongHieu)
                .ForeignKey("dbo.DanhMucs", t => t.MaDanhMuc)
                .Index(t => t.MaDanhMuc);
            
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        ReviewID = c.Int(nullable: false, identity: true),
                        NoiDung = c.String(),
                        MaKH = c.Int(),
                        MaSanPham = c.Int(),
                        NgayDang = c.String(),
                    })
                .PrimaryKey(t => t.ReviewID)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .Index(t => t.MaKH)
                .Index(t => t.MaSanPham);
            
            CreateTable(
                "dbo.TruyCaps",
                c => new
                    {
                        MaTruyCap = c.Int(nullable: false, identity: true),
                        SoLanTruyCap = c.Int(nullable: false),
                        MaSanPham = c.Int(),
                        KhachHang_MaKH = c.Int(),
                    })
                .PrimaryKey(t => t.MaTruyCap)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .ForeignKey("dbo.KhachHangs", t => t.KhachHang_MaKH)
                .Index(t => t.MaSanPham)
                .Index(t => t.KhachHang_MaKH);
            
            CreateTable(
                "dbo.Vouchers",
                c => new
                    {
                        VoucherID = c.Int(nullable: false, identity: true),
                        TenVoucher = c.String(),
                        GhiChu = c.String(),
                        NgayBatDau = c.DateTime(nullable: false),
                        NgayHetHan = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.VoucherID);
            
            CreateTable(
                "dbo.LoaiKhachHangs",
                c => new
                    {
                        MaLoaiKH = c.Int(nullable: false, identity: true),
                        TenLoaiKH = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.MaLoaiKH);
            
            CreateTable(
                "dbo.Quyens",
                c => new
                    {
                        MaQuyen = c.Int(nullable: false, identity: true),
                        TenQuyen = c.String(),
                    })
                .PrimaryKey(t => t.MaQuyen);
            
            CreateTable(
                "dbo.ChiTietGioHangs",
                c => new
                    {
                        MaChiTietGioHang = c.Int(nullable: false, identity: true),
                        MaSanPham = c.Int(),
                        OrderID = c.Int(),
                        SoLuong = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MaChiTietGioHang)
                .ForeignKey("dbo.Orders", t => t.OrderID)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .Index(t => t.MaSanPham)
                .Index(t => t.OrderID);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        VoucherID = c.Int(),
                        TongTien = c.Int(nullable: false),
                        NgayDatHang = c.DateTime(nullable: false),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        HoTen = c.String(),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.Vouchers", t => t.VoucherID)
                .Index(t => t.MaKH)
                .Index(t => t.VoucherID);
            
            CreateTable(
                "dbo.LoginManages",
                c => new
                    {
                        LoginManageKey = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        TimeLogin = c.String(),
                        TimeLogout = c.String(),
                    })
                .PrimaryKey(t => t.LoginManageKey);
            
            CreateTable(
                "dbo.OrderConfirmedDetails",
                c => new
                    {
                        MaChiTietDonHang = c.Int(nullable: false, identity: true),
                        MaSanPham = c.Int(),
                        MaDonHang = c.Int(),
                        SoLuong = c.Int(nullable: false),
                        TinhTrang = c.String(),
                        NgayXacNhan = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.MaChiTietDonHang)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .ForeignKey("dbo.XacNhanDonHangs", t => t.MaDonHang)
                .Index(t => t.MaSanPham)
                .Index(t => t.MaDonHang);
            
            CreateTable(
                "dbo.XacNhanDonHangs",
                c => new
                    {
                        MaDonHang = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        VoucherID = c.Int(),
                        TongTien = c.Int(nullable: false),
                        TinhTrang = c.String(),
                        NgayXacNhan = c.DateTime(nullable: false),
                        DiaChi = c.String(),
                        SDT = c.String(),
                        HoTen = c.String(),
                    })
                .PrimaryKey(t => t.MaDonHang)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.Vouchers", t => t.VoucherID)
                .Index(t => t.MaKH)
                .Index(t => t.VoucherID);
            
            CreateTable(
                "dbo.OrderConfirmeds",
                c => new
                    {
                        MaDonHang = c.Int(nullable: false, identity: true),
                        MaKH = c.Int(),
                        MaSanPham = c.Int(),
                        VoucherID = c.Int(),
                        TongTien = c.Int(nullable: false),
                        NgayDatHang = c.String(),
                        SoLuong = c.Int(nullable: false),
                        TinhTrang = c.String(),
                    })
                .PrimaryKey(t => t.MaDonHang)
                .ForeignKey("dbo.KhachHangs", t => t.MaKH)
                .ForeignKey("dbo.SanPhams", t => t.MaSanPham)
                .ForeignKey("dbo.Vouchers", t => t.VoucherID)
                .Index(t => t.MaKH)
                .Index(t => t.MaSanPham)
                .Index(t => t.VoucherID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderConfirmeds", "VoucherID", "dbo.Vouchers");
            DropForeignKey("dbo.OrderConfirmeds", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.OrderConfirmeds", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.XacNhanDonHangs", "VoucherID", "dbo.Vouchers");
            DropForeignKey("dbo.OrderConfirmedDetails", "MaDonHang", "dbo.XacNhanDonHangs");
            DropForeignKey("dbo.XacNhanDonHangs", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.OrderConfirmedDetails", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietGioHangs", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.Orders", "VoucherID", "dbo.Vouchers");
            DropForeignKey("dbo.Orders", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietGioHangs", "OrderID", "dbo.Orders");
            DropForeignKey("dbo.ChiTietDonHangHuys", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.HuyDonHangs", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.TruyCaps", "KhachHang_MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.KhachHangs", "MaQuyen", "dbo.Quyens");
            DropForeignKey("dbo.KhachHangs", "MaLoaiKH", "dbo.LoaiKhachHangs");
            DropForeignKey("dbo.GioHangs", "VoucherID", "dbo.Vouchers");
            DropForeignKey("dbo.TruyCaps", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.Reviews", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.Reviews", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.GioHangs", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.SanPhams", "MaThuongHieu", "dbo.ThuongHieux");
            DropForeignKey("dbo.ThuongHieux", "MaDanhMuc", "dbo.DanhMucs");
            DropForeignKey("dbo.SanPhams", "MaDanhMuc", "dbo.DanhMucs");
            DropForeignKey("dbo.ChiTietTruyCaps", "MaSanPham", "dbo.SanPhams");
            DropForeignKey("dbo.ChiTietTruyCaps", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.GioHangs", "MaKH", "dbo.KhachHangs");
            DropForeignKey("dbo.ChiTietDonHangHuys", "MaHuyDon", "dbo.HuyDonHangs");
            DropIndex("dbo.OrderConfirmeds", new[] { "VoucherID" });
            DropIndex("dbo.OrderConfirmeds", new[] { "MaSanPham" });
            DropIndex("dbo.OrderConfirmeds", new[] { "MaKH" });
            DropIndex("dbo.XacNhanDonHangs", new[] { "VoucherID" });
            DropIndex("dbo.XacNhanDonHangs", new[] { "MaKH" });
            DropIndex("dbo.OrderConfirmedDetails", new[] { "MaDonHang" });
            DropIndex("dbo.OrderConfirmedDetails", new[] { "MaSanPham" });
            DropIndex("dbo.Orders", new[] { "VoucherID" });
            DropIndex("dbo.Orders", new[] { "MaKH" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "OrderID" });
            DropIndex("dbo.ChiTietGioHangs", new[] { "MaSanPham" });
            DropIndex("dbo.TruyCaps", new[] { "KhachHang_MaKH" });
            DropIndex("dbo.TruyCaps", new[] { "MaSanPham" });
            DropIndex("dbo.Reviews", new[] { "MaSanPham" });
            DropIndex("dbo.Reviews", new[] { "MaKH" });
            DropIndex("dbo.ThuongHieux", new[] { "MaDanhMuc" });
            DropIndex("dbo.ChiTietTruyCaps", new[] { "MaSanPham" });
            DropIndex("dbo.ChiTietTruyCaps", new[] { "MaKH" });
            DropIndex("dbo.SanPhams", new[] { "MaThuongHieu" });
            DropIndex("dbo.SanPhams", new[] { "MaDanhMuc" });
            DropIndex("dbo.GioHangs", new[] { "VoucherID" });
            DropIndex("dbo.GioHangs", new[] { "MaSanPham" });
            DropIndex("dbo.GioHangs", new[] { "MaKH" });
            DropIndex("dbo.KhachHangs", new[] { "MaLoaiKH" });
            DropIndex("dbo.KhachHangs", new[] { "MaQuyen" });
            DropIndex("dbo.HuyDonHangs", new[] { "MaKH" });
            DropIndex("dbo.ChiTietDonHangHuys", new[] { "MaHuyDon" });
            DropIndex("dbo.ChiTietDonHangHuys", new[] { "MaSanPham" });
            DropTable("dbo.OrderConfirmeds");
            DropTable("dbo.XacNhanDonHangs");
            DropTable("dbo.OrderConfirmedDetails");
            DropTable("dbo.LoginManages");
            DropTable("dbo.Orders");
            DropTable("dbo.ChiTietGioHangs");
            DropTable("dbo.Quyens");
            DropTable("dbo.LoaiKhachHangs");
            DropTable("dbo.Vouchers");
            DropTable("dbo.TruyCaps");
            DropTable("dbo.Reviews");
            DropTable("dbo.ThuongHieux");
            DropTable("dbo.DanhMucs");
            DropTable("dbo.ChiTietTruyCaps");
            DropTable("dbo.SanPhams");
            DropTable("dbo.GioHangs");
            DropTable("dbo.KhachHangs");
            DropTable("dbo.HuyDonHangs");
            DropTable("dbo.ChiTietDonHangHuys");
        }
    }
}
