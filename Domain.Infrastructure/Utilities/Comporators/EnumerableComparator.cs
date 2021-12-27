using System.Collections.Generic;

namespace Domain.Infrastructure.Utilities.Comporators
{
    internal class EnumerableComparator<T> : Comparator
    {
        internal override bool AreEqual(object value1, object value2)
        {
            var val1 = (IEnumerable<T>)value1;
            var val2 = (IEnumerable<T>)value2;

            if (val1 == null && val2 == null)
            {
                return true;
            }

            if (val1 == null || val2 == null)
            {
                return false;
            }

            return System.Linq.Enumerable.SequenceEqual(val1, val2);
        }
    }
}
