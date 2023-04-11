﻿using System.IO;
using System.Threading.Tasks;

namespace KnowledgeSpace.Backend.Services
{
    public interface IStorageService
    {
        string GetFileUrl(string fileName);

        Task SaveFileAsync(Stream mediaBinaryStream, string fileName);

        Task DeleteFileAsync(string fileName);
    }
}
