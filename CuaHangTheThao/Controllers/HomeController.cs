using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CuaHangTheThao.Models;

namespace CuaHangTheThao.Controllers
{
    public class HomeController : Controller
    {
        csdl_cuahangthethaoDataContext data = new csdl_cuahangthethaoDataContext();
        // GET: Home
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult trangchu(string danhMucId = null)
        {
            IQueryable<SanPham> spQuery = data.SanPhams;

            if (!string.IsNullOrEmpty(danhMucId))
            {
                spQuery = spQuery.Where(sp => sp.danh_muc_id == danhMucId);
            }

            List<SanPham> SP = spQuery.ToList();
            return View(SP);
        }
        public ActionResult DangNhap()
        {

            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection col)
        {
            var username = col["txtname"];
            var password = col["txtpass"];

            NguoiDung nd = data.NguoiDungs.FirstOrDefault(k => k.ten_dang_nhap == username && k.mat_khau == password);

            if (nd != null)
            {
                Session["nd"] = nd;
                return RedirectToAction("trangchu");
            }

            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
            return View();
        }
        //Tạo nút thêm mới sản phẩm:
        //public ActionResult ThemMoi()
        //{
        //    return View();
        //}
        //[HttpPost]
        //public ActionResult ThemMoi(FormCollection collection ,SanPham sp)
        //{
        //    var ThemSanPham = collection["ten"];
        //    sp.ten = ThemSanPham;
        //    data.SanPhams.InsertOnSubmit(sp);
        //    data.SubmitChanges();
        //    return RedirectToAction("trangchu");
        //    return this.ThemMoi();

        //}
        public ActionResult DanhMucSanPham()
        {
            List<DanhMucSanPham> dmsp = data.DanhMucSanPhams.ToList();
            return PartialView(dmsp);
        }










        //public ActionResult DangKy()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult DangKy(FormCollection col)
        //{
        //    string tenDangNhap = col["txtname"];
        //    string matKhau = col["txtpass"];
        //    string email = col["txtemail"];
        //    string ho = col["txtho"];
        //    string ten = col["txtten"];
        //    string soDienThoai = col["txtphone"];
        //    string diaChi = col["txtaddress"];

        //    if (data.NguoiDungs.Any(nddk => nddk.ten_dang_nhap == tenDangNhap))
        //    {
        //        ViewBag.ErrorMessage = "Tên đăng nhập đã tồn tại.";
        //        return View();
        //    }

        //    NguoiDung nd = new NguoiDung
        //    {
        //        nguoi_dung_id = Guid.NewGuid().ToString(),
        //        ten_dang_nhap = tenDangNhap,
        //        mat_khau = matKhau,
        //        email = email,
        //        ho = ho,
        //        ten = ten,
        //        so_dien_thoai = soDienThoai,
        //        dia_chi = diaChi,
        //        ngay_dang_ky = DateTime.Now
        //    };

        //    data.NguoiDungs.InsertOnSubmit(nd);
        //    data.SubmitChanges();

        //    return RedirectToAction("DangNhap", "Home");
        //}

        //public ActionResult DangNhap()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult DangNhap(FormCollection col)
        //{
        //    string tenDangNhap = col["txtname"];
        //    string matKhau = col["txtpass"];

        //    NguoiDung nd = data.NguoiDungs.FirstOrDefault(k => k.ten_dang_nhap == tenDangNhap && k.mat_khau == matKhau);

        //    if (nd != null)
        //    {
        //        Session["nd"] = nd;
        //        return RedirectToAction("trangchu", "Home");
        //    }

        //    ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không đúng.";
        //    return View();
        //}
        public ActionResult Details(string id)
        {
            // Lấy thông tin sản phẩm theo id
            var sanPham = data.SanPhams.FirstOrDefault(sp => sp.san_pham_id == id);

            if (sanPham == null)
            {
                return HttpNotFound();
            }

            // Lấy danh mục sản phẩm cho sản phẩm này
            List<DanhMucSanPham> danhMuc = data.DanhMucSanPhams.ToList();

            // Truyền dữ liệu cho view
            ViewBag.DanhMuc = danhMuc;

            return View(sanPham);
        }


    }
}