using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common.CoreExport;
using DocumentManagement.Models.DTO;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocumentManagement.Controllers.Export
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportDocumentController : ControllerBase
    {
        private static readonly ExportBUS exportBUS = ExportBUS.GetExportBUSInstance;
        private IHostingEnvironment _hostingEnvironment;

        public ExportDocumentController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> GetDataExportDocument([FromBody] BaseCondition<ExportDocDTO> condition)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var filterItem = new FilterItem();
            filterItem.field = "S_VanBan.NgayTao";
            filterItem.op = "and_date_between";
            if (condition.FilterRuleList.Count() == 0)
            {
                filterItem.value = startDate.ToString() + "-" + endDate.ToString();
                condition.FilterRuleList.Add(filterItem);
                return Ok(await exportBUS.GetDataExportDocument(condition));
            }
            else
            {
                var qfilteritem = condition.FilterRuleList.FirstOrDefault();
                var filters = qfilteritem.value.Split("/");
                condition.FilterRuleList[0].value = "";
                if (!string.IsNullOrEmpty(filters[0].ToString()))
                {
                    condition.FilterRuleList[0].value = Convert.ToDateTime(filters[0]).ToString();
                }
                if (!string.IsNullOrEmpty(filters[1].ToString()))
                {
                    condition.FilterRuleList[0].value = condition.FilterRuleList[0].value + "-" + Convert.ToDateTime(filters[1]).ToString();
                }
                filterItem.value = condition.FilterRuleList[0].value.ToString();
                var condi = new BaseCondition<ExportDocDTO>();
                condi.FilterRuleList.Add(filterItem);
                condi.PageIndex = condition.PageIndex;
                condi.PageSize = 5;
                return Ok(await exportBUS.GetDataExportDocument(condi));
            }
        }

        // GET: Export Gear Box
        [HttpGet]
        public async Task<FileResult> ExportDocument(DateTime? fromDate, DateTime? toDate)
        {
            //
            List<ExportDocDTO> exportDocDTOs = new List<ExportDocDTO>();
            try
            {
                exportDocDTOs = await GetData(fromDate, toDate);
            }
            catch (Exception)
            {
                exportDocDTOs = null;
            }
            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
            CreateExport(exportDocDTOs, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "DanhSachVanBan.xlsx";
            FileInfo fi = new FileInfo(fPath);
            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
            string fileName = @"DanhSachVanBan.xlsx";
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
            //return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachHoSo" + fi.Extension);
        }
        private async Task<List<ExportDocDTO>> GetData(DateTime? fromDate, DateTime? toDate)
        {

            List<ExportDocDTO> exportDocDTOs = new List<ExportDocDTO>();
            var result = await exportBUS.GetDataDocument();
            if (result.ItemList != null)
            {
                exportDocDTOs = result.ItemList;
            }
            if (!string.IsNullOrEmpty(fromDate.ToString()) && exportDocDTOs != null)
            {
                if (CheckConvertDate(fromDate.ToString()))
                {
                    exportDocDTOs = exportDocDTOs.Where(x => x.CreateTime >= fromDate).ToList();
                }
            }
            if (!string.IsNullOrEmpty(toDate.ToString()) && exportDocDTOs != null)
            {
                if (CheckConvertDate(toDate.ToString()))
                {
                    exportDocDTOs = exportDocDTOs.Where(x => x.CreateTime <= toDate).ToList();
                }
            }
            return exportDocDTOs;
        }
        private bool CheckConvertDate(string str)
        {
            try
            {
                Convert.ToDateTime(str);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void CreateExport(List<ExportDocDTO> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "DocumentCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "FileCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "DocOrdinal", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "TypeName", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Subject", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "PageAmount", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "ConfidenceLevel", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Format", WidthSize = 30 });
            lstProperty.Add(new ProperTiesName { PropsName = "Description", WidthSize = 30 });
            //Tạo đối tượng dùng để Export
            ExportCore<ExportDocDTO> exh = new ExportCore<ExportDocDTO>(4)
            {
                FileName = "DanhSachVanBan",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách văn bản"
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
                new HeaderLocation(1,3,20,"Thống kê danh sách văn bản"),
                new HeaderLocation(2,1,20,"Hồ sơ số"),new HeaderLocation(2,2,20,"Tài liệu số"),new HeaderLocation(2,3,50,"Stt trong hồ sơ"),
                new HeaderLocation(2,4,20,"Tên loại tài liệu"),new HeaderLocation(2,5,25,"Nội dung"),new HeaderLocation(2,6,30,"Số tờ")
                ,new HeaderLocation(2,7,30,"Mức độ tin cậy"),new HeaderLocation(2,8,30,"Tình trạng")
                ,new HeaderLocation(2,9,30,"Ghi chú")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
                new MergeTo(1,1,1,9)
            };
            // gán các tham số cho headInput
            headInput.ListHeader = lstHeaderLocation;
            headInput.ListMergeIndex = lstMerge;
            headInput.HeaderHeight = 4; // số hàng mà header chiếm trong excell
            return headInput;
        }
    }
}