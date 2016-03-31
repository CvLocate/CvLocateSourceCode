using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    class CvParser:ICvParser
    {
        //TODO by Zvika
        public CvParsedData ParseCv(CvFileForParsing cvFile)
        {
            string cvText = ExtractText(cvFile.Stream);
            List<string> seperatedCvTexts = SeperateText(cvText);

            CvParsedData cvParsedData = new CvParsedData()
            {
                Text = cvText,
                SeperatedTexts=seperatedCvTexts
            };

            ExtractMoreData(cvParsedData);

            return cvParsedData;
        }

        private void ExtractMoreData(CvParsedData cvParsedData)
        {
            cvParsedData.Name = "Moshe";
            cvParsedData.Email = "moshe@gmail.com";
        }

        private List<string> SeperateText(string cvText)
        {
            return new List<string>()
            {
                "bla","lab","bla"
            };
        }

        private string ExtractText(byte[] p)
        {
            return "blabla";
        }
    }
}
