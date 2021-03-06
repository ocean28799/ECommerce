using ECommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ECommerce.Controllers
{
    public class AddController : Controller
    {
        private CSDLContext db = new CSDLContext();
        // GET: Add
        public ActionResult Index()
        {
            Quyen quyen1 = new Quyen();
            quyen1.TenQuyen = "Quản Trị Hệ Thống";
            Quyen quyen2 = new Quyen();
            quyen2.TenQuyen = "Khách Hàng";
            db.Quyens.Add(quyen1);
            db.SaveChanges();
            db.Quyens.Add(quyen2);
            db.SaveChanges();



            LoaiKhachHang loaiKhachHang1 = new LoaiKhachHang();
            loaiKhachHang1.TenLoaiKH = "Khách Hàng Thân Thiết";
            db.LoaiKhachHangs.Add(loaiKhachHang1);
            db.SaveChanges();

            LoaiKhachHang loaiKhachHang2 = new LoaiKhachHang();
            loaiKhachHang2.TenLoaiKH = "Khách Hàng Thẻ Bạc";
            db.LoaiKhachHangs.Add(loaiKhachHang2);
            db.SaveChanges();

            LoaiKhachHang loaiKhachHang3 = new LoaiKhachHang();
            loaiKhachHang3.TenLoaiKH = "Khách Hàng Thẻ Vàng";
            db.LoaiKhachHangs.Add(loaiKhachHang3);
            db.SaveChanges();

            LoaiKhachHang loaiKhachHang4 = new LoaiKhachHang();
            loaiKhachHang4.TenLoaiKH = "Khách Hàng Thẻ Bạch Kim";
            db.LoaiKhachHangs.Add(loaiKhachHang4);
            db.SaveChanges();

            LoaiKhachHang loaiKhachHang5 = new LoaiKhachHang();
            loaiKhachHang5.TenLoaiKH = "V.I.P";
            db.LoaiKhachHangs.Add(loaiKhachHang5);
            db.SaveChanges();

            KhachHang khachHang1 = new KhachHang();
            khachHang1.TenKH = "Trần Anh Đức";
            khachHang1.NgaySinh = "1999-07-28";
            khachHang1.Email = "ocean28799@gmail.com";
            khachHang1.SDT = "0933131760";
            khachHang1.MaQuyen = 1;
            khachHang1.DiaChi = "SaiGon";
            khachHang1.UserName = "admin";
            khachHang1.Password = "admin";
            khachHang1.MaLoaiKH = 5;
            db.KhachHangs.Add(khachHang1);
            db.SaveChanges();

            KhachHang khachHang2 = new KhachHang();
            khachHang2.TenKH = "Trần Anh Đức";
            khachHang2.SDT = "0933131760";
            khachHang2.Email = "ocean28799@gmail.com";
            khachHang2.NgaySinh = "1999-07-28";
            khachHang2.MaQuyen = 2;
            khachHang2.DiaChi = "SaiGon";
            khachHang2.UserName = "user";
            khachHang2.Password = "user";
            khachHang2.DiemTichLuy = 0;
            khachHang2.MaLoaiKH = 1;
            db.KhachHangs.Add(khachHang2);
            db.SaveChanges();

            KhachHang khachHang3 = new KhachHang();
            khachHang3.TenKH = "Khách Hàng Vãng Lai";
            khachHang3.SDT = "";
            khachHang3.MaQuyen = 2;
            khachHang3.DiaChi = "";
            khachHang3.UserName = "unknown";
            khachHang3.Password = "unknown";
            db.KhachHangs.Add(khachHang3);
            db.SaveChanges();

            DanhMuc danhMuc1 = new DanhMuc();
            danhMuc1.TenDanhMuc = "Điện Thoại";
            DanhMuc danhMuc2 = new DanhMuc();
            danhMuc2.TenDanhMuc = "Máy Ảnh";
            DanhMuc danhMuc3 = new DanhMuc();
            danhMuc3.TenDanhMuc = "Laptop";
            DanhMuc danhMuc4 = new DanhMuc();
            danhMuc4.TenDanhMuc = "Đồng Hồ";
            DanhMuc danhMuc5 = new DanhMuc();
            danhMuc5.TenDanhMuc = "Xe Hơi";
            DanhMuc danhMuc6 = new DanhMuc();
            danhMuc6.TenDanhMuc = "Television";
            db.DanhMucs.Add(danhMuc1);
            db.SaveChanges();
            db.DanhMucs.Add(danhMuc2);
            db.SaveChanges();
            db.DanhMucs.Add(danhMuc3);
            db.SaveChanges();
            db.DanhMucs.Add(danhMuc4);
            db.SaveChanges();
            db.DanhMucs.Add(danhMuc5);
            db.SaveChanges();
            db.DanhMucs.Add(danhMuc6);
            db.SaveChanges();

            ThuongHieu thuongHieu1 = new ThuongHieu();
            thuongHieu1.MaDanhMuc = 1;
            thuongHieu1.TenThuongHieu = "Apple";
            db.ThuongHieus.Add(thuongHieu1);
            db.SaveChanges();

            ThuongHieu thuongHieu2 = new ThuongHieu();
            thuongHieu2.MaDanhMuc = 2;
            thuongHieu2.TenThuongHieu = "Nikon";
            db.ThuongHieus.Add(thuongHieu2);
            db.SaveChanges();

            ThuongHieu thuongHieu3 = new ThuongHieu();
            thuongHieu3.MaDanhMuc = 2;
            thuongHieu3.TenThuongHieu = "Sony";
            db.ThuongHieus.Add(thuongHieu3);
            db.SaveChanges();

            ThuongHieu thuongHieu4 = new ThuongHieu();
            thuongHieu4.MaDanhMuc = 6;
            thuongHieu4.TenThuongHieu = "SamSung";
            db.ThuongHieus.Add(thuongHieu4);
            db.SaveChanges();

            ThuongHieu thuongHieu5 = new ThuongHieu();
            thuongHieu5.MaDanhMuc = 6;
            thuongHieu5.TenThuongHieu = "Sony";
            db.ThuongHieus.Add(thuongHieu5);
            db.SaveChanges();

            ThuongHieu thuongHieu6 = new ThuongHieu();
            thuongHieu6.MaDanhMuc = 5;
            thuongHieu6.TenThuongHieu = "Porsche";
            db.ThuongHieus.Add(thuongHieu6);
            db.SaveChanges();

            ThuongHieu thuongHieu7 = new ThuongHieu();
            thuongHieu7.MaDanhMuc = 3;
            thuongHieu7.TenThuongHieu = "MacBook";
            db.ThuongHieus.Add(thuongHieu7);
            db.SaveChanges();

            ThuongHieu thuongHieu8 = new ThuongHieu();
            thuongHieu8.MaDanhMuc = 4;
            thuongHieu8.TenThuongHieu = "Apple";
            db.ThuongHieus.Add(thuongHieu8);
            db.SaveChanges();

            for (int i = 1; i <= 10; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 2;
                sach.MaThuongHieu =3;
                sach.TenSanPham = "Sony Alpha A7 III " + i + "X";
                sach.GiaBan = 50400000 + i * 100000;
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.MoTa = "Sony Alpha a7 III một trong những “tuyệt tác” được đánh giá cao nhờ thiết kế cảm biến được tiên tiến vượt trội. Với thiết kế phù hợp cho cả ứng dụng chụp ảnh tĩnh và cả quay video trong nhiều tình huống khác nhau. Tính năng tinh chỉnh tốc độ cao và hiệu suất mang lại cực kì hấp dẫn. Với bộ cảm biến CMM 24,2MP Exmor R BSI và bộ xử lý hình ảnh BIONZ X máy ảnh có thể đạt tốc độ chụp liên tục 10 khung hình / giây và cải thiện hiệu suất lấy nét tự động để theo dõi chủ thể nhanh hơn và chính xác hơn rất nhiều lần so với những “người anh em” trước đây. Hệ thống Hybrid AF lấy nét tự động nhanh chóng được cập nhật để sử dụng kết hợp 693 điểm phát hiện pha và 425 phát hiện tương phản nhằm nhanh chóng tập trung trong nhiều điều kiện ánh sáng khác nhau. Hơn thế nữa, máy ảnh còn duy trì sự tập trung vào các đối tượng với mức độ hiệu quả hơn hẳn trước đây. Ngoài các yếu tố trên, những cải tiến mới được trang bị cho Sony Alpha a7 III cũng giúp tạo nên chất lượng hình ảnh rõ nét và giảm tiếng ồn trong phạm vi độ nhạy từ ISO 100-51200. Thậm chí có thể mở rộng đến ISO 50-204800. Hơn nữa, khả năng quay video cũng đã được mở rộng để nâng cao chất lượng khi ghi hình UHD 4K. Ngoài ra, để giúp bạn có được những bức ảnh tĩnh và quay video hoàn hảo thì a7 III đã sử dụng bộ ổn định hình ảnh 5 trục để giảm thiểu sự xuất hiện của việc rung của máy ảnh khi bạn chụp ảnh";
                sach.TomTat = "- Cảm biến CMOS BSI Full frame 24MP - Bộ xử lý hình ảnh BIONZ X và Front-End LSI - Màn hình cảm ứng LCD 3.0'' 922k - Dot - Quay phim UHD 4K30p - Tốc độ chụp liên tiếp lên đến 10fps - Hệ thống lấy nét tự động với 693 điểm lấy nét - ISO 204800 - Hệ thống ổn định hình ảnh 5 trục - Khe cắm thẻ SD đôi -Cổng USB loại C - Tích hợp kết nối Wi-Fi với NFC -Thiết kế chống chịu thời tiết - Ống kính FE 28 - 70mm f/ 3.5 - 5.6 OSS";
                sach.Hinh = "https://zshop.vn/images/thumbnails/200/200/detailed/116/sony_ilce_7m3k_b_alpha_a7_iii_mirrorless_1394219_qvaf-9n.jpg?t=1601689680";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }


            for (int i = 1; i <= 10; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 2;
                sach.MaThuongHieu = 2;
                sach.TenSanPham = "Nikon D6 (Body) " + i + "X";
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 164900000 + i * 100000;
                sach.MoTa = "Tương tự các máy ảnh Nikon flagship khác, Nikon D6 bùng nổ đại tiệc cải tiến. Trang bị hệ thống AF mạnh nhất của Nikon hiện nay để ghi lại những khoảnh khắc xuất thần nhất, máy ảnh D6 hứa hẹn đáp ứng mọi yêu cầu của những nhiếp ảnh gia khắc khe nhất trong bất kỳ tình huống chụp nào. Để tận dụng đến từng giây đáng giá, Nikon D6 được trang bị các củng cố và tăng cường tiết kiệm thời gian mới, cũng như nhiều tùy chọn nâng cao tùy biến linh động hơn.Máy ảnh Nikon D6 vừa lập kỷ lục mới về mảng hiệu suất AF, vượt trội hơn so với hệ thống AF vốn đã rất được khen ngợi trên thế hệ D5. Với mật độ điểm ảnh mới là 105 điểm hoàn toàn tùy chọn trên cảm biến AF sử dụng điểm chữ thập hoàn toàn, cho phép nhiếp ảnh gia lấy nét bất kỳ vị trí nào trong khung hình mà không cần ngắm lại. Bố cục điểm giảm các vùng không nhạy AF với sắp xếp cảm biến gấp 3 lần đối với từng điểm lấy nét làm tăng độ phủ AF thêm 1.6 lần, thậm chí đối với chủ thể đang di chuyển khi chụp ở chế độ liên tiếp tốc độ cao.  Lấy nét chính xác với 17 tùy chọn nhóm Area AF tùy biến  Đối với nhu cầu xác định chủ thể chính xác xung quanh tiền cảnh nhiễu, nhiều vật cản, các nhiếp ảnh gia thể thao sẽ thấy hệ thống 17 nhóm Area AF tùy biến là cực kỳ cần thiết.  Chất lượng hình ảnh tỏa sáng bất kể trong tình trạng ánh sáng nào Sự kết hợp đỉnh cao giữa cảm biến FX 20.8MP phân giải cao với vi xử lý hình ảnh EXPEED 6 tạo điều kiện cho dãy ISO đạt đến 102,400 (mở rộng đến ISO 3,280,000) cho chất lượng hình ảnh hào nhoáng trong phạm vi điều kiện ánh sáng và cảnh chụp rộng nhất. Độ nhạy sáng và phân giải đạt được trên Nikon D6 giảm mạnh nhiễu hạt trong điều kiện thiếu sáng và cải thiện nhận diện AF so với D5.";
                sach.TomTat = "- Cảm biến FX CMOS 20.8MP - Vi xử lý hình ảnh EXPEED 6 - Hệ thống AF Multi-CAM 37K 105 điểm chữ thập hoàn toàn - Chụp liên tiếp 14 fps, ISO mở rộng 32800000";
                sach.Hinh = "https://zshop.vn/images/thumbnails/200/200/detailed/103/1581509785_IMG_1317038.jpg?t=1597377006";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }


            for (int i = 1; i <= 10; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 1;
                sach.MaThuongHieu = 1;
                sach.TenSanPham = "iPhone 12 Pro Max " + 25 + i + "GB";
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 40000000 + i * 100000;
                sach.MoTa = "Điện thoại iPhone 12 Pro Max: Nâng tầm đẳng cấp sử dụng Cứ mỗi năm, đến dạo cuối tháng 8 và gần đầu tháng 9 thì mọi thông tin sôi sục mới về chiếc iPhone mới lại xuất hiện. Apple năm nay lại ra thêm một chiếc iPhone mới với tên gọi mới là iPhone 12 Pro Max, đây là một dòng điện thoại mới và mạnh mẽ nhất của nhà Apple năm nay. Mời bạn tham khảo thêm một số mô tả sản phẩm bên dưới để bạn có thể ra quyết định mua sắm.  Màn hình 6.7 inches Super Retina XDR Năm nay, công nghệ màn hình trên 12 Pro Max cũng được đổi mới và trang bị tốt hơn cùng kích thước lên đến 6.7 inch, lớn hơn nhiều so với điện thoại iPhone 12 Mini. Với công nghệ màn hình OLED cho khả năng hiển thị hình ảnh lên đến 2778 x 1284 pixels. Bên cạnh đó, màn hình này còn cho độ sáng tối đa cao nhất lên đến 800 nits, luôn đảm bảo cho bạn một độ sáng cao và dễ nhìn nhất ngoài nắng.Một điểm đổi mới nữa trên màn hình của chiếc điện thoại Apple iPhone 12 năm nay là việc chúng được thiết kế với khung viền vuông vức, viền thép không gỉ mang đến vẻ đẹp sang trọng cho điện thoại. Máy cũng được trang bị nhiều phiên bản màu sắc đặc biệt cho người dùng lựa chọn.  RAM 6GB đa nhiệm thoải mái, bộ nhớ trong dung lượng lớn Về trang bị phần cứng bên trong thì iPhone 12 Pro Max có một thanh RAM lên đến 6GB. Điều này cho thấy rằng Apple ngày đang lắng nghe người dùng nhiều hơn khi trang bị một dung lượng RAM lớn hơn để việc đa nhiệm ngày càng được cải thiện hơn. Việc thanh ram lớn giúp cho bạn trải nghiệm các tựa game và đa nhiệm mượt mà hơn.  RAM 6GB đa nhiệm thoải mái, bộ nhớ trong dung lượng lớn  Năm nay, 12 Pro Max cũng sẽ có ba phiên bản bộ nhớ trong khác nhau, với bộ nhớ trong nhỏ nhất bắt đầu từ 128GB, 256GB và cao nhất sẽ là 512GB. Một chiếc điện thoại mà có một bộ nhớ trong lớn ngang ngửa một chiếc laptop là điều mà Apple muốn mang lại cho người dùng để có thể san sẻ bớt bộ nhớ cho các thiết bị khác.  Con chip Apple A14 SoC 5nm cùng khả năng kết nối 5G hiện đại iPhone 12 Pro Max không những chỉ có các cải tiến trên, mà chúng còn có một thứ cải tiến được coi là nguồn lõi và là trái tim để vận hành chiếc điện thoại siêu phẩm 2020, đó là con chip Apple A14 SoC 5nm. Trang bị này giúp cho điện thoại có một sức mạnh đáng gờm hơn các đối thủ hơn về hiệu năng, hiệu suất sử dụng pin.   Kết nối 5G hiện đại được trang bị trên điện thoại iPhone 12 Pro Max là một bước tiến mới trong kết nối và phát triển mạng lưới truyền tải thông tin. Chúng giúp cho bạn có thể cải thiện hiệu suất sử dụng mạng và chúng còn giúp các đường truyền tín hiệu luôn được đảm bảo không rớt kết nối và tăng chất lượng hiển thị hình ảnh trên mạng.  Cụm 3 camera sau với độ phân giải 12MP   Có thể nói camera cũng là một bước tiến mới trên iPhone 12 Pro Max khi chúng có một bộ 3 camera với chung một độ phân giải là 12MP. Tuy nhiên chúng có khẩu độ lớn và mật độ điểm ảnh trên một panel cũng lớn hơn, do đó chúng cho hình ảnh chi tiết hơn, bắt sáng tốt hơn. Ngoài ra, kết hợp chống rung quang học OIS thì máy còn có thể quay phim 4K tốt.   Camera trên iPhone 12 Pro Max có chức năng quét chiều sâu và đảm bảo hình ảnh có một độ sâu nhất định. Cùng với đó chức năng chính của chiếc ống kính này là khả năng thể hiện hình ảnh 3D khi quét chúng vào một căn phòng nhất định. Giúp phục vụ cho công việc xây dựng cũng như định dạng hình ảnh trước khi xây.  Camera trước 12MP hỗ trợ mở FaceiD cùng công nghệ chống nước IP68 Camera trước 12MP cũng có cùng khẩu độ và kích thước điểm ảnh trong panel giống như camera. Điều này giúp cho việc sử dụng chúng cho chụp ảnh selfie tốt hơn và chân thực hơn. Cùng với đó một tính năng mà Apple luôn giữ chúng từ đời iPhone X đến giờ là khả năng quét khuôn mặt 3D FaceiD.   Công nghệ chống nước là không thể thiếu trên các dòng điện thoại cao cấp và đặc biệt là đối với iPhone 12 Pro Max. Chúng được trang bị công nghệ chống nước và chống bụi tốt nhất hiện nay trên các dòng smartphone đó là tiêu chuẩn IP68. Giúp bạn luôn có thể yên tâm hơn trong việc sử dụng quay phim dưới nước hay ở môi trường khắc nghiệt.  Viên pin liền cho thời lượng sử dụng lên đến cả ngày cùng công nghệ sạc nhanh  Một viên pin liền với dung lượng lớn trên iPhone 12 Pro Max giúp cho chiếc điện thoại bạn có thể hoạt động được một cách ổn thoả hơn và thời gian sử dụng được lâu dài hơn. Cụ thể, máy cho thời gian nghe nhạc lên tới 80 giờ hoặc 12 giờ xem video trực truyến.   Công nghệ sạc là trên 12 Pro Max là công nghệ sạc nhanh không dây lên đến 15W. Điều này có thể giúp bạn hạn chế được các việc phải ngồi đợi chiếc điện thoại của mình sạc xong khi máy có thể bổ sung 50% dung lượng chỉ trong vòng 30 phút.  iPhone 12 Pro Max 2020 giá bao nhiêu Tại hệ thống CellphoneS, giá bán iPhone 12 Pro Max chỉ từ 27.99 triệu, iPhone 12 từ 19.99 triệu và iPhone 12 Pro từ 25.49 triệu. Thấp nhất sẽ là iPhone 12 Mini chỉ từ 16.99 triệu đồng. Đây là mức giá hàng chính hãng VN/A được bảo hành 12 tháng tại trung tâm bảo hành ủy quyền của Apple tại Việt Nam.  Mua iPhone 12 Pro Max chính hãng VN/A giá tốt tại CellphoneS Bạn là một iFan và bạn đang mong chờ để được trải nghiệm thử chiếc điện thoại iPhone 12 Pro Max mới nhất của Apple? Mời bạn đến ngay với CellphoneS để được tư vấn và trải nghiệm nhanh chóng nhất có thể. ";
                sach.TomTat = "Hãng sản xuất	Apple Kích thước	160.8 x 78.1 x 7.4 mm Trọng lượng	228 Bộ nhớ trong	128 GB Loại SIM	Dual SIM (nano‑SIM and eSIM) Loại màn hình	Super Retina XDR OLED, HDR10, Dolby Vision, Wide color gamut, True-tone Kích thước màn hình	6.7 inches Độ phân giải màn hình	1284 x 2778 pixels Hệ điều hành	iOS Phiên bản hệ điều hành	iOS 14";
                sach.Hinh = "https://cdn.tgdd.vn/Products/Images/42/228743/iphone-12-pro-max-256gb-190320-020344-400x400.jpg";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }


            for (int i = 1; i <= 16; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 3;
                sach.MaThuongHieu = 7;
                sach.TenSanPham = "MacBook Pro Touch 16 inch 2019 i" + i + " 2.6GHz";
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 60000000 + i * 100000;
                sach.TomTat = "MacBook ProTouch 2019 i7 chiếc laptop cấu hình mạnh mẽ của Apple sẽ đem đến những trải nghiệm mà không phải chiếc laptop nào cũng có được. Thiết kế sang trọng tinh tế, cấu hình khủng, được trang bị card đồ họa và các công nghệ độc quyền của Apple sẽ đem lại nhiều thích thú cho người dùng.";
                sach.MoTa = "MacBook Pro Touch đáp ứng mọi tác vụ nặng với cấu hình khủng, máy được trang bị CPU Intel Core i7 và RAM 16 GB thì chiếc MacBook Pro Touch phù hợp với tất cả công việc từ các ứng dụng văn phòng như Office 365 cho đến lập trình, xử lý hình ảnh độ phân giải cao, kết xuất đồ họa 3D, chỉnh sửa nhiều luồng video 4K và nhiều tác vụ chuyên nghiệp khác. Để đáp ứng hầu hết các công việc thì laptop còn được trang bị card đồ họa rời Radeon Pro 5300M 4GB cho trải nghiệm hình ảnh mượt mà, không giật lag, sử dụng các ứng dụng đồ họa kỹ thuật cực tốt.Không chỉ có cấu hình mạnh mẽ, MacBook Pro Touch 2019 còn có tốc độ xử lí cực nhanh nhờ được trang bị ổ cứng SSD 512 GB. Tốc độ mở máy chưa đến 10 giây cũng như các ứng dụng chỉ khoảng vài giây đồng thời thoải mái lưu trữ với dung lượng này.Màn hình Retina 16 inch đem đến hình ảnh sắc nét và chân thật với hơn 4 triệu điểm ảnh. Đặc biệt, màn hình còn được trang bị công nghệ True Tone hiện đại, cân bằng trắng tự động dựa theo nhiệt độ môi trường để mang đến trải nghiệm tự nhiên nhất. Apple luôn không ngừng nâng cấp trải nghiệm của người dùng. Với chiếc MacBook Pro Touch 2019 này, touch bar có thể tùy chỉnh cảm ứng linh hoạt, tự động thay đổi dựa trên ứng dụng và hành động bạn đang làm một cách thông minh để bạn thao tác tiện dụng nhất.	";
                sach.Hinh = "https://cdn.tgdd.vn/Products/Images/44/215941/macbook-pro-16-201926-macbookprotouch16inch-1-400x400.jpg";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }



            for (int i = 1; i <= 16; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 4;
                sach.MaThuongHieu = 8;
                sach.TenSanPham = "Apple Watch S5 44mm " + "X" + i;
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 11000000 + i * 100000;
                sach.TomTat = "Apple Watch S5 44 mm là phiên bản nâng cấp nhẹ so với phiên bản Apple Watch S4 tiền nhiệm. Lần đầu tiên Apple Watch sẽ được trang bị màn hình OLED luôn bật, tính năng la bàn và khả năng cảnh báo khẩn cấp trên nhiều quốc gia hơn. ";
                sach.MoTa = "Tính năng màn hình luôn hiển thị (Always-on) giúp bạn xem giờ và thông báo tiện lợi bất cứ lúc nào. Khi không quan sát, đồng hồ sẽ tự giảm độ sáng xuống để tiết kiệm pin. Màn hình trên Apple Watch S5 44mm sử dụng tấm nền OLED cao cấp, tiết kiệm pin hơn, độ sắc nét đạt chuẩn Retina - nghĩa là bạn rất khó để nhận biết các điểm ảnh, rỗ hạt bằng mắt thường.Đồng hồ thông minh Apple Watch S5 có tính năng định vị GPS giúp định vị chính xác quãng đường đi khi tập, từ đấy cung cấp thông tin về lộ trình trong quá trình luyện tập cho bạn.Điểm cải tiến ở Apple Watch S5 so với các thế hệ trước đó là la bàn từ tính để xác định phương hướng. Tính năng này sẽ hữu ích đối với những người dùng thích đi chạy bộ trong rừng, giúp định vị phương hướng ở nơi không thể truy cập mạng hay GPS không thể định vị được. Tính năng gọi cứu hộ khẩn cấp có hỗ trợ ở Việt Nam, và đặc biệt hữu ích với ai thường xuyên đi du lịch. Apple Watch sẽ định vị bạn đang ở đâu và gọi đến cứu hộ của địa phương đó khi bật SOS. Apple có ghi chú rõ các địa phương không được hỗ trợ, tham khảo";
                sach.Hinh = "https://cdn.tgdd.vn/Products/Images/7077/212770/apple-watch-s5-44mm-vien-nhom-day-cao-su-ava-1-400x400.jpg";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }


            for (int i = 1; i <= 16; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 5;
                sach.MaThuongHieu = 6;
                sach.TenSanPham = "Porsche Taycan Turbo S " + "i" + i;
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 1000000000 + i * 100000;
                sach.TomTat = "Porsche AG, thường được gọi tắt là Porsche, là một công ty chuyên sản xuất xe hơi thể thao hạng sang của Đức kiêm thương hiệu con trực thuộc sở hữu của tập đoàn ô tô hàng đầu thế giới - Volkswagen AG cũng như gia tộc nhà Porsche";
                sach.MoTa = "Porsche AG, thường được gọi tắt là Porsche, là một công ty chuyên sản xuất xe hơi thể thao hạng sang của Đức kiêm thương hiệu con trực thuộc sở hữu của tập đoàn ô tô hàng đầu thế giới - Volkswagen AG cũng như gia tộc nhà Porsche";
                sach.Hinh = "https://files.porsche.com/filestore/image/multimedia/none/j1-taycan-tu-s-modelimage-sideshot/model/60c967a4-ac79-11e9-80c4-005056bbdc38;sR;twebp065/porsche-model.webp";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }



            for (int i = 1; i <= 9; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 6;
                sach.MaThuongHieu = 4;
                sach.TenSanPham = "Smart Tivi QLED Samsung 8K " + "8" + i + " inch";
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 198000000 + i * 100000;
                sach.TomTat = "Thiết kế tinh xảo, đẹp mắt. Độ phân giải 8K ấn tượng cùng màu sắc rực rỡ, sống động với công nghệ Chấm lượng tử QLED. Công nghệ Quantum HDR 4000 nits hiện đại cho độ tương phản cao, hình ảnh chi tiết hơn. Tái tạo sắc đen sâu, sắc sáng tươi sáng hơn qua công nghệ Direct Full Array. Nâng cấp độ phân giải gần chuẩn 8K nhất với công nghệ AI Upscaling 8K. Chế Độ Ambient Mode biến chiếc tivi của bạn thành tác phẩm nghệ thuật. Âm thanh vòm Dolby độc đáo, cho âm thanh bùng nổ hơn. Hệ điều hành Tizen OS, đi cùng One remote có tìm kiếm giọng nói. Hỗ trợ điều khiển tivi bằng điện thoại qua ứng dụng SmartThings. Chiếu màn hình điện thoại lên tivi dễ dàng với tính năng Screen Mirroring, AirPlay 2.";
                sach.MoTa = "Thiết kế tinh xảo, đẹp mắt. Độ phân giải 8K ấn tượng cùng màu sắc rực rỡ, sống động với công nghệ Chấm lượng tử QLED. Công nghệ Quantum HDR 4000 nits hiện đại cho độ tương phản cao, hình ảnh chi tiết hơn. Tái tạo sắc đen sâu, sắc sáng tươi sáng hơn qua công nghệ Direct Full Array. Nâng cấp độ phân giải gần chuẩn 8K nhất với công nghệ AI Upscaling 8K. Chế Độ Ambient Mode biến chiếc tivi của bạn thành tác phẩm nghệ thuật. Âm thanh vòm Dolby độc đáo, cho âm thanh bùng nổ hơn. Hệ điều hành Tizen OS, đi cùng One remote có tìm kiếm giọng nói. Hỗ trợ điều khiển tivi bằng điện thoại qua ứng dụng SmartThings. Chiếu màn hình điện thoại lên tivi dễ dàng với tính năng Screen Mirroring, AirPlay 2.";
                sach.Hinh = "https://cdn.tgdd.vn/Products/Images/1942/200425/samsung-qa82q900r-25-550x340.jpg";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }

            for (int i = 1; i <= 9; i++)
            {
                SanPham sach = new SanPham();
                sach.MaDanhMuc = 6;
                sach.MaThuongHieu = 5;
                sach.TenSanPham = "Android Tivi Sony 8K " + "8" + i + " inch";
                sach.TinhTrang = "Hiển Thị";
                sach.SoLuong = 50;
                sach.GiaBan = 258000000 + i * 100000;
                sach.TomTat = "Thiết kế tinh xảo, đẹp mắt. Độ phân giải 8K ấn tượng cùng màu sắc rực rỡ, sống động với công nghệ Chấm lượng tử QLED. Công nghệ Quantum HDR 4000 nits hiện đại cho độ tương phản cao, hình ảnh chi tiết hơn. Tái tạo sắc đen sâu, sắc sáng tươi sáng hơn qua công nghệ Direct Full Array. Nâng cấp độ phân giải gần chuẩn 8K nhất với công nghệ AI Upscaling 8K. Chế Độ Ambient Mode biến chiếc tivi của bạn thành tác phẩm nghệ thuật. Âm thanh vòm Dolby độc đáo, cho âm thanh bùng nổ hơn. Hệ điều hành Tizen OS, đi cùng One remote có tìm kiếm giọng nói. Hỗ trợ điều khiển tivi bằng điện thoại qua ứng dụng SmartThings. Chiếu màn hình điện thoại lên tivi dễ dàng với tính năng Screen Mirroring, AirPlay 2.";
                sach.MoTa = "Thiết kế tinh xảo, đẹp mắt. Độ phân giải 8K ấn tượng cùng màu sắc rực rỡ, sống động với công nghệ Chấm lượng tử QLED. Công nghệ Quantum HDR 4000 nits hiện đại cho độ tương phản cao, hình ảnh chi tiết hơn. Tái tạo sắc đen sâu, sắc sáng tươi sáng hơn qua công nghệ Direct Full Array. Nâng cấp độ phân giải gần chuẩn 8K nhất với công nghệ AI Upscaling 8K. Chế Độ Ambient Mode biến chiếc tivi của bạn thành tác phẩm nghệ thuật. Âm thanh vòm Dolby độc đáo, cho âm thanh bùng nổ hơn. Hệ điều hành Tizen OS, đi cùng One remote có tìm kiếm giọng nói. Hỗ trợ điều khiển tivi bằng điện thoại qua ứng dụng SmartThings. Chiếu màn hình điện thoại lên tivi dễ dàng với tính năng Screen Mirroring, AirPlay 2.";
                sach.Hinh = "https://cdn.tgdd.vn/Products/Images/1942/224033/sony-kd-85z8h-550x340.jpg";
                db.SanPhams.Add(sach);
                db.SaveChanges();
            }

            for (int i = 1; i <= db.SanPhams.LongCount(); i++)
            {
                TruyCap tmp = new TruyCap();
                tmp.MaSanPham = i;
                tmp.SoLanTruyCap = 0;
                db.TruyCaps.Add(tmp);
                db.SaveChanges();
            }

            db.SaveChanges();
            return RedirectToAction("Index", "Home");


        }
    }
}