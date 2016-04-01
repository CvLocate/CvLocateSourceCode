using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine.Tester
{
    static class ZvikaTester
    {
        public static void TestCvParser()
        {
            string filePath = "../../CvFileExample.doc";
            if (File.Exists(filePath))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);

                CvFileForParsing cvFile = new CvFileForParsing()
                {
                    Id = "idblabla",
                    Extention = "docx",
                    Stream = fileBytes
                };

                CvLocateLogger logger = new CvLocateLogger();

                CvParser parser = new CvParser(new CvLocateLogger("parsingEngineLogger"));


                CvParsedData parsedData = parser.ParseCv(cvFile);

                logger.InfoFormat("parse cv file {0}: Email={1}, Name={2},  Seperated Texts count ={3},Text= {4}",
                    cvFile.Id, parsedData.Email, parsedData.Name, parsedData.SeperatedTexts.Count, parsedData.Text);

            }
        }
    }
}
