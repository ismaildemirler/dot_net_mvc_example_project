using System;
using System.Security;
using PostSharp.Aspects;

namespace Domain.Infrastructure.Aspects.Postsharp.AuthorizationAspects
{
    [Serializable]
    public class SecuredOperation : OnMethodBoundaryAspect
    {
        public string Roles { get; set; }

        public override void OnEntry(MethodExecutionArgs args)
        { 
            var roles = Roles.Split(',');
            var isAuthorized = false;
            foreach (var t in roles)
            {
                if (System.Threading.Thread.CurrentPrincipal.IsInRole(t))
                    isAuthorized = true;
            }

            if (!isAuthorized)
                throw new SecurityException("You are not authorized!");
        }
    }
}
