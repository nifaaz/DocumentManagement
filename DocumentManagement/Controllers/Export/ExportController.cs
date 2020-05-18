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
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var filterItem = new FilterItem();
            filterItem.field = "vb.NgayCapNhat";
            filterItem.op = "and_date_between";
            if (condition.FilterRuleList.Count() == 0)
            {
                filterItem.value = startDate.ToString() + "-" + endDate.ToString();
                condition.FilterRuleList.Add(filterItem);
                return Ok(await exportBUS.GetDataStatisticsPagingWithSearchResults(condition));
            }
            else
            {
                var qfilteritem = condition.FilterRuleList.FirstOrDefault();
                var filters = qfilteritem.value.Split("/");
                condition.FilterRuleList[0].value = "";
                if (!String.IsNullOrEmpty(filters[0].ToString()))
                {
                    condition.FilterRuleList[0].value = Convert.ToDateTime(filters[0]).ToString();
                }
                if (!String.IsNullOrEmpty(filters[1].ToString()))
                {
                    condition.FilterRuleList[0].value = condition.FilterRuleList[0].value + "-" + Convert.ToDateTime(filters[1]).ToString();
                }
                filterItem.value = condition.FilterRuleList[0].value.ToString();
                var condi = new BaseCondition<FilterDTO>();
                condi.FilterRuleList.Add(filterItem);
                condi.PageIndex = condition.PageIndex;
                condi.PageSize = 5;
                return Ok(await exportBUS.GetDataStatisticsPagingWithSearchResults(condi));
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetExportWithPaging([FromBody] BaseCondition<DocumentManagement.Models.Entity.Export> condition)
        {
            try
            {
                return Ok(await exportBUS.GetPagingWithSearchResults(condition));
            }
            catch (Exception ex)
            {
                return Ok(ex);
            }
        }

        // GET: Export Gear Box
        [HttpGet]
        public async Task<FileResult> ExportExcel(DateTime? fromDate, DateTime? toDate)
        {
            //
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            try
            {
                dataStatisticsDTOs = await GetData(fromDate,toDate);
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
        private async Task<List<DataStatisticsDTO>> GetData(DateTime? fromDate, DateTime? toDate)
        {
            List<DataStatisticsDTO> dataStatisticsDTOs = new List<DataStatisticsDTO>();
            var result = await exportBUS.GetDataStatisticss();
            if (result.ItemList != null)
            {
                dataStatisticsDTOs = result.ItemList;
            }
            if (!String.IsNullOrEmpty(fromDate.ToString()) && dataStatisticsDTOs != null)
            {
                if (CheckConvertDate(fromDate.ToString())){
                    dataStatisticsDTOs = dataStatisticsDTOs.Where(x => x.UpdateDate >= fromDate).ToList();
                }
            }
            if (!String.IsNullOrEmpty(toDate.ToString()) && dataStatisticsDTOs != null)
            {
                if (CheckConvertDate(toDate.ToString()))
                {
                    dataStatisticsDTOs = dataStatisticsDTOs.Where(x => x.UpdateDate <= toDate).ToList();
                }
            }
            return dataStatisticsDTOs;
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
        public void CreateExport(List<DataStatisticsDTO> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "FontName", WidthSize = 30 });
            lstProperty.Add(new ProperTiesName { PropsName = "TableOfNumber", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GearBoxCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "ProfileCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "FileName", WidthSize = 50 });
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
                new HeaderLocation(2,1,30,"Tên phông"),new HeaderLocation(2,2,20,"Mục lục số"),new HeaderLocation(2,3,20,"Mã hộp số"),
                new HeaderLocation(2,4,20,"Mã hồ sơ"),new HeaderLocation(2,5,50,"Tên file"),new HeaderLocation(2,6,30,"Văn bản")
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