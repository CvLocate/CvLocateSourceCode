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
                _parsingProcessTask = new Task(ParseWaitingCandidates);
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

        private void ParseWaitingCandidates()
        {
            Candidate candidateForParsing = _parsingQueueManager.GetNextCandidate();
            while (candidateForParsing != null)
            {
                ParseCandidate(candidateForParsing);
               
                candidateForParsing = _parsingQueueManager.GetNextCandidate();
            }
        }

        private void ParseCandidate(Candidate candidateForParsing)
        {
            try
            {
                CvFile candidateCv = _dataWrapper.GetCandidateCvFile(candidateForParsing.CVFileId);

                CvParsedData candidateParsedCv = _cvParser.ParseCv(candidateCv);

                SaveResultOfCandidateParsingCommand saveCommand = BuildSaveParsingCommand(candidateForParsing, candidateParsedCv);

                _dataWrapper.SaveResultOfCandidateParsing(saveCommand);
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        private SaveResultOfCandidateParsingCommand BuildSaveParsingCommand(Candidate candidateForParsing, CvParsedData candidateParsedCv)
        {
            List<BaseCommonCommand> savedSubCommands =new List<BaseCommonCommand>();
            candidateForParsing.Email = candidateParsedCv.Email;
            candidateForParsing.Name = candidateParsedCv.Name;
            if (string.IsNullOrWhiteSpace(candidateForParsing.Email))
            {
                candidateForParsing.ParsingStatus = ParsingProcessStatus.CannotDeciphered;
            }
            else
            {
                Candidate existingCandidate = _dataWrapper.FindCandidate(new FindCandidateQuery(FindCandidateBy.ByEmail, candidateParsedCv.Email));
                if (existingCandidate != null)
                {
                    candidateForParsing.ParsingStatus = ParsingProcessStatus.Duplicate;

                }
                else
                {
                }

                candidateForParsing.MatchingStatus = MatchingProcessStatus.WaitingForMatching;
            }

            SaveResultOfCandidateParsingCommand saveCommand = new SaveResultOfCandidateParsingCommand()
            {
                Candidate = candidateForParsing,
                CvText = candidateParsedCv.Text,
                SeperatedCvTexts = candidateParsedCv.SeperatedTexts
            };

            return saveCommand;
        }

        #endregion
    }
}
