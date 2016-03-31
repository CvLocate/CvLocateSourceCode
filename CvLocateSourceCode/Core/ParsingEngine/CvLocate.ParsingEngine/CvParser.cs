using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    public class CvParser:ICvParser
    {
        ICvLocateLogger _logger;
        public CvParser(ICvLocateLogger logger)
        {
            _logger = logger;
        }

        //TODO by Zvika
        public CvParsedData ParseCv(CvFileForParsing cvFile)
        {
            _logger.InfoFormat("Start parse cv file {0}", cvFile.Id);

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
