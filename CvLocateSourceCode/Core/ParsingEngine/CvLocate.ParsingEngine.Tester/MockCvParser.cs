using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine.Tester
{
    public class MockCvParser : ICvParser
    {
        ICvLocateLogger _logger;
        public MockCvParser(ICvLocateLogger logger)
        {
            _logger = logger;
        }


        public CvParsedData ParseCv(CvFileForParsing cvFile)
        {
            _logger.DebugFormat("CV file {0}: Start parsing.", cvFile.Id);

            CvParsedData result = new CvParsedData()
            {
                Text = "bla bla for " + cvFile.Id,
                SeperatedTexts = new List<string>() { "bla", "bla" }
            };
            int id = Int32.Parse(cvFile.Id);
            if (id <= -1000)
            {//parsing failed
                if (id <= -1500)
                    result.Text = null;
                else
                    result.SeperatedTexts.Clear();
            }
            else if (id <0)
            {//extract email failed
                result.Email = null;
            }
            else if (id <= 1000)
            {
                result.Email = Int32.Parse(cvFile.Id)/100 + "@gmail.com";
            }
            else 
            {
                result.Email = cvFile.Id + "@gmail.com";
            }

            this._logger.DebugFormat("CV file {0}: Parsing result: {1}", cvFile.Id, result);

            return result;
        }
    }
}
