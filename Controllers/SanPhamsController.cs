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
    public class SanPhamsController : Controller
    {
        private CSDLContext db = new CSDLContext();

        // GET: SanPhams
        public ActionResult Index(int? maDanhMuc, int? maThuongHieu, string productName)
        {

            ViewBag.activeDanhMuc = maDanhMuc;
            ViewBag.activeThuongHieu = maThuongHieu;

            ViewBag.danhMuc = db.DanhMucs.ToList();
            ViewBag.thuongHieu = db.ThuongHieus.ToList();

            if(maDanhMuc != null)
            {
                ViewBag.AlternativeThuongHieu = db.ThuongHieus.Where(s => s.MaDanhMuc == maDanhMuc);
            }



            Session["tmp"] = "4";
            

            for(int i = 1; i <= db.SanPhams.Count(); i++)
            {
                SanPham sanPham = db.SanPhams.Find(i);
                if(sanPham.SoLuong == 0 & sanPham.TinhTrang != "Sản Phẩm Hot")
                {
                    sanPham.TinhTrang = "Không Hiển Thị";
                }
            }

            if (productName != null)
            {
                var sanphams = db.SanPhams.Include(k => k.DanhMucs).Include(k => k.ThuongHieu).Where(x => x.TenSanPham.Contains(productName) || productName == null);
                return View(sanphams.ToList());
            }

            if (maDanhMuc != null & maThuongHieu == null)
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucs).Include(s => s.ThuongHieu).Where(s => s.MaDanhMuc == maDanhMuc);
                return View(sanPhams.ToList());
            }
            else if (maThuongHieu != null & maDanhMuc == null)
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucs).Include(s => s.ThuongHieu).Where(s => s.MaThuongHieu == maThuongHieu);
                return View(sanPhams.ToList());
            }
            else if (maDanhMuc != null & maThuongHieu != null)
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucs).Include(s => s.ThuongHieu).Where(s => s.MaDanhMuc == maDanhMuc & s.MaThuongHieu == maThuongHieu);
                return View(sanPhams.ToList());
            }
            else
            {
                var sanPhams = db.SanPhams.Include(s => s.DanhMucs).Include(s => s.ThuongHieu);
                return View(sanPhams.ToList());
            }

            
        }

        public class Tmp3
        {
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
            public string Hinh { get; set; }
        }

        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["qty"] == null)
            {
                Session["qty"] = "0";
            }
            ViewBag.listDanhMuc = db.DanhMucs.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.sanPham = db.SanPhams.Where(s => s.MaSanPham == id).ToList();
            ViewBag.danhmuc = db.DanhMucs.Where(s => s.MaDanhMuc == sanPham.MaDanhMuc).ToList();
            ViewBag.thuongHieu = db.ThuongHieus.Where(s => s.MaThuongHieu == sanPham.MaThuongHieu).ToList();
            ViewBag.relateProduct = db.SanPhams.Where(s => s.MaDanhMuc == sanPham.MaDanhMuc).ToList();
            ViewBag.review = db.Reviews.Where(s => s.MaSanPham == id).ToList();


            ChiTietTruyCap chiTietTruyCap = new ChiTietTruyCap();
            chiTietTruyCap.MaSanPham = id;

            TruyCap truyCap = db.TruyCaps.FirstOrDefault(s => s.MaSanPham == id);
            truyCap.SoLanTruyCap += 1;
            db.SaveChanges();

            if (Session["UserID"] != null)
            {
                string tmp = Session["UserID"].ToString();
                int a = int.Parse(tmp);
                chiTietTruyCap.MaKH = a;
            }
            else
            {
                chiTietTruyCap.MaKH = null;
            }

            chiTietTruyCap.NgayTruyCap = DateTime.Now.ToString();
            db.ChiTietTruyCaps.Add(chiTietTruyCap);
            db.SaveChanges();



            if (Session["cart"] != null)
            {
                List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
                var result = from g in gioHangs
                             join k in db.KhachHangs on g.MaKH equals k.MaKH
                             join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                             select new Tmp3
                             {
                                 TenSanPham = s.TenSanPham,
                                 SoLuong = g.SoLuong,
                                 DonGia = s.GiaBan,
                                 ThanhTien = s.GiaBan * g.SoLuong,
                                 MaSanPham = s.MaSanPham,
                                 Hinh = s.Hinh
                             };
                ViewData["data"] = result;
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }
                ViewBag.totalMoney = total;
                Session["qty"] = gioHangs.Count().ToString();

                return View(result);

            }
            else
            {
                return View();
            }
        }


        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc");
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieus, "MaThuongHieu", "TenThuongHieu");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MaSanPham,TenSanPham,GiaBan,MoTa,TomTat,Hinh,TinhTrang,SoLuong,MaDanhMuc,MaThuongHieu")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                var danhMuc = db.DanhMucs.Find(sanPham.MaDanhMuc);
                danhMuc.SoLuong += 1;
                db.SaveChanges();
                var thuongHieu = db.ThuongHieus.Find(sanPham.MaThuongHieu);
                thuongHieu.SoLuong += 1;
                db.SaveChanges();
                TruyCap truyCap = new TruyCap();
                truyCap.MaSanPham = sanPham.MaSanPham;
                truyCap.SoLanTruyCap = 0;
                db.TruyCaps.Add(truyCap);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieus, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieus, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MaSanPham,TenSanPham,GiaBan,MoTa,TomTat,Hinh,TinhTrang,SoLuong,MaDanhMuc,MaThuongHieu")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MaDanhMuc = new SelectList(db.DanhMucs, "MaDanhMuc", "TenDanhMuc", sanPham.MaDanhMuc);
            ViewBag.MaThuongHieu = new SelectList(db.ThuongHieus, "MaThuongHieu", "TenThuongHieu", sanPham.MaThuongHieu);
            return View(sanPham);
        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
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
