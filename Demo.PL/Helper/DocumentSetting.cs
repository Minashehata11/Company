﻿using System.Net;

namespace Demo.PL.Helper
{
    public static class DocumnetSetting
    {
        public static string UploadFile(IFormFile file,string folderName)
        {
            //1.Get Located Folder Path =>   demo.pl/wwwroot/files/FolderName
            var  folderPath=Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/Files",folderName);
            //2 Make file name make it unique
            var fileName=$"{Guid.NewGuid()}-{Path.GetFileName(file.FileName)}";
            //3 Get FilePath
            var filePath=Path.Combine(folderPath,fileName);
            //4 Create File to filepath
            using var fileStream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(fileStream);

            return fileName;
        }
    }
}
