using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using WebMatrix.WebData;

namespace ECommerce.Controllers
{
    public class AuthenticationController : Controller
    {
        private CSDLContext db = new CSDLContext();
        // GET: Authentication
        public ActionResult Login()
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "0".ToString();
            }
            return View();
        }
        [HttpPost, OutputCache(Duration = 60 * 15)]
        public ActionResult DoLogin(string userName, string password)
        {
            KhachHang k = db.KhachHangs.Where(kh => kh.UserName == userName && kh.Password == password).FirstOrDefault();
            if (k != null)
            {
                System.Web.HttpContext.Current.Cache.Insert("KH", k, null, DateTime.MaxValue, TimeSpan.FromMinutes(15));
                Session["kh"] = k;
                if (Session["lang"] == null)
                {
                    Session["lang"] = "0".ToString();
                }

                Session["UserName"] = k.TenKH.ToString();
                Session["UserID"] = k.MaKH.ToString();
                Session["UserRole"] = k.MaQuyen.ToString();
                Session["UserAddress"] = k.DiaChi.ToString();
                Session["PhoneNumber"] = k.SDT.ToString();
                Session["TimeLogin"] = DateTime.Now.ToString();
                Session["user"] = k.UserName.ToString();
                Session["CustomerType"] = k.MaLoaiKH.ToString();
                Session["DiemTichLuy"] = k.DiemTichLuy.ToString();
                if(k.Email!= null)
                {
                    Session["Email"] = k.Email.ToString();
                }

                FormsAuthentication.SetAuthCookie(k.UserName, false);
                if (Session["UserRole"].ToString() == "1")
                {
                    return RedirectToAction("Admin","Authentication");
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else
            {
                ViewBag.message = "Invalid Username or Password";
                return View("Login");
            }


        }
        public ActionResult LogOut()
        {
            Session.Remove("kh");
            Session.Remove("UserName");
            FormsAuthentication.SignOut();
            Session["TimeLogout"] = DateTime.Now.ToString();
            LoginManage loginManage = new LoginManage();
            loginManage.UserName = Session["user"].ToString();
            loginManage.TimeLogin = Session["TimeLogin"].ToString();
            loginManage.TimeLogout = Session["TimeLogout"].ToString();
            db.LoginManages.Add(loginManage);
            db.SaveChanges();
            return RedirectToAction("Login", "Authentication");
        }
        public ActionResult Register()
        {
            if (Session["lang"] == null)
            {
                Session["lang"] = "0".ToString();
            }

            return View();
        }
        [HttpPost]
        public ActionResult AddIntoDatabase(string name, string address, string phoneNumber, string userName, string password, string ngaySinh,string email)
        {
            KhachHang k = db.KhachHangs.Where(kh => kh.UserName == userName).FirstOrDefault();
            if (k != null)
            {
                ViewBag.message = "Trùng tên đăng nhập";
                return View("Register");
            }

            KhachHang khachHang = new KhachHang { TenKH = name, DiaChi = address, SDT = phoneNumber, UserName = userName, Password = password, MaQuyen = 2, MaLoaiKH=1,DiemTichLuy=0,NgaySinh =ngaySinh,Email = email };
            db.KhachHangs.Add(khachHang);
            db.SaveChanges();
            return DoLogin(userName, password);

        }


        public ActionResult Admin()
        {

            

            var truyCap = db.TruyCaps.Include("SanPham").OrderByDescending(t => t.SoLanTruyCap).Take(10).ToList();
            ViewBag.truyCap = truyCap;
            ViewBag.product = db.SanPhams.Count().ToString();
            ViewBag.user = db.KhachHangs.Count().ToString();
            ViewBag.newOrder = db.Orders.LongCount().ToString();
            ViewBag.totalOrder = db.XacNhanDonHangs.LongCount().ToString();
            var newProduct = db.SanPhams.OrderByDescending(t => t.MaSanPham).Take(10).ToList();
            ViewBag.newProduct = newProduct;
            var bestSeller = (from item in db.OrderConfirmedDetails
                              group item.SoLuong by item.SanPham into g
                              orderby g.Sum() descending
                              select g.Key).Take(10).ToList();
            ViewBag.bestSeller = bestSeller;
            Session["tmp"] = "0".ToString();

            if (Session["UserRole"] == null)
            {
                return RedirectToAction("Login", "Authentication");
            }
            else if (Session["UserRole"].ToString() == "1")
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Authentication");
            }

        }

        public ActionResult ForgotPassword()
        {
            return View();

        }
        [HttpPost]
        public ActionResult DoForgotPassword(string email)
        {
            if (Session["ForgotPassword"] != null)
            {
                Session.Remove("ForgotPassword");
            }
            string content = System.IO.File.ReadAllText(Server.MapPath("~/Views/Authentication/DoForgotPasswordMail.cshtml"));
            KhachHang khachHang = db.KhachHangs.FirstOrDefault(s => s.Email == email);
            if (khachHang != null)
            {
                content = content.Replace("{{Password}}", khachHang.Password.ToString());
                content = content.Replace("{{Email}}", email);
                content = content.Replace("{{TenKH}}", khachHang.TenKH.ToString());
                content = content.Replace("{{UserName}}", khachHang.UserName.ToString());
                content = content.Replace("{{Address}}", khachHang.DiaChi.ToString());
                content = content.Replace("{{PhoneNumber}}", khachHang.SDT.ToString());
                content = content.Replace("{{DOB}}", khachHang.NgaySinh.ToString());
                new MailHelper().SendMail(email, "Quên Mật Khẩu | TechPi", content);
                return RedirectToAction("Login", "Authentication");
            }
            else
            {
                Session["ForgotPassword"] = "Không tìm thấy tài khoản phù hợp";
                return RedirectToAction("ForgotPassword","Authentication");
            }
            
        }

        //[HttpPost]
        //public ActionResult DoForgotPassword(string email)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        KhachHang khachHang = db.KhachHangs.FirstOrDefault(s => s.Email == email);
        //        if (khachHang != null)
        //        {
        //            string To = email, UserID, Password, SMTPPort, Host;
        //            string token = WebSecurity.GeneratePasswordResetToken(email);
        //            if (token == null)
        //            {
        //                // If user does not exist or is not confirmed.  

        //                return View("Index");

        //            }
        //            else
        //            {
        //                //Create URL with above token  

        //                var lnkHref = "<a href='" + Url.Action("ResetPassword", "Account", new { email = email, code = token }, "http") + "'>Reset Password</a>";


        //                //HTML Template for Send email  

        //                string subject = "Your changed password";

        //                string body = "<b>Please find the Password Reset Link. </b><br/>" + lnkHref;


        //                //Get and set the AppSettings using configuration manager.  

        //                EmailManager.AppSettings(out UserID, out Password, out SMTPPort, out Host);


        //                //Call send email methods.  

        //                EmailManager.SendEmail(UserID, subject, body, To, UserID, Password, SMTPPort, Host);

        //            }

        //        }

        //    }
        //    return View();
        //}

        //public ActionResult ResetPassword(string code, string email)
        //{
        //    ResetPasswordModel model = new ResetPasswordModel();
        //    model.ReturnToken = code;
        //    return View(model);
        //}
        //[HttpPost]


        //public ActionResult ResetPassword(ResetPasswordModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        bool resetResponse = WebSecurity.ResetPassword(model.ReturnToken, model.Password);
        //        if (resetResponse)
        //        {
        //            ViewBag.Message = "Successfully Changed";
        //        }
        //        else
        //        {
        //            ViewBag.Message = "Something went horribly wrong!";
        //        }
        //    }
        //    return View(model);
        //}

        public ActionResult LineChart()
        {
            return View();

        }
        public ActionResult BarChart()
        {
            var query = db.OrderConfirmeds.Include("KhachHang").Include("SanPham")
                   .GroupBy(p => p.SanPham.TenSanPham)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.SoLuong) }).ToList();

            var result = from s in query
                         select new Tmp
                         {
                             TenSanPham = s.name,
                             SoLuong = s.count
                         };
            ViewBag.ds = result;
            return View(result);

        }

        public JsonResult GetSearchingData(string SearchValue)
        {
            List<SanPham> ProductList = new List<SanPham>();
            ProductList = db.SanPhams.Where(x => x.TenSanPham.StartsWith(SearchValue) || SearchValue == null).ToList();
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }

        public class Tmp
        {
            public string TenSanPham { get; set; }
            public int SoLuong { get; set; }
        }
        public ActionResult AreaChart()
        {
            return View();

        }
        public ActionResult Chart()
        {
            return View();

        }
        public ActionResult GetData()
        {
            var query = db.OrderConfirmedDetails.Include("SanPham")
                   .GroupBy(p => p.SanPham.TenSanPham)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.SoLuong) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetAccessData()
        {
            var query = db.TruyCaps.Include("SanPham")
                   .GroupBy(p => p.SanPham.TenSanPham)
                   .Select(g => new { name = g.Key, count = g.Sum(w => w.SoLanTruyCap) }).Where(s => s.count != 0).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetAdjustData()
        {
            var query = db.OrderConfirmedDetails.Include("SanPham")
                .Where(s => s.NgayXacNhan >= DateTime.Parse(Session["startDay"].ToString()) & s.NgayXacNhan <= DateTime.Parse(Session["endDay"].ToString()))
                 .GroupBy(p => p.SanPham.TenSanPham)
                 .Select(g => new { name = g.Key, count = g.Sum(w => w.SoLuong) }).ToList();
            return Json(query, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ChartAdjust(string startDay,string endDay)
        {
            DateTime startDateAsString = DateTime.Parse(startDay);
            DateTime endDateAsString = DateTime.Parse(endDay);
            Session["startDay"] = startDateAsString;
            Session["endDay"] = endDateAsString;
            return View();
        }

    }
}