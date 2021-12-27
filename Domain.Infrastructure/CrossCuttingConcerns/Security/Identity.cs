using System;
using System.Globalization;
using System.Security.Principal;

namespace Domain.Infrastructure.CrossCuttingConcerns.Security
{
    [Serializable]
    public class DomainMYSIdentity : IIdentity
    {
        public string Name { get; set; }
        public string AuthenticationType { get; set; }
        public bool IsAuthenticated { get; set; }
        public string Id { get; set; }
        public int UserId { get; set; }
        public int? ProxyUserId { get; set; }
        public string Ip { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string NameSurname
        {
            get
            {
                if (FirstName != null && LastName != null)
                {
                    return FirstName.ToUpper(new CultureInfo("tr-TR")) + " " + LastName.ToUpper(new CultureInfo("tr-TR"));
                }
                return "";
            }
        }
        public string EMail { get; set; }
        public Guid? OperationGuid { get; set; }
        public byte UserTypeId { get; set; }
    }
}
