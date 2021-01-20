using Space101.Enums;
using Space101.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Space101.Interfaces
{
    public interface IModelFilePath
    {
        ModelType ModelType { get; }

        FilePath GetFilePath();
        string GetFileType();
        string GetModelId();
        string GetFullPath();
    }
}