using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Domain.Infrastructure.Utilities.Common
{
    public class FileValidation
    {
        public static bool IsValidFileType(byte[] fileByteContent)
        {
            bool isValid = false;
            string mimetypeOfFile = string.Empty;

            byte[] buffer = new byte[256];
            using (MemoryStream fs = new MemoryStream(fileByteContent))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }

            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                mimetypeOfFile = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
            }
            catch (System.Exception e)
            {
                throw new BusinessException("Dosya türü belirlenirken hata alındı!");
            }

            if (!string.IsNullOrEmpty(mimetypeOfFile))
            {
                switch (mimetypeOfFile.ToLower())
                {
                    case "application/msword": // for .doc  estension
                        isValid = true;
                        break;
                    case "application/vnd.openxmlformats-officedocument.wordprocessingml.document": // for .docx  estension
                        isValid = true;
                        break;
                    case "application/vnd.ms-excel": // for .xls  estension
                        isValid = true;
                        break;
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet": // for  .xlsx estension
                        isValid = true;
                        break;
                    //case "application/vnd.ms-powerpoint":// for .ppt estension
                    //    isValid = true;
                    //    break;
                    case "application/vnd.openxmlformats-officedocument.presentationml.presentation":// for .pptx estension
                        isValid = true;
                        break;
                    case "image/jpeg"://jpeg and jpg both
                        isValid = true;
                        break;
                    case "image/pjpeg"://jpeg and jpg both
                        isValid = true;
                        break;
                    case "image/png":// for .png estension
                        isValid = true;
                        break;
                    case "image/x-png":// for .png estension
                        isValid = true;
                        break;
                    case "image/gif":// for .gif estension
                        isValid = true;
                        break;
                }
            }

            return isValid;

        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
            System.UInt32 pBC,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
            [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
            System.UInt32 cbSize,
            [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
            System.UInt32 dwMimeFlags,
            out System.UInt32 ppwzMimeOut,
            System.UInt32 dwReserverd
        );

        public static string getMimeFromFile(byte[] byteArray)
        {
            byte[] buffer = new byte[256];
            using (MemoryStream fs = new MemoryStream(byteArray))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (System.Exception e)
            {
                throw new BusinessException("Dosya türü belirlenirken hata alındı!");
            }
        }
    }
}
