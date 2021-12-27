using System;

namespace Domain.Infrastructure.CrossCuttingConcerns.Exceptions
{
    public class BusinessException : NotificationException
    {
        public BusinessException(string mesaj)
            : base(mesaj)
        {

        }

        public BusinessException(string mesaj, Exception exception)
            : base(mesaj, exception)
        {

        }
    }
}