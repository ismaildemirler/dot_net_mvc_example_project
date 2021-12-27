using log4net;

namespace Domain.Infrastructure.CrossCuttingConcerns.Logging.Log4Net.Loggers
{
    public class TableLogFileLogger : LoggerService
    {
        public TableLogFileLogger() : base(LogManager.GetLogger("TableLogJsonFileLogger"))
        {
        }
    }

    public class ErrorFileLogger : LoggerService
    {
        public ErrorFileLogger() : base(LogManager.GetLogger("ErrorJsonFileLogger"))
        {
        }
    }

    public class MYSLogger : LoggerService
    {
        public MYSLogger() : base(LogManager.GetLogger("MYSLoggerJsonFileLogger"))
        {
        }
    }
}
