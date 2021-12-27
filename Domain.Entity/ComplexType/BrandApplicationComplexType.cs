using System;

namespace Domain.Entity.ComplexType
{
    public class BrandApplicationComplexType
    {
        public Guid BrandApplicationDetailId { get; set; }
        public string BrandName { get; set; }
        public string BrandCategoryDescription { get; set; }
        public byte? ApplicationTypeId { get; set; }
        public byte? BrandApplicationTypeId { get; set; }
        public string SpecialClass { get; set; }
        public bool SendCoverLetter { get; set; }
        public Guid? ContactId { get; set; }
        public Guid CustomerApplicationId { get; set; }
        public Guid? BeneficiaryId { get; set; }
        public string FirmName { get; set; }
        public string TCNumber { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? TownId { get; set; }
        public string TownName { get; set; }
        public string PhoneNumber { get; set; }
        public byte? FirmStatuTypeId { get; set; }
        public string ApplicationTypeDescription { get; set; }
    }
}
