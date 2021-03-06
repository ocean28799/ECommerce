using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class CSDLContext : DbContext
    {
        public CSDLContext()
        {
            SqlConnectionStringBuilder sqlb = new SqlConnectionStringBuilder();
            sqlb.DataSource = "DESKTOP-HL4O83C\\SQLEXPRESS";
            sqlb.InitialCatalog = "db";
            sqlb.IntegratedSecurity = true;
            this.Database.Connection.ConnectionString = sqlb.ConnectionString;

            //this.Database.Connection.ConnectionString = "Data Source=SQL5065.site4now.net;Initial Catalog=DB_A6C256_techpi;User Id=DB_A6C256_techpi_admin;Password=R3matteblack";
        }
        public DbSet<SanPham> SanPhams { get; set; }
        public DbSet<DanhMuc> DanhMucs { get; set; }
        public DbSet<KhachHang> KhachHangs { get; set; }
        public DbSet<Quyen> Quyens { get; set; }
        public DbSet<GioHang> GioHangs { get; set; }
        public DbSet<LoginManage> LoginManages { get; set; }
        public DbSet<HuyDonHang> HuyDonHangs { get; set; }
        public DbSet<ThuongHieu> ThuongHieus { get; set; }
        public DbSet<TruyCap> TruyCaps { get; set; }
        public DbSet<OrderConfirmedDetail> OrderConfirmedDetails { get; set; }
        public DbSet<ChiTietGioHang> ChiTietGioHangs { get; set; }
        public DbSet<XacNhanDonHang> XacNhanDonHangs { get; set; }
        public System.Data.Entity.DbSet<ECommerce.Models.Voucher> Vouchers { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Review> Reviews { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.OrderConfirmed> OrderConfirmeds { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.Order> Orders { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.ChiTietTruyCap> ChiTietTruyCaps { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.LoaiKhachHang> LoaiKhachHangs { get; set; }

        public System.Data.Entity.DbSet<ECommerce.Models.ChiTietDonHangHuy> ChiTietDonHangHuys { get; set; }
    }
    public class SanPham
    {
        [Key]
        public int MaSanPham { get; set; }
        [DisplayName("Tên Sản Phẩm")]
        public string TenSanPham { get; set; }
        [DisplayName("Đơn Giá"), Range(100, int.MaxValue, ErrorMessage = "Đơn giá phải lớn hơn 100"), Required(ErrorMessage = "Hãy nhập đơn giá")]
        public int GiaBan { get; set; }
        [DisplayName("Mô Tả Sản Phẩm")]
        public string MoTa { get; set; }
        [DisplayName("Tóm Tắt Sản Phẩm")]
        public string TomTat { get; set; }
        [DisplayName("Hình Ảnh")]
        public string Hinh { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }
        [DisplayName("Số Lượng"), Range(0, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 0")]
        public int SoLuong { get; set; }
        public Nullable<int> MaDanhMuc { get; set; }
        public virtual DanhMuc DanhMucs { get; set; }
        public Nullable<int> MaThuongHieu { get; set; }
        public virtual ThuongHieu ThuongHieu { get; set; }
        public virtual ICollection<GioHang> GioHang { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TruyCap> TruyCaps { get; set; }
        public virtual ICollection<ChiTietTruyCap> ChiTietTruyCaps { get; set; }
    }
    public class DanhMuc
    {
        [Key]
        public int MaDanhMuc { get; set; }
        [DisplayName("Tên Danh Mục")]
        public string TenDanhMuc { get; set; }
        [DisplayName("Số Lượng Sản Phẩm")]
        public int SoLuong { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
        public virtual ICollection<ThuongHieu> ThuongHieus { get; set; }
    }
    public class ThuongHieu
    {
        [Key]
        public int MaThuongHieu { get; set; }
        [DisplayName("Tên Thương Hiệu")]
        public string TenThuongHieu { get; set; }
        [DisplayName("Số Lượng Sản Phẩm")]
        public int SoLuong { get; set; }
        public Nullable<int> MaDanhMuc { get; set; }
        public virtual DanhMuc DanhMuc { get; set; }
        public virtual ICollection<SanPham> SanPhams { get; set; }
    }
    public class LoaiKhachHang
    {
        [Key, DisplayName("Loại Khách Hàng")]
        public int MaLoaiKH { get; set; }
        [DisplayName("Tên Loại Khách Hàng")]
        public string TenLoaiKH { get; set; }
        [DisplayName("GhiChu")]
        public string GhiChu { get; set; }
        public virtual ICollection<KhachHang> KhachHang { get; set; }
    }
    public class KhachHang
    {
        [Key]
        public int MaKH { get; set; }
        [DisplayName("Tên Khách Hàng")]
        public string TenKH { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Số Điện Thoại")]
        public string SDT { get; set; }
        [DisplayName("Điểm Tích Luỹ")]
        public int DiemTichLuy { get; set; }
        [DisplayName("Email"),EmailAddress]
        public string Email { get; set; }
        [DisplayName("Ngày Sinh")]
        public string NgaySinh { get; set; }
        [Required(ErrorMessage = "Please input username"), StringLength(maximumLength: 25, MinimumLength = 3, ErrorMessage = "Tên đăng nhập phải từ 3 đến 25 kí tự")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please input password"), StringLength(maximumLength: 30, MinimumLength = 3, ErrorMessage = "Mật khẩu phải từ 3 đến 30 kí tự")]
        public string Password { get; set; }
        public virtual ICollection<GioHang> GioHang { get; set; }
        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<TruyCap> TruyCaps { get; set; }
        public Nullable<int> MaQuyen { get; set; }
        public virtual Quyen Quyen { get; set; }
        public Nullable<int> MaLoaiKH { get; set; }
        public virtual LoaiKhachHang LoaiKhachHang { get; set; }
    }
    public class Quyen
    {
        [Key, DisplayName("Quyền")]
        public int MaQuyen { get; set; }
        [DisplayName("Tên Quyền")]
        public string TenQuyen { get; set; }
        public virtual ICollection<KhachHang> KhachHang { get; set; }
    }
    public class Voucher
    {
        [Key]
        public int VoucherID { get; set; }
        [DisplayName("Tên mã giảm giá")]
        public string TenVoucher { get; set; }
        [DisplayName("Ghi Chú")]
        public string GhiChu { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayHetHan { get; set; }
        public virtual ICollection<GioHang> GioHangs { get; set; }
    }
   

    public class GioHang
    {
        [Key]
        public int MaGioHang { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public virtual Voucher Voucher { get; set; }
        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }
        [DisplayName("Ngày Đặt")]
        public string NgayDatHang { get; set; }
        [DisplayName("Số Lượng"), Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 1")]
        public int SoLuong { get; set; }
    }

    public class Order
    {
        [Key]
        public int OrderID { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public virtual Voucher Voucher { get; set; }
        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }
        [DisplayName("Ngày Đặt")]
        public DateTime NgayDatHang { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Số Điện Thoại")]
        public string SDT { get; set; }
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; } 
        public virtual ICollection<ChiTietGioHang> ChiTietGioHangs { get; set; }
    }

    public class ChiTietGioHang
    {
        [Key]
        public int MaChiTietGioHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Nullable<int> OrderID { get; set; }
        public virtual Order Order { get; set; }
        [DisplayName("Số Lượng"), Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 1")]
        public int SoLuong { get; set; }
    }

    public class LoginManage
    {
        [Key]
        public int LoginManageKey { get; set; }
        [DisplayName("Tên Tài Khoản")]
        public string UserName { get; set; }
        [DisplayName("Thời Gian Đăng Nhập")]
        public string TimeLogin { get; set; }
        [DisplayName("Thời Gian Đăng Xuất")]
        public string TimeLogout { get; set; }
    }
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }
        [DisplayName("Nội Dung")]
        public string NoiDung { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public string NgayDang { get; set; }
    }

    public class TruyCap
    {
        [Key]
        public int MaTruyCap { get; set; }
        [DisplayName("Số Lượng Truy Cập")]
        public int SoLanTruyCap { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
    }
    public class ChiTietTruyCap
    {
        [Key]
        public int MaChiTietTruyCap { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        [DisplayName("Ngày Truy Cập")]
        public string NgayTruyCap { get; set; }
    }

    public class HuyDonHang
    {
        [Key]
        public int MaHuyDon { get; set; }
        [DisplayName("Mã Đơn Hàng")]
        public string MaDonHang { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }
        [DisplayName("Ngày Xác Nhận")]
        public DateTime NgayXacNhan { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }
        public virtual ICollection<ChiTietDonHangHuy> ChiTietDonHangHuys { get; set; }
    }

    public class ChiTietDonHangHuy
    {
        [Key]
        public int MaChiTietDonHangHuy { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Nullable<int> MaHuyDon { get; set; }
        public virtual HuyDonHang HuyDonHang { get; set; }
        [DisplayName("Số Lượng"), Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 1")]
        public int SoLuong { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }
        
    }


    public class XacNhanDonHang
    {
        [Key]
        public int MaDonHang { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public virtual Voucher Voucher { get; set; }
        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }
        [DisplayName("Ngày Xác Nhận")]
        public DateTime NgayXacNhan { get; set; }
        [DisplayName("Địa Chỉ")]
        public string DiaChi { get; set; }
        [DisplayName("Số Điện Thoại")]
        public string SDT { get; set; }
        [DisplayName("Họ Tên")]
        public string HoTen { get; set; }
        public virtual ICollection<OrderConfirmedDetail> OrderConfirmedDetails { get; set; }
    }

    public class OrderConfirmedDetail
    {
        [Key]
        public int MaChiTietDonHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Nullable<int> MaDonHang { get; set; }
        public virtual XacNhanDonHang XacNhanDonHang { get; set; }
        [DisplayName("Số Lượng"), Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 1")]
        public int SoLuong { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }
        [DisplayName("Ngày Xác Nhận")]
        public DateTime NgayXacNhan { get; set; }
    }
    public class OrderConfirmed
    {
        [Key]
        public int MaDonHang { get; set; }
        public Nullable<int> MaKH { get; set; }
        public virtual KhachHang KhachHang { get; set; }
        public Nullable<int> MaSanPham { get; set; }
        public virtual SanPham SanPham { get; set; }
        public Nullable<int> VoucherID { get; set; }
        public virtual Voucher Voucher { get; set; }
        [DisplayName("Tổng Tiền")]
        public int TongTien { get; set; }
        [DisplayName("Ngày Đặt")]
        public string NgayDatHang { get; set; }
        [DisplayName("Số Lượng"), Range(1, int.MaxValue, ErrorMessage = "Số lượng sản phẩm phải lớn hơn 1")]
        public int SoLuong { get; set; }
        [DisplayName("Tình Trạng")]
        public string TinhTrang { get; set; }

    }

}