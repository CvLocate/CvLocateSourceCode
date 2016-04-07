using CvLocate.Common.CommonDto;
using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.DTO;
using CvLocate.Common.CoreDtoInterface.Enums;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Result;
using CvLocate.Common.DbFacadeInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.ParsingEngine.Tester
{
    public class MockCoreDBFacade : ICoreDBFacade
    {
        bool _alreadyReturnCvFiles = false;
        public GetTopCvFilesForParsingResult GetTopCvFilesForParsing()
        {
            if (_alreadyReturnCvFiles == true)
            {
                return new GetTopCvFilesForParsingResult();
            }
            else
            {
                _alreadyReturnCvFiles = true;
            }

            IList<CandidateCvFileForParsing> cvFiles = new List<CandidateCvFileForParsing>();

            int index = -20;
            //check the case of bad cv files (the mockCvParser returns to those files empty email or empaty text)
            for (int i = index; i < index + 20; i++)
            {
                CandidateCvFileForParsing file = new CandidateCvFileForParsing()
                {
                    CvFile = new CvFileForParsing() { Extension = "docx", Id = (i * 100).ToString(), ParsingStatus = ParsingProcessStatus.WaitingForParsing, Status = CvFileStatus.New }
                };
                if (i % 2 == 0)
                {//some files are already connected to candidates
                    file.Candidate = new Candidate() { Id = i.ToString() , CVFileId = (i * 100).ToString(), Email = i.ToString() + "@gmail.com", MatchingStatus = MatchingProcessStatus.NotReadyForMatching, UpdatedAt = DateTime.Today.AddDays(i) };
                    file.CvFile.CandidateId = file.Candidate.Id;
                }

                cvFiles.Add(file);
            }
            index = 0;

            //check the case of cv files that are already connected to existing candidate
            for (int i = index; i < index + 10; i++)
            {
                CandidateCvFileForParsing file = new CandidateCvFileForParsing();
                file.Candidate = new Candidate() { Id = i.ToString(), CVFileId = (i * 100).ToString(), Email = i.ToString() + "@gmail.com", MatchingStatus = MatchingProcessStatus.NotReadyForMatching, UpdatedAt = DateTime.Today.AddDays(-i) };
                file.CvFile = new CvFileForParsing() { CandidateId = file.Candidate.Id, Extension = "docx", Id = (i * 100).ToString(), ParsingStatus = ParsingProcessStatus.WaitingForParsing, Status = CvFileStatus.New };

                cvFiles.Add(file);
            }
            index = 10;
            //check the case of cv files that are already connected to existing candidate but contains another email
            for (int i = index; i < index + 10; i++)
            {
                CandidateCvFileForParsing file = new CandidateCvFileForParsing();
                file.Candidate = new Candidate() { Id = i.ToString() , CVFileId = (i * 100).ToString(), Email = i.ToString() + "-other@gmail.com", MatchingStatus = MatchingProcessStatus.NotReadyForMatching, UpdatedAt = DateTime.Today.AddDays(-i) };
                file.CvFile = new CvFileForParsing() { CandidateId = file.Candidate.Id, Extension = "docx", Id = (i * 100).ToString(), ParsingStatus = ParsingProcessStatus.WaitingForParsing, Status = CvFileStatus.New };

                cvFiles.Add(file);
            }
            index = 20;
            //check the case of cv files that are not connected to candidate
            for (int i = index; i < index + 10; i++)
            {
                CandidateCvFileForParsing file = new CandidateCvFileForParsing()
                {
                    CvFile = new CvFileForParsing() { Extension = "docx", Id = (i * 100).ToString(), ParsingStatus = ParsingProcessStatus.WaitingForParsing, Status = CvFileStatus.New }
                };
                cvFiles.Add(file);
            }

            index = 30;
            //check the case there is a candidate in system with same email as extracted from cv file
            for (int i = index; i < index + 30; i++)
            {
                CandidateCvFileForParsing file = new CandidateCvFileForParsing()
                {
                    CvFile = new CvFileForParsing() { Extension = "docx", Id = (i * 100).ToString(), ParsingStatus = ParsingProcessStatus.WaitingForParsing, Status = CvFileStatus.New, CreatedDate = DateTime.Now.AddDays(-10) }
                };

                cvFiles.Add(file);
            }

            return new GetTopCvFilesForParsingResult(cvFiles);
        }

        public GetParsingEngineConfigurationResult GetParsingEngineConfiguration()
        {
            return new GetParsingEngineConfigurationResult(new ParsingEngineConfiguration() { CheckCandidatesWaitForParsingSecondsInterval = 5000 } );
        }

        public FindCandidateResult FindCandidate(FindCandidateQuery findCandidateQuery)
        {
            FindCandidateResult result = new FindCandidateResult();

            switch (findCandidateQuery.FindByField)
            {
                case FindCandidateBy.ByEmail:
                    int candidateId = Int32.Parse(findCandidateQuery.FindByValue.ToString().Replace("@gmail.com", string.Empty));
                    Candidate candidate = new Candidate()
                              {
                                  Id = "CandidateId " + candidateId,
                                  Email = findCandidateQuery.FindByValue.ToString()

                              };
                    if (candidateId >= 3000 && candidateId < 6000)//check the case there is a candidate in system with same email as extracted from cv file
                    {
                        if (candidateId < 3500)
                        {
                            //check the case there is already a candidate that is connected to this cv file
                            candidate.CVFileId = candidateId.ToString();
                            result.Candidate = candidate;
                        }
                        else //check the case there is a candidate with same email that is connected to another cv file
                        {

                            candidate.CVFileId = "other cv file";
                            result.Candidate = candidate;
                            if (candidateId < 4000)
                            {
                                result.CvFile = new CvFile()
                                {
                                    Id = candidate.CVFileId,
                                    SourceType = CvSourceType.System
                                };
                                if (candidateId < 4200)
                                {
                                    result.CvFile.Status = CvFileStatus.Deleted;
                                }
                            }
                            else if (candidateId < 4500)
                            {
                                result.CvFile = new CvFile()
                                {
                                    Id = candidate.CVFileId,
                                    SourceType = CvSourceType.Email,
                                    Status = CvFileStatus.Deleted
                                };
                            }
                            else if (candidateId < 6000)
                            {
                                result.CvFile = new CvFile()
                                {
                                    Id = candidate.CVFileId,
                                    SourceType = CvSourceType.Email,
                                    Status = CvFileStatus.New

                                };
                                if (candidateId < 5000)
                                {//check the case the cv file that is connected to the candidate is newer
                                    result.CvFile.CreatedDate = DateTime.Now;
                                }
                                else 
                                {//check the case the cv file that is connected to the candidate is older
                                    result.CvFile.CreatedDate = DateTime.Now.AddDays(-100);
                                }

                            }
                        }
                    }


                    break;
                default:
                    break;
            }
            return result;
        }

        public void SaveResultOfCandidateParsing(SaveResultOfCandidateParsingCommand saveCommand)
        {
            
        }
    }
}
