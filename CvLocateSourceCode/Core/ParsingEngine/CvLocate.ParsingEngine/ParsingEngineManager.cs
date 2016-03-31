using CvLocate.Common.CoreDtoInterface.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CvLocate.Common.CommonDto;
using CvLocate.Common.CoreDtoInterface.Command;
using CvLocate.Common.CoreDtoInterface.Query;
using CvLocate.Common.CoreDtoInterface.Enums;
using CvLocate.Common.CoreDtoInterface.Result;


namespace CvLocate.ParsingEngine
{
    public class ParsingEngineManager : IParsingEngineManager
    {
        //todo add injection and unity - singleton

        #region Members

        IParsingEngineDataWrapper _dataWrapper;
        IParsingQueueManager _parsingQueueManager;
        ICvParser _cvParser;

        Task _parsingProcessTask; //todo replace to list of tasks for best performance
        Task _parsingProcessManagerTask;

        bool _stopProcess;
        Timer _waitingForMoreCandidatesToParseTimer;
        ParsingEngineConfiguration _configuration;

        #endregion

        #region ctor
        public ParsingEngineManager(IParsingEngineDataWrapper dataWrapper, IParsingQueueManager parsingQueueManager
            , ICvParser cvParser)
        {
            _dataWrapper = dataWrapper;
            _parsingQueueManager = parsingQueueManager;
            _cvParser = cvParser;
        }
        #endregion

        #region IParsingEngineManager Implementation

        public void Initialize()
        {
            _stopProcess = false;
            LoadConfiguration();
            StartParsingProcess();
        }

        public void Stop()
        {
            _stopProcess = true;
            //todo cancel task
            if (this._waitingForMoreCandidatesToParseTimer != null)
            {
                this._waitingForMoreCandidatesToParseTimer.Dispose();
                this._waitingForMoreCandidatesToParseTimer = null;
            }
        }

        #endregion

        #region Private Methods

        private void StartParsingProcess()
        {
            _parsingProcessManagerTask = new Task(ManageParsingProcesses);
            _parsingProcessManagerTask.Start();
        }

        private void LoadConfiguration()
        {
            _configuration = _dataWrapper.GetParsingEngineConfiguration();
        }


        private void ManageParsingProcesses()
        {
            try
            {
                if (_stopProcess)
                    return;
                //todo check how much candidates wait for parsing and decide how much tasks to start
                _parsingProcessTask = new Task(ParseWaitingCvFiles);
                _parsingProcessTask.Wait();

                WaitForMoreCandidatesForParsing();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void WaitForMoreCandidatesForParsing()
        {
            if (_stopProcess)
                return;
            this._waitingForMoreCandidatesToParseTimer = new Timer(OnWaitForMoreCandidatesToParse, null, _configuration.CheckCandidatesWaitForParsingSecondsInterval, Timeout.Infinite);
        }

        private void OnWaitForMoreCandidatesToParse(object state)
        {
            if (_stopProcess)
                return;

            StartParsingProcess();
        }

        private void ParseWaitingCvFiles()
        {
            CandidateCvFileForParsing candidateCvFileForParsing = _parsingQueueManager.GetNextCandidate();
            while (candidateCvFileForParsing != null)
            {
                ParseCandidateCvFile(candidateCvFileForParsing);

                candidateCvFileForParsing = _parsingQueueManager.GetNextCandidate();
            }
        }

        private void ParseCandidateCvFile(CandidateCvFileForParsing candidateCvFile)
        {
            try
            {

                CvParsedData parsedCv = _cvParser.ParseCv(candidateCvFile.CvFile);

                SaveResultOfCandidateParsingCommand saveCommand = BuildSaveParsingCommand(candidateCvFile, parsedCv);

                _dataWrapper.SaveResultOfCandidateParsing(saveCommand);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private SaveResultOfCandidateParsingCommand BuildSaveParsingCommand(CandidateCvFileForParsing candidateCvFileForParsing, CvParsedData parsedCv)
        {
            CvFileForParsing cvFile = candidateCvFileForParsing.CvFile;
            Candidate relatedCandidate = candidateCvFileForParsing.Candidate;

            SaveResultOfCandidateParsingCommand saveResultOfCandidateParsingCommand = new SaveResultOfCandidateParsingCommand();

            SaveParsedCvFileCommand saveParsedCvFileCommand = new SaveParsedCvFileCommand();
            saveParsedCvFileCommand.CvFile.Id = cvFile.Id;
            saveParsedCvFileCommand.SeperatedTexts = parsedCv.SeperatedTexts;
            saveParsedCvFileCommand.Text = parsedCv.Text;
            saveParsedCvFileCommand.SeperatedTexts = parsedCv.SeperatedTexts;


            SaveCandidateAfterParsingCommand saveCandiateAfterParsingCommand = null;

            if (string.IsNullOrWhiteSpace(parsedCv.Email))
            {
                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.CannotDeciphered;
                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.ParsingFailed;
                saveParsedCvFileCommand.CvFile.StatusReasonDetails = "Cannot extract email from cv file";
                saveParsedCvFileCommand.CvFile.CandidateId = null;
            }
            else
            {
                if (relatedCandidate != null) //this cv file is already connected to exising candidate
                {
                    if (relatedCandidate.Email.ToLower() != parsedCv.Email.ToLower())
                    {
                        saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                        saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                        saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.OtherEmailThenCandidate;
                        saveParsedCvFileCommand.CvFile.StatusReasonDetails = string.Format("This file belong to candidate {0} with email {1}, but contains email {2}", relatedCandidate.Id, relatedCandidate.Email, parsedCv.Email);
                    }
                    else
                    {
                        saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                        saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;

                        saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
                        {
                            CandidateId = relatedCandidate.Id,
                            MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                            Name = string.IsNullOrWhiteSpace(relatedCandidate.Name) ? parsedCv.Name : relatedCandidate.Name
                        };

                        saveResultOfCandidateParsingCommand.Commands.Add(saveCandiateAfterParsingCommand);
                    }
                }
                else
                {
                    FindCandidateResult existingCandidateWithSameEmail = _dataWrapper.FindCandidate(new FindCandidateQuery(FindCandidateBy.ByEmail, parsedCv.Email));

                    if (existingCandidateWithSameEmail == null)//this email doesn't exist in system yet
                    {
                        saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                        saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;

                        saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
                        {//create new candidate
                            CVFileId = cvFile.Id,
                            Email = parsedCv.Email,
                            Name = parsedCv.Name,
                            MatchingStatus = MatchingProcessStatus.WaitingForMatching
                        };
                        saveResultOfCandidateParsingCommand.Commands.Add(saveCandiateAfterParsingCommand);
                    }
                    else//this email already exists in system
                    {
                        if (existingCandidateWithSameEmail.Candidate.CVFileId == cvFile.Id)
                        {//A candidate uploaded this cv file
                            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;

                            saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
                            {
                                CandidateId = existingCandidateWithSameEmail.Candidate.Id,
                                Name = string.IsNullOrWhiteSpace(existingCandidateWithSameEmail.Candidate.Name) ? parsedCv.Name : existingCandidateWithSameEmail.Candidate.Name,
                                MatchingStatus = MatchingProcessStatus.WaitingForMatching
                            };
                            saveResultOfCandidateParsingCommand.Commands.Add(saveCandiateAfterParsingCommand);
                        }
                        else //there is a candidate with same email but with other cv file
                        {
                            bool replaceCvFileForExistingCandidate = false;

                            if (existingCandidateWithSameEmail.CvFile.SourceType == CandidateSourceType.System)
                            {
                                replaceCvFileForExistingCandidate = false;
                            }
                            else if (existingCandidateWithSameEmail.CvFile.Status == CvFileStatus.Deleted)
                            {
                                replaceCvFileForExistingCandidate = true;
                            }
                            else
                            {
                                //take the newest cv file
                                replaceCvFileForExistingCandidate = existingCandidateWithSameEmail.CvFile.CreatedDate < cvFile.CreatedDate;
                            }

                            if (replaceCvFileForExistingCandidate == true)
                            {
                                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
                                saveParsedCvFileCommand.CvFile.CandidateId = existingCandidateWithSameEmail.Candidate.Id;

                                saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
                                {
                                    CandidateId = existingCandidateWithSameEmail.Candidate.Id,
                                    CVFileId = cvFile.Id,
                                    Name = parsedCv.Name,
                                    MatchingStatus = MatchingProcessStatus.WaitingForMatching
                                };


                                if (existingCandidateWithSameEmail.CvFile.Status != CvFileStatus.Deleted)
                                {
                                    //delete the old cv file
                                    SaveCvFileCommand deleteOldCvFileCommand = new SaveCvFileCommand()
                                    {
                                        CvFile = existingCandidateWithSameEmail.CvFile
                                    };

                                    deleteOldCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                                    deleteOldCvFileCommand.CvFile.StatusReason = CvStatusReason.Duplicate;
                                    deleteOldCvFileCommand.CvFile.StatusReasonDetails = string.Format("The system found cv file (id: {0}) with same email {1}, so the candidate {2} will connect to the newest cv file (id: {3})"
                                                                                            , cvFile.Id,parsedCv.Email, existingCandidateWithSameEmail.Candidate.Id,cvFile.Id);

                                    saveResultOfCandidateParsingCommand.Commands.Add(deleteOldCvFileCommand);
                                }
                                saveResultOfCandidateParsingCommand.Commands.Add(saveCandiateAfterParsingCommand);
                            }
                            else//dont replace cv file for existing candidate
                            {
                                saveParsedCvFileCommand.CvFile.CandidateId = existingCandidateWithSameEmail.Candidate.Id;
                                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                                saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.Duplicate;
                                saveParsedCvFileCommand.CvFile.StatusReasonDetails = string.Format("There is already a candidate (id:{0}) with same email {1} that is already connected to other cv file (id: {2})",
                                                                                                        existingCandidateWithSameEmail.Candidate.Id, existingCandidateWithSameEmail.Candidate.Email, existingCandidateWithSameEmail.Candidate.CVFileId);
                            }
                        }

                    }

                }
            }

            saveResultOfCandidateParsingCommand.Commands.Add(saveParsedCvFileCommand);


            return saveResultOfCandidateParsingCommand;
        }

        #endregion
    }
}
