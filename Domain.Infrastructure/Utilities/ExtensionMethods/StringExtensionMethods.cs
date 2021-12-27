using System;
using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Security;

namespace Domain.Infrastructure.Utilities.ExtensionMethods
{
    public static class StringExtensionMethods
    {
        /// <summary>
        /// new line \n karakterini <br/> tagına çevirir
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string Nl2Br(this string str)
        {
            return str.Replace("\n", "<br/>");
        }

        /// <summary>
        /// öçşğüıİÖÇŞĞÜ karaklerleri içerip içermediğini sorgular
        /// </summary>
        /// <param name="sStr"></param>
        /// <returns></returns>
        public static bool TurkceKarakterVarMi(this string sStr)
        {
            const string sTurkceKarakterler = "öçşğüıİÖÇŞĞÜ";
            for (var i = 0; i < sTurkceKarakterler.Length; i++)
            {
                if (sStr.Contains(sTurkceKarakterler[i].ToString()))
                {
                    return true;
                }
            }
            return false;
        }
        /// <summary>
        /// küçük harfe çevirip öçşğüı harflerini ocsgui harflerine dönüşür, trim eder
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string TurkceKarakterleriKaldir(this string str)
        {
            str = str.Trim().ToLower(new System.Globalization.CultureInfo("tr-TR"));
            str = str.Replace('ö', 'o').Replace('ü', 'u').Replace('ğ', 'g').Replace('ş', 's').Replace('ı', 'i').Replace('ç', 'c');
            return str;
        }

        /// <summary>
        /// PassPhrase anahtarına göre  texti şifreler
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Encrypt_(this string message, string privateKey = "")
        {
            var passPhrase = ConfigurationManager.AppSettings["CryptoPassPhrase"];
            if (privateKey != "")
            {
                passPhrase = privateKey;
            }
            passPhrase += DateTime.Now.ToShortDateString();
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passPhrase));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var dataToEncrypt = utf8.GetBytes(message);
            try
            {
                var encryptor = tdesAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return Convert.ToBase64String(results).Replace('+', '-').Replace('/', '_').Replace('=', ',');
        }

        /// <summary>
        /// PassPhrase anahtarına göre şifrelenmiş texti çözer
        /// </summary>
        /// <param name="message"></param>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        public static string Decrypt_(this string message, string privateKey = "")
        {
            message = message.Replace('-', '+').Replace('_', '/').Replace(',', '=');
            var passPhrase = ConfigurationManager.AppSettings["CryptoPassPhrase"];
            if (privateKey != "")
            {
                passPhrase = privateKey;
            }
            passPhrase += DateTime.Now.ToShortDateString();
            byte[] results;
            var utf8 = new UTF8Encoding();
            var hashProvider = new MD5CryptoServiceProvider();
            var tdesKey = hashProvider.ComputeHash(utf8.GetBytes(passPhrase));
            var tdesAlgorithm = new TripleDESCryptoServiceProvider
            {
                Key = tdesKey,
                Mode = CipherMode.ECB,
                Padding = PaddingMode.PKCS7
            };
            var dataToDecrypt = Convert.FromBase64String(message);
            try
            {
                var decryptor = tdesAlgorithm.CreateDecryptor();
                results = decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                tdesAlgorithm.Clear();
                hashProvider.Clear();
            }
            return utf8.GetString(results);
        }


        /// <summary>
        /// MD5 Hash e çevirir
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToMd5Hash(this string input)
        {
            // step 1, calculate MD5 hash from input
            var md5 = MD5.Create();
            var inputBytes = Encoding.ASCII.GetBytes(input);
            var hash = md5.ComputeHash(inputBytes);

            // step 2, convert byte array to hex string
            var sb = new StringBuilder();
            foreach (var t in hash)
            {
                sb.Append(t.ToString("X2"));
            }
            return sb.ToString();
        }
        public static string Encrypt(this string value)
        {
            return FormsAuthentication.Encrypt(new FormsAuthenticationTicket(1,
                string.Empty,
                DateTime.Now,
                DateTime.Now.AddMinutes(20160),
                true,
                value));
        }
        public static string Decrypt(this string encryptedTicket)
        {
            return FormsAuthentication.Decrypt(encryptedTicket).UserData;
        }

        public static int DecryptToInt32(this string encryptedTicket)
        {
            return int.Parse(FormsAuthentication.Decrypt(encryptedTicket).UserData);
        }

        public static string ToTitleCase(this string str)
        {
            var cultureInfo = System.Threading.Thread.CurrentThread.CurrentCulture;
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the culture info with the specified name
        /// </summary>
        public static string ToTitleCase(this string str, string cultureInfoName)
        {
            var cultureInfo = new CultureInfo(cultureInfoName);
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }

        /// <summary>
        /// Overload which uses the specified culture info
        /// </summary>
        public static string ToTitleCase(this string str, CultureInfo cultureInfo)
        {
            return cultureInfo.TextInfo.ToTitleCase(str.ToLower());
        }


        #region LeftRightTrim

        public static string Left(this string str, int count)
        {
            if (string.IsNullOrEmpty(str) || count < 1)
                return string.Empty;
            else
                return str.Substring(0, Math.Min(count, str.Length));
        }

        public static string Right(this string str, int count)
        {
            if (string.IsNullOrEmpty(str) || count < 1)
                return string.Empty;
            else if (str.Length >= count)
                return str.Substring(str.Length - count, Math.Min(count, str.Length));
            else
                return string.Empty;
        }

        public static string TrimPrefix(this string str, string prefix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(prefix) && str.StartsWith(prefix))
            {
                return str.Substring(prefix.Length);
            }
            return str;
        }

        public static string TrimSuffix(this string str, string suffix)
        {
            if (!String.IsNullOrEmpty(str) && !String.IsNullOrEmpty(suffix) && str.EndsWith(suffix))
            {
                return str.Remove(str.Length - suffix.Length);
            }
            return str;
        }

        public static string RemoveLastCharacter(this string str)
        {
            return str.Substring(0, str.Length - 1);
        }

        public static string RemoveLast(this string str, int number)
        {
            return str.Substring(0, str.Length - number);
        }

        public static string RemoveFirstCharacter(this string str)
        {
            return str.Substring(1);
        }

        public static string RemoveFirst(this string str, int number)
        {
            return str.Substring(number);
        }

        public static string Truncate(this string str, int maxLength)
        {
            // replaces the truncated string to a ...
            const string suffix = "...";
            string truncatedString = str;

            if (maxLength <= 0) return truncatedString;
            int strLength = maxLength - suffix.Length;

            if (strLength <= 0) return truncatedString;

            if (str == null || str.Length <= maxLength) return truncatedString;

            truncatedString = str.Substring(0, strLength);
            truncatedString = truncatedString.TrimEnd();
            truncatedString += suffix;
            return truncatedString;
        }

        #endregion

        #region Encrypt/Decrypt

        public static string Encrypt(this string str, string key)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return string.Empty;
            }

            CspParameters cspp = new CspParameters();
            cspp.KeyContainerName = key;

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            byte[] bytes = rsa.Encrypt(System.Text.UTF8Encoding.UTF8.GetBytes(str), true);

            return BitConverter.ToString(bytes);
        }

        public static string Decrypt(this string str, string key)
        {
            string result = null;

            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(key))
            {
                return result;
            }

            try
            {
                CspParameters cspp = new CspParameters();
                cspp.KeyContainerName = key;

                RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(cspp);
                rsa.PersistKeyInCsp = true;

                string[] decryptArray = str.Split(new string[] { "-" }, StringSplitOptions.None);
                byte[] decryptByteArray = Array.ConvertAll<string, byte>(decryptArray, (s => Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));


                byte[] bytes = rsa.Decrypt(decryptByteArray, true);

                result = System.Text.UTF8Encoding.UTF8.GetString(bytes);

            }
            catch { result = null; }
            return result;
        }

        #endregion

        public static string Format(this string s, params object[] args)
        {
            if (args != null && args.Length > 0)
                return string.Format(s, args);
            else
                return s;
        }

        public static bool IsMatch(this string str, string pattern)
        {
            return Regex.IsMatch(str, pattern);
        }

        public static bool IsBlank(this string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static bool IsTcKimlikNo(this string str)
        {
            if (str.Length < 11)
                return false;
            else if (str.Substring(0, 1) == "0")
                return false;

            int toplam1 = Convert.ToInt32(str[0].ToString()) + Convert.ToInt32(str[2].ToString()) + Convert.ToInt32(str[4].ToString()) + Convert.ToInt32(str[6].ToString()) + Convert.ToInt32(str[8].ToString());
            int toplam2 = Convert.ToInt32(str[1].ToString()) + Convert.ToInt32(str[3].ToString()) + Convert.ToInt32(str[5].ToString()) + Convert.ToInt32(str[7].ToString());

            int sonuc = ((toplam1 * 7) - toplam2) % 10;

            if (sonuc.ToString() != str[9].ToString())
                return false;

            int toplam3 = 0;
            for (int i = 0; i < 10; i++)
                toplam3 += Convert.ToInt32(str[i].ToString());

            if ((toplam3 % 10).ToString() != str[10].ToString())
                return false;

            return true;
        }

        public static bool IsEmail(this string str)
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,6}$").IsMatch(str);
        }

        public static bool IsUrl(this string str)
        {
            string strRegex = "^(https?://)"
        + "?(([0-9a-z_!~*'().&=+$%-]+: )?[0-9a-z_!~*'().&=+$%-]+@)?" //user@
        + @"(([0-9]{1,3}\.){3}[0-9]{1,3}" // IP Address
        + "|" // IP or domain
        + @"([0-9a-z_!~*'()-]+\.)*" // www.
        + @"([0-9a-z][0-9a-z-]{0,61})?[0-9a-z]" // second level
        + @"(\.[a-z]{2,6})?)" // .com or .ext
        + "(:[0-9]{1,5})?" // port number
        + "((/?)|" // slash
        + "(/[0-9a-z_!~*'().;?:@&=+$,%#-]+)+/?)$";
            return new Regex(strRegex).IsMatch(str);
        }

        public static bool IsGuid(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            Regex format = new Regex(
                "^[A-Fa-f0-9]{32}$|" +
                "^({|\\()?[A-Fa-f0-9]{8}-([A-Fa-f0-9]{4}-){3}[A-Fa-f0-9]{12}(}|\\))?$|" +
                "^({)?[0xA-Fa-f0-9]{3,10}(, {0,1}[0xA-Fa-f0-9]{3,6}){2}, {0,1}({)([0xA-Fa-f0-9]{3,4}, {0,1}){7}[0xA-Fa-f0-9]{3,4}(}})$");
            Match match = format.Match(str);

            return match.Success;
        }

        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            long retNum;
            return long.TryParse(str, System.Globalization.NumberStyles.Integer, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
        }

        public static bool IsDouble(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            double retNum;
            return double.TryParse(str, out retNum);
        }

        public static bool IsDate(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            DateTime dt;
            return (DateTime.TryParse(str, out dt));
        }

        public static T ToEnum<T>(this string str) where T : struct
        {
            return (T)System.Enum.Parse(typeof(T), str, true);
        }

        public static string ToCompareableString(this string str)
        {
            str = str.Replace("Ğ", "G").Replace("Ü", "U").Replace("Ş", "S").Replace("İ", "I").Replace("Ö", "O").Replace("Ç", "C");
            str = str.Replace("ğ", "g").Replace("ü", "u").Replace("ş", "s").Replace("ı", "i").Replace("ö", "o").Replace("ç", "c");
            str = str.Replace(".", string.Empty).Replace(",", string.Empty).Replace("'", string.Empty).Replace("´", string.Empty).Replace("`", string.Empty);
            str = str.Replace("_", string.Empty).Replace("\"", string.Empty);
            str = str.Replace(" ", string.Empty);
            str = str.ToLower();
            return str;
        }

        //public static Guid ToGuid(this string str)
        //{
        //    if (str == null)
        //        return Guid.Empty; //throw new ArgumentNullException("s");

        //    Guid tmp;
        //    if (Guid.TryParse(str, out tmp))
        //        return tmp;
        //    else
        //        return Guid.Empty;
        //}
        
        public static DateTime ToDateTime(this string str)
        {
            if (str == null)
                return DateTime.MinValue;

            DateTime tmp;
            CultureInfo cultureinfo = new System.Globalization.CultureInfo("tr-TR");
            DateTimeStyles styles;
            styles = DateTimeStyles.AssumeLocal;

            if (DateTime.TryParse(str, cultureinfo, styles, out tmp))
                return tmp;
            else
                return DateTime.MinValue;

        }
    }
}
