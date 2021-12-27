using System;
using log4net.Core;
using Domain.Infrastructure.Utilities.Common;

namespace Domain.Infrastructure.CrossCuttingConcerns.Logging.Log4Net
{
    [Serializable]
    public class SerializableLogEvent
    {
        private readonly LoggingEvent _loggingEvent;
        public SerializableLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }
        public string Domain => _loggingEvent.Domain;
        public DateTime TimeStamp => _loggingEvent.TimeStamp;
        public string UserId => CurrentUserPrincipal.Identity.UserId.ToString();
        public string UserName => CurrentUserPrincipal.Identity.UserName ?? "Anonymous";
        public string Ip => CurrentUserPrincipal.Identity.Ip ?? "";
        public string AppId => Utilities.Common.Utilities.GetAppId().ToString();
        public object MessageObject => _loggingEvent.MessageObject;
    }
    [Serializable]
    public class SerializableMYSLogEvent
    {
        private readonly LoggingEvent _loggingEvent;
        public SerializableMYSLogEvent(LoggingEvent loggingEvent)
        {
            _loggingEvent = loggingEvent;
        }
        public DateTime TimeStamp => DateTime.Now;
        public object MessageObject => _loggingEvent.MessageObject;
    }
}
