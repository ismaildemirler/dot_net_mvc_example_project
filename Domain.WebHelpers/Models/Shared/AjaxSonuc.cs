using Newtonsoft.Json;

namespace Domain.WebHelpers.Models.Shared
{
    public class AjaxResult
    {
        public AjaxResultState Result { get; set; }
        public int State => Result.GetHashCode();
        public string Message { get; set; }
        public string RedirectUrl { get; set; }
        public string ModelStr => JsonConvert.SerializeObject(Model);
        public object Model { get; set; }
    }

    public enum AjaxResultState
    {
        Fail = 0,
        Success = 1,
        Warning = 2,
        Confirm = 3,
    }
}