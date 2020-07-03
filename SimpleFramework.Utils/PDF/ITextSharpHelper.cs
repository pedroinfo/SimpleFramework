using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace SimpleFramework.Utils.PDF
{
    public static class ITextSharpHelper
    {
        public static void MergeFilesPdf(List<string> fileNames, string destinationFile)
        {
            Document document = new Document();

            using (var stream = new FileStream(destinationFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                PdfWriter.GetInstance(document, stream);
                document.Open();

                foreach (string strFileName in fileNames)
                {
                    using (var imageStream = new FileStream(strFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                    {
                        var image = iTextSharp.text.Image.GetInstance(imageStream);
                        image.Alignment = Element.ALIGN_CENTER;
                        image.SetAbsolutePosition(0, 0);
                        image.ScaleToFit(document.PageSize.Width, document.PageSize.Height);
                        document.Add(image);
                        document.NewPage();
                    }
                }
                document.Close();
            }
        }

        public static void SplitPdf()
        {

        }
    }
}
