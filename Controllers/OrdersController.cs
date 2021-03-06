using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;

namespace ECommerce.Controllers
{
    public class OrdersController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: Orders
        public ActionResult Index(string startDay, string endDay, string phoneNumber)
        {
            Session["tmp"] = "7";

            if (phoneNumber != null)
            {
                var xacNhanDonHangs = db.XacNhanDonHangs.Include(k => k.KhachHang).Include(k => k.Voucher).Where(x => x.SDT.StartsWith(phoneNumber) || phoneNumber == null);
                return View(xacNhanDonHangs.ToList());
            }

            if (startDay != null & endDay != null)
            {
                DateTime startDateAsString = DateTime.Parse(startDay);
                DateTime endDateAsString = DateTime.Parse(endDay);
                var orders = db.Orders.Include(o => o.KhachHang).Include(o => o.Voucher)
                    .Where(s => s.NgayDatHang.CompareTo(startDateAsString) >= 0 & s.NgayDatHang.CompareTo(endDateAsString) <= 0);
                return View(orders.ToList());
            }
            else if (startDay != null & endDay == null)
            {
                DateTime startDateAsString = DateTime.Parse(startDay);
                var orders = db.Orders.Include(o => o.KhachHang).Include(o => o.Voucher)
                   .Where(s => s.NgayDatHang.CompareTo(startDateAsString) >= 0 & s.NgayDatHang.CompareTo(DateTime.Now.ToString()) <= 0);
                return View(orders.ToList());
            }
            else
            {
                var orders = db.Orders.Include(o => o.KhachHang).Include(o => o.Voucher);
                return View(orders.ToList());
            }
            
        }

        public ActionResult Denied(int? maGH)
        {
            Order order = db.Orders.FirstOrDefault(m => m.OrderID == maGH);

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
                huyDonHang.TinhTrang = "Đã Từ Chối";
                db.HuyDonHangs.Add(huyDonHang);
                db.SaveChanges();

                ChiTietDonHangHuy chiTietDonHangHuy = new ChiTietDonHangHuy();
                chiTietDonHangHuy.MaHuyDon = huyDonHang.MaHuyDon;
                chiTietDonHangHuy.MaSanPham = item.MaSanPham;
                chiTietDonHangHuy.SoLuong = item.SoLuong;
                chiTietDonHangHuy.TinhTrang = "Đã Từ Chối";
                db.ChiTietDonHangHuys.Add(chiTietDonHangHuy);
                db.SaveChanges();

                ChiTietGioHang chiTietGioHangXoa = db.ChiTietGioHangs.FirstOrDefault(m => m.OrderID == maGH);
                db.ChiTietGioHangs.Remove(chiTietGioHangXoa);
                db.SaveChanges();


            }

            Order deleteOrder = db.Orders.Find(maGH);
            db.Orders.Remove(deleteOrder);
            db.SaveChanges();


            return RedirectToAction("Index");
        }



        public ActionResult Confirm(int? maGH)
        {
            Order order = db.Orders.FirstOrDefault(m => m.OrderID == maGH);
            XacNhanDonHang tmp = new XacNhanDonHang();
            tmp.MaKH = order.MaKH;
            tmp.VoucherID = order.VoucherID;
            tmp.TongTien = order.TongTien;
            tmp.TinhTrang = "Đã Xác Nhận";
            tmp.DiaChi = order.DiaChi;
            tmp.SDT = order.SDT;
            tmp.HoTen = order.HoTen;
            tmp.NgayXacNhan = DateTime.Now;
            db.XacNhanDonHangs.Add(tmp);
            db.SaveChanges();

            var chiTietGioHang = db.ChiTietGioHangs.Where(s => s.OrderID == maGH).ToList();
            foreach(var item in chiTietGioHang)
            {
                OrderConfirmedDetail orderConfirmedDetail =new OrderConfirmedDetail();
                orderConfirmedDetail.MaSanPham = item.MaSanPham;
                orderConfirmedDetail.SoLuong = item.SoLuong;
                orderConfirmedDetail.MaDonHang = tmp.MaDonHang;
                orderConfirmedDetail.TinhTrang = tmp.TinhTrang;
                orderConfirmedDetail.NgayXacNhan = DateTime.Now;
                db.OrderConfirmedDetails.Add(orderConfirmedDetail);
                db.SaveChanges();
                ChiTietGioHang chiTietGioHangXoa = db.ChiTietGioHangs.FirstOrDefault(m => m.OrderID == maGH);
                db.ChiTietGioHangs.Remove(chiTietGioHangXoa);
                db.SaveChanges();

            }



            Order deleteOrder = db.Orders.Find(maGH);
            db.Orders.Remove(deleteOrder);
            db.SaveChanges();



            return RedirectToAction("Index");
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,MaKH,VoucherID,TongTien,NgayDatHang,DiaChi,SDT,HoTen")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", order.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", order.VoucherID);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", order.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", order.VoucherID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,MaKH,VoucherID,TongTien,NgayDatHang,DiaChi,SDT,HoTen")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", order.MaKH);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", order.VoucherID);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
