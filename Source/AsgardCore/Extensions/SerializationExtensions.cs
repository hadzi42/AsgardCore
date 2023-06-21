using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AsgardCore
{
    public static partial class Extensions
    {
        public static void Serialize32(this IList<byte> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this IList<int> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this IList<string> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32<T>(this IList<T> list, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                list[i].Serialize(bw);
        }

        public static void Serialize32(this HashSet<int> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            foreach (var i in list)
                bw.Write(i);
        }

        public static void Serialize32(this HashSet<string> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            foreach (var i in list)
                bw.Write(i);
        }

        public static void Serialize32<T>(this HashSet<T> list, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(list.Count);
            foreach (var i in list)
                i.Serialize(bw);
        }

        public static void Serialize32(this IDictionary<int, string> dict, BinaryWriter bw)
        {
            bw.Write(dict.Count);
            foreach (var kvp in dict)
            {
                bw.Write(kvp.Key);
                bw.Write(kvp.Value);
            }
        }

        public static void Serialize32<T>(this IDictionary<int, T> dict, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(dict.Count);
            foreach (var kvp in dict)
            {
                bw.Write(kvp.Key);
                kvp.Value.Serialize(bw);
            }
        }

        public static void Serialize32<T>(this IDictionary<string, T> dict, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(dict.Count);
            foreach (var kvp in dict)
            {
                bw.Write(kvp.Key);
                kvp.Value.Serialize(bw);
            }
        }

        public static void Serialize32<TKey, TValue>(this Dictionary<TKey, TValue> dict, BinaryWriter bw)
            where TKey : ISerializable
            where TValue : ISerializable
        {
            bw.Write(dict.Count);
            foreach (KeyValuePair<TKey, TValue> kvp in dict)
            {
                kvp.Key.Serialize(bw);
                kvp.Value.Serialize(bw);
            }
        }

        /// <summary>
        /// Serializes the given <see cref="ISerializable"/> instance to a byte array.
        /// </summary>
        public static byte[] SerializeToByteArray(this ISerializable serializable)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    serializable.Serialize(bw);
                }
                return ms.ToArray();
            }
        }
    }
}
