using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Response.Brand
{
    public class ResponseBrandWatchingApplication
    {
        public CustomerApplication CustomerApplication { get; set; }
        public BrandWatchingApplicationDetail BrandWatchingApplicationDetail { get; set; }
    }
}
