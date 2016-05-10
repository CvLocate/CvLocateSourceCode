using CvLocate.Common.CommonDto;
using CvLocate.Common.CommonDto.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DbInterface.DBEntities
{
    public class CvFileDBEntity
    {
        public string CvFileId { get; set; }
        public string FileId { get; set; }
        public string Text { get; set; }
        public byte[] FileImage { get; set; }
        public TextEncoding FileEncoding { get; set; }
        public CvFileStatus Status { get; set; }
        public ParsingProcessStatus ParsingStatus { get; set; }

    }
}
