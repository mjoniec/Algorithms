using System.IO;
using System.Linq;

namespace CodeSamples
{
    public class DocxToPdfConverter
    {
        /*
pdf converter invoke
     private static void Main(string[] args)
        {
            //Replace with dynamic env dir
            //var pathToFolderWithDocuments = @"C:\Users\marcin_joniec\Desktop\Bookatable";

            var pathToFolderWithDocuments = Environment.CurrentDirectory;
            var docxToPdfConverter = new DocxToPdfConverter(pathToFolderWithDocuments);

            var result = docxToPdfConverter.ConvertAllFiles();

            Console.WriteLine(result);
        }
  */

        private const string DocxExtension = ".docx";
        private readonly string _pathToFolderWithDocuments;

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

            if (fileEntries.All(fileName =>
                !string.Equals(DocxExtension, Path.GetExtension(fileName))))
            {
                //exceptions are costly
                //throw new FileNotFoundException("No .docx documents available at: " + _pathToFolderWithDocuments);

                return "No .docx documents available at: " + _pathToFolderWithDocuments;
            }

            //foreach (var fileName in fileEntries)
            //{
            //    ProcessFile(fileName);
            //}

            return "xxx";
        }
    }
}
