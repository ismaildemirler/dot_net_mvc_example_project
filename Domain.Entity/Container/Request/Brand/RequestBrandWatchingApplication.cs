using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Request.Brand
{
    public class RequestBrandWatchingApplication
    {
        public CustomerApplication CustomerApplication { get; set; }
        public BrandWatchingApplicationDetail BrandWatchingApplicationDetail { get; set; }
    }
}
