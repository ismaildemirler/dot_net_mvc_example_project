using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Response.IndustrialDesign
{
    public class ResponseIndustrialDesignApplication
    {
        public CustomerApplication CustomerApplication { get; set; }
        public IndustrialDesignApplicationDetail IndustrialDesignApplicationDetail { get; set; }
    }
}
