using Space101.Enums;
using Space101.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace Space101.Services
{
    public class FileManager
    {
        private string AssetsPath { get; set; }

        public FileManager(HttpServerUtilityBase server)
        {
            AssetsPath = server.MapPath("~/App_Assets/");
        }

        public FileManager(string HostingEnvironmentPath)
        {
            AssetsPath = HostingEnvironmentPath;
        }

        public void DeleteFile(IModelFilePath modelFile)
        {
            string path = $"{AssetsPath}{modelFile.GetFullPath()}";
            if(File.Exists(path))
                File.Delete(path);
        }

        public void DeleteFolders(ModelType type, string modelId)
        {
            string path = $"{AssetsPath}{type}/{modelId}/";
            if (Directory.Exists(path))
                Directory.Delete(path, true);
        }

        private void InitializeFolders(IModelFilePath modelFile)
        {
            string path = $"{AssetsPath}{modelFile.ModelType}/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += $"{modelFile.GetModelId()}/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += $"{modelFile.GetFileType()}/";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void SaveUploadedFile(HttpPostedFileBase upload, IModelFilePath modelFile)
        {
            if (!FileIsValid(upload))
                throw new Exception("Invalid HttpPostedFileBase");

            InitializeFolders(modelFile);
            string path = $"{AssetsPath}{modelFile.GetFullPath()}";
            upload.SaveAs(path);
        }

        public void SaveTextFile(IModelFilePath modelFile, string text)
        {
            if (modelFile.GetFilePath().FileType != FileType.Text)
                throw new Exception("FileType is not text");

            InitializeFolders(modelFile);
            string path = $"{AssetsPath}{modelFile.GetFullPath()}";
            File.WriteAllText(path, text);
        }

        public string ReadTextFile(IModelFilePath modelFile)
        {
            if (modelFile.GetFilePath().FileType != FileType.Text)
                throw new Exception("FileType is not text");

            string path = $"{AssetsPath}{modelFile.GetFullPath()}";

            if (!File.Exists(path))
                return "";

            return File.ReadAllText(path);
        }

        public bool FileIsValid(HttpPostedFileBase file)
        {
            return (file != null && file.ContentLength > 0);
        }

        public bool FilesAreValid(IEnumerable<HttpPostedFileBase> files)
        {
            if (files == null)
                return false;

            bool isValid = true;
            foreach (var file in files)
            {
                isValid = (isValid && FileIsValid(file));
            }
            return isValid;
        }

        public IEnumerable<HttpPostedFileBase> FilterValidFiles(IEnumerable<HttpPostedFileBase> files)
        {
            var filterdFiles = new List<HttpPostedFileBase>();

            if (files == null)
                return filterdFiles;

            foreach (var file in files)
            {
                if (FileIsValid(file))
                    filterdFiles.Add(file);
            }
            return filterdFiles;
        }
    }
}