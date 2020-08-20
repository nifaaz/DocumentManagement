using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DocumentManagement.Common
{
    public static class FilesUtillities
    {
        public static async Task CopyFileToPhysicalDisk(IFormFile file, string filePath)
        {
            //filePaths.Add(filePath);
            //File.SetAttributes(filePath, FileAttributes.Normal);
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);
            }
        }
        public static void CopyFileToPhysicalDiskSync(IFormFile file, string filePath)
        {
            //var stream = new FileStream(filePath, FileMode.CreateNew);
            //file.CopyTo(stream);
            //stream.Dispose();
            //    stream.Close();
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                file.CopyTo(stream);
            }
        }

        public static void CopyFile(IFormFile file, string filePath)
        {
            using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                file.CopyTo(stream);
            }
        }

        public static string GetFilePath(IFormFile file)
        {
            //   string FILE_DIRECTORY_PATH = @"E:\New folder\";
            string FILE_DIRECTORY_PATH = Const.FILE_UPLOAD_DIR;
            string filePath = FILE_DIRECTORY_PATH + file.FileName;
            return filePath;
        }

        public static string ConvertImageToBase64String(string imagePath)
        {
            string base64String = "";
            try
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    string[] lstFile = Directory.GetFiles(Const.FILE_UPLOAD_DIGITAL_SIGNATURE);
                    if (lstFile.Length > 0)
                    {
                        foreach (var item in lstFile)
                        {
                            if (Path.GetFileName(item) == Path.GetFileName(imagePath))
                            {
                                base64String = "data:image/png;base64," + Convert.ToBase64String(File.ReadAllBytes(imagePath));
                                break;
                            }
                        }
                    }
                }
            }
            catch (ArgumentNullException ex)
            {
                return "";
            }
            return base64String;
        }

        public static bool InsertSignatureIntoDocument(string imagePath, string pdfPath)
        {
            string[] arrImage = imagePath.Split("/");
            string[] arrPdf = pdfPath.Split("/");
            string imagePathStream = Path.Combine(Const.FILE_UPLOAD_DIGITAL_SIGNATURE, arrImage[arrImage.Length - 1]);
            string pdfPathStream = Path.Combine(Const.FILE_UPLOAD_DIR, arrPdf[arrPdf.Length - 2], arrPdf[arrPdf.Length - 1]);
            using (Stream inputPdfStream = new FileStream(pdfPathStream, FileMode.Open, FileAccess.Read, FileShare.Read))
            using (Stream inputImageStream = new FileStream(imagePathStream, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    var reader = new PdfReader(inputPdfStream);
                    Rectangle pageSize = reader.GetPageSizeWithRotation(1);

                    // xóa file đã tồn tại trước đó
                    //File.Delete(pdfPathStream);

                    using (Stream outputPdfStream = new FileStream(pdfPathStream, FileMode.Open, FileAccess.Write, FileShare.None))
                    {
                        var stamper = new PdfStamper(reader, outputPdfStream);
                        var pdfContentByte = stamper.GetOverContent(1);
                        Image image = Image.GetInstance(inputImageStream);
                        image.SetAbsolutePosition(50, pageSize.Height - image.Height - 25);
                        pdfContentByte.AddImage(image);
                        stamper.Close();
                    }
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
