using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Infrastructure.File
{
    public class DefaultFileStore : IFileStore
    {
        public Task<string> SaveAsStream(Stream stream, string path, string fileName)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveAsTextAsyc(string text, string path, string fileName)
        {
            var directory = Path.GetDirectoryName(path);
            if (string.IsNullOrEmpty(directory))
                throw new Exception("目录找不到");
            var fullPath = $"{path.TrimEnd('/')}/{DateTime.Now.Year}/{DateTime.Now.ToString("HH")}";
            if (Directory.Exists(fullPath) == false)
                Directory.CreateDirectory(fullPath);
            var fullPathFileName = $"{fullPath}/{fileName}";

            byte[] buffer = Encoding.UTF8.GetBytes(text);
            using (var stream = new FileStream(fullPathFileName, FileMode.OpenOrCreate))
            {
                await stream.WriteAsync(buffer);
            }
            return fileName;

        }

      

        public Task<string> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
