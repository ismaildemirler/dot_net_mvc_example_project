using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumCurrencyType
    {
        [EnumMember]
        [Description("₺")]
        tl = 0,

        [EnumMember]
        [Description("$")]
        dollar = 1
    }
}
