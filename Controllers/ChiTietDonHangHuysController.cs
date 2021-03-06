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
    public class ChiTietDonHangHuysController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: ChiTietDonHangHuys
        public ActionResult Index()
        {
            var chiTietDonHangHuys = db.ChiTietDonHangHuys.Include(c => c.HuyDonHang).Include(c => c.SanPham);
            return View(chiTietDonHangHuys.ToList());
        }

        // GET: ChiTietDonHangHuys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangHuy chiTietDonHangHuy = db.ChiTietDonHangHuys.Find(id);
            if (chiTietDonHangHuy == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHangHuy);
        }

        // GET: ChiTietDonHangHuys/Create
        public ActionResult Create()
        {
            ViewBag.MaHuyDon = new SelectList(db.HuyDonHangs, "MaHuyDon", "MaDonHang");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: ChiTietDonHangHuys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietDonHangHuy,MaSanPham,MaHuyDon,SoLuong,TinhTrang")] ChiTietDonHangHuy chiTietDonHangHuy)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietDonHangHuys.Add(chiTietDonHangHuy);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaHuyDon = new SelectList(db.HuyDonHangs, "MaHuyDon", "MaDonHang", chiTietDonHangHuy.MaHuyDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDonHangHuy.MaSanPham);
            return View(chiTietDonHangHuy);
        }

        // GET: ChiTietDonHangHuys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangHuy chiTietDonHangHuy = db.ChiTietDonHangHuys.Find(id);
            if (chiTietDonHangHuy == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaHuyDon = new SelectList(db.HuyDonHangs, "MaHuyDon", "MaDonHang", chiTietDonHangHuy.MaHuyDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDonHangHuy.MaSanPham);
            return View(chiTietDonHangHuy);
        }

        // POST: ChiTietDonHangHuys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietDonHangHuy,MaSanPham,MaHuyDon,SoLuong,TinhTrang")] ChiTietDonHangHuy chiTietDonHangHuy)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietDonHangHuy).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaHuyDon = new SelectList(db.HuyDonHangs, "MaHuyDon", "MaDonHang", chiTietDonHangHuy.MaHuyDon);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietDonHangHuy.MaSanPham);
            return View(chiTietDonHangHuy);
        }

        // GET: ChiTietDonHangHuys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietDonHangHuy chiTietDonHangHuy = db.ChiTietDonHangHuys.Find(id);
            if (chiTietDonHangHuy == null)
            {
                return HttpNotFound();
            }
            return View(chiTietDonHangHuy);
        }

        // POST: ChiTietDonHangHuys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietDonHangHuy chiTietDonHangHuy = db.ChiTietDonHangHuys.Find(id);
            db.ChiTietDonHangHuys.Remove(chiTietDonHangHuy);
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
