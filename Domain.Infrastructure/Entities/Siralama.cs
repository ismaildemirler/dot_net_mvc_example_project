using Domain.Infrastructure.Enums;

namespace Domain.Infrastructure.Entities
{
    public class Siralama
    {
        public Siralama()
        {
            OrderType = EnumOrders.Asc;
        }

        public string KolonAdi { get; set; }
        public EnumOrders OrderType { get; set; }
    }
}
