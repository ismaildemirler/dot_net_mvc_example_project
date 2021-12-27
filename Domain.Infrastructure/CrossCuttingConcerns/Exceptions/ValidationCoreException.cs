using System;
using System.Text;
using System.Web.UI;
using System.Web.UI.HtmlControls;

namespace Domain.Infrastructure.CrossCuttingConcerns.Exceptions
{
    public class ValidationCoreException : NotificationException
    {
        public ValidationCoreException(ArgumentException innerException) :
            base(null, innerException)
        {

        }

        public ValidationCoreException(string mesaj)
            : base(mesaj)
        {

        }

        public ValidationCoreException(string mesaj, ArgumentException hata)
            : base(mesaj, hata)
        {

        }

        public override string Message
        {
            get
            {
                var innerException = InnerException as ArgumentException;
                if (innerException != null)
                {
                    HtmlGenericControl divmenu = new HtmlGenericControl();
                    StringBuilder sb = new StringBuilder();
                    sb.Append("<div class=" + "\"" + "validationmesaj" + "\"" + ">");
                    sb.Append(innerException.ParamName);
                    sb.Append("</div>");
                    divmenu.InnerHtml = sb.ToString();

                    string contents = null;
                    using (System.IO.StringWriter swriter = new System.IO.StringWriter())
                    {
                        HtmlTextWriter writer = new HtmlTextWriter(swriter);
                        divmenu.RenderControl(writer);
                        contents = swriter.ToString();
                    }

                    //HtmlControl control = Control.FindControl("divAllItemList") as HtmlControl;
                    //control.Attributes["class"] = "hidden"; 

                    return contents;
                }

                return base.Message;
            }
        }
    }
}