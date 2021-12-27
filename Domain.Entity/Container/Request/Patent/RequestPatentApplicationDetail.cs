using System;

namespace Domain.Entity.Container.Request.Patent
{
    public class RequestPatentApplicationDetail
    {
        public Guid CustomerApplicationId { get; set; }
        public Guid PatentApplicationDetailId { get; set; }
        public string Title { get; set; }
    }
}
