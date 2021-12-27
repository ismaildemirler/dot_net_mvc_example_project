using Domain.Entity.Enum;

namespace Domain.Entity.Container.Request.Parameter
{
    public class RequestTLDType
    {
        public int TLDTypeId { get; set; }
        public string Description { get; set; }
        public EnumState StateId { get; set; }
    }
}
