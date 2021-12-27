using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumApplicationType
    {
        [EnumMember]
        [Description("Marka Başvurusu")]
        Marka_Basvurusu = 1,

        [EnumMember]
        [Description("Patent Başvurusu")]
        Patent_Basvurusu = 2,

        [EnumMember]
        [Description("Marka İzleme Başvurusu")]
        Marka_Izleme_Basvurusu = 3,

        [EnumMember]
        [Description("Marka Yenileme Başvurusu (Normal)")]
        Marka_Yenileme_Normal_Basvurusu = 4,

        [EnumMember]
        [Description("Marka Yenileme Başvurusu (Cezalı)")]
        Marka_Yenileme_Cezali_Basvurusu = 5,

        [EnumMember]
        [Description("Adres Değişikliği Başvurusu")]
        Adres_Degisikligi_Basvurusu = 6,

        [EnumMember]
        [Description("Unvan Değişikliği Başvurusu")]
        Unvan_Degisikligi_Basvurusu = 7,

        [EnumMember]
        [Description("Nevi Değişikliği Başvurusu")]
        Nevi_Degisikligi_Basvurusu = 8,

        [EnumMember]
        [Description("Devir İşlemi Başvurusu")]
        Devir_Islemi_Basvurusu = 9,

        [EnumMember]
        [Description("Veraset İntikal Başvurusu")]
        Veraset_Intikal_Basvurusu = 10,

        [EnumMember]
        [Description("Rehin İşlemi Başvurusu")]
        Rehin_Islemi_Basvurusu = 11,

        [EnumMember]
        [Description("Lisans İşlemi Başvurusu")]
        Lisans_Islemi_Basvurusu = 12,

        [EnumMember]
        [Description("Enstitü Kararlarına İtiraz İşlemi")]
        Enstitu_Kararlarina_Itiraz_Islemi = 13,

        [EnumMember]
        [Description("Marka Yayın Kararına İtiraz İşlemi")]
        Marka_Yayin_Kararina_Itiraz_Islemi = 14,

        [EnumMember]
        [Description("İtiraza Karşı Görüş")]
        Itiraza_Karsi_Gorus = 15,

        [EnumMember]
        [Description("Endüstriyel Tasarım Başvurusu")]
        Endustriyel_Tasarim_Basvurusu = 16
    }
}
