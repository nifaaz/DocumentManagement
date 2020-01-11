using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DocumentManagement.BUS;
using DocumentManagement.Common.CoreExport;
using DocumentManagement.Model.Entity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocumentManagement.Controllers.Export
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportFontController : ControllerBase
    {
        private IHostingEnvironment _hostingEnvironment;

        public ExportFontController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }
        // GET: Export Gear Box
        public FileResult FontExport()
        {
            //
            List<Font> lstFont = new List<Font>();
            try
            {
                lstFont = GetData();
            }
            catch (Exception)
            {
                lstFont = null;
            }
            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
            CreateExport(lstFont, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "DanhSachPhong.xlsx";
            FileInfo fi = new FileInfo(fPath);
            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
            string fileName = @"DanhSachPhong.xlsx";
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
            //return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachPhong" + fi.Extension);
        }
        private List<Font> GetData()
        {
            List<Font> lstFont = new List<Font>();
            FontBUS fontBUS = new FontBUS();
            var result = fontBUS.FontExport();
            if (result.ItemList != null)
            {
                lstFont = result.ItemList;
            }
            return lstFont;
        }
        public void CreateExport(List<Font> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "OrganID", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "FontID", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "FontName", WidthSize = 50 });
            lstProperty.Add(new ProperTiesName { PropsName = "History", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Lang", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "Note", WidthSize = 30 });
            //Tạo đối tượng dùng để Export
            ExportCore<Font> exh = new ExportCore<Font>(4)
            {
                FileName = "DanhSachPhong",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách phông"
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
                new HeaderLocation(1,1,20,"Thống kê danh sách phông"),
                new HeaderLocation(2,1,20,"Mã cơ quan"),new HeaderLocation(2,2,20,"Mã phông"),new HeaderLocation(2,3,50,"Tên phông"),
                new HeaderLocation(2,4,20,"Lịch sử"),new HeaderLocation(2,5,25,"Ngôn ngữ"),new HeaderLocation(2,6,30,"Ghi chú")
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