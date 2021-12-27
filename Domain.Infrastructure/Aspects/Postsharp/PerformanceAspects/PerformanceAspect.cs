using System;
using System.Diagnostics;
using System.Reflection;
using PostSharp.Aspects;

namespace Domain.Infrastructure.Aspects.Postsharp.PerformanceAspects
{
    [Serializable]
    public class PerformanceAspect : OnMethodBoundaryAspect
    {
        private readonly int _interval;
        [NonSerialized]
        private Stopwatch _stopwatch;

        public PerformanceAspect(int interval = 5)
        {
            _interval = interval;
        }

        public override void RuntimeInitialize(MethodBase method)
        {
            _stopwatch = Activator.CreateInstance<Stopwatch>();
        }

        public override void OnEntry(MethodExecutionArgs args)
        {
            _stopwatch.Start();
            base.OnEntry(args);
        }

        public override void OnExit(MethodExecutionArgs args)
        {
            _stopwatch.Stop();
            if (_stopwatch.Elapsed.TotalSeconds > _interval)
            {
                Debug.WriteLine($"Performance:{args.Method.DeclaringType.FullName}.{args.Method.Name}->>{_stopwatch.Elapsed.TotalSeconds}");
            }
            base.OnExit(args);
        }
    }
}
