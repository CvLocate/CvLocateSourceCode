using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine
{
    public class CvParser : ICvParser
    {
        ICvLocateLogger _logger;
        CvFileForParsing _cvFile;
        CvParsedData _cvParsedData;

        public CvParser(ICvLocateLogger logger)
        {
            _logger = logger;
        }

        public CvParsedData ParseCv(CvFileForParsing cvFile)
        {
            _logger.DebugFormat("CV file {0}: Start parsing.", cvFile.Id);

            this._cvFile = cvFile;
            this._cvParsedData = new CvParsedData();


            SeperateText();

            ExtractMoreData();

            this._logger.DebugFormat("CV file {0}: Parsing result: {1}", cvFile.Id, this._cvParsedData);

            return this._cvParsedData;
        }

        private void ExtractMoreData()
        {
            this._cvParsedData.Name = "Moshe";
            this._cvParsedData.Email = "moshe@gmail.com";
        }

        private void SeperateText()
        {
            this._cvParsedData.SeperatedTexts = new List<string>()
            {
                "bla","lab","bla"
            };
        }


    }
}
