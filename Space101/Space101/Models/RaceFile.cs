using Space101.Enums;
using Space101.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class RaceFile : IModelFilePath
    {
        [Key]
        [ForeignKey("FilePath")]
        [Column(Order = 1)]
        public int FilePathId { get; private set; }

        [Key]
        [ForeignKey("Race")]
        [Column(Order = 2)]
        public int RaceID { get; private set; }

        public ModelType ModelType { get; } = ModelType.Race; 
        public FilePath FilePath { get; private set; }
        public Race Race { get; private set; }

        protected RaceFile()
        { }

        public RaceFile(FilePath filePath)
        {
            FilePath = filePath;
            FilePathId = filePath.FilePathId;
        }

        public FilePath GetFilePath()
        {
            return FilePath;
        }

        public string GetFileType()
        {
            return FilePath.FileType.ToString();
        }

        public string GetFullPath()
        {
            return $"{ModelType}/{RaceID}/{FilePath.FileType}/{FilePath.FileName}";
        }

        public string GetModelId()
        {
            return RaceID.ToString();
        }
    }
}