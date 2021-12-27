using System;

namespace Domain.Entity.Container.Request.Brand
{
    public class RequestBrandApplicationDetail
    {
        public Guid CustomerApplicationId { get; set; }
        public Guid BrandApplicationDetailId { get; set; }
        public string BrandName { get; set; }
    }
}
