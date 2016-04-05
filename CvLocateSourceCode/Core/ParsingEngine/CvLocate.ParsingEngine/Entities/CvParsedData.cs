﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.Utils;

namespace CvLocate.ParsingEngine
{
    public class CvParsedData
    {
        public string Text { get; set; }
        public List<string> SeperatedTexts { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public CvParsedData()
        {
            SeperatedTexts = new List<string>();
        }

        public override string ToString()
        {
            return string.Format("Email: {0},\nText: {1}\nSeperatedTexts: {2}", Email, Text, SeperatedTexts.StringsListToString());
        }
    }
}
