using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//using iTextSharp.text.pdf;
//using iTextSharp.text.pdf.parser;
using System.IO;

namespace DocumentConverter
{

    public class PdfConverter : IDocumentConverter
    {

        public string ConvertFileToText(string filePath)
        {
            //need install ifilter
            StringBuilder result = new StringBuilder();
            new FilterLibrary.FilterCode().GetTextFromDocument(filePath, ref result);
            if (result.Length == 0)
                return string.Empty;
            //because IFilter return hebrew word in reverse, we replace the result
            string sResult = string.Join(string.Empty, result.ToString().Reverse());
            //reverse back the email
            var untilAtMail = sResult.TakeWhile(c => c != '@');
            if (sResult.Length > untilAtMail.Count())
            {
                var mailEnd = sResult.Skip(untilAtMail.Count()).TakeWhile(c => c != ' ');
                string mailStart = "";
                for (int i = untilAtMail.Count() - 1; i >= 0; i--)
                {
                    var currentChar = untilAtMail.ElementAt(i);
                    if (currentChar == ' ')
                        break;
                    mailStart += currentChar;

                }
                mailStart = string.Join(string.Empty, mailStart.Reverse());

                string reveresedMail = mailStart + string.Join(string.Empty, mailEnd);
                sResult = sResult.Replace(reveresedMail, string.Join(string.Empty, reveresedMail.Reverse()));

            }
            

            return sResult;

            //free
            //PdfReader reader = new PdfReader(filePath);
            //string text = string.Empty;
            //for (int page = 1; page <= reader.NumberOfPages; page++)
            //{
            //    ITextExtractionStrategy its = new LocationTextExtractionStrategy();

            //    string s = PdfTextExtractor.GetTextFromPage(reader, page, its);
            //    s = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(s)));
            //    text += s;
            //}
            //reader.Close();
            //return text;


            //150$
            //byte[] pdf = ReadBytesFromFile(filePath);
            //string text = "";

            ////Convert PDF to text in memory
            //SautinSoft.PdfFocus f = new SautinSoft.PdfFocus();
            ////this property is necessary only for registered version
            ////f.Serial = "XXXXXXXXXXX";

            //f.OpenPdf(pdf);

            //if (f.PageCount > 0)
            //{
            //    text = f.ToText(1, 1);



            //}

            //return text;


            //Cost 500$ - Winnovative

            //// the pdf file to converter 
            //string pdfFileName = filePath;

            //// get PDF pages range to be extracted. 0 for end page number
            //// means to extract all the pages from start page to the end
            //// of the document    
            //int startPageNumber = 1;
            //int endPageNumber = 3;

            //// the output text layout
            //TextLayout textLayout = TextLayout.ReadingLayout;

            //// the html output options
            //bool htmlEnabled = false;
            //string htmlCharset = "utf-8";

            //// the output text encoding
            //System.Text.Encoding textEncoding = System.Text.Encoding.UTF8;
            ////if (radioButtonLatin.Checked)
            ////    textEncoding = System.Text.Encoding.GetEncoding("iso-8859-1");
            ////else if (radioButtonAscii.Checked)
            ////    textEncoding = System.Text.Encoding.ASCII;

            //// page breaks
            //bool markPageBreaks = true;

            ////optional user password to open the PDF document
            //string password = null;

            //// get the license key
            //// in demo mode only the first 2 pages of the PDF document will be converted
            //string licenseKey = null;


            //// create the converter object and set the user options

            //PdfToTextConverter pdfToTextConverter = new PdfToTextConverter();

            //if (licenseKey != null)
            //    pdfToTextConverter.LicenseKey = licenseKey;

            //pdfToTextConverter.Layout = textLayout;
            //pdfToTextConverter.StartPageNumber = startPageNumber;
            //pdfToTextConverter.EndPageNumber = endPageNumber;
            //if (htmlEnabled)
            //{
            //    pdfToTextConverter.AddHtmlMetaTags = true;
            //    pdfToTextConverter.HtmlCharset = htmlCharset;
            //}
            //pdfToTextConverter.MarkPageBreaks = markPageBreaks;
            //if (password != null)
            //    pdfToTextConverter.UserPassword = password;


            //// extract text from PDF
            //string extractedText = pdfToTextConverter.ConvertToText(pdfFileName);
            //return extractedText;

        }
        public static byte[] ReadBytesFromFile(string fileName)
        {
            byte[] buff = null;
            try
            {

                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fs);
                long numBytes = new FileInfo(fileName).Length;
                buff = br.ReadBytes((int)numBytes);
            }
            catch { }
            return buff;
        }
        //public class TextWithFontExtractionStategy : iTextSharp.text.pdf.parser.ITextExtractionStrategy
        //{

        //    //HTML buffer
        //    private StringBuilder result = new StringBuilder();

        //    //Store last used properties
        //    private Vector lastBaseLine;
        //    private string lastFont;
        //    private float lastFontSize;

        //    //http://api.itextpdf.com/itext/com/itextpdf/text/pdf/parser/TextRenderInfo.html
        //    private enum TextRenderMode
        //    {
        //        FillText = 0,
        //        StrokeText = 1,
        //        FillThenStrokeText = 2,
        //        Invisible = 3,
        //        FillTextAndAddToPathForClipping = 4,
        //        StrokeTextAndAddToPathForClipping = 5,
        //        FillThenStrokeTextAndAddToPathForClipping = 6,
        //        AddTextToPaddForClipping = 7
        //    }



        //    public void RenderText(iTextSharp.text.pdf.parser.TextRenderInfo renderInfo)
        //    {
        //        // string curFont = renderInfo.GetFont().PostscriptFontName;
        //        // //Check if faux bold is used
        //        // if ((renderInfo.GetTextRenderMode() == (int)TextRenderMode.FillThenStrokeText))
        //        // {
        //        //     curFont += "-Bold";
        //        // }

        //        // //This code assumes that if the baseline changes then we're on a newline
        //        // Vector curBaseline = renderInfo.GetBaseline().GetStartPoint();
        //        // Vector topRight = renderInfo.GetAscentLine().GetEndPoint();
        //        // iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(curBaseline[Vector.I1], curBaseline[Vector.I2], topRight[Vector.I1], topRight[Vector.I2]);
        //        // Single curFontSize = rect.Height;

        //        // //See if something has changed, either the baseline, the font or the font size
        //        // if ((this.lastBaseLine == null) || (curBaseline[Vector.I2] != lastBaseLine[Vector.I2]) || (curFontSize != lastFontSize) || (curFont != lastFont))
        //        // {
        //        //     //if we've put down at least one span tag close it
        //        //     if ((this.lastBaseLine != null))
        //        //     {
        //        //        // this.result.AppendLine("</span>");
        //        //     }
        //        //     //If the baseline has changed then insert a line break
        //        //     if ((this.lastBaseLine != null) && curBaseline[Vector.I2] != lastBaseLine[Vector.I2])
        //        //     {
        //        //        // this.result.AppendLine("<br />");
        //        //     }
        //        //     //Create an HTML tag with appropriate styles
        //        //    // this.result.AppendFormat("<span style=\"font-family:{0};font-size:{1}\">", curFont, curFontSize);
        //        // }

        //        // //Append the current text
        //        //// this.result.Append(renderInfo.GetText());
        //        var text = renderInfo.GetText();
        //        text = String.Join(string.Empty, text.Reverse());
        //        this.result.Append(text);

        //        ////Set currently used properties
        //        //this.lastBaseLine = curBaseline;
        //        //this.lastFontSize = curFontSize;
        //        //this.lastFont = curFont;
        //    }

        //    public string GetResultantText()
        //    {
        //        //If we wrote anything then we'll always have a missing closing tag so close it here
        //        if (result.Length > 0)
        //        {
        //            result.Append("</span>");
        //        }
        //        return result.ToString();
        //    }

        //    //Not needed
        //    public void BeginTextBlock() { }
        //    public void EndTextBlock() { }
        //    public void RenderImage(ImageRenderInfo renderInfo) { }
        //}

    }
}
