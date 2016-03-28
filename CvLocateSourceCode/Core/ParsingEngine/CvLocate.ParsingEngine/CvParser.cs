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
        public CvParsedData ParseCv(CvFile cvFile)
        {
            return new CvParsedData()
            {
                Text="blabla",
                Name="Moshe",
                Email="moshe@gmail.com"
            };
        }
    }
}
