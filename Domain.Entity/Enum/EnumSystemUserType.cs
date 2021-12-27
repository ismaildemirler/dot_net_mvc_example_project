using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Domain.Entity.Enum
{
    [Flags]
    public enum EnumSystemUserType
    {
        [EnumMember]
        [Description("Müşteri")]
        Customer = 0,

        [EnumMember]
        [Description("Admin")]
        Admin = 1
    }
}
