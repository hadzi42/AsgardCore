using System;
using System.Collections.Generic;
using System.IO;

namespace AsgardCore
{
    public static partial class Extensions
    {
        public static byte[] DeserializeByteArray32(BinaryReader br)
        {
            int count = br.ReadInt32();
            return br.ReadBytes(count);
        }

        public static List<byte> DeserializeByteList32(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<byte> list = new List<byte>(count);
            for (int i = 0; i < count; i++)
                list.Add(br.ReadByte());
            return list;
        }

        public static int[] DeserializeIntArray32(BinaryReader br)
        {
            int count = br.ReadInt32();
            int[] list = new int[count];
            for (int i = 0; i < count; i++)
                list[i] = br.ReadInt32();
            return list;
        }

        public static List<int> DeserializeIntList32(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(br.ReadInt32());
            return list;
        }

        public static string[] DeserializeStringArray32(BinaryReader br)
        {
            int count = br.ReadInt32();
            string[] list = new string[count];
            for (int i = 0; i < count; i++)
                list[i] = br.ReadString();
            return list;
        }

        public static List<string> DeserializeStringList32(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<string> list = new List<string>(count);
            for (int i = 0; i < count; i++)
                list.Add(br.ReadString());
            return list;
        }

        public static T[] DeserializeISerializableArray32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            T[] list = new T[count];
            for (int i = 0; i < count; i++)
                list[i] = deserializationAction(br);
            return list;
        }

        public static List<T> DeserializeISerializableList32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            List<T> list = new List<T>(count);
            for (int i = 0; i < count; i++)
                list.Add(deserializationAction(br));
            return list;
        }

        public static HashSet<int> DeserializeIntHashSet32(BinaryReader br)
        {
            int count = br.ReadInt32();
            HashSet<int> list = new HashSet<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(br.ReadInt32());
            return list;
        }

        public static HashSet<T> DeserializeISerializableHashSet32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            HashSet<T> set = new HashSet<T>(count);
            for (int i = 0; i < count; i++)
                set.Add(deserializationAction(br));
            return set;
        }

        public static HashSet<string> DeserializeStringHashSet32(BinaryReader br)
        {
            int count = br.ReadInt32();
            HashSet<string> set = new HashSet<string>(count);
            for (int i = 0; i < count; i++)
                set.Add(br.ReadString());
            return set;
        }

        public static Dictionary<string, T> DeserializeStringISerializableDictionary32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            Dictionary<string, T> dictionary = new Dictionary<string, T>(count);
            for (int i = 0; i < count; i++)
                dictionary.Add(br.ReadString(), deserializationAction(br));
            return dictionary;
        }
    }
}
