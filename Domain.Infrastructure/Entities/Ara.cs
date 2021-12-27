using System.Collections.Generic;

namespace Domain.Infrastructure.Entities
{
    public class Ara
    {
        public Ara()
        {
            Baslangic = 0;
            Uzunluk = 10;
            Siralama = new List<Siralama>();
        }
        public int? Baslangic { get; set; }
        public int? Uzunluk { get; set; }
        public List<Siralama> Siralama { get; set; }
    }
}
