using System;

namespace Domain.Infrastructure.CrossCuttingConcerns.Exceptions
{
    public class NotificationException : Exception
    {
        public NotificationException(string mesaj)
            : base(mesaj + "|NotificationException")
        {
            
        }

        public NotificationException(string mesaj, Exception hata)
            : base(mesaj + "|NotificationException", hata)
        {

        }
    }
}
