//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Threading.Tasks;
//using DocumentManagement.BUS;
//using DocumentManagement.Common.CoreExport;
//using DocumentManagement.Model.Entity.TableOfContens;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.FileProviders;

//namespace DocumentManagement.Controllers.Export
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ExportTabOfContController : ControllerBase
//    {
//        private IHostingEnvironment _hostingEnvironment;

//        public ExportTabOfContController(IHostingEnvironment environment)
//        {
//            _hostingEnvironment = environment;
//        }
//        // GET: Export Gear Box
//        public FileResult ExportTabOfCont()
//        {
//            //
//            List<TableOfContents> lstFont = new List<TableOfContents>();
//            try
//            {
//                lstFont = GetData();
//            }
//            catch (Exception)
//            {
//                lstFont = null;
//            }
//            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
//            CreateExport(lstFont, sWebRootFolder);
//            string fPath = sWebRootFolder + "\\" + "DanhSachMucLucHoSo.xlsx";
//            FileInfo fi = new FileInfo(fPath);
//            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
//            string fileName = @"DanhSachMucLucHoSo.xlsx";
//            IFileInfo fileInfo = provider.GetFileInfo(fileName);
//            var readStream = fileInfo.CreateReadStream();
//            var mimeType = "application/vnd.ms-excel";
//            return File(readStream, mimeType, fileName);
//        }
//        private List<TableOfContents> GetData()
//        {
//            List<TableOfContents> lstFont = new List<TableOfContents>();
//            TableOfContentsBUS tabOfContBUS = new TableOfContentsBUS();
//            var result = tabOfContBUS.FontExport();
//            if (result.ItemList != null)
//            {
//                lstFont = result.ItemList;
//            }
//            return lstFont;
//        }
//        public void CreateExport(List<TableOfContents> lst, string sWebRootFolder)
//        {
//            //Khởi tạo tham số đầu vào
//            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
//            lstProperty.Add(new ProperTiesName { PropsName = "OrganID", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "FontID", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "FontName", WidthSize = 50 });
//            lstProperty.Add(new ProperTiesName { PropsName = "History", WidthSize = 20 });
//            lstProperty.Add(new ProperTiesName { PropsName = "Lang", WidthSize = 25 });
//            lstProperty.Add(new ProperTiesName { PropsName = "Note", WidthSize = 30 });
//            //Tạo đối tượng dùng để Export
//            ExportCore<TableOfContents> exh = new ExportCore<TableOfContents>(4)
//            {
//                FileName = "DanhSachMucLucHoSo",
//                LstObj = lst,
//                LstProperTies = lstProperty,
//                SWebRootFolder = sWebRootFolder,
//                SheetName = "Danh sách mục lục hồ sơ"
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
//                new HeaderLocation(1,1,20,"Thống kê danh sách mục lục hồ sơ"),
//                new HeaderLocation(2,1,20,"Mã danh mục"),new HeaderLocation(2,2,20,"Tên danh mục"),new HeaderLocation(2,3,50,"Hộp số"),
//                new HeaderLocation(2,4,20,"Hồ sơ số"),new HeaderLocation(2,5,25,"Số lượng hộp số"),new HeaderLocation(2,6,30,"Tên kho")
//            };
//            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
//            List<MergeTo> lstMerge = new List<MergeTo>()
//            {
//                new MergeTo(1,1,1,6)
//            }; 
//            // gán các tham số cho headInput
//            headInput.ListHeader = lstHeaderLocation;
//            headInput.ListMergeIndex = lstMerge;
//            headInput.HeaderHeight = 4; // số hàng mà header chiếm trong excell
//            return headInput;
//        }
//    }
//}