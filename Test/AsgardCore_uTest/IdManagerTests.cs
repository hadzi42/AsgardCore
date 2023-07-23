using System;
using System.Diagnostics.CodeAnalysis;
using AsgardCore;
using AsgardCore.Test;
using NUnit.Framework;

namespace AsgardCore_uTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class IdManagerTests
    {
        [Test]
        public void IdManager_Type_TypeIsSealed()
        {
            IdManager i = new IdManager();

            Assert.IsInstanceOf<ISerializable>(i);
            Assert.IsInstanceOf<IIdManager>(i);
            Assert.IsTrue(typeof(IdManager).IsSealed);
        }

        [Test]
        public void Constructor_Default_IdStartsWith0()
        {
            IdManager i = new IdManager();

            Assert.IsNotNull(i._Locker);
            CheckDefaultValues(i, 0);
        }

        [Test]
        public void Constructor_WithFirstId_IdStartsWithGivenValue()
        {
            const int FirstId = 3;
            IdManager i = new IdManager(FirstId);

            Assert.IsNotNull(i._Locker);
            CheckDefaultValues(i, FirstId);
        }

        [Test]
        public void Constructor_WithNegativeId_ThrowsArgumentException()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new IdManager(-1));
            StringAssert.Contains("-1", ex.Message);
        }

        [Test]
        public void NextId_Sequential_ReturnsNextValue()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            Assert.AreEqual(4, i.NextID);

            int newId = i.GenerateId();
            Assert.AreEqual(4, newId);
        }

        [Test]
        public void NextId_RecycledValue_ReturnsSmallestRecycledValue()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            i.GenerateId();
            i.GenerateId();

            i.RecycleID(5, 3);
            Assert.AreEqual(3, i.NextID);
            int id = i.GenerateId();
            Assert.AreEqual(3, id);

            Assert.AreEqual(5, i.NextID);
            id = i.GenerateId();
            Assert.AreEqual(5, id);
        }

        [Test]
        public void Serialization_AllCases_StateIsRestored()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            i.GenerateId();
            int recycledId = i.GenerateId();
            i.RecycleID(recycledId);

            TestCommon.SerializationCore(ref i, br => new IdManager(br));

            Assert.AreEqual(6, i._NextId);
            Assert.AreEqual(3, i._FirstId);
            Assert.AreEqual(5, i._LastId);
            Assert.IsNotNull(i._Locker);
            CollectionAssert.AreEqual(new[] { 5 }, i._FreeIDs);
        }

        [Test]
        public void GenerateId_Sequential_ReturnsNextValue()
        {
            IdManager i = new IdManager(3);
            Assert.AreEqual(3, i.GenerateId());
            Assert.AreEqual(4, i.GenerateId());
            Assert.AreEqual(5, i.GenerateId());
        }

        [Test]
        public void GenerateId_Recycled_ReturnsSmallestRecycledValue()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            i.GenerateId();
            i.GenerateId();

            i.RecycleID(4, 3);

            Assert.AreEqual(3, i.GenerateId());
            Assert.AreEqual(4, i.GenerateId());
        }

        [Test]
        public void GenerateId_Mixed_ReturnsRecycledThanNextValue()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            i.GenerateId();
            i.GenerateId();

            i.RecycleID(4);

            Assert.AreEqual(4, i.GenerateId());
            Assert.AreEqual(6, i.GenerateId());
        }

        [Test]
        public void RecycleID_ErrorCases_ThrowsArgumentException()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();

            Assert.Throws<ArgumentNullException>(() => i.RecycleID(null));
            // Invalid value (less than first).
            Assert.Throws<ArgumentException>(() => i.RecycleID(2));
            Assert.Throws<ArgumentException>(() => i.RecycleID(2, 2));
            // Not yet generated ID.
            Assert.Throws<ArgumentException>(() => i.RecycleID(4));
            Assert.Throws<ArgumentException>(() => i.RecycleID(4, 4));

            // Recycle the same valid value twice.
            i.RecycleID(3);
            Assert.DoesNotThrow(() => i.RecycleID(3));
            Assert.DoesNotThrow(() => i.RecycleID(3, 3));
        }

        [Test]
        public void Clear_Default_ResetsIdManager()
        {
            IdManager i = new IdManager();
            i.GenerateId();
            i.GenerateId();
            i.RecycleID(1);
            i.Clear();

            CheckDefaultValues(i, 0);
        }

        [Test]
        public void Clear_WithFirstId_ResetsIdManager()
        {
            IdManager i = new IdManager(3);
            i.GenerateId();
            i.GenerateId();
            i.RecycleID(3);
            i.Clear();

            CheckDefaultValues(i, 3);
        }

        private static void CheckDefaultValues(IdManager manager, int firstId)
        {
            Assert.AreEqual(firstId, manager._NextId);
            Assert.AreEqual(firstId, manager._FirstId);
            Assert.AreEqual(firstId, manager._LastId);
            Assert.IsNotNull(manager._FreeIDs);
            Assert.IsEmpty(manager._FreeIDs);
        }
    }
}
