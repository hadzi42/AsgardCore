using System.Text;

namespace AsgardCore.Test
{
    /// <summary>
    /// Class containing common test-helpers.
    /// </summary>
    public static class TestCommon
    {
        public static void SerializationCore<T>(ref T obj, Func<BinaryReader, T> deserialization)
            where T : ISerializable
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    obj.Serialize(bw);

                    ms.Seek(0, SeekOrigin.Begin);
                    using (BinaryReader br = new BinaryReader(ms, Encoding.UTF8))
                    {
                        obj = deserialization(br);
                    }
                }
            }
        }
    }
}