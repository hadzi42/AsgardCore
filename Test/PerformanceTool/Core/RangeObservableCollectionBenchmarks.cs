using AsgardCore;
using AsgardCore.MVVM;
using BenchmarkDotNet.Attributes;

namespace PerformanceTool.Core
{
    [HideColumns("Error", "Median", "StdDev")]
    [MemoryDiagnoser(false)]
    public class RangeObservableCollectionBenchmarks
    {
        private static readonly RangeObservableCollection<string> _List = new RangeObservableCollection<string>
        {
            "1", "2", "3", "4", "5", "6", "7", "8"
        };

        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 1.10 us, 658 B
        /// V 2.0.0.0, .NET 6,      AMD 5800X, 863 ns, 712 B
        /// </summary>
        //[Benchmark]
        public void SortNatural()
        {
            _List.SortNatural(Selectors.SelfSelector);
        }

        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 677 ns, 1.77 KB
        /// V 2.0.0.0, .NET 6,      AMD 5800X, 640 ns, 1.07 KB
        /// </summary>
        //[Benchmark]
        public void AddToSorted_Natural()
        {
            RangeObservableCollection<string> list = new RangeObservableCollection<string>
            {
                "1", "2", "3", "4", "5", "6", "7", "8"
            };
            list.AddToSorted("5", Selectors.SelfSelector);
        }

        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 410 ns, 1.87 KB
        /// V 2.0.0.0, .NET 6     , AMD 5800X, 238 ns, 1.17 KB
        /// </summary>
        [Benchmark]
        public void AddAndSort_Generic()
        {
            RangeObservableCollection<float> list = new RangeObservableCollection<float>
            {
                1, 2, 3, 4, 5, 6, 7, 8
            };
            list.AddToSorted(5, Selectors.SelfSelector);
        }
    }
}
