using System;
using System.IO;
using System.Linq;
using LaunchableSample;
using Microsoft.Office.Interop.Word;

namespace CodeSamples
{
    public class DocxToPdfConverter : ILaunchableSample
    {
        private const string DocxExtension = ".docx";
        private readonly string _pathToFolderWithDocuments;

        public string Run()
        {
            return ConvertAllFiles();
        }

        public string GetName()
        {
            return "DocxToPdfConverter";
        }

        public DocxToPdfConverter()
        {
            _pathToFolderWithDocuments = Environment.CurrentDirectory;
        }

        /// <summary>
        /// When path non existing throws exception
        /// info on when the constructor should throw exception ttps://stackoverflow.com/questions/77639/when-is-it-right-for-a-constructor-to-throw-an-exception
        /// </summary>
        /// <param name="pathToFolderWithDocuments"></param>
        public DocxToPdfConverter(string pathToFolderWithDocuments)
        {
            if (!Directory.Exists(pathToFolderWithDocuments))
            {
                throw new DirectoryNotFoundException(pathToFolderWithDocuments);
            }

            _pathToFolderWithDocuments = pathToFolderWithDocuments;
        }

        public string ConvertAllFiles()
        {
            var fileEntries = Directory.GetFiles(_pathToFolderWithDocuments);

            if (fileEntries.All(fileEntry =>
                !string.Equals(DocxExtension, Path.GetExtension(fileEntry))))
            {
                //exceptions are costly
                //throw new FileNotFoundException("No .docx documents available at: " + _pathToFolderWithDocuments);

                return "No .docx documents available at: " + _pathToFolderWithDocuments;
            }

            var application = new Application();

            foreach (var fileEntry in fileEntries.Where(s => string.Equals(DocxExtension, Path.GetExtension(s))))
            {
                ProcessFile(fileEntry, application);
            }

            return "xxx";
        }

        private void ProcessFile(string fileEntry, Application application)
        {
            var wordDocument = application.Documents.Open(fileEntry);

            //if a file has the same name it is overrided
            wordDocument.ExportAsFixedFormat(Path.GetDirectoryName(fileEntry) + "\\" + Path.GetFileNameWithoutExtension(fileEntry) + ".pdf", WdExportFormat.wdExportFormatPDF);

            //I was trying to close word processing so that service in windows dont get blocked
            //Type mismatch exception??? 
            //Now I must close word process in task manager each time the app is run
            //application.Documents.Close(fileEntry);
        }
    }
}
