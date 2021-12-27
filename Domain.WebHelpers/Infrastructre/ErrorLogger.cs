using Domain.Infrastructure.MYS;
using Domain.Infrastructure.MYS.Infrastructure;
using Domain.Infrastructure.MYS.Infrastructure.Containers.Request.Logging;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Domain.WebHelpers.Infrastructre
{
    public class ErrorLogger
    {
        public void HataLogla(string exMessage, string hata, string methodName = "")
        {
            try
            {
                MYSHelper helper = new MYSHelper();
                helper.ErrorLogSave(new RequestErrorLog
                {
                    UserId = CurrentUser.Identity.UserId,
                    MethodName = methodName,
                    ErrorDetail = hata,
                    ErrorMessage = exMessage,
                    ErrorTime = DateTime.Now,
                    Ip = Domain.Infrastructure.MYS.Infrastructure.Utilities.GetIp(),
                    ServerName = Environment.MachineName
                });
            }
            catch (Exception ex)
            {
                EventLogYaz(hata + ex);
            }
        }
        public void HataLogla(Exception hata, string methodName = "")
        {
            var exList = new List<Exception> { hata };
            var exStr = "";
            if (hata is DbEntityValidationException)
            {
                var dbEx = (DbEntityValidationException)hata;
                var errorMessages = dbEx.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                   .Select(x => x.ErrorMessage);

                var fullErrorMessage = string.Join("; ", errorMessages);
                exStr = string.Concat(hata.Message, " The validation errors are: ", fullErrorMessage);
            }
            while (hata.InnerException != null)
            {
                hata = hata.InnerException;
                exList.Add(hata);
            }
            exStr += exList.Aggregate("", (current, ex) => current + ex.ToString());
            HataLogla(hata.Message, exStr, methodName);

        }
        public void EventLogYaz(string hataMesaji, EventLogEntryType erType = EventLogEntryType.Error)
        {
            var appId = ConfigurationManager.AppSettings["DomainMYSAppId"];
            try
            {
                var klasorYolu = HttpContext.Current.Server.MapPath("~/App_Data/Logs");

                if (!Directory.Exists(klasorYolu))
                    Directory.CreateDirectory(klasorYolu);

                var logDosyasi = $"{klasorYolu}/{DateTime.Now.ToShortDateString()} - {appId}.txt";

                using (var w = File.AppendText(logDosyasi))
                {
                    w.Write("\r\n");
                    w.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(),
                        DateTime.Now.ToLongDateString());
                    w.WriteLine("  :{0}", hataMesaji);
                    w.WriteLine("-------------------------------");
                }
            }
            catch (Exception e)
            {
                if (!EventLog.SourceExists(appId))
                {
                    EventLog.CreateEventSource(appId, "Application");
                }
                EventLog.WriteEntry(appId, hataMesaji + e.Message, erType);
            }
        }
    }
}