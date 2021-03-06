using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ECommerce.Models;
using PayPal.Api;

namespace ECommerce.Controllers
{
    public class KhachHangsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: KhachHangs
        public ActionResult Index(int? loaiKH, int? loaiTK, string phoneNumber)
        {
            ViewBag.activeLoaiKH = loaiKH;
            ViewBag.activeLoaiTK = loaiTK;

            

            

            ViewBag.loaiKH = db.LoaiKhachHangs.ToList();
            ViewBag.loaiTK = db.Quyens.ToList();
            Session["tmp"] = "2";
            for (int i = 1; i <= db.KhachHangs.LongCount(); i++)
            {
                var tmp = db.KhachHangs.FirstOrDefault(s => s.MaKH == i);
                if (tmp != null)
                {
                    if (tmp.DiemTichLuy >= 1000000)
                    {
                        tmp.MaLoaiKH = 5;
                        db.SaveChanges();
                    }
                    else if (tmp.DiemTichLuy >= 100000)
                    {
                        tmp.MaLoaiKH = 4;
                        db.SaveChanges();
                    }
                    else if (tmp.DiemTichLuy >= 10000)
                    {
                        tmp.MaLoaiKH = 3;
                        db.SaveChanges();
                    }
                    else if (tmp.DiemTichLuy >= 1000)
                    {
                        tmp.MaLoaiKH = 2;
                        db.SaveChanges();
                    }
                    else
                    {
                        tmp.MaLoaiKH = 1;
                        db.SaveChanges();
                    }
                }
                else
                {
                    continue;
                }




            }

            if (phoneNumber != null)
            {
                var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.Quyen).Where(x => x.SDT.StartsWith(phoneNumber) || phoneNumber == null);
                return View(khachHangs.ToList());
            }

            if (loaiKH != null & loaiTK == null)
            {
                var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.Quyen).Where(s => s.MaLoaiKH == loaiKH);
                return View(khachHangs.ToList());
            }
            else if(loaiTK != null & loaiKH == null)
            {
                var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.Quyen).Where(s => s.MaQuyen == loaiTK);
                return View(khachHangs.ToList());
            }
            else if(loaiKH != null & loaiTK != null)
            {
                var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.Quyen).Where(s => s.MaQuyen == loaiTK & s.MaLoaiKH ==loaiKH);
                return View(khachHangs.ToList());
            }
            else
            {
                var khachHangs = db.KhachHangs.Include(k => k.LoaiKhachHang).Include(k => k.Quyen);
                return View(khachHangs.ToList());
            }



           
        }

      

        // GET: KhachHangs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Create
        public ActionResult Create()
        {
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoaiKH", "TenLoaiKH");
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen");
            return View();
        }

        // POST: KhachHangs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaKH,TenKH,DiaChi,SDT,DiemTichLuy,Email,NgaySinh,UserName,Password,MaQuyen,MaLoaiKH")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.KhachHangs.Add(khachHang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoaiKH", "TenLoaiKH", khachHang.MaLoaiKH);
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachHang.MaQuyen);
            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoaiKH", "TenLoaiKH", khachHang.MaLoaiKH);
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachHang.MaQuyen);
            return View(khachHang);
        }

        // POST: KhachHangs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaKH,TenKH,DiaChi,SDT,DiemTichLuy,Email,NgaySinh,UserName,Password,MaQuyen,MaLoaiKH")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoaiKH", "TenLoaiKH", khachHang.MaLoaiKH);
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachHang.MaQuyen);
            return View(khachHang);
        }

        // GET: KhachHangs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);
            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // POST: KhachHangs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            KhachHang khachHang = db.KhachHangs.Find(id);
            db.KhachHangs.Remove(khachHang);
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
