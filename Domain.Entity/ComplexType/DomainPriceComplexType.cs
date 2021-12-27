namespace Domain.Entity.ComplexType
{
    public class DomainPriceComplexType
    {
        public int DomainPriceId { get; set; }
        public int TLDTypeId { get; set; }
        public string TLDTypeDescription { get; set; }
        public decimal RegisterPrice { get; set; }
        public decimal? RegisterPriceWithDiscount { get; set; }
        public decimal TransferPrice { get; set; }
        public decimal? TransferPriceWithDiscount { get; set; }
        public decimal RefreshPrice { get; set; }
        public decimal? RefreshPriceWithDiscount { get; set; }
    }
}
