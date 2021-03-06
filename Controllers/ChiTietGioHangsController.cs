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
    public class ChiTietGioHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: ChiTietGioHangs
        public ActionResult Index()
        {
            Session["tmp"] = "11";
            var chiTietGioHangs = db.ChiTietGioHangs.Include(c => c.Order).Include(c => c.SanPham);
            return View(chiTietGioHangs.ToList());
        }

        // GET: ChiTietGioHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Create
        public ActionResult Create()
        {
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: ChiTietGioHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietGioHang,MaSanPham,OrderID,SoLuong")] ChiTietGioHang chiTietGioHang)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietGioHangs.Add(chiTietGioHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", chiTietGioHang.OrderID);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietGioHang.MaSanPham);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", chiTietGioHang.OrderID);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietGioHang.MaSanPham);
            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietGioHang,MaSanPham,OrderID,SoLuong")] ChiTietGioHang chiTietGioHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietGioHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "OrderID", chiTietGioHang.OrderID);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietGioHang.MaSanPham);
            return View(chiTietGioHang);
        }

        // GET: ChiTietGioHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            if (chiTietGioHang == null)
            {
                return HttpNotFound();
            }
            return View(chiTietGioHang);
        }

        // POST: ChiTietGioHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietGioHang chiTietGioHang = db.ChiTietGioHangs.Find(id);
            db.ChiTietGioHangs.Remove(chiTietGioHang);
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
