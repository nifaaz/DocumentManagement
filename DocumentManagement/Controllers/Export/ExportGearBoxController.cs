using DocumentManagement.BUS;
using DocumentManagement.Common;
using DocumentManagement.Common.CoreExport;
using DocumentManagement.Model.Entity.GearBox;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace DocumentManagement.Controllers.Export
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportGearBoxController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public ExportGearBoxController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        [HttpGet]
        // GET: Export Gear Box
        public FileResult GearBoxExport()
        {
            //
            List<GearBox> lstGearBox = new List<GearBox>();
            try
            {
                lstGearBox = GetData();
            }
            catch (Exception)
            {
                lstGearBox = null;
            }
            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
            CreateExport(lstGearBox, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "DanhSachHopSo.xlsx";
            FileInfo fi = new FileInfo(fPath);
            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
            string fileName = @"DanhSachHopSo.xlsx"; 
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
            //return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachHopSo" + fi.Extension);
        }
        private List<GearBox> GetData()
        {
            List<GearBox> lstGearBox = new List<GearBox>();
            GearBoxBUS gearBoxBUS = new GearBoxBUS();
            var result = gearBoxBUS.GearBoxExport();
            if(result.ItemList != null)
            {
                lstGearBox = result.ItemList;
            }
            return lstGearBox;
        }
        public void CreateExport(List<GearBox> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "GearBoxName", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "ProfileCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GearBoxTitle", WidthSize = 50 });
            lstProperty.Add(new ProperTiesName { PropsName = "NumDoc", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Preservationperiod", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "Note", WidthSize = 30 });
            //Tạo đối tượng dùng để Export
            ExportCore<GearBox> exh = new ExportCore<GearBox>(4)
            {
                FileName = "DanhSachHopSo",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách hộp số"
            };
            exh.HeaderInput = CreateHeader();
            exh.RunExport();
            //
        }
        // Tạo header
        public HeaderInputs CreateHeader()
        {
            HeaderInputs headInput = new HeaderInputs();
            // Tạo danh sách header với các đầu vào(row, colum,size,text)
            List<HeaderLocation> lstHeaderLocation = new List<HeaderLocation>()
            {
                new HeaderLocation(1,1,20,"Thống kê danh sách hộp số"),
                new HeaderLocation(2,1,20,"Hộp số"),new HeaderLocation(2,2,20,"Hồ sơ số"),new HeaderLocation(2,3,50,"Tiêu đề hồ sơ"),
                new HeaderLocation(2,4,20,"Số tờ"),new HeaderLocation(2,5,25,"THBQ"),new HeaderLocation(2,6,30,"Ghi chú")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
                new MergeTo(1,1,1,6)
            };
            // gán các tham số cho headInput
            headInput.ListHeader = lstHeaderLocation;
            headInput.ListMergeIndex = lstMerge;
            headInput.HeaderHeight = 4; // số hàng mà header chiếm trong excell
            return headInput;
        }
    }
}