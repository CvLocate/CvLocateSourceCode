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
using CvLocate.Common.Logging;


namespace CvLocate.ParsingEngine
{
    public class ParsingEngineManager : IParsingEngineManager
    {
        //todo add injection and unity - singleton

        #region Members

        IParsingEngineDataWrapper _dataWrapper;
        IParsingQueueManager _parsingQueueManager;
        ICvParser _cvParser;
        ICvLocateLogger _logger;

        Task _parsingProcessTask; //todo replace to list of tasks for best performance
        Task _parsingProcessManagerTask;

        bool _stopProcess;
        Timer _waitingForMoreCandidatesToParseTimer;
        ParsingEngineConfiguration _configuration;

        #endregion

        #region ctor
        public ParsingEngineManager(IParsingEngineDataWrapper dataWrapper, IParsingQueueManager parsingQueueManager
            , ICvParser cvParser, ICvLocateLogger logger)
        {
            _dataWrapper = dataWrapper;
            _parsingQueueManager = parsingQueueManager;
            _cvParser = cvParser;
            _logger = logger;
        }
        #endregion

        #region IParsingEngineManager Implementation

        public void Initialize()
        {
            this._logger.Info("CV parsing - Initialize.");

            _stopProcess = false;
            LoadConfiguration();
            StartParsingProcess();
        }

        public void Stop()
        {
            this._logger.Info("CV parsing - Stop process.");

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
            this._logger.Info("Start parsing processes.");
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
                _parsingProcessTask = new Task(ParseCvFiles);
                _parsingProcessTask.Start();
                _parsingProcessTask.Wait();

                WaitForCVFiles();


            }
            catch (Exception)
            {

                throw;
            }
        }

        private void WaitForCVFiles()
        {
            if (_stopProcess)
                return;
            this._logger.InfoFormat("CV parsing - Start waiting {0} seconds for more CV files to parse.", _configuration.CheckCandidatesWaitForParsingSecondsInterval);
            this._waitingForMoreCandidatesToParseTimer = new Timer(OnWaitForCvFiles, null, _configuration.CheckCandidatesWaitForParsingSecondsInterval, Timeout.Infinite);
        }

        private void OnWaitForCvFiles(object state)
        {
            if (_stopProcess)
                return;

            this._logger.Info("CV parsing - Check if exist more CV files to parse.");

            StartParsingProcess();
        }

        private void ParseCvFiles()
        {
            this._logger.Info("CV parsing - Start CV parsing process.");

            int parsedCvFilesCount = 0;
            CandidateCvFileForParsing candidateCvFileForParsing = _parsingQueueManager.GetNextCandidate();
            while (candidateCvFileForParsing != null)
            {
                parsedCvFilesCount++;

                ParseCvFile(candidateCvFileForParsing);

                candidateCvFileForParsing = _parsingQueueManager.GetNextCandidate();
            }

            this._logger.InfoFormat("CV parsing - Finish CV parsing process. {0} files was parsed.", parsedCvFilesCount);
        }

        private void ParseCvFile(CandidateCvFileForParsing candidateCvFile)
        {
            try
            {
                this._logger.DebugFormat("CV file {0}: Start parsing process. Details:\n{1}", candidateCvFile.CvFile.Id, candidateCvFile);

                CvParsedData parsedCv = _cvParser.ParseCv(candidateCvFile.CvFile);

                SaveResultOfCandidateParsingCommand saveCommand = BuildSaveParsingCommand(candidateCvFile, parsedCv);

                _dataWrapper.SaveResultOfCandidateParsing(saveCommand);

                this._logger.DebugFormat("CV file {0}: Finish parsing process", candidateCvFile.CvFile.Id);

            }
            catch (Exception ex)
            {
                this._logger.Error(ex.ToString());
            }
        }

        private SaveResultOfCandidateParsingCommand BuildSaveParsingCommand(CandidateCvFileForParsing candidateCvFileForParsing, CvParsedData parsedCvData)
        {
            this._logger.DebugFormat("CV file {0}: Start build save commands.", candidateCvFileForParsing.CvFile.Id);

            CvFileForParsing parsedCvFile = candidateCvFileForParsing.CvFile;
            Candidate relatedCandidateOfParsedCvFile = candidateCvFileForParsing.Candidate;


            SaveParsedCvFileCommand saveParsedCvFileCommand = new SaveParsedCvFileCommand();
            saveParsedCvFileCommand.CvFile = new CvFile(candidateCvFileForParsing.CvFile); ;
            saveParsedCvFileCommand.SeperatedTexts = parsedCvData.SeperatedTexts;


            List<BaseCommonCommand> moreCommands = null;
            if (parsedCvData.SeperatedTexts.Count == 0) //parsing failed
            {
                moreCommands = ParsingCommandsForFailedParsing(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand);
            }
            else if (string.IsNullOrWhiteSpace(parsedCvData.Email))//Cannot extract email from CV file
            {
                moreCommands = ParsingCommandsForFailedExtractEmail(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand);

            }
            else//parsing succeeded
            {
                moreCommands = ParsingCommandsForScucceedParsing(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand);
            }

            SaveResultOfCandidateParsingCommand saveResultOfCandidateParsingCommand = new SaveResultOfCandidateParsingCommand();
            if (moreCommands != null)
            {
                moreCommands.ForEach(command => saveResultOfCandidateParsingCommand.Commands.Add(command));
            }

            this._logger.DebugFormat("CV file {0}: Add 'SaveParsedCvFileCommand' command: {1}", parsedCvFile.Id, saveParsedCvFileCommand.ToString());
            saveResultOfCandidateParsingCommand.Commands.Add(saveParsedCvFileCommand);

            this._logger.DebugFormat("CV file {0}: Finish build {1} save commands. Details:\n{2}", parsedCvFile.Id, saveResultOfCandidateParsingCommand.Commands.Count, saveResultOfCandidateParsingCommand.ToString());
            return saveResultOfCandidateParsingCommand;
        }

        private List<BaseCommonCommand> ParsingCommandsForScucceedParsing(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand)
        {
            List<BaseCommonCommand> parsingCommands = null;

            if (relatedCandidateOfParsedCvFile != null) //this CV file is already connected to exising candidate
            {
                parsingCommands = ParsingCommandsForAlreadyConnectedToCandidate(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand);
            }
            else
            {
                FindCandidateResult existingCandidateWithSameEmail = _dataWrapper.FindCandidate(new FindCandidateQuery(FindCandidateBy.ByEmail, parsedCvData.Email));

                if (existingCandidateWithSameEmail.Candidate == null)//this email doesn't exist in system yet
                {
                    parsingCommands = ParsingCommandsForNewEmail(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand);
                }
                else//this email already exists in system
                {
                    parsingCommands = ParsingCommandsForExistingEmail(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);
                }

            }
            return parsingCommands;
        }

        private List<BaseCommonCommand> ParsingCommandsForExistingEmail(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            List<BaseCommonCommand> parsingCommands = null;
            if (existingCandidateWithSameEmail.Candidate.CVFileId == parsedCvFile.Id)
            {//A candidate uploaded this CV file

                parsingCommands = ParsingCommandsForCandidateConnectedToCvThatNotConnected(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);
            }
            else //there is a candidate with same email but with other CV file
            {
                parsingCommands = ParsingCommandsForCandidateWithSameEmailButOtherCvFile(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);
            }
            return parsingCommands;
        }

        private List<BaseCommonCommand> ParsingCommandsForCandidateWithSameEmailButOtherCvFile(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            bool replaceCvFileForExistingCandidate = CheckIfReplaceCvFileForExistingCandidate(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);

            List<BaseCommonCommand> parsingCommands = null;

            if (replaceCvFileForExistingCandidate == true)
            {
                parsingCommands = ParsingCommandsForReplcaeCvFileForExistingCandidate(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);
            }
            else//dont replace CV file for existing candidate
            {
                parsingCommands = ParsingCommandsForNotReplcaeCvFileForExistingCandidate(parsedCvFile, parsedCvData, relatedCandidateOfParsedCvFile, saveParsedCvFileCommand, existingCandidateWithSameEmail);
            }
            return parsingCommands;
        }

        private bool CheckIfReplaceCvFileForExistingCandidate(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            bool replaceCvFileForExistingCandidate = false;

            string log = string.Format("CV file {0}: Candidate {1} with same email {2} is connected to other CV file {3} ",
                parsedCvFile.Id, existingCandidateWithSameEmail.Candidate.Id, parsedCvData.Email, existingCandidateWithSameEmail.CvFile.Id);

            if (existingCandidateWithSameEmail.CvFile.SourceType == CvSourceType.System)
            {
                this._logger.DebugFormat(log + "with source type {0}, so delete the parsed CV file and leave the candidate with the current CV file.", existingCandidateWithSameEmail.CvFile.SourceType);
                replaceCvFileForExistingCandidate = false;
            }
            else if (existingCandidateWithSameEmail.CvFile.Status == CvFileStatus.Deleted)
            {

                this._logger.DebugFormat(log + "with status {0}, so delete this CV file and replace it with the parsed CV file.", existingCandidateWithSameEmail.CvFile.Status);

                replaceCvFileForExistingCandidate = true;
            }
            else
            {
                //take the newest CV file
                replaceCvFileForExistingCandidate = existingCandidateWithSameEmail.CvFile.CreatedDate < parsedCvFile.CreatedDate;
                if (replaceCvFileForExistingCandidate == true)
                {
                    this._logger.DebugFormat(log + "with source type {0} that is older (created date: {1}) than the parsed CV file (created date: {2}), so delete the previous CV file and replace it with the parsed CV file.",
                        existingCandidateWithSameEmail.CvFile.Status, existingCandidateWithSameEmail.CvFile.CreatedDate, parsedCvFile.CreatedDate);
                }
                else
                {
                    this._logger.DebugFormat(log + "with source type {0} that is newer (created date: {1}) than the parsed CV file (created date: {2}), so delete the parsed CV file and leave the candidate with the current CV file.",
                        existingCandidateWithSameEmail.CvFile.Status, existingCandidateWithSameEmail.CvFile.CreatedDate, parsedCvFile.CreatedDate);
                }
            }
            return replaceCvFileForExistingCandidate;
        }

        private List<BaseCommonCommand> ParsingCommandsForNotReplcaeCvFileForExistingCandidate(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            saveParsedCvFileCommand.CvFile.CandidateId = existingCandidateWithSameEmail.Candidate.Id;
            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
            saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.Duplicate;
            saveParsedCvFileCommand.CvFile.StatusReasonDetails = string.Format("There is already a candidate (id:{0}) with same email {1} that is already connected to other CV file (id: {2})",
                                                                                    existingCandidateWithSameEmail.Candidate.Id, existingCandidateWithSameEmail.Candidate.Email, existingCandidateWithSameEmail.Candidate.CVFileId);
            return null;
        }

        private List<BaseCommonCommand> ParsingCommandsForReplcaeCvFileForExistingCandidate(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            List<BaseCommonCommand> parsingCommands = new List<BaseCommonCommand>();

            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
            saveParsedCvFileCommand.CvFile.CandidateId = existingCandidateWithSameEmail.Candidate.Id;

            SaveCandidateAfterParsingCommand saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
            {
                CandidateId = existingCandidateWithSameEmail.Candidate.Id,
                CVFileId = parsedCvFile.Id,
                Name = parsedCvData.Name,
                MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                Email = existingCandidateWithSameEmail.Candidate.Email,
                CVFileImage = parsedCvFile.FileImage
            };


            if (existingCandidateWithSameEmail.CvFile.Status != CvFileStatus.Deleted)
            {
                //delete the old CV file
                SaveCvFileCommand deleteOldCvFileCommand = new SaveCvFileCommand()
                {
                    CvFile = new CvFile(existingCandidateWithSameEmail.CvFile)
                };

                deleteOldCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                deleteOldCvFileCommand.CvFile.StatusReason = CvStatusReason.Duplicate;
                deleteOldCvFileCommand.CvFile.StatusReasonDetails = string.Format("The system found CV file (id: {0}) with same email {1}, so the candidate {2} will be connect to the newer CV file (id: {3})"
                                                                        , parsedCvFile.Id, parsedCvData.Email, existingCandidateWithSameEmail.Candidate.Id, parsedCvFile.Id);

                this._logger.DebugFormat("CV file {0}: Add 'SaveCvFileCommand' command: {1}", parsedCvFile.Id, deleteOldCvFileCommand);
                parsingCommands.Add(deleteOldCvFileCommand);
            }
            this._logger.DebugFormat("CV file {0}: Add 'SaveCandidateAfterParsingCommand' command: {1}", parsedCvFile.Id, saveCandiateAfterParsingCommand);
            parsingCommands.Add(saveCandiateAfterParsingCommand);

            return parsingCommands;
        }

        private List<BaseCommonCommand> ParsingCommandsForCandidateConnectedToCvThatNotConnected(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand, FindCandidateResult existingCandidateWithSameEmail)
        {
            //bug in system
            this._logger.WarnFormat("CV file {0}: Bug in system! Candidate {1} is connected to this CV file but the CV file is not connected to him", parsedCvFile.Id, existingCandidateWithSameEmail.Candidate.Id);
            this._logger.DebugFormat("CV file {0}: Accept File - Candidate {1} already connected to this file", parsedCvFile.Id, existingCandidateWithSameEmail.Candidate.Id);

            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
            saveParsedCvFileCommand.CvFile.CandidateId = existingCandidateWithSameEmail.Candidate.Id;

            SaveCandidateAfterParsingCommand saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
            {
                CandidateId = existingCandidateWithSameEmail.Candidate.Id,
                Name = string.IsNullOrWhiteSpace(existingCandidateWithSameEmail.Candidate.Name) ? parsedCvData.Name : existingCandidateWithSameEmail.Candidate.Name,
                MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                Email = existingCandidateWithSameEmail.Candidate.Email,
                CVFileId = existingCandidateWithSameEmail.Candidate.CVFileId,
                CVFileImage = parsedCvFile.FileImage
            };
            this._logger.DebugFormat("CV file {0}: Add 'SaveCandidateAfterParsingCommand' command: {1}", parsedCvFile.Id, saveCandiateAfterParsingCommand);
            return new List<BaseCommonCommand>() { saveCandiateAfterParsingCommand };
        }

        private List<BaseCommonCommand> ParsingCommandsForNewEmail(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand)
        {
            this._logger.DebugFormat("CV file {0}: Accept File - Extracted email {1} doesn't exist in system yet, so create new candidate in system", parsedCvFile.Id, parsedCvData.Email);

            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;

            SaveCandidateAfterParsingCommand saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
            {//create new candidate
                CVFileId = parsedCvFile.Id,
                Email = parsedCvData.Email,
                Name = parsedCvData.Name,
                MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                CVFileImage = parsedCvFile.FileImage
            };
            this._logger.DebugFormat("CV file {0}: Add 'SaveCandidateAfterParsingCommand' command: {1}", parsedCvFile.Id, saveCandiateAfterParsingCommand);
            return new List<BaseCommonCommand>() { saveCandiateAfterParsingCommand };
        }

        private List<BaseCommonCommand> ParsingCommandsForAlreadyConnectedToCandidate(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand)
        {
            List<BaseCommonCommand> parsingCommands = new List<BaseCommonCommand>();

            if (relatedCandidateOfParsedCvFile.Email.ToLower() != parsedCvData.Email.ToLower())
            {
                this._logger.DebugFormat("CV file {0}: Accept File - This file is already connected to existing candidate {1} but the extracted email {2} is different than the candidate email {3}",
                    parsedCvFile.Id, relatedCandidateOfParsedCvFile.Id, parsedCvData.Email, relatedCandidateOfParsedCvFile.Email);

                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.ParsedWithWarnings;
                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
                saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.OtherEmailThenCandidate;
                saveParsedCvFileCommand.CvFile.StatusReasonDetails = string.Format("This file belong to candidate {0} with email {1}, but contains email {2}", relatedCandidateOfParsedCvFile.Id, relatedCandidateOfParsedCvFile.Email, parsedCvData.Email);
            }
            else
            {
                this._logger.DebugFormat("CV file {0}: Accept File - This file is already connected to existing candidate {1} and the extracted email {2} is same as the candidate",
                    parsedCvFile.Id, relatedCandidateOfParsedCvFile.Id, parsedCvData.Email);

                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.Parsed;
                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
            }

            SaveCandidateAfterParsingCommand saveCandiateAfterParsingCommand = new SaveCandidateAfterParsingCommand()
            {
                CandidateId = relatedCandidateOfParsedCvFile.Id,
                MatchingStatus = MatchingProcessStatus.WaitingForMatching,
                Name = string.IsNullOrWhiteSpace(relatedCandidateOfParsedCvFile.Name) ? parsedCvData.Name : relatedCandidateOfParsedCvFile.Name,
                Email = relatedCandidateOfParsedCvFile.Email,
                CVFileId = relatedCandidateOfParsedCvFile.CVFileId,
                CVFileImage = parsedCvFile.FileImage
            };

            this._logger.DebugFormat("CV file {0}: Add 'SaveCandidateAfterParsingCommand' command: {1}", parsedCvFile.Id, saveCandiateAfterParsingCommand);
            parsingCommands.Add(saveCandiateAfterParsingCommand);


            return parsingCommands;
        }

        private List<BaseCommonCommand> ParsingCommandsForFailedExtractEmail(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand)
        {
            if (relatedCandidateOfParsedCvFile == null)
            {
                this._logger.WarnFormat("CV file {0}: Delete File - Parsing didn't succeed extract email.", parsedCvFile.Id);

                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.CannotDeciphered;
                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
                saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.ParsingFailed;
                saveParsedCvFileCommand.CvFile.StatusReasonDetails = "Cannot extract email from CV file";
            }
            else //if this CV file is already connected to candidate, this candidate already has an email, so we can continue work with this cv 
            {
                this._logger.WarnFormat("CV file {0}: Accept File - Parsing didn't succeed extract email , but this CV file is already connected to existing candidate {1} with email {2}", parsedCvFile.Id, relatedCandidateOfParsedCvFile.Id, relatedCandidateOfParsedCvFile.Email);

                saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.ParsedWithWarnings;
                saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Accepted;
                saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.CannotParseEmail;
                saveParsedCvFileCommand.CvFile.StatusReasonDetails = string.Format("Parsing didn't succeed extract email , but this CV file is already connected to existing candidate {0} with email {1}", relatedCandidateOfParsedCvFile.Id, relatedCandidateOfParsedCvFile.Email);
            }
            return null;
        }

        private List<BaseCommonCommand> ParsingCommandsForFailedParsing(CvFileForParsing parsedCvFile, CvParsedData parsedCvData, Candidate relatedCandidateOfParsedCvFile, SaveParsedCvFileCommand saveParsedCvFileCommand)
        {
            this._logger.WarnFormat("CV file {0}: Delete File - Parsing failed.", parsedCvFile.Id);

            saveParsedCvFileCommand.CvFile.ParsingStatus = ParsingProcessStatus.CannotDeciphered;
            saveParsedCvFileCommand.CvFile.Status = CvFileStatus.Deleted;
            saveParsedCvFileCommand.CvFile.StatusReason = CvStatusReason.ParsingFailed;
            saveParsedCvFileCommand.CvFile.StatusReasonDetails = "Cannot get text from CV file";
            //todo - not for mvp - change candidate matching status if the parsing is failed.
            return null;
        }



        #endregion
    }
}
