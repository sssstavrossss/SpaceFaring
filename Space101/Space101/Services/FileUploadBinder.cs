using Space101.Interfaces;
using System.Web;

namespace Space101.Services
{
    public class FileUploadBinder
    {
        public IModelFilePath FilePath { get; private set; }
        public HttpPostedFileBase PostedFile { get; private set; }

        private FileUploadBinder()
        { }
        public FileUploadBinder(IModelFilePath filePath, HttpPostedFileBase postedFile)
        {
            FilePath = filePath;
            PostedFile = postedFile;
        }
    }
}