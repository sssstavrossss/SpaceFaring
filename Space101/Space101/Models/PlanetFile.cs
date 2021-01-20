using Space101.Enums;
using Space101.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Space101.Models
{
    public class PlanetFile : IModelFilePath
    {
        [Key]
        [ForeignKey("FilePath")]
        [Column(Order = 1)]
        public int FilePathId { get; private set; }

        [Key]
        [ForeignKey("Planet")]
        [Column(Order = 2)]
        public int PlanetId { get; private set; }

        public ModelType ModelType { get; } = ModelType.Planet;

        protected PlanetFile()
        { }

        public PlanetFile(FilePath filePath)
        {
            FilePath = filePath;
            FilePathId = filePath.FilePathId;
        }

        public FilePath FilePath { get; private set; }
        public Planet Planet { get; private set; }


        public FilePath GetFilePath()
        {
            return FilePath;
        }

        public string GetFileType()
        {
            return FilePath.FileType.ToString();
        }

        public string GetModelId()
        {
            return PlanetId.ToString();
        }

        public string GetFullPath()
        {
            return $"{ModelType}/{PlanetId}/{FilePath.FileType}/{FilePath.FileName}";
        }
    }
}