
namespace Domain.Web.Models.Domain
{
    public class DomainApiResponseInfoViewModel
    {
        public string ClassKey { get; set; }
        public string Command { get; set; }
        public string Currency { get; set; }
        public string DomainName { get; set; }
        public string Status { get; set; }
        public bool? IsFee { get; set; }
        public int? Period { get; set; }
        public decimal? Price { get; set; }
        public string Reason { get; set; }
        public string Tld { get; set; }
        public int TldId { get; set; }
    }
}