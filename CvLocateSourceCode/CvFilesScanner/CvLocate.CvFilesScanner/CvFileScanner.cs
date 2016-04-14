using CvLocate.Common.CommonDto.Enums;
using CvLocate.CvFilesScanner.Entities;
using CvLocate.CvFilesScanner.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CvLocate.Common.Utils;
//using DocumentConverter;

namespace CvLocate.CvFilesScanner
{
    public class CvFileScanner : ICvFileScanner
    {
        string _filePath;
        ScanResult _result;
        FileType _fileType;
        IDocumentConverterFactory _documentConverterFactory;

        public CvFileScanner(IDocumentConverterFactory documentConverterFactory)
        {
            this._documentConverterFactory = documentConverterFactory;
        }

        public ScanResult Scan(string filePath, FileType fileType)
        {
            this._filePath = filePath;
            this._fileType = fileType;
            this._result = new ScanResult();
            try
            {
                ExtractText();//todo for pdf file 
                FindEncoding();//todo by zvi
                ScanText();//todo by zvi
                FillStream();
                CreateImage();//todo by zvi
                this._result.Succeed = true;
            }
            catch (Exception ex)
            {
                this._result.Succeed = false;
                this._result.ErrorMessage = ex.Message;
                if (ex.InnerException != null)
                {
                    this._result.ErrorMessage += string.Format(" Origional Exception: {0}", ex.InnerException.ToString());
                }
            }

            return this._result;

        }

       
        private void FindEncoding()
        {
            this._result.Encoding = "utf-8";
        }

        private void FillStream()
        {
            try
            {
                this._result.Stream = System.IO.File.ReadAllBytes(this._filePath);
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get file stream", ex);
            }
        }

        private void CreateImage()
        {
            try
            {
               
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to create an image from the file", ex);
            }
        }

        private void ScanText()
        {
            try
            {
                this._result.IsSafeFile = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to scan file", ex);
            }
        }

        private void ExtractText()
        {
            try
            {
                //IDocumentConverter converter = this._documentConverterFactory.GetDocumentConverter((FileType)this._fileType);
                //if (converter == null)
                //{
                //    throw new Exception(string.Format("Cannot find converter for file type {0}", this._fileType));
                //}
                //this._result.Text = converter.ConvertFileToText(this._filePath);
                //if (string.IsNullOrWhiteSpace(this._result.Text))
                //{
                //    throw new Exception("File is empty");
                //}
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to extract text from the file", ex);
            }
        }
    }
}
