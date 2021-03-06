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
    public class TruyCapsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: TruyCaps
        public ActionResult Index()
        {
            Session["tmp"] = "15";

            //for (int i = 1; i <= db.SanPhams.LongCount(); i++)
            //{
            //    TruyCap tmp = new TruyCap();
            //    tmp.MaSanPham = i;
            //    tmp.SoLanTruyCap = 0;
            //    db.TruyCaps.Add(tmp);
            //    db.SaveChanges();
            //}
            var truyCaps = db.TruyCaps.Include(t => t.SanPham).OrderByDescending(t => t.SoLanTruyCap);
            return View(truyCaps.ToList());
        }

        // GET: TruyCaps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruyCap truyCap = db.TruyCaps.Find(id);
            if (truyCap == null)
            {
                return HttpNotFound();
            }
            return View(truyCap);
        }

        // GET: TruyCaps/Create
        public ActionResult Create()
        {
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham");
            return View();
        }

        // POST: TruyCaps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaTruyCap,SoLanTruyCap,MaSanPham")] TruyCap truyCap)
        {
            if (ModelState.IsValid)
            {
                db.TruyCaps.Add(truyCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", truyCap.MaSanPham);
            return View(truyCap);
        }

        // GET: TruyCaps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruyCap truyCap = db.TruyCaps.Find(id);
            if (truyCap == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", truyCap.MaSanPham);
            return View(truyCap);
        }

        // POST: TruyCaps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaTruyCap,SoLanTruyCap,MaSanPham")] TruyCap truyCap)
        {
            if (ModelState.IsValid)
            {
                db.Entry(truyCap).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaSanPham = new SelectList(db.SanPhams, "MaSanPham", "TenSanPham", truyCap.MaSanPham);
            return View(truyCap);
        }

        // GET: TruyCaps/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TruyCap truyCap = db.TruyCaps.Find(id);
            if (truyCap == null)
            {
                return HttpNotFound();
            }
            return View(truyCap);
        }

        // POST: TruyCaps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TruyCap truyCap = db.TruyCaps.Find(id);
            db.TruyCaps.Remove(truyCap);
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
