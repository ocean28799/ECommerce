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
    public class ThuongHieuxController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: ThuongHieux
        public ActionResult Index()
        {
            Session["tmp"] = "10";
            for (int i = 1; i <= db.ThuongHieus.LongCount(); i++)
            {
                int count = db.SanPhams.Where(s => s.MaThuongHieu == i).Count();
                ThuongHieu thuongHieu = db.ThuongHieus.Find(i);
                thuongHieu.SoLuong = count;
                db.SaveChanges();
            }
            var thuongHieus = db.ThuongHieus.Include(t => t.DanhMuc);
            return View(thuongHieus.ToList());
        }

        // GET: ThuongHieux/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieus.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // GET: ThuongHieux/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            return View();
        }

        // POST: ThuongHieux/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaThuongHieu,TenThuongHieu,SoLuong,MaDanhMuc")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.ThuongHieus.Add(thuongHieu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", thuongHieu.MaDanhMuc);
            return View(thuongHieu);
        }

        // GET: ThuongHieux/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieus.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", thuongHieu.MaDanhMuc);
            return View(thuongHieu);
        }

        // POST: ThuongHieux/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaThuongHieu,TenThuongHieu,SoLuong,MaDanhMuc")] ThuongHieu thuongHieu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(thuongHieu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", thuongHieu.MaDanhMuc);
            return View(thuongHieu);
        }

        // GET: ThuongHieux/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ThuongHieu thuongHieu = db.ThuongHieus.Find(id);
            if (thuongHieu == null)
            {
                return HttpNotFound();
            }
            return View(thuongHieu);
        }

        // POST: ThuongHieux/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ThuongHieu thuongHieu = db.ThuongHieus.Find(id);
            db.ThuongHieus.Remove(thuongHieu);
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
