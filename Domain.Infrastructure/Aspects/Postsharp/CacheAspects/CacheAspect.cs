using Domain.Infrastructure.CrossCuttingConcerns.Caching;
using PostSharp.Aspects;
using System;
using System.Linq;
using System.Reflection;

namespace Domain.Infrastructure.Aspects.Postsharp.CacheAspects
{
    [Serializable]
    public class CacheAspect : MethodInterceptionAspect
    {
        private readonly Type _cacheType;
        private readonly int _cacheByMunite;
        private ICacheManager _cacheManager;

        public CacheAspect(Type cacheType, int cacheByMunite = 60)
        {
            _cacheType = cacheType;
            _cacheByMunite = cacheByMunite;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            if (!typeof(ICacheManager).IsAssignableFrom(_cacheType))
            {
                throw new Exception("Wrong Cache Manager");
            }
            _cacheManager = (ICacheManager)Activator.CreateInstance(_cacheType);

            base.RuntimeInitialize(method);
        }

        public override void OnInvoke(MethodInterceptionArgs args)
        {
            var methodName = $"{args.Method.ReflectedType.Namespace}.{args.Method.ReflectedType.Name}.{args.Method.Name}";
            var arguments = args.Arguments.ToList();

            var key = $"{methodName}({string.Join(",", arguments.Select(p => p?.ToString() ?? "<Null>"))})";

            if (_cacheManager.IsAdd(key))
                args.ReturnValue = _cacheManager.Get<object>(key);
            else
            {
                base.OnInvoke(args);
                _cacheManager.Add(key, args.ReturnValue, _cacheByMunite);
            }
        }
    }
}
