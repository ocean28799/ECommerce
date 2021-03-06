using ECommerce.Models;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class PaymentController : Controller
    {
        private CSDLContext db = new CSDLContext();

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Payment/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("FailureView");
                    }
                }
            }
            catch (Exception)
            {
                return View("FailureView");
            }
            //on successful payment, show success page to user.  
            return View("SuccessView");
        }

        public class Tmp4
        {
            public string Ngay { get; set; }
            public int DonGia { get; set; }
            public int SoLuong { get; set; }
            public string TenSanPham { get; set; }
            public string Hinh { get; set; }
            public int ThanhTien { get; set; }
            public int MaSanPham { get; set; }
        }

        public ActionResult SuccessView()
        {
            
            List<GioHang> gioHangs = Session["cart"] as List<GioHang>;
            var result = from g in gioHangs
                         join k in db.KhachHangs on g.MaKH equals k.MaKH
                         join s in db.SanPhams on g.MaSanPham equals s.MaSanPham
                         select new Tmp4
                         {
                             Ngay = Session["OrderTime"].ToString(),
                             TenSanPham = s.TenSanPham,
                             SoLuong = g.SoLuong,
                             DonGia = s.GiaBan,
                             ThanhTien = s.GiaBan * g.SoLuong,
                             Hinh = s.Hinh,
                             MaSanPham = s.MaSanPham
                         };

            if (Session["TotalMoney"] == null)
            {
                int total = 0;
                foreach (var money in result)
                {
                    total = total + money.ThanhTien;
                }

                if (Session["voucher"] == null)
                {
                    total += 23000;
                    Session["TotalMoney"] = total;
                }
                else
                {
                    Session["TotalMoney"] = total;
                }



            }
            ViewData["data"] = result;


            string tmp1 = Session["TotalMoney"].ToString();
            int tmp = int.Parse(tmp1);

            if (Session["kh"] == null)
            {
                ECommerce.Models.Order order = new ECommerce.Models.Order { MaKH = 3, TongTien = tmp, NgayDatHang = DateTime.Parse(Session["OrderTime"].ToString()), DiaChi = "PayPal", SDT = "PayPal", HoTen = "PayPal" };
                db.Orders.Add(order);
                db.SaveChanges();
                foreach (var item in gioHangs)
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang { MaSanPham = item.MaSanPham, SoLuong = item.SoLuong, OrderID = order.OrderID };
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();

                    SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                    sanPham.SoLuong = sanPham.SoLuong - item.SoLuong;
                    db.SaveChanges();

                    if (sanPham.SoLuong == 0 & sanPham.TinhTrang != "Sản Phẩm Hot")
                    {

                        sanPham.TinhTrang = "Không Hiển Thị";
                        db.SaveChanges();

                    }
                }
            }
            else
            {
                string kh = Session["UserID"].ToString();
                int maKH = int.Parse(kh);
                ECommerce.Models.Order order = new ECommerce.Models.Order { MaKH = maKH, TongTien = tmp, NgayDatHang = DateTime.Parse(Session["OrderTime"].ToString()), DiaChi = "PayPal", SDT = "PayPal", HoTen = "Payment with PayPal" };
                db.Orders.Add(order);
                db.SaveChanges();

                KhachHang khachHang = db.KhachHangs.Find(maKH);
                khachHang.DiemTichLuy += tmp / 1000;
                db.SaveChanges();

                if (khachHang.DiemTichLuy >= 1000000)
                {
                    khachHang.MaLoaiKH = 5;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 100000)
                {
                    khachHang.MaLoaiKH = 4;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 10000)
                {
                    khachHang.MaLoaiKH = 3;
                    db.SaveChanges();
                }
                else if (khachHang.DiemTichLuy >= 1000)
                {
                    khachHang.MaLoaiKH = 2;
                    db.SaveChanges();
                }
                else
                {
                    khachHang.MaLoaiKH = 1;
                    db.SaveChanges();
                }

                foreach (var item in gioHangs)
                {
                    ChiTietGioHang chiTietGioHang = new ChiTietGioHang { MaSanPham = item.MaSanPham, SoLuong = item.SoLuong, OrderID = order.OrderID };
                    db.ChiTietGioHangs.Add(chiTietGioHang);
                    db.SaveChanges();

                    SanPham sanPham = db.SanPhams.Find(item.MaSanPham);
                    sanPham.SoLuong = sanPham.SoLuong - item.SoLuong;
                    db.SaveChanges();

                    if (sanPham.SoLuong == 0 & sanPham.TinhTrang != "Sản Phẩm Hot")
                    {

                        sanPham.TinhTrang = "Không Hiển Thị";
                        db.SaveChanges();

                    }
                }
            }
            ViewBag.total = tmp;
            ViewBag.ds = gioHangs;

            Session.Remove("cart");
            Session.Remove("TotalMoney");
            if (Session["voucher"] != null)
            {
                Session.Remove("voucher");
            }
            return View();
        }

        public ActionResult FailureView()
        {
            return View();
        }
        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }
        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {

            //create itemlist and add item objects to it  
            var itemList = new ItemList()
            {
                items = new List<Item>()
            };
            //Adding Item Details like name, currency, price etc  



            itemList.items.Add(new Item()
            {
                name = "iPhone 12 Pro Max 253GB",
                currency = "USD",
                price = "1743",
                quantity = "1",
                sku = "sku"
            });


            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = "1",
                shipping = "1",
                subtotal = "1743"
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = "1745", // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Transaction description",
                invoice_number = "your generated invoice number", //Generate an Invoice No  
                amount = amount,
                item_list = itemList
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }


    }
}