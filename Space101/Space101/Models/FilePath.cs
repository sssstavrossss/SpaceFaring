using Space101.Enums;

namespace Space101.Models
{
    public class FilePath
    {
        public int FilePathId { get; private set; }
        public string FileName { get; private set; }
        public FileType FileType { get; private set; }
        public string FileExtencion { get; private set; }

        protected FilePath()
        { }

        public FilePath(string filename, FileType fileType, string fileExtension)
        {
            FileName = filename;
            FileType = fileType;
            FileExtencion = fileExtension;
        }
    }
}