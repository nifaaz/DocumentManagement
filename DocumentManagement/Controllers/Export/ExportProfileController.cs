using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Common.Common;
using DocumentManagement.BUS;
using DocumentManagement.Common.CoreExport;
using DocumentManagement.Models.Entity.Profile;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;

namespace DocumentManagement.Controllers.Export
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ExportProfileController : ControllerBase
    {
        private static readonly ExportBUS exportBUS = ExportBUS.GetExportBUSInstance;
        private IHostingEnvironment _hostingEnvironment;

        public ExportProfileController(IHostingEnvironment environment)
        {
            _hostingEnvironment = environment;
        }

        [HttpPost]
        public async Task<IActionResult> GetDataExportProfile([FromBody] BaseCondition<Profiles> condition)
        {
            DateTime now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);
            var filterItem = new FilterItem();
            filterItem.field = "hs.NgayTao";
            filterItem.op = "and_date_between";
            if (condition.FilterRuleList.Count() == 0)
            {
                filterItem.value = startDate.ToString() + "-" + endDate.ToString();
                condition.FilterRuleList.Add(filterItem);
                return Ok(await exportBUS.GetDataExportProfile(condition));
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
                var condi = new BaseCondition<Profiles>();
                condi.FilterRuleList.Add(filterItem);
                condi.PageIndex = 1;
                condi.PageSize = 5;
                return Ok(await exportBUS.GetDataExportProfile(condi));
            }
        }

        // GET: Export Gear Box
        [HttpGet]
        public async Task<FileResult> ExportProfile(DateTime? fromDate, DateTime? toDate)
        {
            //
            List<Profiles> lstProfile = new List<Profiles>();
            try
            {
                lstProfile = await GetData(fromDate, toDate);
            }
            catch (Exception)
            {
                lstProfile = null;
            }
            string sWebRootFolder = _hostingEnvironment.ContentRootPath + "\\FilesUpload";
            CreateExport(lstProfile, sWebRootFolder);
            string fPath = sWebRootFolder + "\\" + "DanhSachHoSo.xlsx";
            FileInfo fi = new FileInfo(fPath);
            IFileProvider provider = new PhysicalFileProvider(sWebRootFolder);
            string fileName = @"DanhSachHoSo.xlsx";
            IFileInfo fileInfo = provider.GetFileInfo(fileName);
            var readStream = fileInfo.CreateReadStream();
            var mimeType = "application/vnd.ms-excel";
            return File(readStream, mimeType, fileName);
            //return File(fPath, System.Net.Mime.MediaTypeNames.Application.Octet, "DanhSachHoSo" + fi.Extension);
        }
        private async Task<List<Profiles>> GetData(DateTime? fromDate, DateTime? toDate)
        {

            List<Profiles> profiles = new List<Profiles>();
            var result = await exportBUS.GetDataProfiles();
            if (result.ItemList != null)
            {
                profiles = result.ItemList;
            }
            if (!String.IsNullOrEmpty(fromDate.ToString()) && profiles != null)
            {
                if (CheckConvertDate(fromDate.ToString()))
                {
                    profiles = profiles.Where(x => x.CreateTime >= fromDate).ToList();
                }
            }
            if (!String.IsNullOrEmpty(toDate.ToString()) && profiles != null)
            {
                if (CheckConvertDate(toDate.ToString()))
                {
                    profiles = profiles.Where(x => x.CreateTime <= toDate).ToList();
                }
            }
            return profiles;
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

        public void CreateExport(List<Profiles> lst, string sWebRootFolder)
        {
            //Khởi tạo tham số đầu vào
            List<ProperTiesName> lstProperty = new List<ProperTiesName>();
            lstProperty.Add(new ProperTiesName { PropsName = "ProfileId", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "GearBoxCode", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Title", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "StartTime", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "EndTime", WidthSize = 25 });
            lstProperty.Add(new ProperTiesName { PropsName = "Maintenance", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "ProfileTypeName", WidthSize = 20 });
            lstProperty.Add(new ProperTiesName { PropsName = "Description", WidthSize = 30 });
            //Tạo đối tượng dùng để Export
            ExportCore<Profiles> exh = new ExportCore<Profiles>(4)
            {
                FileName = "DanhSachHoSo",
                LstObj = lst,
                LstProperTies = lstProperty,
                SWebRootFolder = sWebRootFolder,
                SheetName = "Danh sách hồ số"
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
                new HeaderLocation(1,3,20,"Thống kê danh sách hồ số"),
                new HeaderLocation(2,1,20,"Mã Hồ số"),new HeaderLocation(2,2,20,"Mã hộp số"),new HeaderLocation(2,3,50,"Tiêu đề hồ sơ"),
                new HeaderLocation(2,4,20,"Thời gian bắt đầu"),new HeaderLocation(2,5,25,"Thời gian kết thúc"),new HeaderLocation(2,6,30,"Thời hạn bảo quản")
                ,new HeaderLocation(2,7,30,"Loại hồ sơ"),new HeaderLocation(2,8,30,"Ghi chú")
            };
            // tạo danh sách các ô bị merge(từ hàng , từ cột, đến hàng,đến cột)
            List<MergeTo> lstMerge = new List<MergeTo>()
            {
                new MergeTo(1,1,1,8)
            };
            // gán các tham số cho headInput
            headInput.ListHeader = lstHeaderLocation;
            headInput.ListMergeIndex = lstMerge;
            headInput.HeaderHeight = 4; // số hàng mà header chiếm trong excell
            return headInput;
        }
    }
}