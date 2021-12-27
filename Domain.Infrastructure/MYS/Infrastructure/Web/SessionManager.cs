using System.Web;
using System.Web.Configuration;
using System.Web.SessionState;
using Newtonsoft.Json;

namespace Domain.Infrastructure.MYS.Infrastructure.Web
{
    internal class SessionManager
    {
        private static readonly SessionStateSection SessionSection = (SessionStateSection)WebConfigurationManager.GetSection("system.web/sessionState");
        private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings
        {
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static void Add(object o, string name)
        {
            if (o == null)
            {
                Remove(name);
                return;
            }

            if (!IsExist(name))
                HttpContext.Current.Session.Add(name, SessionSection.Mode == SessionStateMode.InProc ? o : JsonConvert.SerializeObject(o, JsonSerializerSettings));
            else
                Set(name, o);
        }
        public static void Remove(string name)
        {
            HttpContext.Current.Session.Remove(name);
        }
        public static object Get(string name)
        {
            if (HttpContext.Current.Session[name] == null)
                return null;

            return SessionSection.Mode == SessionStateMode.InProc ?
                HttpContext.Current.Session[name] :
                JsonConvert.DeserializeObject<object>(HttpContext.Current.Session[name].ToString());
        }

        public static bool IsExist(string name) { return HttpContext.Current.Session[name] != null; }

        public static void Set(string name, object o)
        {
            if (o != null)
            {
                if (SessionSection.Mode == SessionStateMode.InProc)
                    HttpContext.Current.Session[name] = o;
                else
                    HttpContext.Current.Session[name] = JsonConvert.SerializeObject(o, JsonSerializerSettings);
            }
            else
                Remove(name);
        }

        public static T Get<T>(string name) where T : new()
        {
            if (HttpContext.Current.Session[name] == null)
                return new T();

            return SessionSection.Mode == SessionStateMode.InProc ?
                (T)HttpContext.Current.Session[name] :
                JsonConvert.DeserializeObject<T>(HttpContext.Current.Session[name].ToString());
        }
    }
}
