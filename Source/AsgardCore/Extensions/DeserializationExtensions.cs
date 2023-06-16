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

        public static List<int> DeserializeIntList32(BinaryReader br)
        {
            int count = br.ReadInt32();
            List<int> list = new List<int>(count);
            for (int i = 0; i < count; i++)
                list.Add(br.ReadInt32());
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

        public static Dictionary<int, string> DeserializeIntStringDictionary32(BinaryReader br)
        {
            int count = br.ReadInt32();
            Dictionary<int, string> dict = new Dictionary<int, string>(count);
            for (int i = 0; i < count; i++)
                dict[br.ReadInt32()] = br.ReadString();
            return dict;
        }

        public static Dictionary<int, T> DeserializeIntISerializableDictionary32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            Dictionary<int, T> dict = new Dictionary<int, T>(count);
            for (int i = 0; i < count; i++)
                dict[br.ReadInt32()] = deserializationAction(br);
            return dict;
        }

        public static Dictionary<string, T> DeserializeStringISerializableDictionary32<T>(BinaryReader br, Func<BinaryReader, T> deserializationAction)
            where T : ISerializable
        {
            int count = br.ReadInt32();
            Dictionary<string, T> dict = new Dictionary<string, T>(count);
            for (int i = 0; i < count; i++)
                dict[br.ReadString()] = deserializationAction(br);
            return dict;
        }

        public static Dictionary<TKey, TValue> DeserializeISerializableDictionary32<TKey, TValue>(BinaryReader br, Func<BinaryReader, TKey> deserializeKey, Func<BinaryReader, TValue> deserializeValue)
            where TKey : ISerializable
            where TValue : ISerializable
        {
            int count = br.ReadInt32();
            Dictionary<TKey, TValue> dict = new Dictionary<TKey, TValue>(count);
            for (int i = 0; i < count; i++)
                dict[deserializeKey(br)] = deserializeValue(br);
            return dict;
        }
    }
}
