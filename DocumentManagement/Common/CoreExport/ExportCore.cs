using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace DocumentManagement.Common.CoreExport
{
    public class ProperTiesName
    {
        public int WidthSize { get; set; }
        public string PropsName { get; set; }
    }
    public class MergeTo
    {
        public int StartRow { get; set; }
        public int StartCol { get; set; }
        public int EndRow { get; set; }
        public int EndCol { get; set; }
        public MergeTo(int startRow, int startCol, int endRow, int endCol)
        {
            this.StartRow = startRow;
            this.StartCol = startCol;
            this.EndCol = endCol;
            this.EndRow = endRow;
        }
    }
    //public class MergeTo1
    //{
    //    public int StartRow { get; set; }
    //    public int StartCol { get; set; }
    //    public int EndRow { get; set; }
    //    public int EndCol { get; set; }
    //    public MergeTo1(int startRow, int startCol, int endRow, int endCol)
    //    {
    //        this.StartRow = startRow;
    //        this.StartCol = startCol;
    //        this.EndCol = endCol;
    //        this.EndRow = endRow;
    //    }
    //}
    public class HeaderLocation
    {
        public int Row { get; set; }
        public int Col { get; set; }
        public int WidthSize { get; set; }
        public string HeaderText { get; set; }
        public HeaderLocation(int row, int col, int width, string headertext)
        {
            this.Row = row;
            this.Col = col;
            this.WidthSize = width;
            this.HeaderText = headertext;
        }

    }
    public class HeaderInputs
    {
        public List<HeaderLocation> ListHeader { get; set; }
        public List<MergeTo> ListMergeIndex { get; set; }
        public int HeaderHeight { get; set; }
    }
    public class ExportCore<T>
    {
        public string FileName { get; set; }
        public string SWebRootFolder { get; set; }
        public string SheetName { get; set; }
        public List<T> LstObj { get; set; }
        public List<ProperTiesName> LstProperTies { get; set; }
        public ExcelWorksheet Worksheet { get; set; }
        public string[,] ExcelValuesResult { get; set; }
        public HeaderInputs HeaderInput { get; set; }
        public int HeaderHeight { get; set; }
        public ExportCore(int headerHeight = 2)
        {
            HeaderHeight = headerHeight;
        }
        private FileInfo CreateFile()
        {
            string sFileName = this.FileName + @".xlsx";
            FileInfo file = new FileInfo(Path.Combine(SWebRootFolder, sFileName));
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(Path.Combine(SWebRootFolder, sFileName));
            }
            return file;
        }
        private void CreateHeader()
        {
            foreach (HeaderLocation head in HeaderInput.ListHeader)
            {
                Worksheet.Cells[head.Row, head.Col].Value = head.HeaderText;
                Worksheet.Cells[head.Row, head.Col].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
                Worksheet.Cells[head.Row, head.Col].Style.Font.SetFromFont(new Font("Times New Roman", 12));
                Worksheet.Cells[head.Row, head.Col].Style.Font.Bold = true;
                Worksheet.Cells[head.Row, head.Col].Style.WrapText = true;
                Worksheet.Cells[head.Row, head.Col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                //Worksheet.Cells[head.Row, head.Col].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);
                Worksheet.Column(head.Col).Width = head.WidthSize;
            }
            // Merge file
            foreach (MergeTo merge in HeaderInput.ListMergeIndex)
            {
                Worksheet.Cells[merge.StartRow, merge.StartCol, merge.EndRow, merge.EndCol].Merge = true;
            }
        }
        private void CreateType(ExcelWorksheet Worksheet)
        {
            for (int i = HeaderHeight; i < (LstObj.Count + HeaderHeight); i++)
            {
                Worksheet.Row(i).Height = 30;
                for (int j = 1; j <= LstProperTies.Count; j++)
                {
                    Worksheet.Cells[i, j].Style.WrapText = true;
                }
            }
            Worksheet.Cells[Worksheet.Dimension.Address].Style.Font.Name = "Times New Roman";
            Worksheet.Cells[Worksheet.Dimension.Address].Style.VerticalAlignment = ExcelVerticalAlignment.Top;
        }
        private Object GetObject(Object obj, string prName)
        {
            if (obj == null) return null;
            if (obj.GetType().GetProperty(prName) == null)
            {
                return null;
            }
            else
                return obj.GetType().GetProperty(prName).GetValue(obj); ;
        }
        private string GetStringResultByArr(dynamic lstObj, string[] arr, int index)
        {
            string strResult = "";
            foreach (Object obx in lstObj)
            {
                Object ob = new Object();
                Object temp = obx;
                for (int i = index + 1; i < arr.Length; i++)
                {
                    ob = GetObject(temp, arr[i]);
                    temp = ob;
                }
                if (ob.GetType().Name == "String" || ob.GetType().Name == "Int32")
                {
                    strResult += ob.ToString() + Environment.NewLine;
                }
            }
            return strResult;
        }
        private string GetValueByPName(Object obj, string eh)
        {
            string[] arr = eh.Split('.');
            Object ob = new Object();
            Object temp = obj;
            for (int i = 0; i < arr.Length; i++)
            {
                ob = GetObject(temp, arr[i]);
                temp = ob;
                if (ob != null)
                {
                    if (ob.GetType().Name.Contains("HashSet"))
                    {
                        dynamic lstValue = ob as dynamic;
                        return GetStringResultByArr(lstValue, arr, i);
                    }
                }
                else
                {
                    return "";
                }
            }
            string typeName = ob.GetType().Name;
            if (typeName == "String" || typeName == "Int32" || typeName == "DateTime"|| typeName == "float" || typeName == "Single")
            {
                if (ob !=null && !ob.Equals(""))
                {
                    if (typeName == "DateTime")
                    {
                        if (((DateTime)ob).ToString("dd/M/yyyy").Equals("01/1/0001"))
                        {
                            return null;
                        }
                        else
                            return ((DateTime)ob).ToString("dd/M/yyyy");
                    }
                }
                return ob.ToString();
            }
            return "";
        }
        public void RunExport()
        {
            using (ExcelPackage package = new ExcelPackage(CreateFile()))
            {
                if(LstObj != null)
                {
                    ExcelValuesResult = new string[LstObj.Count, LstProperTies.Count];
                }
                int j = 0;
                this.Worksheet = package.Workbook.Worksheets.Add(SheetName);
                CreateHeader();
                Worksheet.View.FreezePanes(HeaderInput.HeaderHeight, 1);
                int i = 0;
                foreach (Object obj in LstObj)
                {
                    foreach (ProperTiesName eh in LstProperTies)
                    {
                        string strResult = GetValueByPName(obj, eh.PropsName);
                        ExcelValuesResult[i, j] = strResult;
                        j++;
                    }

                    i++;
                    j = 0;
                }
                SaveExport(package, Worksheet);
            }
        }
        private void SaveExport(ExcelPackage package, ExcelWorksheet Worksheet)
        {
            Worksheet.SelectedRange[HeaderInput.HeaderHeight, 1,LstObj.Count + 2*HeaderHeight, LstProperTies.Count].Value = ExcelValuesResult;
            Console.Write(Worksheet);
            CreateType(Worksheet);
            package.Save();
        }


    }
}