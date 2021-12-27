namespace Domain.Infrastructure.Utilities.Exception
{
    public class ExceptionUtility
    {
        public static string FormattedException(string exception)
        {
            string exceptionFormat = "";
            exceptionFormat += "<ul class='validationexceptions'>";
            foreach (var item in exception.Split(','))
            {
                if (!string.IsNullOrWhiteSpace(item))
                {
                    exceptionFormat += "<li class='validationexception'>" + item + "</li>";
                }
            }
            exceptionFormat += "</ul>";
            return exceptionFormat;
        }
    }
}
