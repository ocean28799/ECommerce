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
    public class OrderConfirmedsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: OrderConfirmeds
        public ActionResult Index()
        {
            var orderConfirmeds = db.OrderConfirmeds.Include(o => o.KhachHang).Include(o => o.SanPham).Include(o => o.Voucher);
            return View(orderConfirmeds.ToList());
        }

        // GET: OrderConfirmeds/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmed orderConfirmed = db.OrderConfirmeds.Find(id);
            if (orderConfirmed == null)
            {
                return HttpNotFound();
            }
            return View(orderConfirmed);
        }

        // GET: OrderConfirmeds/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher");
            return View();
        }

        // POST: OrderConfirmeds/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaDonHang,MaKH,MaSanPham,VoucherID,TongTien,NgayDatHang,SoLuong,TinhTrang")] OrderConfirmed orderConfirmed)
        {
            if (ModelState.IsValid)
            {
                db.OrderConfirmeds.Add(orderConfirmed);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", orderConfirmed.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmed.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", orderConfirmed.VoucherID);
            return View(orderConfirmed);
        }

        // GET: OrderConfirmeds/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmed orderConfirmed = db.OrderConfirmeds.Find(id);
            if (orderConfirmed == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", orderConfirmed.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmed.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", orderConfirmed.VoucherID);
            return View(orderConfirmed);
        }

        // POST: OrderConfirmeds/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaDonHang,MaKH,MaSanPham,VoucherID,TongTien,NgayDatHang,SoLuong,TinhTrang")] OrderConfirmed orderConfirmed)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderConfirmed).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", orderConfirmed.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmed.MaSanPham);
            ViewBag.VoucherID = new SelectList(db.Vouchers, "VoucherID", "TenVoucher", orderConfirmed.VoucherID);
            return View(orderConfirmed);
        }

        // GET: OrderConfirmeds/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmed orderConfirmed = db.OrderConfirmeds.Find(id);
            if (orderConfirmed == null)
            {
                return HttpNotFound();
            }
            return View(orderConfirmed);
        }

        // POST: OrderConfirmeds/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderConfirmed orderConfirmed = db.OrderConfirmeds.Find(id);
            db.OrderConfirmeds.Remove(orderConfirmed);
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
