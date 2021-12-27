using System;

namespace Domain.Entity.ComplexType
{
    public class PatentApplicationComplexType
    {
        public Guid PatentApplicationDetailId { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public byte? ApplicationTypeId { get; set; }
        public Guid CustomerApplicationId { get; set; }
        public Guid? BeneficiaryId { get; set; }
        public string FirmName { get; set; }
        public string PersonName { get; set; }
        public string TCNumber { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string Address { get; set; }
        public int? CityId { get; set; }
        public string CityName { get; set; }
        public int? TownId { get; set; }
        public string TownName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
