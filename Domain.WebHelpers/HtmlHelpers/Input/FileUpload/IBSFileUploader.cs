using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.WebHelpers.HtmlHelpers.Input.FileUpload
{
    public interface IBSFileUploader
    {
        IBSFileUploader SetMultiple(bool isMultiple);
        //IBSFileUploader SetActionUrl(string actionUrl);
        //IBSFileUploader SetDeleteUrl(string deleteUrl);
        //IBSFileUploader SetDeleteType(string deleteType);
        //IBSFileUploader SetServerMapPath(string serverMapPath);
        //IBSFileUploader SetTempPath(string tempPath);
        //IBSFileUploader SetBaseUrl(string urlBase);
        //IBSFileUploader SetStorageRoot(string storageRoot);
    }
}
