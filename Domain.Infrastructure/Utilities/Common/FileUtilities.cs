using Domain.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Domain.Infrastructure.Utilities.Common
{
    public static class FileUtilities
    {
        public static bool CheckUploadedFileSize(int maxLengthMegaByte, HttpPostedFileBase file)
        {
            if (file.ContentLength > maxLengthMegaByte * 1048576) // 1mb = 1048576byte
                return false;
            else
                return true;
        }

        public static bool CheckUploadedFileSize(int maxLengthMegaByte, HttpFileCollectionBase fileCollection)
        {
            for (int i = 0; i < fileCollection.Count; i++)
            {
                if (fileCollection[i].ContentLength > maxLengthMegaByte * 1048576) // 1mb = 1048576byte
                    return false;
            }

            return true;
        }

        public static bool IsRequestContainsFile(HttpFileCollectionBase fileCollection)
        {
            return fileCollection.Count > 0;
        }

        public static string GetMimeTypebyExtension(string extension)
        {
            if (extension == null)
                throw new ArgumentNullException("extension");

            if (!extension.StartsWith("."))
                extension = "." + extension;

            var value = FileType.MimeTypeMappings.FirstOrDefault(w => w.Key == extension).Value.ToLower();

            if (string.IsNullOrEmpty(value))
                throw new ArgumentNullException("extension");

            return value;
        }

        public static List<string> GetExtensionbyMimeType(string mimeType)
        {
            if (mimeType == null)
                throw new ArgumentNullException("mimeType");

            var values = FileType.MimeTypeMappings.Where(w => w.Value == mimeType).Select(s => s.Key.Replace(".", "").ToString().ToLower()).ToList();

            if (!values.Any())
                throw new ArgumentNullException("mimeType");

            return values;
        }
    }
}
