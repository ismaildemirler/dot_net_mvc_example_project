using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Web;
using System.Text;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Newtonsoft.Json;

namespace Domain.Infrastructure.Utilities.Common
{
    public class Utilities
    {
        public static string RandomString(int length, bool advance)
        {
            var random = new Random();
            Thread.Sleep(10);
            var chars = advance ? "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789" : "0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static int RandomNumber(int min, int max)
        {
            Random random = new Random();
            Thread.Sleep(20);
            return random.Next(min, max + 1);
        }

        public static string GetDescription(Enum value)
        {
            var fieldInfo = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static string BenzersizAnahtarOlustur(int sayi = 10, bool advance = false)
        {
            return Guid.NewGuid().ToString().Replace("-", "").ToUpper() + RandomString(sayi, advance);
        }

        public static string GuidsizBenzersizAnahtarOlustur(int sayi = 8, bool advance = false)
        {
            return RandomString(sayi, advance);
        }

        public static string GetIp()
        {
            try
            {
                if (!string.IsNullOrEmpty(HttpContext.Current.Request.Headers["cc-ip"]))
                    return HttpContext.Current.Request.Headers["cc-ip"];

                if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

                if (HttpContext.Current.Request.UserHostAddress.Length != 0)
                    return HttpContext.Current.Request.UserHostAddress;
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool IsWebApp => HttpContext.Current?.Session != null;

        public static Guid GetAppId()
        {
            return Guid.Parse(ConfigurationManager.AppSettings["DomainMYSAppId"]);
        }

        public static string Browsergetir()
        {
            return HttpContext.Current.Request.Browser.Browser;
        }

        public static string Osgetir()
        {
            var os = Environment.OSVersion;
            var userAgent = HttpContext.Current.Request.UserAgent;

            if (userAgent.IndexOf("Windows NT 6.3", StringComparison.Ordinal) > 0)
            {
                return "Windows 8.1";
            }
            if (userAgent.IndexOf("Windows NT 6.2", StringComparison.Ordinal) > 0)
            {
                return "Windows 8";
            }
            if (userAgent.IndexOf("Windows NT 6.1", StringComparison.Ordinal) > 0)
            {
                return "Windows 7";
            }
            if (userAgent.IndexOf("Windows NT 6.0", StringComparison.Ordinal) > 0)
            {
                return "Windows Vista";
            }
            if (userAgent.IndexOf("Windows NT 5.2", StringComparison.Ordinal) > 0)
            {
                return "Windows Server 2003; Windows XP x64 Edition";
            }
            if (userAgent.IndexOf("Windows NT 5.1", StringComparison.Ordinal) > 0)
            {
                return "Windows XP";
            }
            if (userAgent.IndexOf("Windows NT 5.01", StringComparison.Ordinal) > 0)
            {
                return "Windows 2000, Service Pack 1 (SP1)";
            }
            if (userAgent.IndexOf("Windows NT 5.0", StringComparison.Ordinal) > 0)
            {
                return "Windows 2000";
            }
            if (userAgent.IndexOf("Windows NT 4.0", StringComparison.Ordinal) > 0)
            {
                return "Microsoft Windows NT 4.0";
            }
            if (userAgent.IndexOf("Win 9x 4.90", StringComparison.Ordinal) > 0)
            {
                return "Windows Millennium Edition (Windows Me)";
            }
            if (userAgent.IndexOf("Windows 98", StringComparison.Ordinal) > 0)
            {
                return "Windows 98";
            }
            if (userAgent.IndexOf("Windows 95", StringComparison.Ordinal) > 0)
            {
                return "Windows 95";
            }
            if (userAgent.IndexOf("Windows CE", StringComparison.Ordinal) > 0)
            {
                return "Windows CE";
            }
            return "Others";
        }

        public static string ToEnglish(string text, bool clearDot = true)
        {
            var linkText = text.Trim();
            if (clearDot)
                linkText = linkText.Replace(".", string.Empty);

            linkText = linkText.Replace('Ç', 'C').Replace('ç', 'c');
            linkText = linkText.Replace('Ğ', 'G').Replace('ğ', 'g');
            linkText = linkText.Replace('İ', 'I').Replace('ı', 'i');
            linkText = linkText.Replace('Ö', 'O').Replace('ö', 'o');
            linkText = linkText.Replace('Ş', 'S').Replace('ş', 's');
            linkText = linkText.Replace('Ü', 'U').Replace('ü', 'u');

            return linkText;
        }

        public static long Base36StringToLong(string base36String)
        {
            const string CharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var inputBuffer = base36String.Reverse();
            var reversedStr = new string(inputBuffer.ToArray());

            var reversed = reversedStr.ToArray();
            BigInteger result = 0;
            var pos = 0;
            var size = CharList.Length;
            foreach (var c in reversed)
            {
                result += CharList.IndexOf(c) * BigInteger.Pow(size, pos);
                pos++;
            }
            return Convert.ToInt64(result.ToString());
        }

        public static string LongToBase36String(long inputVal)
        {

            const string CharList = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var input = new BigInteger(inputVal);
            var clistarr = CharList.ToCharArray();
            var result = new Stack<char>();

            var size = CharList.Length;
            while (input != 0)
            {
                var val = input % size;
                var intVal = (int)val;
                result.Push(clistarr[intVal]);
                input /= size;
            }
            return new string(result.ToArray());
        }        

        public static int CalculateAge(DateTime birthDate, DateTime? referanceDate = null)
        {
            referanceDate = referanceDate ?? DateTime.Today;
            var age = referanceDate.Value.Year - birthDate.Year;
            if (birthDate > referanceDate.Value.AddYears(-age)) age--;
            return age;
        }

        public static T GetFromQueryString<T>(bool isEncrypted = false) where T : new()
        {
            var queryString = HttpContext.Current.Request.QueryString.ToString();
            if (isEncrypted)
                queryString = HttpContext.Current.Request.QueryString.Get("q").Decrypt().Replace("?", "&");

            var dict = HttpUtility.ParseQueryString(queryString);
            var json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static T GetFromQueryString<T>(string queryString, bool isEncrypted = false) where T : new()
        {
            if (isEncrypted)
                queryString = HttpUtility.ParseQueryString(queryString).Get("q").Decrypt().Replace("?", "&");

            var dict = HttpUtility.ParseQueryString(queryString);
            var json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static T GetFromParams<T>(bool isEncrypted = false) where T : new()
        {
            var queryString = HttpContext.Current.Request.Params.ToString();
            if (isEncrypted)
                queryString = HttpContext.Current.Request.Params.Get("q").Decrypt().Replace("?", "&");

            var dict = HttpUtility.ParseQueryString(queryString);
            var json = JsonConvert.SerializeObject(dict.Cast<string>().ToDictionary(k => k, v => dict[v]));
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }

        public static string GetMonthNamebyId(int monthId)
        {
            var result = "";
            switch (monthId)
            {
                case 1: result = "Ocak"; break;
                case 2: result = "Şubat"; break;
                case 3: result = "Mart"; break;
                case 4: result = "Nisan"; break;
                case 5: result = "Mayıs"; break;
                case 6: result = "Haziran"; break;
                case 7: result = "Temmuz"; break;
                case 8: result = "Ağustos"; break;
                case 9: result = "Eylül"; break;
                case 10: result = "Ekim"; break;
                case 11: result = "Kasım"; break;
                case 12: result = "Aralık"; break;
            }
            return result;
        }

        /*
            RDLC chart'ları için tasarlanmış bir fonksiyondur. Y Axis'indeki pozitif veya negatif max değeri hesaplar 
        */
        public static double GetYAxisMaxDoubleValueForBothSide(double maxGraphYAxisPointValue)
        {
            double firstDigit = Math.Abs(maxGraphYAxisPointValue);

            while (firstDigit >= 10)
                firstDigit /= 10;

            double digitPlaceNumber = Math.Abs(maxGraphYAxisPointValue).ToString().Split(',')[0].Length - 1;

            double maxValue = (firstDigit + 1) * Math.Pow(10.0, digitPlaceNumber);

            return maxGraphYAxisPointValue >= 0 ? maxValue : -1 * maxValue;
        }

        public static double GetYAxisMaxIntValueForBothSide(double maxGraphYAxisPointValue)
        {
            double firstDigit = Math.Abs(maxGraphYAxisPointValue);
            while (firstDigit >= 10)
                firstDigit /= 10;

            double digitPlaceNumber = Math.Abs(maxGraphYAxisPointValue).ToString().Split(',')[0].Length - 1;

            double maxValue = (Math.Floor(firstDigit) + 1) * Math.Pow(10.0, digitPlaceNumber);

            return maxGraphYAxisPointValue >= 0 ? maxValue : -1 * maxValue;
        }

        /*
            RDLC chart'ları için tasarlanmış bir fonksiyondur. Y Axis'indeki hesaplanan en büyük ve en küçük
            değerlere göre, yAxis'in 2 uç'değerlerini oluşturur.
        */
        public static void SetYAxisMaxMinValues(ref double yAxisMaxValue, ref double yAxisMinValue)
        {
            if (yAxisMinValue >= 0)
            {
                yAxisMinValue = 0;
                try
                {
                    yAxisMaxValue = GetYAxisMaxIntValueForBothSide(yAxisMaxValue);
                }
                catch (System.Exception)
                {

                    yAxisMaxValue = GetYAxisMaxDoubleValueForBothSide(yAxisMaxValue);
                }
                
            }
            else if (yAxisMaxValue <= 0)
            {
                yAxisMaxValue = 0;
                yAxisMinValue = GetYAxisMaxIntValueForBothSide(yAxisMinValue);
            }
            else
            {
                try
                {
                    yAxisMaxValue = GetYAxisMaxIntValueForBothSide(yAxisMaxValue);
                }
                catch (System.Exception)
                {

                    yAxisMaxValue = GetYAxisMaxDoubleValueForBothSide(yAxisMaxValue);
                }
                yAxisMinValue = GetYAxisMaxIntValueForBothSide(yAxisMinValue);
            }
        }

        public static string TextCensor(string text, char censorChar = '*')
        {
            if (string.IsNullOrEmpty(text)) return "";
            text = text.Trim();
            if (text.Length == 0) return "";

            var strArray = text.ToCharArray();
            for (var i = 0; i < strArray.Length; i++)
            {
                if (i % 2 != 0)
                    strArray[i] = censorChar;
            }

            return new string(strArray);
        }

        public T Deserialize<T>(byte[] param)
        {
            using (MemoryStream ms = new MemoryStream(param))
            {
                var br = new BinaryFormatter();
                return (T)br.Deserialize(ms);
            }
        }

        public static byte[] ObjectToByteArray(object obj)
        {
            if (obj == null)
                return null;
            var bf = new BinaryFormatter();
            using (var ms = new MemoryStream())
            {
                bf.Serialize(ms, obj);
                return ms.ToArray();
            }
        }
    }
}