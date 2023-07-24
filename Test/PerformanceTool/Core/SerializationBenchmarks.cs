using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using AsgardCore;
using BenchmarkDotNet.Attributes;

namespace PerformanceTool.Core
{
    [HideColumns("Error", "Median", "StdDev")]
    [MemoryDiagnoser(false)]
    public class SerializationBenchmarks
    {
        private static readonly List<int> _List = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };

        /// <summary>
        /// V 2.0.0.0, .NET 6, AMD 5800X, 97.5 ns, 448 B
        /// </summary>
        //[Benchmark]
        public byte[] Serialize_AsList()
        {
            List<int> data = _List;

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    Extensions.Serialize32(data, bw);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// V 2.0.0.0, .NET 6, AMD 5800X, 113.1 ns, 448 B
        /// </summary>
        //[Benchmark]
        public byte[] Serialize_AsReadOnlyList()
        {
            IReadOnlyList<int> data = _List;

            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    Extensions.Serialize32(data, bw);
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// V 2.0.0.0, .NET 6, AMD 5800X, 20.9 ns, 104 B
        /// </summary>
        [Benchmark]
        public byte[] Serialize_Overhead()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                { }
                return ms.ToArray();
            }
        }
    }
}
