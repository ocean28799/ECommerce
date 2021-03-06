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
    public class HuyDonHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: HuyDonHangs
        public ActionResult Index()
        {
            Session["tmp"] = "14";
            var huyDonHangs = db.HuyDonHangs.Include(h => h.KhachHang);
            return View(huyDonHangs.ToList());
        }

        // GET: HuyDonHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HuyDonHang huyDonHang = db.HuyDonHangs.Find(id);
            if (huyDonHang == null)
            {
                return HttpNotFound();
            }
            return View(huyDonHang);
        }

        // GET: HuyDonHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH");
            return View();
        }

        // POST: HuyDonHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaHuyDon,MaDonHang,MaKH,TongTien,NgayXacNhan,TinhTrang")] HuyDonHang huyDonHang)
        {
            if (ModelState.IsValid)
            {
                db.HuyDonHangs.Add(huyDonHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", huyDonHang.MaKH);
            return View(huyDonHang);
        }

        // GET: HuyDonHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HuyDonHang huyDonHang = db.HuyDonHangs.Find(id);
            if (huyDonHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", huyDonHang.MaKH);
            return View(huyDonHang);
        }

        // POST: HuyDonHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaHuyDon,MaDonHang,MaKH,TongTien,NgayXacNhan,TinhTrang")] HuyDonHang huyDonHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(huyDonHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaKH = new SelectList(db.KhachHangs, "MaKH", "TenKH", huyDonHang.MaKH);
            return View(huyDonHang);
        }

        // GET: HuyDonHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HuyDonHang huyDonHang = db.HuyDonHangs.Find(id);
            if (huyDonHang == null)
            {
                return HttpNotFound();
            }
            return View(huyDonHang);
        }

        // POST: HuyDonHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HuyDonHang huyDonHang = db.HuyDonHangs.Find(id);
            db.HuyDonHangs.Remove(huyDonHang);
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
