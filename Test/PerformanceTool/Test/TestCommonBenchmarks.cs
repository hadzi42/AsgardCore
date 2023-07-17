using AsgardCore;
using AsgardCore.Test;
using BenchmarkDotNet.Attributes;

namespace PerformanceTool.Test
{
    [HideColumns("Error", "Median", "StdDev")]
    [MemoryDiagnoser(false)]
    public class TestCommonBenchmarks
    {
        /// <summary>
        /// V 2.0.0.0, .NET FW 4.8, AMD 5800X, 101.0 ns, 554 B
        /// V 2.0.0.0, .NET 6,      AMD 5800X,  53.6 ns, 376 B
        /// </summary>
        [Benchmark]
        public ISerializable SerializationCore()
        {
            Serializable s = new Serializable();
            TestCommon.SerializationCore(ref s, Serializable.Restore);
            return s;
        }

        private sealed class Serializable : ISerializable
        {
            public Serializable()
            { }

            public Serializable(BinaryReader _)
            { }

            public static Serializable Restore(BinaryReader br)
            {
                return new Serializable(br);
            }

            public void Serialize(BinaryWriter bw)
            { }
        }
    }
}
