using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ECommerce
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");



            routes.MapRoute(
            name: "Register",
            url: "tai-khoan/dang-ky",
            defaults: new { controller = "Authentication", action = "Register", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "Login",
            url: "tai-khoan/dang-nhap",
            defaults: new { controller = "Authentication", action = "Login", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "Shop",
            url: "trang-chu/cua-hang",
            defaults: new { controller = "Home", action = "Shop", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Category Shop",
            url: "trang-chu/danh-muc/{maDM}",
            defaults: new { controller = "Home", action = "ShopWithCategory", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Brand Shop",
            url: "trang-chu/thuong-hieu/{maTH}",
            defaults: new { controller = "Home", action = "ShopWithBrand", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "User Information",
            url: "thong-tin-ca-nhan/{id}",
            defaults: new { controller = "User", action = "UserInformation", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "User Order",
            url: "don-hang",
            defaults: new { controller = "Home", action = "MyOrder", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Product Details",
            url: "chi-tiet-san-pham/{id}",
            defaults: new { controller = "SanPhams", action = "Details", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "LogOut",
            url: "dang-xuat",
            defaults: new { controller = "Authentication", action = "LogOut", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Cart",
            url: "gio-hang",
            defaults: new { controller = "Home", action = "Cart", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "CheckOut",
            url: "thanh-toan",
            defaults: new { controller = "Home", action = "AddIntoDatabase", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "Review",
            url: "danh-gia/{id}/{maCTDH}",
            defaults: new { controller = "Home", action = "AddReview", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "ChoXacNhan",
            url: "don-hang/cho-xac-nhan",
            defaults: new { controller = "Home", action = "ChoXacNhan", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DaXacNhan",
            url: "don-hang/da-xac-nhan",
            defaults: new { controller = "Home", action = "DaXacNhan", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DangGiaoHang",
            url: "don-hang/dang-giao",
            defaults: new { controller = "Home", action = "DangGiaoHang", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DaGiaoHang",
            url: "don-hang/da-giao",
            defaults: new { controller = "Home", action = "DaGiaoHang", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "DaHuyDon",
            url: "don-hang/da-huy",
            defaults: new { controller = "Home", action = "DaHuyDon", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "AddIntoCart",
            url: "them-vao-gio-hang/{maSanPham}",
            defaults: new { controller = "Home", action = "AddIntoCart", id = UrlParameter.Optional }
            );


            routes.MapRoute(
            name: "SearchByPrice",
            url: "san-pham/tim-kiem/{price}",
            defaults: new { controller = "Home", action = "SearchByPrice", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Admin",
            url: "quan-tri-he-thong",
            defaults: new { controller = "Authentication", action = "Admin", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "UserRole",
            url: "quan-ly/quyen-tai-khoan",
            defaults: new { controller = "Quyens", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Account",
            url: "quan-ly/tai-khoan",
            defaults: new { controller = "KhachHangs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Category Manage",
            url: "quan-ly/danh-muc",
            defaults: new { controller = "DanhMucs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Brand Manage",
            url: "quan-ly/thuong-hieu",
            defaults: new { controller = "ThuongHieux", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Product Manage",
            url: "quan-ly/san-pham",
            defaults: new { controller = "SanPhams", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
           name: "Customer Type Manage",
           url: "quan-ly/loai-khach-hang",
           defaults: new { controller = "LoaiKhachHangs", action = "Index", id = UrlParameter.Optional }
           );

            routes.MapRoute(
            name: "Review Manage",
            url: "quan-ly/danh-gia",
            defaults: new { controller = "Reviews", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Voucher Manage",
            url: "quan-ly/ma-giam-gia",
            defaults: new { controller = "Vouchers", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Order Manage",
            url: "quan-ly/don-hang-moi",
            defaults: new { controller = "Orders", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Order Details Manage",
            url: "quan-ly/don-hang-moi/chi-tiet",
            defaults: new { controller = "ChiTietGioHangs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Order Confirmed Manage",
            url: "quan-ly/don-hang/da-xac-nhan",
            defaults: new { controller = "XacNhanDonHangs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Order Confirmed Details Manage",
            url: "quan-ly/don-hang/da-xac-nhan/chi-tiet",
            defaults: new { controller = "OrderConfirmedDetails", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Canceled Order Manage",
            url: "quan-ly/don-hang/da-huy",
            defaults: new { controller = "HuyDonHangs", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Login History",
            url: "quan-ly/lich-su-dang-nhap",
            defaults: new { controller = "LoginManages", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Product Access Manage",
            url: "quan-ly/san-pham/truy-cap",
            defaults: new { controller = "TruyCaps", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Product Access Details Manage",
            url: "quan-ly/san-pham/truy-cap/chi-tiet",
            defaults: new { controller = "ChiTietTruyCaps", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Create Product",
            url: "san-pham/tao-moi",
            defaults: new { controller = "SanPhams", action = "Create", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Edit Product",
            url: "san-pham/chinh-sua/{id}",
            defaults: new { controller = "SanPhams", action = "Edit", id = UrlParameter.Optional }
            );

            routes.MapRoute(
            name: "Delete Product",
            url: "san-pham/xoa/{id}",
            defaults: new { controller = "SanPhams", action = "Delete", id = UrlParameter.Optional }
            );












            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );





        }
    }
}
