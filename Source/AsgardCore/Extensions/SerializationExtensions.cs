using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AsgardCore
{
    public static partial class Extensions
    {
        public static void Serialize32(this IReadOnlyList<byte> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this byte[] list, BinaryWriter bw)
        {
            bw.Write(list.Length);
            for (int i = 0; i < list.Length; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this List<byte> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this IReadOnlyList<int> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this int[] list, BinaryWriter bw)
        {
            bw.Write(list.Length);
            for (int i = 0; i < list.Length; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this List<int> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this IReadOnlyList<string> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this string[] list, BinaryWriter bw)
        {
            bw.Write(list.Length);
            for (int i = 0; i < list.Length; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32(this List<string> list, BinaryWriter bw)
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                bw.Write(list[i]);
        }

        public static void Serialize32<T>(this IReadOnlyList<T> list, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(list.Count);
            for (int i = 0; i < list.Count; i++)
                list[i].Serialize(bw);
        }

        public static void Serialize32<T>(this T[] list, BinaryWriter bw)
            where T : ISerializable
        {
            bw.Write(list.Length);
            for (int i = 0; i < list.Length; i++)
                list[i].Serialize(bw);
        }

        public static void Serialize32<T>(this List<T> list, BinaryWriter bw)
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

        /// <summary>
        /// Serializes the given <see cref="ISerializable"/> instance to a byte array,
        /// using <see cref="Encoding.UTF8"/> encoding.
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
