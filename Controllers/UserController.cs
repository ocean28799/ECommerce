using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class UserController : Controller
    {
        private CSDLContext db = new CSDLContext();
        // GET: User
        public ActionResult UserInformation(int? id)
        {
            Session["tmp1"] = "1";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            KhachHang khachHang = db.KhachHangs.Find(id);

            Session["Role"] = khachHang.Quyen.TenQuyen.ToString();

            if (khachHang == null)
            {
                return HttpNotFound();
            }
            return View(khachHang);
        }

        // GET: KhachHangs/Edit/5
        public ActionResult EditUserInformation(int? id)
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
        public ActionResult EditUserInformation([Bind(Include = "MaKH,TenKH,DiaChi,SDT,DiemTichLuy,NgaySinh,UserName,Password,MaQuyen,MaLoaiKH")] KhachHang khachHang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(khachHang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("UserInformation","User");
            }
            ViewBag.MaLoaiKH = new SelectList(db.LoaiKhachHangs, "MaLoaiKH", "TenLoaiKH", khachHang.MaLoaiKH);
            ViewBag.MaQuyen = new SelectList(db.Quyens, "MaQuyen", "TenQuyen", khachHang.MaQuyen);
            return View(khachHang);
        }

    }
}