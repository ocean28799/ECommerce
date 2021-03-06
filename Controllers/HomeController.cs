using ECommerce.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class HomeController : Controller
    {
        private CSDLContext db = new CSDLContext();


        public class Tmp3
        {
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public string Hinh { get; set; }
        }

        public ActionResult Index()
        {
            var hotItem = db.SanPhams.Where(s => s.TinhTrang == "Sản Phẩm Hot").ToList();
            ViewBag.hotItem = hotItem;

            var bestSeller = (from item in db.OrderConfirmedDetails
                              group item.SoLuong by item.SanPham into g
                              orderby g.Sum() descending
                              select g.Key).Take(6).ToList();
            ViewBag.bestSeller = bestSeller;


            Session["qty"] = "0";
            if (Session["lang"] == null)
            {
                Session["lang"] = "0".ToString();
            }



            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }



        }


        public ActionResult ShopWithCategory(int maDM, int? page, int? quantityBook)
        {
            ViewBag.maDM = maDM;
            int number = 0;
            if (page != null && quantityBook == null)
            {
                number = page.GetValueOrDefault() * 9;
            }
            else if (page != null && quantityBook != null)
            {
                int a = (int)quantityBook;
                number = page.GetValueOrDefault() * a;
            }
            if (quantityBook != null)
            {
                ViewBag.count = db.SanPhams.Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Count() / quantityBook;
            }
            else
            {
                ViewBag.count = db.SanPhams.Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Count() / 9;
            }
            if (quantityBook != null)
            {
                int a = (int)quantityBook;
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Skip(number).Take(a).ToList();
                ViewBag.ds = SanPhams;
            }
            else
            {
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Skip(number).Take(9).ToList();
                ViewBag.ds = SanPhams;
            }

            if (page == null)
            {
                page = 0;
            }
            ViewBag.quantityBook = quantityBook;
            if (ViewBag.quantityBook == null)
            {
                ViewBag.quantityBook = 9;
            }
            ViewBag.page = page;
            if (quantityBook == null)
            {
                quantityBook = 9;
            }
            int? from = page * quantityBook + 1;
            int? to = page * quantityBook + quantityBook;
            if (to > db.SanPhams.Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Count())
            {
                to = db.SanPhams.Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Count();
            }
            ViewBag.TongSanPham = db.SanPhams.Where(s => s.MaDanhMuc == maDM & s.TinhTrang != "Không Hiển Thị").Count().ToString();
            ViewBag.a = from.ToString();
            ViewBag.b = to.ToString();
            ViewBag.listDanhMuc = db.DanhMucs.ToList();




            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }


        public ActionResult ShopWithBrand(int? page, int? quantityBook, int maTH)
        {
            ViewBag.maTH = maTH;
            int number = 0;
            if (page != null && quantityBook == null)
            {
                number = page.GetValueOrDefault() * 9;
            }
            else if (page != null && quantityBook != null)
            {
                int a = (int)quantityBook;
                number = page.GetValueOrDefault() * a;
            }
            if (quantityBook != null)
            {
                ViewBag.count = db.SanPhams.Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Count() / quantityBook;
            }
            else
            {
                ViewBag.count = db.SanPhams.Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Count() / 9;
            }
            if (quantityBook != null)
            {
                int a = (int)quantityBook;
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Skip(number).Take(a).ToList();
                ViewBag.ds = SanPhams;
            }
            else
            {
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Skip(number).Take(9).ToList();
                ViewBag.ds = SanPhams;
            }

            if (page == null)
            {
                page = 0;
            }
            ViewBag.quantityBook = quantityBook;
            if (ViewBag.quantityBook == null)
            {
                ViewBag.quantityBook = 9;
            }
            ViewBag.page = page;
            if (quantityBook == null)
            {
                quantityBook = 9;
            }
            int? from = page * quantityBook + 1;
            int? to = page * quantityBook + quantityBook;
            if (to > db.SanPhams.Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Count())
            {
                to = db.SanPhams.Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Count();
            }
            ViewBag.TongSanPham = db.SanPhams.Where(s => s.MaThuongHieu == maTH & s.TinhTrang != "Không Hiển Thị").Count().ToString();
            ViewBag.a = from.ToString();
            ViewBag.b = to.ToString();
            ViewBag.listDanhMuc = db.DanhMucs.ToList();




            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult AddIntoCart(int maSanPham)
        {
            if (Session["kh"] != null)
            {
                KhachHang khachHang = Session["kh"] as KhachHang;
                if (Session["cart"] == null)
                {
                    Session["cart"] = new List<GioHang>();
                }
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                if (gioHangs.FirstOrDefault(m => m.MaSanPham == maSanPham) == null)
                {
                    SanPham SanPham = db.SanPhams.Find(maSanPham);
                    GioHang gioHang = new GioHang()
                    {
                        MaSanPham = maSanPham,
                        SoLuong = 1,
                        MaKH = khachHang.MaKH
                    };
                    gioHangs.Add(gioHang);
                }
                else
                {
                    GioHang gioHang = gioHangs.FirstOrDefault(m => m.MaSanPham == maSanPham);
                    gioHang.SoLuong++;
                }
            }
            else
            {
                if (Session["cart"] == null)
                {
                    Session["cart"] = new List<GioHang>();
                }
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                if (gioHangs.FirstOrDefault(m => m.MaSanPham == maSanPham) == null)
                {
                    SanPham SanPham = db.SanPhams.Find(maSanPham);
                    GioHang gioHang = new GioHang()
                    {
                        MaSanPham = maSanPham,
                        SoLuong = 1,
                        MaKH = 3
                    };
                    gioHangs.Add(gioHang);
                }
                else
                {
                    GioHang gioHang = gioHangs.FirstOrDefault(m => m.MaSanPham == maSanPham);
                    gioHang.SoLuong++;
                }
            }


            return RedirectToAction("Shop", "Home");
        }

        public ActionResult Shop(int? page, int? quantityBook)
        {
            if (Session["qty"] == null)
            {
                Session["qty"] = "0";
            }
            if (Session["lang"] == null)
            {
                Session["lang"] = "0".ToString();
            }
            int number = 0;
            if (page != null && quantityBook == null)
            {
                number = page.GetValueOrDefault() * 9;
            }
            else if (page != null && quantityBook != null)
            {
                int a = (int)quantityBook;
                number = page.GetValueOrDefault() * a;
            }
            if (quantityBook != null)
            {
                ViewBag.count = db.SanPhams.Where(s => s.TinhTrang != "Không Hiển Thị").Count() / quantityBook;
            }
            else
            {
                ViewBag.count = db.SanPhams.Where(s => s.TinhTrang != "Không Hiển Thị").Count() / 9;
            }
            if (quantityBook != null)
            {
                int a = (int)quantityBook;
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.TinhTrang != "Không Hiển Thị").Skip(number).Take(a).ToList();
                ViewBag.ds = SanPhams;
            }
            else
            {
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.TinhTrang != "Không Hiển Thị").Skip(number).Take(9).ToList();
                ViewBag.ds = SanPhams;
            }

            if (page == null)
            {
                page = 0;
            }
            ViewBag.quantityBook = quantityBook;
            if (ViewBag.quantityBook == null)
            {
                ViewBag.quantityBook = 9;
            }
            ViewBag.page = page;
            if (quantityBook == null)
            {
                quantityBook = 9;
            }
            int? from = page * quantityBook + 1;
            int? to = page * quantityBook + quantityBook;
            if (to > db.SanPhams.Where(s => s.TinhTrang != "Không Hiển Thị").Count())
            {
                to = db.SanPhams.Where(s => s.TinhTrang != "Không Hiển Thị").Count();
            }
            ViewBag.TongSanPham = db.SanPhams.Where(s => s.TinhTrang != "Không Hiển Thị").Count().ToString();
            ViewBag.a = from.ToString();
            ViewBag.b = to.ToString();
            ViewBag.listDanhMuc = db.DanhMucs.ToList();

            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult SearchByPrice(int price, int? page, int? quantityBook)
        {
            ViewBag.price = price;


            int number = 0;
            if (page != null && quantityBook == null)
            {
                number = page.GetValueOrDefault() * 9;
            }
            else if (page != null && quantityBook != null)
            {
                int a = (int)quantityBook;
                number = page.GetValueOrDefault() * a;
            }
            if (quantityBook != null)
            {
                ViewBag.count = db.SanPhams.Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").Count() / quantityBook;
            }
            else
            {
                ViewBag.count = db.SanPhams.Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").Count() / 9;
            }
            if (quantityBook != null)
            {
                int a = (int)quantityBook;
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").OrderByDescending(s => s.GiaBan).Skip(number).Take(a).ToList();
                ViewBag.ds = SanPhams;
            }
            else
            {
                var SanPhams = db.SanPhams.OrderBy(s => s.MaSanPham).Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").OrderByDescending(s => s.GiaBan).Skip(number).Take(9).ToList();
                ViewBag.ds = SanPhams;
            }

            if (page == null)
            {
                page = 0;
            }
            ViewBag.quantityBook = quantityBook;
            if (ViewBag.quantityBook == null)
            {
                ViewBag.quantityBook = 9;
            }
            ViewBag.page = page;
            if (quantityBook == null)
            {
                quantityBook = 9;
            }
            int? from = page * quantityBook + 1;
            int? to = page * quantityBook + quantityBook;
            if (to > db.SanPhams.Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").Count())
            {
                to = db.SanPhams.Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").Count();
            }
            ViewBag.TongSanPham = db.SanPhams.Where(s => s.GiaBan <= price & s.TinhTrang != "Không Hiển Thị").Count().ToString();
            ViewBag.a = from.ToString();
            ViewBag.b = to.ToString();
            ViewBag.listDanhMuc = db.DanhMucs.ToList();






            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }



        public ActionResult Vietnamese()
        {
            Session["lang"] = "1".ToString();
            return View();
        }



        public class Tmp4
        {
            public string Ngay { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
        }
        public ActionResult English()
        {
            Session["lang"] = "0".ToString();
            List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
            var result = from g in gioHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                         select new Tmp4
                         {
                             Ngay = Session["OrderTime"].ToString(),
                             TenSanPham = s.TenSanPham,
                             SoLuong = g.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * g.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham
                         };
            ViewData["data"] = result;
            return View();
        }

        public ActionResult Cart()
        {
            if (Session["kh"] != null)
            {
                List<GioHang> giohang = Session["cart"] as List<GioHang>;
                if (giohang == null)
                {
                    return View("ExceptionCartView");
                }
                else
                {
                    var result = from g in giohang
                                 join k in db.KhachHangs on g.MaKH equals k.MaKH
                                 join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                                 select new Tmp
                                 {
                                     TenKH = k.TenKH,
                                     Hinh = s.Hinh,
                                     TenSanPham = s.TenSanPham,
                                     SoLuong = g.SoLuong,
                                     DonGia = s.GiaBan,
                                     ThanhTien = s.GiaBan * g.SoLuong,
                                     MaSanPham = s.MaSanPham,
                                     MaKH = k.MaKH,
                                     SoLuongCon = s.SoLuong
                                 };
                    int total = 0;
                    foreach (var money in result)
                    {
                        total = total + money.ThanhTien;
                    }
                    Session["TotalMoney"] = total;
                    ViewBag.totalMoney = total;

                    int moneyTotal = total + 23000;

                    Session["MoneyTotal"] = moneyTotal;
                    ViewBag.moneyTotal = moneyTotal;

                    ViewData["data"] = result;

                    return View(result);
                }
            }
            else
            {
                List<GioHang> giohang = Session["cart"] as List<GioHang>;
                if (giohang == null)
                {
                    return View("ExceptionCartView");
                }
                else
                {
                    var result = from g in giohang
                                 join k in db.KhachHangs on g.MaKH equals k.MaKH
                                 join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                                 select new Tmp7
                                 {
                                     Hinh = s.Hinh,
                                     TenSanPham = s.TenSanPham,
                                     SoLuong = g.SoLuong,
                                     DonGia = s.GiaBan,
                                     ThanhTien = s.GiaBan * g.SoLuong,
                                     MaSanPham = s.MaSanPham,
                                     MaKH = 3,
                                     SoLuongCon = s.SoLuong
                                 };
                    int total = 0;
                    foreach (var money in result)
                    {
                        total = total + money.ThanhTien;
                    }
                    Session["TotalMoney"] = total;
                    ViewBag.totalMoney = total;
                    ViewData["data"] = result;


                    int moneyTotal = total + 23000;

                    Session["MoneyTotal"] = moneyTotal;
                    ViewBag.moneyTotal = moneyTotal;

                    return View(result);
                }
            }

        }

        public class Tmp7
        {
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public int SoLuongCon { get; set; }
        }

        public class Tmp
        {
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenKH { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public int SoLuongCon { get; set; }
        }
        public ActionResult ExceptionCartView()
        {
            return View();
        }
        public ActionResult UpdateQuantity(int maSanPham, int newQuantity)
        {
            List<GioHang> giohang = Session["cart"] as List<GioHang>;
            GioHang gioHangSua = giohang.FirstOrDefault(m => m.MaSanPham == maSanPham);
            if (gioHangSua != null)
            {
                gioHangSua.SoLuong = newQuantity;
            }
            return RedirectToAction("Cart", "Home");
        }
        public RedirectToRouteResult DeleteItemFromCart(int maSanPham)
        {
            List<GioHang> giohang = Session["cart"] as List<GioHang>;
            GioHang gioHangXoa = giohang.FirstOrDefault(m => m.MaSanPham == maSanPham);
            if (gioHangXoa != null)
            {
                giohang.Remove(gioHangXoa);
            }
            return RedirectToAction("Cart", "Home");
        }
        public RedirectToRouteResult DeleteCart()
        {
            List<GioHang> giohang = Session["cart"] as List<GioHang>;
            giohang.Clear();
            return RedirectToAction("Index", "Home");
        }

        public class Tmp8
        {
            public string Ngay { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
        }

        public ActionResult AddIntoDatabase()
        {
            Session["OrderTime"] = DateTime.Now.ToString();
            List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
            foreach (var item in gioHangs)
            {
                SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                if (item.SoLuong > sanPham.SoLuong)
                {
                    item.SoLuong = sanPham.SoLuong;
                }
            }
            if (Session["kh"] != null)
            {
                var result1 = from g in gioHangs
                              join k in db.KhachHangs on g.MaKH equals k.MaKH
                              join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                              select new Tmp1
                              {
                                  Ngay = Session["OrderTime"].ToString(),
                                  TenKH = k.TenKH,
                                  TenSanPham = s.TenSanPham,
                                  SoLuong = g.SoLuong,
                                  DonGia = s.GiaBan,
                                  ThanhTien = s.GiaBan * g.SoLuong,
                                  MaSanPham = s.MaSanPham,
                                  MaKH = k.MaKH
                              };
                ViewData["data1"] = result1;
                int total = 0;
                foreach (var money in result1)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;

                int moneyTotal = total + 23000;

                Session["MoneyTotal"] = moneyTotal;
                ViewBag.moneyTotal = moneyTotal;

            }
            else
            {
                var result1 = from g in gioHangs
                              join k in db.KhachHangs on g.MaKH equals k.MaKH
                              join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                              select new Tmp8
                              {
                                  Ngay = Session["OrderTime"].ToString(),
                                  TenSanPham = s.TenSanPham,
                                  SoLuong = g.SoLuong,
                                  DonGia = s.GiaBan,
                                  ThanhTien = s.GiaBan * g.SoLuong,
                                  MaSanPham = s.MaSanPham,
                                  MaKH = 3
                              };
                ViewData["data1"] = result1;
                int total = 0;
                foreach (var money in result1)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;

                int moneyTotal = total + 23000;

                Session["MoneyTotal"] = moneyTotal;
                ViewBag.moneyTotal = moneyTotal;

            }



            var result = from g in gioHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                         select new Tmp3
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = g.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * g.SoLuong,
                             MaSanPham = s.MaSanPham,
                             Hinh = s.Hinh
                         };
            ViewData["data"] = result;
            Session["qty"] = gioHangs.Count().ToString();

            return View(result);

        }
        [HttpPost]
        public ActionResult CheckOut(string shipName, string mobile, string address, string email)
        {
            List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
            var result = from g in gioHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                         select new Tmp4
                         {
                             Ngay = Session["OrderTime"].ToString(),
                             TenSanPham = s.TenSanPham,
                             SoLuong = g.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * g.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham
                         };

            if (Session["TotalMoney"] == null)
            {
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }

                if (Session["voucher"] == null)
                {
                    total += 23000;
                    Session["TotalMoney"] = total;
                }
                else
                {
                    Session["TotalMoney"] = total;
                }



            }
            ViewData["data"] = result;


            string tmp1 = Session["TotalMoney"].ToString();
            int tmp = int.Parse(tmp1);

            if (Session["kh"] == null)
            {
                ECommerce.Models.Order order = new ECommerce.Models.Order { MaKH = 3, TongTien = tmp, NgayDatHang = DateTime.Parse(Session["OrderTime"].ToString()), DiaChi = address, SDT = mobile, HoTen = shipName };
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var item in gioHangs)
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang { MaSanPham = item.MaSanPham, SoLuong = item.SoLuong, OrderID = order.OrderID };
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();

                    SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                    sanPham.SoLuong = sanPham.SoLuong - item.SoLuong;
                    db.SaveChanges();

                    if (sanPham.SoLuong == 0 & sanPham.TinhTrang != "Sản Phẩm Hot")
                    {

                        sanPham.TinhTrang = "Không Hiển Thị";
                        db.SaveChanges();

                    }
                }
            }
            else
            {
                string kh = Session["UserID"].ToString();
                int maKH = int.Parse(kh);
                ECommerce.Models.Order order = new ECommerce.Models.Order { MaKH = maKH, TongTien = tmp, NgayDatHang = DateTime.Parse(Session["OrderTime"].ToString()), DiaChi = address, SDT = mobile, HoTen = shipName };
                db.Orders.Add(order);
                db.SaveChanges();

                KhachHang khachHang = db.KhachHangs.Find(maKH);
                khachHang.DiemTichLuy += tmp / 1000;
                db.SaveChanges();

                if (khachHang.DiemTichLuy >= 1000000)
                {
                    khachHang.MaLoaiKH = 5;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 100000)
                {
                    khachHang.MaLoaiKH = 4;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 10000)
                {
                    khachHang.MaLoaiKH = 3;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 1000)
                {
                    khachHang.MaLoaiKH = 2;
                    db.SaveChanges();
                }
                else
                {
                    khachHang.MaLoaiKH = 1;
                    db.SaveChanges();
                }

                foreach (var item in gioHangs)
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang { MaSanPham = item.MaSanPham, SoLuong = item.SoLuong, OrderID = order.OrderID };
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();

                    SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                    sanPham.SoLuong = sanPham.SoLuong - item.SoLuong;
                    db.SaveChanges();

                    if (sanPham.SoLuong == 0 & sanPham.TinhTrang != "Sản Phẩm Hot")
                    {

                        sanPham.TinhTrang = "Không Hiển Thị";
                        db.SaveChanges();

                    }
                }
            }

            ViewBag.name = shipName;
            ViewBag.mobile = mobile;
            ViewBag.address = address;
            ViewBag.email = email;
            ViewBag.total = tmp;
            ViewBag.ds = gioHangs;
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Home/CheckOutMail.cshtml"));

            content = content.Replace("{{CustomerName}}", shipName);
            content = content.Replace("{{Phone}}", mobile);
            content = content.Replace("{{Email}}", email);
            content = content.Replace("{{Address}}", address);
            content = content.Replace("{{Total}}", tmp.ToString("N0"));
            var toEmail = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();

            new MailHelper().SendMail(email, "Đơn hàng mới từ TechPi", content);
            new MailHelper().SendMail(toEmail, "Đơn hàng mới từ TechPi", content);

            Session.Remove("cart");
            Session.Remove("TotalMoney");
            if (Session["voucher"] != null)
            {
                Session.Remove("voucher");
            }

            return View();
        }

        public ActionResult CheckOutMail()
        {
            return View();
        }

        public class Tmp1
        {
            public string Ngay { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenKH { get; set; }
            public string TenSanPham { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
        }
        public JsonResult GetSearchingData(string SearchValue)
        {
            List<SanPham> ProductList = new List<SanPham>();
            ProductList = db.SanPhams.Where(x => x.TenSanPham.StartsWith(SearchValue) || SearchValue == null).ToList();
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }

        public class Tmp6
        {
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public DateTime Ngay { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public int MaDonHang { get; set; }
            public int TongTien { get; set; }
        }

        public ActionResult MyOrder()
        {

            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());
            var result = from g in db.Orders
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join c in db.ChiTietGioHangs on g.OrderID equals c.OrderID
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         where k.MaKH == a
                         select new Tmp6
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * c.SoLuong,
                             Hinh = s.Hinh,
                             Ngay = g.NgayDatHang,
                             MaSanPham = s.MaSanPham,
                             MaKH = k.MaKH,
                             MaDonHang = g.OrderID,
                             TongTien = g.TongTien
                         };


            var result1 = from g in db.XacNhanDonHangs
                          join k in db.KhachHangs on g.MaKH equals k.MaKH
                          join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                          join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                          where k.MaKH == a
                          select new Tmp2
                          {
                              TenSanPham = s.TenSanPham,
                              SoLuong = c.SoLuong,
                              DonGia = s.GiaBan,
                              ThanhTien = s.GiaBan * c.SoLuong,
                              Hinh = s.Hinh,
                              MaSanPham = s.MaSanPham,
                              MaKH = k.MaKH,
                              MaDonHang = g.MaDonHang,
                              TinhTrang = g.TinhTrang,
                              NgayXacNhan = g.NgayXacNhan,
                              MaCTDH = c.MaChiTietDonHang,
                              TinhTrangMonHang = c.TinhTrang
                          };


            //var result2 = from g in db.HuyDonHangs
            //              join k in db.KhachHangs on g.MaKH equals k.MaKH
            //              join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
            //              where k.MaKH == a
            //              select new Tmp10
            //              {
            //                  TenSanPham = s.TenSanPham,
            //                  SoLuong = g.SoLuong,
            //                  DonGia = s.GiaBan,
            //                  ThanhTien = s.GiaBan * g.SoLuong,
            //                  Hinh = s.Hinh,
            //                  MaSanPham = s.MaSanPham,
            //                  MaKH = k.MaKH,
            //                  MaDonHang = g.MaDonHang,
            //                  TinhTrang = g.TinhTrang,
            //                  NgayXacNhan = g.NgayXacNhan
            //              };

            if (result == null & result1 == null )
            {
                ViewBag.message = "Không có sản phẩm nào trong đơn hàng của bạn";
            }
            ViewBag.ds1 = result1;
            ViewBag.ds = result;
            //ViewBag.ds2 = result2;
            return View();
        }

        public class Tmp2
        {
            public string TinhTrang { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public int MaDonHang { get; set; }
            public DateTime NgayXacNhan { get; set; }
            public int MaCTDH { get; set; }
            public string TinhTrangMonHang { get; set; }
        }
        public class Tmp12
        {
            public string TinhTrang { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public int MaDonHang { get; set; }
            public DateTime NgayXacNhan { get; set; }
            public int MaCTDH { get; set; }
            public string TinhTrangMonHang { get; set; }
        }
        public class Tmp10
        {
            public string TinhTrang { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public int MaKH { get; set; }
            public string MaDonHang { get; set; }
            public string NgayXacNhan { get; set; }
        }

        public ActionResult Cancel(int? maGH)
        {
            ECommerce.Models.Order order = db.Orders.FirstOrDefault(m => m.OrderID == maGH);

            if (order.MaKH != 3)
            {
                KhachHang khachHang = db.KhachHangs.Find(order.MaKH);
                khachHang.DiemTichLuy -= order.TongTien / 1000;
                db.SaveChanges();
                if (khachHang.DiemTichLuy >= 1000000)
                {
                    khachHang.MaLoaiKH = 5;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 100000)
                {
                    khachHang.MaLoaiKH = 4;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 10000)
                {
                    khachHang.MaLoaiKH = 3;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 1000)
                {
                    khachHang.MaLoaiKH = 2;
                    db.SaveChanges();
                }
                else
                {
                    khachHang.MaLoaiKH = 1;
                    db.SaveChanges();
                }
            }

            var chiTietGioHang = db.ChiTietGioHangs.Where(s => s.OrderID == maGH).ToList();
            foreach (var item in chiTietGioHang)
            {
                SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                sanPham.SoLuong = sanPham.SoLuong + item.SoLuong;
                sanPham.TinhTrang = "Hiển Thị";
                db.SaveChanges();


                HuyDonHang huyDonHang = new HuyDonHang();
                huyDonHang.MaDonHang = order.OrderID.ToString();
                huyDonHang.MaKH = order.MaKH;
                huyDonHang.TongTien = order.TongTien;
                huyDonHang.NgayXacNhan = DateTime.Parse(DateTime.Now.ToShortDateString());
                huyDonHang.TinhTrang = "Đã Huỷ Đơn";
                db.HuyDonHangs.Add(huyDonHang);
                db.SaveChanges();


                ChiTietDonHangHuy chiTietDonHangHuy = new ChiTietDonHangHuy();
                chiTietDonHangHuy.MaHuyDon = huyDonHang.MaHuyDon;
                chiTietDonHangHuy.MaSanPham = item.MaSanPham;
                chiTietDonHangHuy.SoLuong = item.SoLuong;
                chiTietDonHangHuy.TinhTrang = "Đã Huỷ Đơn";
                db.ChiTietDonHangHuys.Add(chiTietDonHangHuy);
                db.SaveChanges();


                ChiTietGioHang chiTietGioHangXoa = db.ChiTietGioHangs.FirstOrDefault(m => m.OrderID == maGH);
                db.ChiTietGioHangs.Remove(chiTietGioHangXoa);
                db.SaveChanges();

            }

            ECommerce.Models.Order deleteOrder = db.Orders.Find(maGH);
            db.Orders.Remove(deleteOrder);
            db.SaveChanges();


            return RedirectToAction("MyOrder", "Home");
        }



        public ActionResult AddReview(int? id, int? maCTDH)
        {
            ViewBag.maSP = id;
            ViewBag.maCTDH = maCTDH;
            if (Session["qty"] == null)
            {
                Session["qty"] = "0";
            }
            ViewBag.listDanhMuc = db.DanhMucs.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanPham = db.SanPhams.Where(s => s.MaSanPham == id).ToList();
            ViewBag.danhmuc = db.DanhMucs.Where(s => s.MaDanhMuc == sanPham.MaDanhMuc).ToList();
            ViewBag.thuongHieu = db.ThuongHieus.Where(s => s.MaThuongHieu == sanPham.MaThuongHieu).ToList();
            ViewBag.relateProduct = db.SanPhams.Where(s => s.MaDanhMuc == sanPham.MaDanhMuc).ToList();


            ChiTietTruyCap chiTietTruyCap = new ChiTietTruyCap();
            chiTietTruyCap.MaSanPham = id;
            if (Session["UserID"] != null)
            {
                string tmp = Session["UserID"].ToString();
                int a = int.Parse(tmp);
                chiTietTruyCap.MaKH = a;
            }
            else
            {
                chiTietTruyCap.MaKH = null;
            }

            chiTietTruyCap.NgayTruyCap = DateTime.Now.ToString();
            db.ChiTietTruyCaps.Add(chiTietTruyCap);
            db.SaveChanges();



            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }

        public ActionResult SubmitReview(string maSP, string maKH, string reviewContent, string maCTDH)
        {
            OrderConfirmedDetail orderConfirmedDetail = db.OrderConfirmedDetails.Find(int.Parse(maCTDH));
            orderConfirmedDetail.TinhTrang = "Đã Đánh Giá";


            Review review = new Review();
            review.MaKH = int.Parse(maKH);
            review.MaSanPham = int.Parse(maSP);
            review.NgayDang = DateTime.Now.ToShortDateString();
            review.NoiDung = reviewContent;
            db.Reviews.Add(review);
            db.SaveChanges();




            return RedirectToAction("Details", "SanPhams", new { id = maSP });
        }

        [HttpPost]
        public ActionResult ApplyVoucher(string maVoucher)
        {
            var kq = db.Vouchers.Where(s => s.TenVoucher == maVoucher).FirstOrDefault();
            Session["voucher"] = kq;
            return RedirectToAction("AddIntoDatabase", "Home");
        }


        public ActionResult DaXacNhan()
        {
            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());

            var result = from g in db.XacNhanDonHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         where k.MaKH == a & g.TinhTrang == "Đã Xác Nhận"
                         select new Tmp2
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * c.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham,
                             MaKH = k.MaKH,
                             MaDonHang = g.MaDonHang,
                             TinhTrang = g.TinhTrang,
                             NgayXacNhan = g.NgayXacNhan,
                             MaCTDH = c.MaChiTietDonHang,
                             TinhTrangMonHang = c.TinhTrang
                         };

            if (result == null)
            {
                ViewBag.message = "Không có sản phẩm nào trong khu vực này";
            }
            ViewBag.ds = result;
            var orders = db.XacNhanDonHangs.Where(s => s.MaKH == a & s.TinhTrang=="Đã Xác Nhận");
            return View(orders.ToList());
            //return View();
        }


        public ActionResult DangGiaoHang()
        {
            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());

            var result = from g in db.XacNhanDonHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         where k.MaKH == a & g.TinhTrang == "Đang Giao Hàng"
                         select new Tmp2
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * c.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham,
                             MaKH = k.MaKH,
                             MaDonHang = g.MaDonHang,
                             TinhTrang = g.TinhTrang,
                             NgayXacNhan = g.NgayXacNhan,
                             MaCTDH = c.MaChiTietDonHang,
                             TinhTrangMonHang = c.TinhTrang
                         };

            if (result == null)
            {
                ViewBag.message = "Không có sản phẩm nào trong khu vực này";
            }
            ViewBag.ds = result;
            var orders = db.XacNhanDonHangs.Where(s => s.MaKH == a & s.TinhTrang == "Đang Giao Hàng");
            return View(orders.ToList());
        }

        public ActionResult ChoXacNhan()
        {
            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());

            var result = from g in db.Orders
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join c in db.ChiTietGioHangs on g.OrderID equals c.OrderID
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         where k.MaKH == a
                         select new Tmp6
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * c.SoLuong,
                             Hinh = s.Hinh,
                             Ngay = g.NgayDatHang,
                             MaSanPham = s.MaSanPham,
                             MaKH = k.MaKH,
                             MaDonHang = g.OrderID
                         };

            if (result == null)
            {
                ViewBag.message = "Không có sản phẩm nào trong khu vực này";
            }
            ViewBag.ds = result;
            var orders = db.Orders.Where(s => s.MaKH==a);
            return View(orders.ToList());
            //return View();
        }

        public ActionResult DaHuyDon()
        {
            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());


            var orders = db.HuyDonHangs.Where(s => s.MaKH == a);
            return View(orders.ToList());
        }




        public ActionResult DaGiaoHang()
        {
            Session["tmp1"] = "2";
            if (Session["kh"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            int a = Convert.ToInt32(Session["UserID"].ToString());

            var result = from g in db.XacNhanDonHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                         join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                         where k.MaKH == a & g.TinhTrang == "Đã Giao Hàng"
                         select new Tmp2
                         {
                             TenSanPham = s.TenSanPham,
                             SoLuong = c.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * c.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham,
                             MaKH = k.MaKH,
                             MaDonHang = g.MaDonHang,
                             TinhTrang = g.TinhTrang,
                             NgayXacNhan = g.NgayXacNhan,
                             MaCTDH = c.MaChiTietDonHang,
                             TinhTrangMonHang = c.TinhTrang
                         };
            var result1 = from g in db.XacNhanDonHangs
                          join k in db.KhachHangs on g.MaKH equals k.MaKH
                          join c in db.OrderConfirmedDetails on g.MaDonHang equals c.MaDonHang
                          join s in db.SanPhams on c.MaSanPham equals s.MaSanPham
                          where k.MaKH == a & c.TinhTrang == "Đã Đánh Giá"
                          select new Tmp12
                          {
                              TenSanPham = s.TenSanPham,
                              SoLuong = c.SoLuong,
                              DonGia = s.GiaBan,
                              ThanhTien = s.GiaBan * c.SoLuong,
                              Hinh = s.Hinh,
                              MaSanPham = s.MaSanPham,
                              MaKH = k.MaKH,
                              MaDonHang = g.MaDonHang,
                              TinhTrang = g.TinhTrang,
                              NgayXacNhan = g.NgayXacNhan,
                              MaCTDH = c.MaChiTietDonHang,
                              TinhTrangMonHang = c.TinhTrang
                          };

            if (result == null)
            {
                ViewBag.message = "Không có sản phẩm nào trong khu vực này";
            }
            ViewBag.ds = result;
            ViewBag.ds1 = result1;
            return View();
        }



    }
}