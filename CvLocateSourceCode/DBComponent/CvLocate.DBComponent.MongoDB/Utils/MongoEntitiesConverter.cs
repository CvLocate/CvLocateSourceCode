using AutoMapper;
using CvLocate.Common.EndUserDtoInterface.DTO;
using CvLocate.DBComponent.MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.MongoDB
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
