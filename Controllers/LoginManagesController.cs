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
    public class LoginManagesController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: LoginManages
        public ActionResult Index()
        {
            Session["tmp"] = "8".ToString();
            return View(db.LoginManages.ToList());
        }

        // GET: LoginManages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginManage loginManage = db.LoginManages.Find(id);
            if (loginManage == null)
            {
                return HttpNotFound();
            }
            return View(loginManage);
        }

        // GET: LoginManages/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginManages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LoginManageKey,UserName,TimeLogin,TimeLogout")] LoginManage loginManage)
        {
            if (ModelState.IsValid)
            {
                db.LoginManages.Add(loginManage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(loginManage);
        }

        // GET: LoginManages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginManage loginManage = db.LoginManages.Find(id);
            if (loginManage == null)
            {
                return HttpNotFound();
            }
            return View(loginManage);
        }

        // POST: LoginManages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LoginManageKey,UserName,TimeLogin,TimeLogout")] LoginManage loginManage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loginManage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(loginManage);
        }

        // GET: LoginManages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LoginManage loginManage = db.LoginManages.Find(id);
            if (loginManage == null)
            {
                return HttpNotFound();
            }
            return View(loginManage);
        }

        // POST: LoginManages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LoginManage loginManage = db.LoginManages.Find(id);
            db.LoginManages.Remove(loginManage);
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
