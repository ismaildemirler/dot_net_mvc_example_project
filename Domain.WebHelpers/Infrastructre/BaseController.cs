using Domain.Entity.Concrete;
using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.CrossCuttingConcerns.Session;
using Domain.Infrastructure.Utilities.Common;
using Domain.Infrastructure.Utilities.ExtensionMethods;
using Domain.WebHelpers.Models.Shared;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using static Domain.WebHelpers.Infrastructre.Enums;
using AjaxResult = Domain.WebHelpers.Models.Shared.AjaxResult;
using Utilities = Domain.Infrastructure.Utilities.Common.Utilities;

//using Recaptcha.Web;
//using Recaptcha.Web.Mvc;

namespace Domain.WebHelpers.Infrastructre
{
    public class BaseController : Controller
    {
        public string RedirectUrlSession
        {
            get
            {
                if (SessionManager.Get("RedirectUrlSession") == null)
                    SessionManager.Set("RedirectUrlSession", "");
                return (string)SessionManager.Get("RedirectUrlSession");

            }
            set => SessionManager.Set("RedirectUrlSession", value);
        }

        public Attachment AttachmentSession
        {
            get
            {
                if (SessionManager.Get("AttachmentSession") == null)
                    SessionManager.Set("AttachmentSession", new Attachment());
                return (Attachment)SessionManager.Get("AttachmentSession");

            }
            set => SessionManager.Set("AttachmentSession", value);
        }

        protected string FindIpAddress()
        {
            var context = System.Web.HttpContext.Current;
            return context.Request.ServerVariables["REMOTE_ADDR"];
        }

        protected void PageTitle(string baslik)
        {
            ViewBag.Title = baslik;
        }

        protected void ShowMessageCustom(Message msg)
        {
            const string dataKey = "ShowMessageCustom";

            if (TempData[dataKey] == null)
            {
                TempData[dataKey] = new List<Message> { msg };
            }
            else
            {
                var mesajlar = (List<Message>)TempData[dataKey];
                mesajlar.Add(msg);
                TempData[dataKey] = mesajlar;
            }
        }

        protected void ShowMessageCustomSwal(Message msg)
        {
            const string dataKey = "ShowMessageCustomSwal";

            if (TempData[dataKey] == null)
            {
                TempData[dataKey] = new List<Message> { msg };
            }
            else
            {
                var mesajlar = (List<Message>)TempData[dataKey];
                mesajlar.Add(msg);
                TempData[dataKey] = mesajlar;
            }
        }

        public virtual void ShowMessageEski(string message, MessageType type = MessageType.info)
        {
            var msgBtnIcon = new[] { "btn btn-outline-info btn-sm", "btn btn-outline-warning btn-sm", "btn btn-outline-danger btn-sm", "btn btn-outline-success btn-sm" };
            var msg = new Message
            {
                Msg = message,
                Type = type,
                Buttons = new List<MsgButton>
                {
                    new MsgButton
                    {
                        IFrame = false,
                        CssClass = msgBtnIcon[type.GetHashCode()],
                        Text = "Tamam"
                    }
                }
            };
            ShowMessageCustom(msg);
        }

        public virtual void ShowMessage(string message, MessageType type = MessageType.info, bool showCloseButton = false)
        {
            const string dataKey = "ShowMessageSwal";
            TempData[dataKey] = new
            {
                message,
                type,
                showCloseButton = showCloseButton.ToString().ToLower()
            };
        }

        public virtual void ShowPartialModal(PartialViewModal partial)
        {
            const string dataKey = "ShowPartialCustom";
            TempData[dataKey] = System.Web.Helpers.Json.Encode(partial);
        }

        public virtual string ShowPartialModalGetStr(PartialViewModal partial)
        {
            return string.Format("ShowPartial('{0}','{1}','{2}',{3},'{4}','{5}',{6},'{7}','{8}','{9}')",
                partial.Title,
                partial.Icon,
                partial.PartialViewUrl,
                partial.ShowCloseButton.ToString().ToLower(),
                partial.ShowCallBackFunction,
                partial.CloseCallBackFunction,
                partial.Modal.ToString().ToLower(),
                partial.LoadingText,
                partial.Model,
                partial.Method);
        }

        public void ManageException(Exception ex, string methodName = "")
        {
            if (ex is BusinessException ||
                ex is NotificationException ||
                ex.Message.Contains("|NotificationException"))
            {
                ShowMessage(ex.Message.Replace("|NotificationException", ""), MessageType.warning);
            }
            else
            {
                ErrorLogger errorLogger = new ErrorLogger();
                errorLogger.HataLogla(ex, methodName);
                ShowMessage("Sistemde beklenmedik bir hata oluştu! Hatanın bilgileri kaydedilmiştir.", MessageType.error);
            }
        }

        private AjaxResultState GetAjaxResultState(MessageType type)
        {
            var result = AjaxResultState.Success;

            switch (type)
            {
                case MessageType.warning:
                case MessageType.info:
                    result = AjaxResultState.Warning;
                    break;
                case MessageType.error:
                    result = AjaxResultState.Fail;
                    break;
                case MessageType.success:
                    result = AjaxResultState.Success;
                    break;
                case MessageType.question:
                    result = AjaxResultState.Confirm;
                    break;
            }
            return result;

        }

        public ContentResult RedirectJSONCustom(Message msg)
        {
            if (IsAjax())
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new AjaxResult
                    {
                        Result = GetAjaxResultState(msg.Type),
                        Model = msg
                    }),
                    ContentType = "application/json"
                };
            }
            return new ContentResult
            {
                Content = $"<script type='text/javascript'>window.parent.location.href = '{msg.Url}';</script>",
                ContentType = "text/html"
            };
        }

        public ContentResult ShowMessageCustomJSON(Message msg)
        {
            return RedirectJSONCustom(msg);
        }

        public ContentResult ShowMessageJSON(string msg, AjaxResultState state = AjaxResultState.Success, bool isMessageShowDirectly = true)
        {
            return RedirectJSON("", msg, state, isMessageShowDirectly);
        }

        public ContentResult RedirectJSON(string url, string msg = "", AjaxResultState state = AjaxResultState.Success, bool isMessageShowDirectly = false)
        {
            if (!string.IsNullOrEmpty(msg) && !isMessageShowDirectly)
            {
                if (state == AjaxResultState.Success)
                    ShowMessage(msg, MessageType.success);
                else if (state == AjaxResultState.Warning)
                    ShowMessage(msg, MessageType.warning);
                else
                    ShowMessage(msg, MessageType.error);
            }

            if (IsAjax())
            {
                return new ContentResult
                {
                    Content = JsonConvert.SerializeObject(new AjaxResult
                    {
                        Result = state,
                        Message = msg,
                        RedirectUrl = url
                    }),
                    ContentType = "application/json"
                };
            }

            return new ContentResult
            {
                Content = $"<script type='text/javascript'>window.parent.location.href = '{url}';</script>",
                ContentType = "text/html"
            };
        }

        public bool IsAjax()
        {
            return HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public bool IsAjax(ExceptionContext filterContext)
        {
            return filterContext.HttpContext.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
        }

        public string ReturnUrl
        {
            get
            {
                if (!string.IsNullOrEmpty(Request.QueryString["returnurl"]))
                {
                    return Request.QueryString["returnurl"];
                }
                return $"/{ControllerContext.RouteData.Values["controller"]}";// ControllerContext.Controller.ToString();
            }
        }

        public static List<SelectListItem> DDLListHazirla<T>(List<T> lst, string dataValueField, string dataTextField,
            Enums.ListeIlkYazı liy = Enums.ListeIlkYazı.Seçiniz, Enums.ListeIlkDeger lid = Enums.ListeIlkDeger.Guid)
        {
            var newList = new List<SelectListItem>();
            newList.AddRange(new SelectList(lst, dataValueField, dataTextField, null));
            if (liy != Enums.ListeIlkYazı.Yok)
                newList.Insert(0, new SelectListItem
                {
                    Text = liy == Enums.ListeIlkYazı.Boş ? "" : liy.ToString(),
                    Value = lid == Enums.ListeIlkDeger.Guid ? Guid.Empty.ToString() : "-1"
                });
            return newList;
        }

        public static List<SelectListItem> DDLListHazirlaEnum(Type T,
            Enums.ListeIlkYazı liy = Enums.ListeIlkYazı.Seçiniz, Enums.ListeIlkDeger lid = Enums.ListeIlkDeger.Guid)
        {
            var values = Enum.GetValues(T);
            var newList = new List<SelectListItem>();

            foreach (var value in values)
            {
                newList.Add(new SelectListItem
                {
                    Value = value.GetHashCode().ToString(),
                    Text = ((Enum)value).ToDescription()
                });
            }
            if (liy != Enums.ListeIlkYazı.Yok)
                newList.Insert(0, new SelectListItem
                {
                    Text = liy == Enums.ListeIlkYazı.Boş ? "" : liy.ToString(),
                    Value = lid == Enums.ListeIlkDeger.Guid ? Guid.Empty.ToString() : "-1"
                });
            return newList;
        }

        public static List<SelectListItem> SetSelectedValue(List<SelectListItem> lst, string selectedValue)
        {
            foreach (var item in lst)
            {
                item.Selected = item.Value == selectedValue;
            }
            return lst;
        }

        protected ActionResult ValidateFormFileUpload(string mimeType, int contentLength, string filename, long size = 5242880, List<EnumFileExtensionTypes> permittedExtensions = null, int attachmentTypeId = 0)
        {
            if (contentLength > size)
                return ShowMessageJSON(String.Format("Seçili dosya yüklenemez.Yüklenen dosyanın boyutu {0} MB ile sınırlıdır!", size / 1048576), AjaxResultState.Fail);

            var message = "Seçili dosya yüklenemez.Yüklenen dosyanın türü PDF, ZIP, RAR, PNG, JPEG, JPG , TIF veya TIFF olmalıdır!";
            var ext = filename.Substring(filename.Length - 4).ToLower().Replace(".", "");

            if (permittedExtensions == null)
            {
                permittedExtensions = new List<EnumFileExtensionTypes> {
                EnumFileExtensionTypes.pdf,
                EnumFileExtensionTypes.tiff,
                EnumFileExtensionTypes.tif,
                EnumFileExtensionTypes.png,
                EnumFileExtensionTypes.jpeg,
                EnumFileExtensionTypes.jpg,
                EnumFileExtensionTypes.zip,
                EnumFileExtensionTypes.rar
                };
            }

            var mimeTypeExts = FileUtilities.GetExtensionbyMimeType(mimeType);
            if (permittedExtensions.Select(s => s.ToString().ToLower()).ToList().Intersect(mimeTypeExts).Any() &&
                permittedExtensions.Select(s => s.ToString().ToLower()).Contains(ext))
            {
                return null;
            }
            else
            {
                if (attachmentTypeId == 744)
                    message = "Seçili dosya yüklenemez.Yüklenen dosyanın türü PDF olmalıdır!";
                else
                    message = $"Seçili dosya yüklenemez.Yüklenen dosyanın türü {string.Join(",", permittedExtensions)}  formatında olmalıdır!";
            }

            return ShowMessageJSON(message, AjaxResultState.Fail);
        }

        protected bool ValidateFormFileUpload(string mimeType, int contentLength, string filename, out string message, long size = 5242880, List<EnumFileExtensionTypes> permittedExtensions = null)
        {
            message = string.Empty;

            if (contentLength > size)
            {
                message = string.Format("Seçili dosya yüklenemez.Yüklenen dosyanın boyutu {0} MB ile sınırlıdır!", size / 1048576);
                return false;
            }
            var ext = filename.Substring(filename.Length - 4).ToLower().Replace(".","");

            if (permittedExtensions == null)
            {
                permittedExtensions = new List<EnumFileExtensionTypes> {
                EnumFileExtensionTypes.pdf,
                EnumFileExtensionTypes.tiff,
                EnumFileExtensionTypes.tif,
                EnumFileExtensionTypes.png,
                EnumFileExtensionTypes.jpeg,
                EnumFileExtensionTypes.jpg,
                EnumFileExtensionTypes.zip,
                EnumFileExtensionTypes.rar
                };
            }

            var mimeTypeExts = FileUtilities.GetExtensionbyMimeType(mimeType);
            if (permittedExtensions.Select(s => s.ToString().ToLower()).ToList().Intersect(mimeTypeExts).Any() &&
                permittedExtensions.Select(s => s.ToString().ToLower()).Contains(ext))
                return true;

            message = $"Seçili dosya yüklenemez.Yüklenen dosyanın türü {string.Join(",", permittedExtensions)}  formatında olmalıdır!";

            return false;
        }

        public static string GetMonthNamebyId(int monthId)
        {
            return Utilities.GetMonthNamebyId(monthId);
        }
    }

    public static class AppConstants
    {
#if DEBUG
        public const bool IS_DEBUG = true;
#else
        public const bool IS_DEBUG = false;
#endif
    }
}