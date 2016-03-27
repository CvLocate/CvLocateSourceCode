using AutoMapper;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.MongoDB.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.MongoDB.Common
{
    public class MongoEntitiesConverter
    {
        public CandidateEntity ConvertCandidate(Candidate candidate)
        {
            Mapper.CreateMap<Candidate, CandidateEntity>();
            return Mapper.Map<CandidateEntity>(candidate);
        }
    }
}
