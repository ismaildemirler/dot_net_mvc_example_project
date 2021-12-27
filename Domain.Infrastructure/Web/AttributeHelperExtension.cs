using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Web;

namespace Domain.Infrastructure.Web
{
    public static class AttributesHelperExtension
    {
        

        public static byte[] ToByteArray(this HttpPostedFileBase httpPostedFileBase)
        {
            var ms = new MemoryStream();
            httpPostedFileBase.InputStream.CopyTo(ms);

            return ms.ToArray();
        }

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        public static bool IsAjaxRequest(this HttpRequestBase request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            if (request["X-Requested-With"] == "XMLHttpRequest")
                return true;
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }

        /// <summary>
        /// Determines whether the specified HTTP request is an AJAX request.
        /// </summary>
        /// 
        /// <returns>
        /// true if the specified HTTP request is an AJAX request; otherwise, false.
        /// </returns>
        /// <param name="request">The HTTP request.</param><exception cref="T:System.ArgumentNullException">The <paramref name="request"/> parameter is null (Nothing in Visual Basic).</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));
            return request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        /// <summary>
        /// objeyi key=val&amp;key1=val1... formatına çevirir
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToQueryString(this object obj)
        {
            var properties = from p in obj.GetType().GetProperties()
                where p.GetValue(obj, null) != null
                select p.Name + "=" + HttpUtility.UrlEncode(p.GetValue(obj, null).ToString());

            return string.Join("&", properties.ToArray());
        }

        public static string ToJson(this object obj,bool IsEscapeHtml = false)
        {
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            if (IsEscapeHtml)
            {
                settings.StringEscapeHandling = StringEscapeHandling.EscapeHtml;
            }
            return JsonConvert.SerializeObject(obj,Formatting.None,settings).Replace('"', '\'');

        }

        /// <summary>
        /// Para formatında gösterim
        /// </summary>
        /// <param name="para"></param>
        /// <param name="simge"></param>
        /// <returns></returns>
        public static string ToMoney(this decimal para, string simge = "")
        {
            return simge != "" ? $"{para:N2} {HttpUtility.HtmlDecode(simge)}" : para.ToString("N2");
        }

        /// <summary>
        /// Para formatında gösterim
        /// </summary>
        /// <param name="para"></param>
        /// <param name="simge"></param>
        /// <returns></returns>
        public static string ToMoney(this decimal? para, string simge = "")
        {
            if (para.HasValue)
            {
                return simge != "" ? $"{para.Value:N2} {HttpUtility.HtmlDecode(simge)}" : para.Value.ToString("N2");
            }
            return "";
        }
    }
}
