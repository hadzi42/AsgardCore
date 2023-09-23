using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using AsgardCore;
using NUnit.Framework;
using AcExtensions = AsgardCore.Extensions;

namespace AsgardCore_uTest.Extensions
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class SerializationExtensionTests
    {
        [Test]
        public void Serialize32_ByteArray_HappyPath()
        {
            byte[] data = new byte[] { 1, 2, 3, 4 };
            byte[] copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeByteArray32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_ByteList_HappyPath()
        {
            List<byte> data = new List<byte> { 1, 2, 3, 4 };
            List<byte> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeByteList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_ReadOnlyByteList_HappyPath()
        {
            IReadOnlyList<byte> data = new List<byte> { 1, 2, 3, 4 };
            IReadOnlyList<byte> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeByteList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new byte[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_IntArray_HappyPath()
        {
            int[] data = new int[] { 1, 2, 3, 4 };
            int[] copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeIntArray32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_IntList_HappyPath()
        {
            List<int> data = new List<int> { 1, 2, 3, 4 };
            List<int> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeIntList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_ReadOnlyIntList_HappyPath()
        {
            IReadOnlyList<int> data = new List<int> { 1, 2, 3, 4 };
            IReadOnlyList<int> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeIntList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new int[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_StringArray_HappyPath()
        {
            string[] data = new string[] { "1", "2", "3", "4" };
            string[] copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeStringArray32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4" }, copy);
        }

        [Test]
        public void Serialize32_StringList_HappyPath()
        {
            List<string> data = new List<string> { "1", "2", "3", "4" };
            List<string> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeStringList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4" }, copy);
        }

        [Test]
        public void Serialize32_ReadOnlyStringList_HappyPath()
        {
            IReadOnlyList<string> data = new List<string> { "1", "2", "3", "4" };
            IReadOnlyList<string> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeStringList32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new string[] { "1", "2", "3", "4" }, copy);
        }

        [Test]
        public void Serialize32_ISerializableArray_HappyPath()
        {
            ISerializable[] data = new ISerializable[] { new Serializable(1), new Serializable(2) };
            ISerializable[] copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, br => AcExtensions.DeserializeISerializableArray32(br, Serializable.Restore));

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new ISerializable[] { new Serializable(1), new Serializable(2) }, copy);
        }

        [Test]
        public void Serialize32_ISerializableList_HappyPath()
        {
            List<Serializable> data = new List<Serializable> { new Serializable(1), new Serializable(2) };
            List<Serializable> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, br => AcExtensions.DeserializeISerializableList32(br, Serializable.Restore));

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new Serializable[] { new Serializable(1), new Serializable(2) }, copy);
        }

        [Test]
        public void Serialize32_ReadOnlyISerializableList_HappyPath()
        {
            IReadOnlyList<Serializable> data = new List<Serializable> { new Serializable(1), new Serializable(2) };
            IReadOnlyList<Serializable> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, br => AcExtensions.DeserializeISerializableList32(br, Serializable.Restore));

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEqual(new Serializable[] { new Serializable(1), new Serializable(2) }, copy);
        }

        [Test]
        public void Serialize32_IntHashSet_HappyPath()
        {
            HashSet<int> data = new HashSet<int> { 1, 2, 3, 4 };
            HashSet<int> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeIntHashSet32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEquivalent(new int[] { 1, 2, 3, 4 }, copy);
        }

        [Test]
        public void Serialize32_StringHashSet_HappyPath()
        {
            HashSet<string> data = new HashSet<string> { "1", "2", "3", "4" };
            HashSet<string> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, AcExtensions.DeserializeStringHashSet32);

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEquivalent(new string[] { "1", "2", "3", "4" }, copy);
        }

        [Test]
        public void Serialize32_ISerializableHashSet_HappyPath()
        {
            HashSet<Serializable> data = new HashSet<Serializable> { new Serializable(1), new Serializable(2) };
            HashSet<Serializable> copy = data;
            SerializationCore(ref copy, AcExtensions.Serialize32, br => AcExtensions.DeserializeISerializableHashSet32(br, Serializable.Restore));

            Assert.AreNotSame(data, copy);
            CollectionAssert.AreEquivalent(new Serializable[] { new Serializable(1), new Serializable(2) }, copy);
        }

        [Test]
        public void Serialize32_StringISerializableDictionary_HappyPath()
        {
            Dictionary<string, Serializable> dictionary = new Dictionary<string, Serializable>
            {
                { "a", new Serializable(1) },
                { "b", new Serializable(2) }
            };
            Dictionary<string, Serializable> copy = dictionary;

            SerializationCore(ref dictionary, AcExtensions.Serialize32, br => AcExtensions.DeserializeStringISerializableDictionary32(br, Serializable.Restore));

            Assert.AreNotSame(dictionary, copy);
            CollectionAssert.AreEquivalent(new[] { "a", "b" }, dictionary.Keys);
            CollectionAssert.AreEquivalent(new[] { new Serializable(1), new Serializable(2) }, dictionary.Values);
        }

        [Test]
        public void SerializeToByteArray_AllCases_ReturnsByteArray()
        {
            byte[] data = new Serializable(42).SerializeToByteArray();
            CollectionAssert.AreEqual(new byte[] { 42, 0, 0, 0 }, data);
        }        

        public static void SerializationCore<T>(ref T obj, Action<T, BinaryWriter> serialization, Func<BinaryReader, T> deserialization)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms, Encoding.UTF8))
                {
                    serialization(obj, bw);

                    ms.Seek(0, SeekOrigin.Begin);
                    using (BinaryReader br = new BinaryReader(ms, Encoding.UTF8))
                    {
                        obj = deserialization(br);
                    }
                }
            }
        }

        private sealed class Serializable : ISerializable
        {
            public readonly int Value;

            public Serializable(int v)
            {
                Value = v;
            }

            public Serializable(BinaryReader br)
            {
                Value = br.ReadInt32();
            }

            public static Serializable Restore(BinaryReader br)
            {
                return new Serializable(br);
            }

            public void Serialize(BinaryWriter bw)
            {
                bw.Write(Value);
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(this, obj))
                    return true;

                return
                    obj is Serializable other &&
                    Value == other.Value;
            }

            public override int GetHashCode()
            {
                return Value;
            }
        }
    }
}
