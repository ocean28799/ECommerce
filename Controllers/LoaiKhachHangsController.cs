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
    public class LoaiKhachHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: LoaiKhachHangs
        public ActionResult Index()
        {
            Session["tmp"] = "17";
            return View(db.LoaiKhachHangs.ToList());
        }

        // GET: LoaiKhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKhachHang loaiKhachHang = db.LoaiKhachHangs.Find(id);
            if (loaiKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiKhachHang);
        }

        // GET: LoaiKhachHangs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoaiKhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaLoaiKH,TenLoaiKH,GhiChu")] LoaiKhachHang loaiKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.LoaiKhachHangs.Add(loaiKhachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loaiKhachHang);
        }

        // GET: LoaiKhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKhachHang loaiKhachHang = db.LoaiKhachHangs.Find(id);
            if (loaiKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiKhachHang);
        }

        // POST: LoaiKhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaLoaiKH,TenLoaiKH,GhiChu")] LoaiKhachHang loaiKhachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loaiKhachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loaiKhachHang);
        }

        // GET: LoaiKhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoaiKhachHang loaiKhachHang = db.LoaiKhachHangs.Find(id);
            if (loaiKhachHang == null)
            {
                return HttpNotFound();
            }
            return View(loaiKhachHang);
        }

        // POST: LoaiKhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoaiKhachHang loaiKhachHang = db.LoaiKhachHangs.Find(id);
            db.LoaiKhachHangs.Remove(loaiKhachHang);
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
