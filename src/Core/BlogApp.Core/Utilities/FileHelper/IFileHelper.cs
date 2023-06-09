using System;
using Microsoft.AspNetCore.Http;

namespace BlogApp.Core.Utilities.FileHelper
{
	public interface IFileHelper
	{
        Task<string> UploadImage(IFormFile file);
    }
}

