using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Spider.Infrastructure.File
{
    public interface IFileStore
    {
        /// <summary>
        /// 保存文件,返回完整文件路径
        /// </summary>
        Task<string> SaveAsync();

        Task<string> SaveAsTextAsyc(string text, string path, string fileName);

        Task<string> SaveAsStream(Stream stream, string path, string fileName);
    }
}
