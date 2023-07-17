using BenchmarkDotNet.Running;
using PerformanceTool.Core;
using PerformanceTool.Test;

namespace PerformanceTool
{
    internal sealed class Program
    {
        public static void Main()
        {
            BenchmarkRunner.Run(typeof(PointBenchmarks));
        }
    }
}