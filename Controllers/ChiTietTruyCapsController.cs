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
    public class ChiTietTruyCapsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: ChiTietTruyCaps
        public ActionResult Index()
        {
            Session["tmp"] = "16";
            var chiTietTruyCaps = db.ChiTietTruyCaps.Include(c => c.KhachHang).Include(c => c.SanPham);
            return View(chiTietTruyCaps.ToList());
        }

        // GET: ChiTietTruyCaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTruyCap chiTietTruyCap = db.ChiTietTruyCaps.Find(id);
            if (chiTietTruyCap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTruyCap);
        }

        // GET: ChiTietTruyCaps/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: ChiTietTruyCaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaChiTietTruyCap,MaKH,MaSanPham,NgayTruyCap")] ChiTietTruyCap chiTietTruyCap)
        {
            if (ModelState.IsValid)
            {
                db.ChiTietTruyCaps.Add(chiTietTruyCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", chiTietTruyCap.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietTruyCap.MaSanPham);
            return View(chiTietTruyCap);
        }

        // GET: ChiTietTruyCaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTruyCap chiTietTruyCap = db.ChiTietTruyCaps.Find(id);
            if (chiTietTruyCap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", chiTietTruyCap.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietTruyCap.MaSanPham);
            return View(chiTietTruyCap);
        }

        // POST: ChiTietTruyCaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaChiTietTruyCap,MaKH,MaSanPham,NgayTruyCap")] ChiTietTruyCap chiTietTruyCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(chiTietTruyCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", chiTietTruyCap.MaKH);
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", chiTietTruyCap.MaSanPham);
            return View(chiTietTruyCap);
        }

        // GET: ChiTietTruyCaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ChiTietTruyCap chiTietTruyCap = db.ChiTietTruyCaps.Find(id);
            if (chiTietTruyCap == null)
            {
                return HttpNotFound();
            }
            return View(chiTietTruyCap);
        }

        // POST: ChiTietTruyCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ChiTietTruyCap chiTietTruyCap = db.ChiTietTruyCaps.Find(id);
            db.ChiTietTruyCaps.Remove(chiTietTruyCap);
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
