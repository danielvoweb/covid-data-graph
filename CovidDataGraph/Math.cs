using System.Collections.Generic;
using System.Linq;

namespace CovidDataGraph
{
    public static partial class Math
    {
        public static decimal StdDev(this IEnumerable<decimal> values)
        {
            decimal average = values.Average();
            decimal sum = values.Sum(x => (x - average) * (x - average));
            return (decimal)System.Math.Sqrt((double)(sum / values.Count()));
        }
    }
}
