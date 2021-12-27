using Domain.Entity.Concrete;

namespace Domain.Entity.Container.Request.IndustrialDesign
{
    public class RequestIndustrialDesignApplication
    {
        public Concrete.Attachment Attachment { get; set; }
        public CustomerApplication CustomerApplication { get; set; }
        public IndustrialDesignApplicationDetail IndustrialDesignApplicationDetail { get; set; }
    }
}
