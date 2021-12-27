using System;

namespace Domain.Entity.ComplexType
{
    public class BrandForWatchingApplicationComplexType
    {
        public Guid BrandWatchingApplicationDetailId { get; set; }
        public Guid BrandForWatchingId { get; set; }
        public string BrandFirmName { get; set; }
        public string BrandName { get; set; }
        public string ClassesToWatch { get; set; }
        public string RegistryNumber { get; set; }
        public string ApplicationFirmName { get; set; }
        public string IdentityNumber { get; set; }
        public string Address { get; set; }
        public string TownName { get; set; }
        public string CityName { get; set; }
        public string Phone { get; set; }
    }
}
