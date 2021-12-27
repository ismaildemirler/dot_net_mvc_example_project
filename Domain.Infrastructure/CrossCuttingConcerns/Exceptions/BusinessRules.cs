using System;
using System.Collections.Generic;

namespace Domain.Infrastructure.CrossCuttingConcerns.Exceptions
{
    
    public static class BusinessRules
    {
        static BusinessRules()
        {
            BusinessExceptions=new List<BusinessException>();
        }

        [ThreadStatic] public static List<BusinessException> BusinessExceptions;

        public static void Add(BusinessException businessException)
        {
            BusinessExceptions.Insert(0,businessException);
            
        }
    }
}
