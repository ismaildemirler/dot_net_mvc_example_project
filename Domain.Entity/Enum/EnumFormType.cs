using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumFormType
    {
        [EnumMember]
        [Description("Seçimsiz")]
        Nothing = 0,

        [EnumMember]
        [Description("Buluş Anlatım Dosyası")]
        Bulus_Anlatim_Dosyasi = 1,

        [EnumMember]
        [Description("Buluş Çizim Dosyası")]
        Bulus_Cizim_Dosyasi = 2,

        [EnumMember]
        [Description("Marka Başvuru Dosyası")]
        Marka_Basvuru_Dosyasi = 3,

        [EnumMember]
        [Description("Endüstriyel Tasarım Fotoğrafı")]
        Endustriyel_Tasarim_Fotograf_Dosyasi = 4,

        [EnumMember]
        [Description("Endüstriyel Tasarım Çoklu Fotoğrafları")]
        Endustriyel_Tasarim_Coklu_Fotograf_Dosyasi = 5
    }
}
