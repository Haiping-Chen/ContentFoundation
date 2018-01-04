using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ContentFoundation.RestApi
{
    public class FileController : CoreController
    {
        [HttpPost]
        public string Upload()
        {
            // https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads
            // full path to file in temp location
            var filePath = Path.GetTempFileName();

            return filePath;

            /*using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            var gb2312 = Encoding.GetEncoding("GB2312");
            var content = System.IO.File.ReadAllText(filePath, gb2312);*/

            /*var fileName = DateTime.UtcNow.Ticks.ToString();
            AwsS3Helper file = new AwsS3Helper();
            file.SaveFileToS3(filePath, "Temp", fileName, Database.Configuration);

            var presignedUrl = file.GeneratePreSignedURLFromS3("Temp", fileName, Database.Configuration);
            return presignedUrl;*/
        }
    }
}
