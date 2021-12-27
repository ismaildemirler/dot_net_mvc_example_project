using System.Runtime.Serialization;

namespace Domain.Infrastructure.MYS.Infrastructure.Enums
{
    public enum EnumUserStates
    {
        [EnumMember]
        Pending = 1,
        [EnumMember]
        Active = 2,
        [EnumMember]
        Passive = 3,
        [EnumMember]
        All = 4,
        [EnumMember]
        Closed = 5
    }
}
