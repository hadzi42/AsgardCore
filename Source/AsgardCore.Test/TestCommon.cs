using System;
using System.IO;
using System.Text;

namespace AsgardCore.Test
{
    /// <summary>
    /// Class containing common test-helpers.
    /// </summary>
    public static class TestCommon
    {
        /// <summary>
        /// Serializes the given <see cref="ISerializable"/> object and deserializes it into a new instance using the provided <paramref name="deserialization"/>.
        /// </summary>
        /// <remarks>This type of binary serialization is very fragile and needs thorough testing. This method greatly helps to achieve this.</remarks>
        /// <typeparam name="T">An <see cref="ISerializable"/> type.</typeparam>
        /// <param name="obj">The object instance to serialize and replace.</param>
        /// <param name="deserialization">The deserialization logic which also creates a new instance.</param>
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