//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;

//namespace DocumentManagement.Controllers.Export
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExportVanBanController : ControllerBase
//    {
//        private IHostingEnvironment _hostingC:\Users\ThinkPro\Desktop\QuanLyLuuTruVaSoHoa\DocumentManagement\DocumentManagement\Controllers\Export\ExportVanBanController.csEnvironment;

//        public ExportVanBanController(IHostingEnvironment environment)
//        {
//            _hostingEnvironment = environment;
//        }
//        // GET: Export Gear Box
          //[HttpGet]
//        public FileResult ProfileExport(DateTime? tuNgay, DateTime? denNgay, int? donViID)
//        {
//            //
//            List<Profile> lstProfile = new List<Profile>();
//            try
//            {
//                lstProfile = GetData(tuNgay, denNgay, donViID);
//            }
//            catch (Exception)
//            {
//                lstProfile = null;
//            }
//            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
//            CreateExport(lstProfile, sWebRootFolder);
//            string fPath = sWebRootFolder + "\\" + "DanhSachHoSo.xlsx";
//            FileInfo fi = new FileInfo(fPath);
//            return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachHoSo" + fi.Extension);
//        }
//        private List<Profile> GetData(DateTime? tuNgay, DateTime? denNgay, int? donViID)
//        {
//            List<Profile> lstProfile = new List<Profile>();
//            ProfileBUS profileBUS = new ProfileBUS();
//            var result = profileBUS.GetAllProfile();
//            if (result.ItemList != null)
//            {
//                lstProfile = result.ItemList;
//            }
//            return lstProfile;
//        }
//        public void CreateExport(List<Profile> lst, string sWebRootFolder)
//        {
//            //Khởi tạo tham số đầu vào
//            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
//            lstProperty.Add(new ProperTiesName { PropsName = "MaHoSo", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "MaHopSo", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "TieuDeHoSo", WidthSize = 25 });
//            lstProperty.Add(new ProperTiesName { PropsName = "ThoiGianBatDau", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "ThoiGianKetThuc", WidthSize = 25 });
//            lstProperty.Add(new ProperTiesName { PropsName = "THBQ", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "LoaiHoSo", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "GhiChu", WidthSize = 30 });
//            //Tạo đối tượng dùng để Export
//            ExportCore<Profile> exh = new ExportCore<Profile>(5)
//            {
//                FileName = "DanhSachHoSo",
//                LstObj = lst,
//                LstProperTies = lstProperty,
//                SWebRootFolder = sWebRootFolder,
//                SheetName = "Danh sách hồ số"
//            };
//            exh.HeaderInput = CreateHeader();
//            exh.RunExport();
//            //
//        }
//        // Tạo header
//        public HeaderInputs CreateHeader()
//        {
//            HeaderInputs headInput = new HeaderInputs();
//            // Tạo danh sách header với các đầu vào(row, colum,size,text)
//            List<HeaderLocation> lstHeaderLocation = new List<HeaderLocation>()
//            {
//                new HeaderLocation(1,3,20,"Thống kê danh sách hồ số"),
//                new HeaderLocation(2,1,20,"Mã Hồ số"),new HeaderLocation(2,2,20,"Mã hộp số"),new HeaderLocation(2,3,50,"Tiêu đề hồ sơ"),
//                new HeaderLocation(2,4,20,"Thời gian bắt đầu"),new HeaderLocation(2,5,25,"Thời gian kết thúc"),new HeaderLocation(2,6,30,"Thời hạn bảo quản")
//                ,new HeaderLocation(2,7,30,"Loại hồ sơ"),new HeaderLocation(2,8,30,"Ghi chú")
//            };
//            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
//            List<MergeTo> lstMerge = new List<MergeTo>()
//            {
//                new MergeTo(1,1,1,8)
//            };
//            // gán các tham số cho headInput
//            headInput.ListHeader = lstHeaderLocation;
//            headInput.ListMergeIndex = lstMerge;
//            headInput.HeaderHeight = 5; // số hàng mà header chiếm trong excell
//            return headInput;
//        }
//    }
//}