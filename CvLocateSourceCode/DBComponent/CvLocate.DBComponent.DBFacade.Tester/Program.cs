using CvLocate.Common.CommonDto;
using CvLocate.Common.CvFilesScannerDtoInterface.Command;
using CvLocate.Common.CvFilesScannerDtoInterface.Result;
using CvLocate.Common.EmailServerDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface;
using CvLocate.Common.EndUserDtoInterface.Command;
using CvLocate.Common.EndUserDtoInterface.Response;
using CvLocate.DBComponent.EmailServerDBFacade;
using CvLocate.DBComponent.EndUserDBFacade;
using CvLocate.DBComponent.FilesDBFacade;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CvLocate.DBComponent.DBFacade.Tester
{
    class Program
    {
        static void Main(string[] args)
        {

            //RecruiterDBFacadeTester();
            CvFilesTester();


            Console.ReadKey();
        }

        private static void RecruiterDBFacadeTester()
        {
            Users userDbFacade = new Users();
            Recruiters recruiterDbFacade = new Recruiters();

            //SignResponse response = userDbFacade.SignIn(new SigninCommand() { Email = "rachelifff@gmail.com", Password = "1234567" });
            SignResponse response = userDbFacade.SignUp(new SignUpCommand() { Email = "nnn@gmail.com", Password = "1234567", UserType = UserType.Recruiter });

            //byte[] recruiterImage;
            //using (FileStream fsSource = new FileStream(@"C:\Users\InternetMatrix\Documents\t.jpg", FileMode.Open, FileAccess.Read))
            //{

            //    // Read the source file into a byte array.
            //    recruiterImage = new byte[fsSource.Length];
            //    int numBytesToRead = (int)fsSource.Length;
            //    int numBytesRead = 0;
            //    while (numBytesToRead > 0)
            //    {
            //        // Read may return anything from 0 to numBytesToRead.
            //        int n = fsSource.Read(recruiterImage, numBytesRead, numBytesToRead);

            //        // Break when the end of the file is reached.
            //        if (n == 0)
            //            break;

            //        numBytesRead += n;
            //        numBytesToRead -= n;
            //    }
            //    numBytesToRead = recruiterImage.Length;
            //}



            //Recruiter rec = recruiterDbFacade.GetRecruiterProfile(new Common.EndUserDtoInterface.Query.GetRecruiterProfileQuery(response.UserId)).Recruiter;

            //recruiterDbFacade.UpdateRecruiterProfile(new UpdateRecruiterProfileCommand(rec.Id)
            //{
            //    FirstName = "Racheli",
            //    LastName = "Fishman",
            //    CompanyName = "Matrix",
            //    Gender = Gender.Male,
            //    Image = recruiterImage
            //});

            //recruiterDbFacade.RecruiterCreateJob(new CreateJobCommand(response.UserId)
            //{
            //    Address = "נועם אלימלך 10",
            //    JobLocation = new Location() { Latitude = 1111, Longitude = 2222 },
            //    JobName = "Job name",
            //    MandatoryRequirements = "3 שנות ניסיון בניהול חנות",
            //    OptionalRequirements = "יתרון- ניסיון בניהול חנות בגדים"
            //});

            //recruiterDbFacade.RecruiterUpdateJob(new UpdateJobCommand("56faaec4f8ebfe138c8aebed")
            //{
            //    JobId = "56fb7af1f8ebfe0bbc1d181b",
            //    JobName = "new job name",
            //    Address = "Noam 3",
            //    MandatoryRequirements = "mandatories",
            //    OptionalRequirements = "optionals",
            //    Location = new Location() { Latitude = 2222, Longitude = 3333 }
            //});

            //recruiterDbFacade.RecruiterGetJobs(new Common.EndUserDtoInterface.Query.RecruiterGetJobsQuery(response.UserId) { JobState = Common.CommonDto.JobState.Active });
            //recruiterDbFacade.RecruiterGetJob(new Common.EndUserDtoInterface.Query.RecruiterGetJobQuery(response.UserId) { JobId = "56fb7af1f8ebfe0bbc1d181b" });
            recruiterDbFacade.RecruiterCloseJob(new CloseJobCommand(response.UserId) { JobId = "56fb7af1f8ebfe0bbc1d181b" });

        }

        private static void CvFilesTester()
        {
            EmailServer emailServer = new EmailServer();
            string id = emailServer.CreateCvFile(new CreateCvFileCommand() { Extension = Common.CommonDto.Enums.FileType.Docx, SourceType = CvSourceType.Email, Source = "rachelifishman1@gmail.com" }).Id;
            CvFilesScannerDBFacade cvFiles = new CvFilesScannerDBFacade();

            byte[] cvStream;
            using (FileStream fsSource = new FileStream(@"C:\Users\InternetMatrix\Documents\Doc1.docx", FileMode.Open, FileAccess.Read))
            {

                // Read the source file into a byte array.
                cvStream = new byte[fsSource.Length];
                int numBytesToRead = (int)fsSource.Length;
                int numBytesRead = 0;
                while (numBytesToRead > 0)
                {
                    // Read may return anything from 0 to numBytesToRead.
                    int n = fsSource.Read(cvStream, numBytesRead, numBytesToRead);

                    // Break when the end of the file is reached.
                    if (n == 0)
                        break;

                    numBytesRead += n;
                    numBytesToRead -= n;
                }
                numBytesToRead = cvStream.Length;
            }

            BaseResult result = cvFiles.UploadScannedCvFile(new UploadScannedCvFileCommand() { CvFileId = id, Stream = cvStream});
            if (result.Success)
                cvFiles.DeleteScannedCvFile(new DeleteScannedCvFileCommand() { CvFileId = id, StatusReason = CvStatusReason.CannotParseEmail, StatusReasonDetails = "something" });
        }
    }
}
