using System.IO;

namespace Reusables
{
    public static class FileProcessing
    {
        public static string GetFileDirectoryAndNameWithoutExtension(string filePath)
        {
            return Path.GetDirectoryName(filePath) + 
                "\\" + 
                Path.GetFileNameWithoutExtension(filePath);
        }
    }
}
