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
    public class OrderConfirmedDetailsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: OrderConfirmedDetails
        public ActionResult Index()
        {
            Session["tmp"] = "12";
            for(int i = 1; i <= db.OrderConfirmedDetails.LongCount(); i++)
            {
                OrderConfirmedDetail orderConfirmedDetail = new OrderConfirmedDetail();
                orderConfirmedDetail = db.OrderConfirmedDetails.Find(i);
                orderConfirmedDetail.NgayXacNhan = orderConfirmedDetail.XacNhanDonHang.NgayXacNhan;
                db.SaveChanges();
            }
            var orderConfirmedDetails = db.OrderConfirmedDetails.Include(o => o.SanPham).Include(o => o.XacNhanDonHang);
            return View(orderConfirmedDetails.ToList());
        }

        // GET: OrderConfirmedDetails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmedDetail orderConfirmedDetail = db.OrderConfirmedDetails.Find(id);
            if (orderConfirmedDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderConfirmedDetail);
        }

        // GET: OrderConfirmedDetails/Create
        public ActionResult Create()
        {
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            ViewBag.MaDonHang = new SelectList(db.XacNhanDonHangs, "MaDonHang", "TinhTrang");
            return View();
        }

        // POST: OrderConfirmedDetails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietDonHang,MaSanPham,MaDonHang,SoLuong,TinhTrang")] OrderConfirmedDetail orderConfirmedDetail)
        {
            if (ModelState.IsValid)
            {
                db.OrderConfirmedDetails.Add(orderConfirmedDetail);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmedDetail.MaSanPham);
            ViewBag.MaDonHang = new SelectList(db.XacNhanDonHangs, "MaDonHang", "TinhTrang", orderConfirmedDetail.MaDonHang);
            return View(orderConfirmedDetail);
        }

        // GET: OrderConfirmedDetails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmedDetail orderConfirmedDetail = db.OrderConfirmedDetails.Find(id);
            if (orderConfirmedDetail == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmedDetail.MaSanPham);
            ViewBag.MaDonHang = new SelectList(db.XacNhanDonHangs, "MaDonHang", "TinhTrang", orderConfirmedDetail.MaDonHang);
            return View(orderConfirmedDetail);
        }

        // POST: OrderConfirmedDetails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietDonHang,MaSanPham,MaDonHang,SoLuong,TinhTrang")] OrderConfirmedDetail orderConfirmedDetail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderConfirmedDetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", orderConfirmedDetail.MaSanPham);
            ViewBag.MaDonHang = new SelectList(db.XacNhanDonHangs, "MaDonHang", "TinhTrang", orderConfirmedDetail.MaDonHang);
            return View(orderConfirmedDetail);
        }

        // GET: OrderConfirmedDetails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderConfirmedDetail orderConfirmedDetail = db.OrderConfirmedDetails.Find(id);
            if (orderConfirmedDetail == null)
            {
                return HttpNotFound();
            }
            return View(orderConfirmedDetail);
        }

        // POST: OrderConfirmedDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderConfirmedDetail orderConfirmedDetail = db.OrderConfirmedDetails.Find(id);
            db.OrderConfirmedDetails.Remove(orderConfirmedDetail);
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
