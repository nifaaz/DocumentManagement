using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common.CoreExport;
using DocumentManagement.Models.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocumentManagement.Controllers.Export
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportController : ControllerBase
    {
        private static readonly ExportBUS exportBUS = ExportBUS.GetExportBUSInstance;
        private readonly IHostingEnvironment _hostingEnvironment;

        public ExportController(IHostingEnvironment environment)
        {
            this._hostingEnvironment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> GetDataStatisticsPagingWithSearchResults([FromBody] BaseCondition<FilterDTO> condition)
        {
            try
            {
                return Ok(exportBUS.GetDataStatisticsPagingWithSearchResults(condition));
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetExportWithPaging([FromBody] BaseCondition<DocumentManagement.Models.Entity.Export> condition)
        {
            try
            {
                return Ok(exportBUS.GetPagingWithSearchResults(condition));
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        // GET: Export Gear Box
        [HttpGet]
        public async Task<FileResult> ExportExcel()
        {
            //
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            try
            {
                dataStatisticsDTOs = GetData();
            }
            catch (Exception)
            {
                dataStatisticsDTOs = null;
            }
            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
            CreateExport(dataStatisticsDTOs, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "ThongKeTongQuat.xlsx";
            FileInfo fi = new FileInfo(fPath);
            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
            string fileName = @"ThongKeTongQuat.xlsx";
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
            //return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "ThongKeTongQuat" + fi.Extension);
        }
        private List<DataStatisticsDTO> GetData()
        {
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            var result = exportBUS.GetDataStatisticss();
            if (result.ItemList != null)
            {
                dataStatisticsDTOs = result.ItemList;
            }
            return dataStatisticsDTOs;
        }
        public void CreateExport(List<DataStatisticsDTO> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "FontName", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "TableOfName", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GearBoxCode", WidthSize = 50 });
            lstProperty.Add(new ProperTiesName { PropsName = "ProfileCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "FileName", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "UpdateDate", WidthSize = 30 });
            //Tạo đối tượng dùng để Export
            ExportCore<DataStatisticsDTO> exh = new ExportCore<DataStatisticsDTO>(4)
            {
                FileName = "ThongKeTongQuat",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Thống kê"
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
                new HeaderLocation(1,1,20,"Thống kê dữ liệu"),
                new HeaderLocation(2,1,20,"Tên phông"),new HeaderLocation(2,2,20,"Danh mục"),new HeaderLocation(2,3,50,"Mã hộp số"),
                new HeaderLocation(2,4,20,"Mã hồ sơ"),new HeaderLocation(2,5,25,"Tên file"),new HeaderLocation(2,6,30,"Văn bản")
                ,new HeaderLocation(2,7,30,"Ngày cập nhật")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
                new MergeTo(1,1,1,7)
            };
            // gán các tham số cho headInput
            headInput.ListHeader = lstHeaderLocation;
            headInput.ListMergeIndex = lstMerge;
            headInput.HeaderHeight = 4; // số hàng mà header chiếm trong excell
            return headInput;
        }
    }
}