using System.IO;

namespace WebBrowser
{
    public class FileSystem
    {
        public string GetFileContents(string filename)
        {
            string filePath = Directory.GetCurrentDirectory() + filename;
            if (File.Exists(filePath))
                return File.ReadAllText(filePath);
            return null;
         }
    }
}

