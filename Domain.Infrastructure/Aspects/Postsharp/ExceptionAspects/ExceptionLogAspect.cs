using Domain.Infrastructure.CrossCuttingConcerns.Exceptions;
using Domain.Infrastructure.CrossCuttingConcerns.Logging;
using Domain.Infrastructure.CrossCuttingConcerns.Logging.Log4Net;
using PostSharp.Aspects;
using System;
using System.Reflection;

namespace Domain.Infrastructure.Aspects.Postsharp.ExceptionAspects
{
    [Serializable]
    public class ExceptionLogAspect : OnExceptionAspect
    {
        private readonly Type _loggerType;
        private LoggerService _loggerService;

        public ExceptionLogAspect(Type loggerType)
        {
            _loggerType = loggerType;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (_loggerType.BaseType != typeof(LoggerService))
            {
                throw new Exception("Wrong Logger Type");
            }
            _loggerService = (LoggerService)Activator.CreateInstance(_loggerType);
            base.RuntimeInitialize(method);
        }

        public override void OnException(MethodExecutionArgs args)
        {
            try
            {
                if (!_loggerService.IsErrorEnabled) return;
                if (args.Method.GetCustomAttributes(typeof(NoLog), false).Length > 0) return;

                if (args.Exception is BusinessException ||
                    args.Exception is ValidationCoreException) //Uyarı ve Validation Exceptionlar loglanmayacak 
                    return;

                _loggerService?.Info(args.Arguments);
                _loggerService?.Error(args.Exception);

            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
