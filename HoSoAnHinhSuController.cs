using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using System.Threading.Tasks;
using HVITCore.Controllers;
using System.Data.Entity.Infrastructure;
using CMS.Models;
using CMS.Services;
using HVIT.Security;
using System.Collections.Generic;

namespace VKS.Controllers
{
    [RoutePrefix("api/hosoanhinhsu")]
    public class HoSoAnHinhSuController : BaseApiController
    {
        [AuthorizeUser, HttpGet, Route("")]
        public async Task<IHttpActionResult> Search([FromUri]Pagination pagination, 
            [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]string q = null,
            [FromUri]int? trangThaiToTung = null,
            [FromUri]bool? laKiemSatDieuTra = null,
            [FromUri]int? trangThaiToTungKhac = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null,
            [FromUri]bool? dangTraHoSo = null)
        {
            using (var db = new ApplicationDbContext())
            {
                Nhansu nhansu = GetNhanSu();
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results
                        .Include(o => o.BiCanHoSo.Select(x => x.BiCan))
                        .Include(o => o.QuyetDinhHoSo.Select(x => x.QuyetDinh.LoaiCongVanQuyetDinh));
                }

                results = results.Where(o => o.DonViID == nhansu.PhongBan.DonViID);
                if (trangThaiToTungKhac.HasValue)
                    results = results.Where(o => o.TrangThaiToTung != trangThaiToTungKhac);
              
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));
                if (!string.IsNullOrEmpty(q))
                {
                    results = results.Where(x => x.MaSoVuAn.ToString().Contains(q)
                     || x.TenVuAn.Contains(q));
                }
                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
                if (dangTraHoSo.HasValue && dangTraHoSo==true)
                {
                    if (trangThaiToTung.HasValue)
                        results = results.Where(o => o.TrangThaiToTung == trangThaiToTung|| o.TrangThaiToTung == 11);
                }
                else
                {
                    if (trangThaiToTung.HasValue)
                        results = results.Where(o => o.TrangThaiToTung == trangThaiToTung);
                }
                results = results.Where(o => o.TrangThaiToTung != trangThaiToTungKhac);

                results = results.OrderByDescending(o => o.HoSoAnHinhSuID);

                return Ok((await GetPaginatedResponse(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("danhsachchuyenan")]
        public async Task<IHttpActionResult> GetDanhSachChuyenAn([FromUri]Pagination pagination, 
            [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null,
            [FromUri]bool chuaChuyen = true,
            [FromUri]string tenVKSNhan = null
            )
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.ThuLyVuAn);
                }
                results = results.Where(o => o.DonViID == nhansu.PhongBan.DonViID);
                if (chuaChuyen)
                {
                    results = results.Where(o => 
                    o.ChuyenNhanAn.Where(x=>x.DonViChuyenID == nhansu.PhongBan.DonViID && x.NhanAn != true ).Count() == 0);
                }
                else
                {
                    results = results.Where(o =>
                    o.ChuyenNhanAn.Where(x => x.DonViChuyenID == nhansu.PhongBan.DonViID && x.NhanAn != false).Count() > 0);
                }
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y=>y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));

                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }

                results = results.OrderByDescending(o => o.HoSoAnHinhSuID);

                return Ok((await GetPaginatedResponse(results, pagination)));
            }
        }


        [AuthorizeUser, HttpGet, Route("danhsachnhanan")]
        public async Task<IHttpActionResult> GetDanhSachNhanAn([FromUri]Pagination pagination,
            [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null,
            [FromUri]bool chuaNhan = true,
            [FromUri]string tenVKSNhan = null
            )
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.ThuLyVuAn);
                }
                if (chuaNhan)
                {
                    results = results.Where(o =>
                    o.ChuyenNhanAn.Any(x=>x.DonViNhanID == nhansu.PhongBan.DonViID && x.NgayNhan == null));
                }
                else
                {
                    results = results.Where(o =>
                    o.ChuyenNhanAn.Any(x => x.DonViNhanID == nhansu.PhongBan.DonViID && x.NgayNhan != null));
                }
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));

                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }

                results = results.OrderByDescending(o => o.HoSoAnHinhSuID);

                return Ok((await GetPaginatedResponse(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("danhsachxetxu")]
        public async Task<IHttpActionResult> GetDanhSachXetXuSoTham([FromUri]Pagination pagination,
           [FromUri]string maVuAn = null,
           [FromUri]string tenVuAn = null,
           [FromUri]string tenBiCan = null,
           [FromUri]DateTime? ngayVuAnTu = null,
           [FromUri]DateTime? ngayVuAnDen = null,
           [FromUri]int? trangThaiToTung = null,
           [FromUri]int? daNhanAn = null,
           [FromUri]bool? dangTraHoSo = null
           )
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.ThuLyVuAn)
                        .Include(o => o.BiCanHoSo.Select(x => x.BiCan))
                        .Include(o => o.QuyetDinhHoSo.Select(x=>x.QuyetDinh.DonVi));
                }
                //lọc các hồ sơ ở gđ xxst
               
                if (daNhanAn.HasValue)
                    results = results.Where(o=>o.DaNhanAn == daNhanAn);
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));

                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
                if (dangTraHoSo.HasValue && dangTraHoSo == true)
                {
                    if (trangThaiToTung.HasValue)
                        results = results.Where(o => o.TrangThaiToTung == trangThaiToTung || o.TrangThaiToTung == 11);
                }
                else
                {
                    if (trangThaiToTung.HasValue)
                        results = results.Where(o => o.TrangThaiToTung == trangThaiToTung);
                }
                results = results.OrderByDescending(o => o.HoSoAnHinhSuID);
                var results2 = results.Select(x=>new {
                    x.HoSoAnHinhSuID,
                    x.TenVuAn,
                    x.MaSoVuAn,
                    x.LoaiToiPham,
                    BiCanHoSo = x.BiCanHoSo.Select( y=> new
                    {
                        BiCan = new
                        {
                            y.BiCan.HoTen,
                            y.BiCan.LaBiCanDauVu
                        }
                    }
                        ),
                    QuyetDinhHoSo = x.QuyetDinhHoSo.Select(y=>new {
                        y.QuyetDinhHoSoID,
                        y.QuyetDinhID,
                        y.HoSoAnHinhSuID,
                        QuyetDinh = new
                        {
                            y.QuyetDinh.QuyetDinhID,
                            y.QuyetDinh.LoaiQuyetDinhID,
                            y.QuyetDinh.SoQuyetDinh,
                            y.QuyetDinh.DonVi
                        }
                    })
                });

                return Ok((await GetPaginatedResponse(results2, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("danhsachxetxubosung")]
        public async Task<IHttpActionResult> GetDanhSachXetXuBoSung([FromUri]Pagination pagination,
          [FromUri]string maVuAn = null,
          [FromUri]string tenVuAn = null,
          [FromUri]string tenBiCan = null,
          [FromUri]DateTime? ngayVuAnTu = null,
          [FromUri]DateTime? ngayVuAnDen = null,
          [FromUri]int? trangThaiToTungTraVe = null
          )
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                var lstHoSo = db.ChoPhepSuaHoSo.Select(x =>x.HoSoAnHinhSuID);
                var lstHoSo1 = db.ChoPhepSuaHoSo.Select(x => x.TrangThaiToTungTraVe);
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu.Where(x => lstHoSo.Contains(x.HoSoAnHinhSuID));


                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.ThuLyVuAn)
                        .Include(o => o.BiCanHoSo.Select(x => x.BiCan))
                        .Include(o => o.QuyetDinhHoSo.Select(x => x.QuyetDinh.LoaiCongVanQuyetDinh))
                        .Include(o => o.QuyetDinhHoSo.Select(x => x.QuyetDinh.DonVi));
                }
                //lọc các hồ sơ ở gđ xx bổ sung
                if (trangThaiToTungTraVe.HasValue)
                    results = results.Where(o => lstHoSo1.Contains(trangThaiToTungTraVe));
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));

                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
               
                results = results.OrderByDescending(o => o.HoSoAnHinhSuID);

                return Ok((await GetPaginatedResponse(results, pagination)));
            }
        }
        [AuthorizeUser, HttpGet, Route("{hoSoAnHinhSuID}/dieuluatkhoito")]
        public async Task<IHttpActionResult> DieuLuatKhoiToHoSo([FromUri]Pagination pagination,
            [FromUri]int? hoSoAnHinhSuID = null, [FromUri]int? dieuLuatID = null, [FromUri]int? khoanLuatID = null, [FromUri]int? diemLuatID = null)
        {
            using (var db = new ApplicationDbContext())
            {
                IQueryable<DieuLuatKhoiTo> results = db.DieuLuatKhoiTo;
                if (pagination == null)
                    pagination = new Pagination();
                if (pagination.includeEntities)
                {
                    results = results.Include(o => o.DieuLuat)
                                     .Include(o => o.KhoanLuat)
                                     .Include(o => o.DiemLuat)
                                     .Include(o => o.HoSoAnHinhSu);
                }

                if (hoSoAnHinhSuID.HasValue) results = results.Where(o => o.HoSoAnHinhSuID == hoSoAnHinhSuID);
                if (dieuLuatID.HasValue) results = results.Where(o => o.DieuLuatID == dieuLuatID);
                if (khoanLuatID.HasValue) results = results.Where(o => o.KhoanLuatID == khoanLuatID);
                if (diemLuatID.HasValue) results = results.Where(o => o.DiemLuatID == diemLuatID);

                results = results.OrderBy(o => o.DieuLuatKhoiToID);

                return Ok((await GetPaginatedResponse(results, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("GetDanhSachHoSo")]
        public async Task<IHttpActionResult> GetDanhSachHoSo([FromUri]Pagination pagination,
            [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]string q = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null,
            [FromUri]int? trangThaiToTung = null)
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));
                if (!string.IsNullOrEmpty(q))
                {
                    results = results.Where(x => x.MaSoVuAn.ToString().Contains(q)
                     || x.TenVuAn.Contains(q));
                }
                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
                var donVi = nhansu.PhongBan.DonViID;
                if(donVi != null)
                    results = results.Where(o => o.ThuLyVuAn.Any(x => x.DonViID == donVi && x.TinhTrang == true));
                results = results.Where(o => o.TrangThaiToTung == trangThaiToTung);
                results = results.Include(o => o.BiCanHoSo.Select(x => x.BiCan)).Include(x => x.QuyetDinhHoSo.Select(y => y.QuyetDinh.DonVi));
                var res = results.Select(x => new {
                    x.HoSoAnHinhSuID,
                    x.TenVuAn,
                    x.MaSoVuAn,
                    x.LoaiToiPham,
                    BiCanDauVu = x.BiCanHoSo.Where(y => y.BiCan.LaBiCanDauVu == true).Select(y => y.BiCan).FirstOrDefault(),
                    QuyetDinhKhoiTo = x.QuyetDinhHoSo.Where(y => y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_KHOI_TO_VU_AN_HINH_SU).Select(y =>
                    new {
                        y.QuyetDinh.SoQuyetDinh,
                        DonVi = new
                        {
                            y.QuyetDinh.DonVi.TenDonVi
                        },
                        y.QuyetDinh.NgayRaQuyetDinh
                    }).FirstOrDefault()
                }).OrderByDescending(o => o.HoSoAnHinhSuID);
                return Ok((await GetPaginatedResponse(res, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("GetDanhSachDieuTraBoSung")]
        public async Task<IHttpActionResult> GetDanhSachDieuTraBoSung([FromUri]Pagination pagination,
             [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]string q = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null,
            [FromUri]int? trangThaiToTung = null)
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));
                if (!string.IsNullOrEmpty(q))
                {
                    results = results.Where(x => x.MaSoVuAn.ToString().Contains(q)
                     || x.TenVuAn.Contains(q));
                }
                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
                var donVi = nhansu.PhongBan.DonViID;
                if (donVi != null)
                    results = results.Where(o => o.ThuLyVuAn.Any(x => x.DonViID == donVi && x.TinhTrang == true));
                results = results.Include(o => o.BiCanHoSo.Select(x => x.BiCan)).Include(x => x.QuyetDinhHoSo.Select(y => y.QuyetDinh.DonVi));
                results = results.Where(o => o.TrangThaiToTung == trangThaiToTung);
                var res = results.Select(x => new {
                    x.HoSoAnHinhSuID,
                    x.TenVuAn,
                    x.MaSoVuAn,
                    x.TrangThaiToTung,
                    x.LoaiToiPham,
                    BiCanDauVu = x.BiCanHoSo.Where(y => y.BiCan.LaBiCanDauVu == true).Select(y => y.BiCan).FirstOrDefault(),
                    QuyetDinhKhoiTo = x.QuyetDinhHoSo.Where(y => y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_KHOI_TO_VU_AN_HINH_SU).Select(y =>
                    new {
                        y.QuyetDinh.SoQuyetDinh,
                        DonVi = new
                        {
                            y.QuyetDinh.DonVi.TenDonVi
                        },
                        y.QuyetDinh.NgayRaQuyetDinh
                    }).FirstOrDefault(),
                    LaChapNhanDieuTra = x.QuyetDinhHoSo.Where(y => y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_TRA_HO_SO_DE_YEU_CAU_DIEU_TRA_BO_SUNG || y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_TRA_HO_SO_YEU_CAU_DTBS_CUA_TOA_AN).OrderByDescending(y => y.QuyetDinhHoSoID).Select(y => y.DieuTraBoXung.FirstOrDefault().ChapNhanDieuTraBoSung).FirstOrDefault()
                }).OrderByDescending(o => o.HoSoAnHinhSuID);
                return Ok((await GetPaginatedResponse(res, pagination)));
            }
        }

        [AuthorizeUser, HttpGet, Route("GetDanhSachGiaoNhan")]
        public async Task<IHttpActionResult> GetDanhSachGiaoNhan([FromUri]Pagination pagination,
            [FromUri]string maVuAn = null,
            [FromUri]string tenVuAn = null,
            [FromUri]string tenBiCan = null,
            [FromUri]string q = null,
            [FromUri]DateTime? ngayVuAnTu = null,
            [FromUri]DateTime? ngayVuAnDen = null)
        {
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                IQueryable<HoSoAnHinhSu> results = db.HoSoAnHinhSu;
                if (pagination == null)
                    pagination = new Pagination();
                if (!string.IsNullOrEmpty(maVuAn))
                    results = results.Where(o => o.MaSoVuAn.Contains(maVuAn));
                if (!string.IsNullOrEmpty(tenBiCan))
                    results = results.Where(o => o.BiCanHoSo.Any(y => y.BiCan.HoTen.Contains(tenBiCan)));
                if (!string.IsNullOrEmpty(tenVuAn))
                    results = results.Where(o => o.TenVuAn.Contains(tenVuAn));
                if (!string.IsNullOrEmpty(q))
                {
                    results = results.Where(x => x.MaSoVuAn.ToString().Contains(q)
                     || x.TenVuAn.Contains(q));
                }
                if (ngayVuAnTu.HasValue)
                    results = results.Where(o => o.NgayVuAn >= ngayVuAnTu);

                if (ngayVuAnDen.HasValue)
                {
                    ngayVuAnDen = new DateTime(ngayVuAnDen.Value.Year, ngayVuAnDen.Value.Month, ngayVuAnDen.Value.Day)
                        .AddDays(1);
                    results = results.Where(o => o.NgayVuAn < ngayVuAnDen);
                }
                var donVi = nhansu.PhongBan.DonViID;
                if (donVi != null)
                    results = results.Where(o => o.ThuLyVuAn.Any(x => x.DonViID == donVi && x.TinhTrang == true));
                results = results.Include(o => o.BiCanHoSo.Select(x => x.BiCan)).Include(x => x.QuyetDinhHoSo.Select(y => y.QuyetDinh.DonVi));
                var res = results.Select(x => new {
                    x.HoSoAnHinhSuID,
                    x.TenVuAn,
                    x.MaSoVuAn,
                    x.LoaiToiPham,
                    BiCanDauVu = x.BiCanHoSo.Where(y => y.BiCan.LaBiCanDauVu == true).Select(y => y.BiCan).FirstOrDefault(),
                    QuyetDinhKhoiTo = x.QuyetDinhHoSo.Where(y => y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_KHOI_TO_VU_AN_HINH_SU).Select(y =>
                    new {
                        y.QuyetDinh.SoQuyetDinh,
                        DonVi = new
                        {
                            y.QuyetDinh.DonVi.TenDonVi
                        },
                        y.QuyetDinh.NgayRaQuyetDinh
                    }).FirstOrDefault()
                }).OrderByDescending(o => o.HoSoAnHinhSuID);
                return Ok((await GetPaginatedResponse(res, pagination)));
            }
        }

        [HttpGet, Route("vuancothenhap/{Pid}")]
        public async Task<IHttpActionResult> GetVuAnCoTheNhap(int? Pid)
        {
            using (var db = new ApplicationDbContext())
            {
                List<HoSoAnHinhSu> lstHS = new List<HoSoAnHinhSu>();
                try
                {
                    lstHS = await db.HoSoAnHinhSu
                    .Include(o => o.DonVi)
                    .Include(o => o.BanAn)
                    .Include(o => o.BiCanHoSo.Select(x => x.BiCan))
                    .Include(o => o.XuLyTinToGiac)
                    .Where(o=>o.HoSoAnHinhSuPID == Pid && o.TrangThaiToTung != 10)
                    .ToListAsync();
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }

                return Ok(lstHS);
            }
        }
        [HttpPost, Route("updatetrangthaitotung")]
        public async Task<IHttpActionResult> UpdateTTTT([FromBody]List<HoSoAnHinhSu> hoSoAnHinhSu)
        {
            if (hoSoAnHinhSu == null) return BadRequest("Invalid HoSoAnHinhSu");

            using (var db = new ApplicationDbContext())
            {
                try
                {
                    foreach (HoSoAnHinhSu qd in hoSoAnHinhSu)
                    {
                        HoSoAnHinhSu hs = new HoSoAnHinhSu();
                        hs = db.HoSoAnHinhSu.Where(x => x.HoSoAnHinhSuID == qd.HoSoAnHinhSuID).FirstOrDefault();
                        if (hs != null)
                        {
                            hs.TrangThaiToTung = 10;
                        }
                        await db.SaveChangesAsync();
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return Ok(hoSoAnHinhSu);
        }
        [HttpGet, Route("{hoSoAnHinhSuID:int}")]
        public async Task<IHttpActionResult> Get(int hoSoAnHinhSuID)
        {
            using (var db = new ApplicationDbContext())
            {
                HoSoAnHinhSu hoSoAnHinhSu = new HoSoAnHinhSu();
                try
                {
                    hoSoAnHinhSu = await db.HoSoAnHinhSu
                    .Include(o => o.DonVi)
                    .Include(o => o.BanAn)
                    .Include(o => o.BiCanHoSo.Select(x => x.BiCan))
                    .Include(o => o.XuLyTinToGiac)
                    .SingleOrDefaultAsync(o => o.HoSoAnHinhSuID == hoSoAnHinhSuID);
                }
                catch (Exception e)
                {
                    Console.Write(e);
                }
                
                if (hoSoAnHinhSu == null)
                    return NotFound();

                return Ok(hoSoAnHinhSu);
            }
        }

        [HttpPost, Route(""), AuthorizeUser]
        public async Task<IHttpActionResult> Insert([FromBody]HoSoAnHinhSu hoSoAnHinhSu)
        {
            if (hoSoAnHinhSu.HoSoAnHinhSuID != 0) return BadRequest("Invalid HoSoAnHinhSuID");
            Nhansu nhansu = GetNhanSu();
            using (var db = new ApplicationDbContext())
            {
                hoSoAnHinhSu.DonViID = nhansu.PhongBan.DonViID;
                hoSoAnHinhSu.TrangThaiToTung = 2;
                var quyetDinh = hoSoAnHinhSu.QuyetDinhHoSo.FirstOrDefault();
                if(quyetDinh != null)
                {
                    DieuLuatKhoiTo dieuLuatKhoiTo = new DieuLuatKhoiTo();
                    dieuLuatKhoiTo.DieuLuatID = quyetDinh.DieuLuatID;
                    dieuLuatKhoiTo.KhoanLuatID = quyetDinh.KhoanLuatID;
                    dieuLuatKhoiTo.DiemLuatID = quyetDinh.DiemLuatID;
                    dieuLuatKhoiTo.HoSoAnHinhSuID = hoSoAnHinhSu.HoSoAnHinhSuID;
                    db.DieuLuatKhoiTo.Add(dieuLuatKhoiTo);
                }
                db.HoSoAnHinhSu.Add(hoSoAnHinhSu);
                await db.SaveChangesAsync();
            }

            return Ok(hoSoAnHinhSu);
        }

        [HttpPut, Route("{hoSoAnHinhSuID:int}")]
        public async Task<IHttpActionResult> Update(int hoSoAnHinhSuID, [FromBody]HoSoAnHinhSu hoSoAnHinhSu)
        {
            if (hoSoAnHinhSu.HoSoAnHinhSuID != hoSoAnHinhSuID) return BadRequest("Id mismatch");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            using (var db = new ApplicationDbContext())
            {
                db.Entry(hoSoAnHinhSu).State = EntityState.Modified;

                try
                {
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ducEx)
                {
                    bool exists = db.HoSoAnHinhSu.Count(o => o.HoSoAnHinhSuID == hoSoAnHinhSuID) > 0;
                    if (!exists)
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw ducEx;
                    }
                }

                return Ok(hoSoAnHinhSu);
            }
        }

        [HttpDelete, Route("{hoSoAnHinhSuID:int}")]
        public async Task<IHttpActionResult> Delete(int hoSoAnHinhSuID)
        {
            using (var db = new ApplicationDbContext())
            {
                var hoSoAnHinhSu = await db.HoSoAnHinhSu
                    .Include(x => x.BiCanHoSo.Select(y => y.BiCan))
                    .Include(x => x.QuyetDinhHoSo.Select(y => y.QuyetDinh))
                    .Include(x => x.XuLyTinToGiac)
                    .FirstOrDefaultAsync(o => o.HoSoAnHinhSuID == hoSoAnHinhSuID);

                if (hoSoAnHinhSu == null)
                    return NotFound();

                if (await db.ThuLyVuAn.AnyAsync(o => o.HoSoAnHinhSuID == hoSoAnHinhSu.HoSoAnHinhSuID))
                    return BadRequest("Thao tác xóa thất bại vì: Hồ sơ đã được thụ lý");

                var dsBiCanHoSo = hoSoAnHinhSu.BiCanHoSo.ToList();
                foreach (BiCanHoSo biCanHoSo in dsBiCanHoSo)
                {
                    var biCan = biCanHoSo.BiCan;
                    db.Entry(biCanHoSo).State = EntityState.Deleted;
                    db.Entry(biCan).State = EntityState.Deleted;
                }
                var dsXuLyTinToGiac = hoSoAnHinhSu.XuLyTinToGiac.ToList();
                foreach(XuLyTinToGiac xuLyTinToGiac in dsXuLyTinToGiac)
                {
                    db.Entry(xuLyTinToGiac).State = EntityState.Deleted;
                }
                var dsQuyetDinhHoSo = hoSoAnHinhSu.QuyetDinhHoSo.ToList();
                foreach(QuyetDinhHoSo quyetDinhHoSo in dsQuyetDinhHoSo)
                {
                    var quyetDinh = quyetDinhHoSo.QuyetDinh;
                    db.Entry(quyetDinhHoSo).State = EntityState.Deleted;
                    db.Entry(quyetDinh).State = EntityState.Deleted;
                }
                db.Entry(hoSoAnHinhSu).State = EntityState.Deleted;

                await db.SaveChangesAsync();

                return Ok();
            }
        }

        [HttpGet, Route("CheckGiaoNhan")]
        public IHttpActionResult CheckGiaoNhan(int hoSoAnHinhSuID, bool LaVKSGiao)
        {
            using (var db = new ApplicationDbContext())
            {
                var thuLyVuAn = db.ThuLyVuAn.Where(x => x.HoSoAnHinhSuID == hoSoAnHinhSuID && x.TinhTrang == true).FirstOrDefault();
                if (thuLyVuAn == null)
                    return BadRequest("hoSoAnHinhSuID invalid");
                if (LaVKSGiao)
                {
                    if (db.CaoTrangChuyenToa.Any(x => x.HoSoAnHinhSuID == hoSoAnHinhSuID))
                    {
                        if (thuLyVuAn.GiaiDoanThuLy == 4)
                            return Ok(true);
                        if (thuLyVuAn.GiaiDoanThuLy == 5 && db.QuyetDinhHoSo.Any(y => y.HoSoAnHinhSuID == hoSoAnHinhSuID && y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_TRA_HO_SO_YEU_CAU_DTBS_CUA_TOA_AN))
                            return Ok(true);
                    }
                }
                else
                {
                    if (db.KetLuanDieuTra.Any(x => x.HoSoAnHinhSuID == hoSoAnHinhSuID && x.TrangThai == true && x.LoaiKetLuan != 3))
                    {
                        if (thuLyVuAn.GiaiDoanThuLy == 2)
                            return Ok(true);
                        if (thuLyVuAn.GiaiDoanThuLy == 4 && db.QuyetDinhHoSo.Any(y => y.HoSoAnHinhSuID == hoSoAnHinhSuID && y.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_TRA_HO_SO_DE_YEU_CAU_DIEU_TRA_BO_SUNG && y.QuyetDinh.DieuTraBoXung.Any(z => z.TrangThai == true)))
                            return Ok(true);
                    }
                }
                return Ok(false);
            }
        }

        [HttpGet, Route("checkketluandieutra")]
        public IHttpActionResult CheckKetLuanDieuTra(int hoSoAnHinhSuID)
        {
            using (var db = new ApplicationDbContext())
            {
                    if (db.QuyetDinhBiCan.Any(x => x.BiCanHoSo.HoSoAnHinhSuID == hoSoAnHinhSuID && x.QuyetDinh.LoaiQuyetDinhID == LoaiQuyetDinhConst.QD_KHOI_TO_BI_CAN))
                    {
                        return Ok(true);
                    }

                return Ok(false);
            }
        }

        [HttpGet, Route("xacdinhloaitoipham")]
        public IHttpActionResult XacDinhLoaiToiPham(List<int> dsKhoanLuatID, int? boLuat = 2015)
        {
            CommonBusiness commonBusiness = new CommonBusiness();
            int loaiToiPham = commonBusiness.XacDinhLoaiToiPham(dsKhoanLuatID, boLuat);
            return Ok(loaiToiPham);
        }
    }
}
