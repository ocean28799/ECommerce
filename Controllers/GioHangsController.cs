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
    public class GioHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: GioHangs
        public ActionResult Index()
        {
            Session["tmp"] = "7".ToString();
            var gioHangs = db.GioHangs.Include(g => g.KhachHang).Include(g => g.SanPham).Include(g => g.Voucher);
            return View(gioHangs.ToList());
        }

        public ActionResult Confirm(int? maGH)
        {
            GioHang gioHang = db.GioHangs.FirstOrDefault(m => m.MaGioHang == maGH);
            OrderConfirmed tmp1 = new OrderConfirmed();
            tmp1.MaKH = gioHang.MaKH;
            tmp1.MaSanPham = gioHang.MaSanPham;
            tmp1.SoLuong = gioHang.SoLuong;
            tmp1.TongTien = gioHang.TongTien;
            tmp1.TinhTrang = "Đã Xác Nhận";
            tmp1.VoucherID = gioHang.VoucherID;
            tmp1.NgayDatHang = DateTime.Now.ToShortDateString();
            db.OrderConfirmeds.Add(tmp1);
            db.SaveChanges();



            GioHang gioHangXoa = db.GioHangs.Find(maGH);
            db.GioHangs.Remove(gioHangXoa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: GioHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // GET: GioHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher");
            return View();
        }

        // POST: GioHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaGioHang,MaKH,MaSanPham,VoucherID,SoLuong,TongTien,NgayDatHang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.GioHangs.Add(gioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", gioHang.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", gioHang.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", gioHang.VoucherID);
            return View(gioHang);
        }

        // GET: GioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", gioHang.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", gioHang.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", gioHang.VoucherID);
            return View(gioHang);
        }

        // POST: GioHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaGioHang,MaKH,MaSanPham,VoucherID,SoLuong,TongTien,NgayDatHang")] GioHang gioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", gioHang.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", gioHang.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", gioHang.VoucherID);
            return View(gioHang);
        }

        // GET: GioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GioHang gioHang = db.GioHangs.Find(id);
            if (gioHang == null)
            {
                return HttpNotFound();
            }
            return View(gioHang);
        }

        // POST: GioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GioHang gioHang = db.GioHangs.Find(id);
            db.GioHangs.Remove(gioHang);
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
