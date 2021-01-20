using Space101.DAL;
using Space101.Models;
using System.Collections.Generic;
using System.Data.Entity;

namespace Space101.Repositories
{
    public class FilePathRepository
    {
        private readonly ApplicationDbContext context;

        public FilePathRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void RemoveFilepaths(IEnumerable<FilePath> filePaths)
        {
            foreach (var item in filePaths)
            {
                context.Entry(item).State = EntityState.Deleted;
            }
        }

        public void RemoveFilepath(FilePath filepath)
        {
            context.Entry(filepath).State = EntityState.Deleted;
        }

        //public void RemoveRaceFilepaths(IEnumerable<FilePath> RaceFilePaths)
        //{
        //    foreach (var item in RaceFilePaths)
        //    {
        //        context.Entry(item).State = EntityState.Deleted;
        //    }
        //}

    }
}