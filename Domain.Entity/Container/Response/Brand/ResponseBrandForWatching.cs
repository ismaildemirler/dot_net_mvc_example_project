using Domain.Entity.Concrete;
using System.Collections.Generic;

namespace Domain.Entity.Container.Response.Brand
{
    public class ResponseBrandForWatching
    {
        public BrandForWatching BrandForWatching { get; set; }
        public List<BrandForWatching> BrandForWatchings { get; set; }
    }
}
