using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Domain.Infrastructure.CrossCuttingConcerns.Validation.ValidationAttributes
{
    public class ValidationRules
    {
        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class NotNullAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value == null)
                {
                    var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                        $"{argumentName} gereklidir. Lütfen boş bırakmayınız!"));
                    HttpContext.Current.Session["Validation"] += validationresult.Message;
                    HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class EmailAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    if (!YardimciFonksiyonlar.EmailGecerliMi(value.ToString()))
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"Lütfen {argumentName} alanına geçerli bir email adresi giriniz."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class NumberAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    int i = 0;
                    bool result = int.TryParse(value.ToString(), out i);
                    if (!result)
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"Lütfen {argumentName} alanına sadece sayı giriniz."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class PhoneNumberAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    string[] parca = value.ToString().Split(' ');
                    value = parca[0].Replace("(", "").Replace(")", " ") + parca[1];
                    if (!value.ToString().StartsWith("0"))
                    {
                        value = "0" + value;
                    }
                    value = value.ToString().Replace("-", " ");
                    string pattern = @"^(0(\d{3}) (\d{3}) (\d{4}))$";
                    Match match = Regex.Match(value.ToString(), pattern, RegexOptions.IgnoreCase);
                    if (!match.Success)
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"Lütfen {argumentName} alanına geçerli bir telefon numarası giriniz."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class WebAddressAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    Uri uriResult;
                    if (Uri.TryCreate(value.ToString(), UriKind.Absolute, out uriResult) && uriResult.Scheme == Uri.UriSchemeHttp)
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"Lütfen {argumentName} alanına geçerli bir internet adresi giriniz."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class Min3CharactersAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    Regex regex = new Regex(@"^(?:[a-zA-Z][^a-zA-Z]*){3}");
                    Match match = regex.Match(value.ToString());
                    if (!match.Success)
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"{argumentName} alanı en az 3 karakterli olmalıdır."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class Min6CharactersAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (value != null)
                {
                    Regex regex = new Regex(@"^(?:[a-zA-Z][^a-zA-Z]*){6}");
                    Match match = regex.Match(value.ToString());
                    if (!match.Success)
                    {
                        var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                            $"{argumentName} alanı en az 6 karakterli olmalıdır."));
                        HttpContext.Current.Session["Validation"] += validationresult.Message;
                        HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                    }
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class ValidateDateTimeAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                if (!YardimciFonksiyonlar.TarihGecerliMi(value.ToString()))
                {
                    var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                        $"Lütfen {argumentName} alanı için tarih seçiniz."));
                    HttpContext.Current.Session["Validation"] += validationresult.Message;
                    HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                }
            }
        }

        [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
        public class ValidateFileAttribute : IArgumentValidationAttribute
        {
            public override void ValidateArgument(object value, string argumentName, string gercekisim)
            {
                int MaxContentLength = 1024 * 1024 * 10; //10 MB
                string[] allowedFileExtensions = { ".jpg", ".gif", ".png", ".pdf", ".doc" };
                var file = value as HttpPostedFileBase;

                if (file == null)
                {
                    var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                        $"{argumentName} gereklidir. Lütfen boş bırakmayınız!"));
                    HttpContext.Current.Session["Validation"] += validationresult.Message;
                    HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                }
                else if (!allowedFileExtensions.Contains(file.FileName.Substring(file.FileName.LastIndexOf('.'))))
                {
                    var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                        $"{argumentName} gereklidir. Lütfen boş bırakmayınız!"));
                    HttpContext.Current.Session["Validation"] += validationresult.Message;
                    HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                }
                else if (file.ContentLength > MaxContentLength)
                {
                    var validationresult = new ValidationCoreException(new ArgumentException(argumentName,
                        $"{argumentName} gereklidir. Lütfen boş bırakmayınız!"));
                    HttpContext.Current.Session["Validation"] += validationresult.Message;
                    HttpContext.Current.Session["Alanlar"] += gercekisim + "-";
                }
            }
        }

        public class YardimciFonksiyonlar
        {
            public static bool TarihGecerliMi(String date)
            {
                DateTime temp;

                if (DateTime.TryParse(date, out temp) == true &&
                    temp.Hour == 0 &&
                    temp.Minute == 0 &&
                    temp.Second == 0 &&
                    temp.Millisecond == 0 &&
                    temp > DateTime.MinValue)
                    return true;
                return false;
            }

            public static bool EmailGecerliMi(string value)
            {
                Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
                Match match = regex.Match(value);
                if (!match.Success)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
