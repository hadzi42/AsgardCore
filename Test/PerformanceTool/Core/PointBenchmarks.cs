using AsgardCore.Modeling;
using BenchmarkDotNet.Attributes;

namespace PerformanceTool.Core
{
    [HideColumns("Error", "Median", "StdDev")]
    [MemoryDiagnoser(false)]
    public class PointBenchmarks
    {
        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 1.74 us, 2.68 KB
        /// V 2.0.0.0, .NET 6,      AMD 5800X, 1.48 us, 2.67 KB
        /// </summary>
        //[Benchmark]
        public HashSet<Point> AddToHashSet()
        {
            HashSet<Point> set = new HashSet<Point>(121);
            for (int i = -5; i < 6; i++)
                for (int j = -5; j < 6; j++)
                    set.Add(new Point(i, j));
            return set;
        }

        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 28.6 ns, 0 B
        /// V 2.0.0.0, .NET 6,      AMD 5800X, 29.8 ns, 0 B
        /// </summary>
        [Benchmark]
        public int HashCode()
        {
            int hash = 0;
            for (int i = -5; i < 6; i++)
                for (int j = -5; j < 6; j++)
                    hash ^= new Point(i, j).GetHashCode();
            return hash;
        }
    }
}
