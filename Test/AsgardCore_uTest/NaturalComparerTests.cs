using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using AsgardCore;
using NUnit.Framework;

namespace AsgardCore_uTest
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class NaturalComparerTests
    {
        [Test]
        public void NaturalComparer_AllCases_TypeIsSealed()
        {
            Assert.IsInstanceOf<IComparer<string>>(NaturalComparer.Instance);
            Assert.IsTrue(typeof(IdManager).IsSealed);
        }

        [Test]
        public void StrCmpLogicalW_BasicCases_HappyPath()
        {
            Assert.AreEqual(0, NaturalComparer.StrCmpLogicalW("", ""));
            Assert.AreEqual(0, NaturalComparer.Instance.Compare("", ""));
            Assert.AreEqual(0, string.CompareOrdinal("", ""));

            Assert.AreEqual(-1, NaturalComparer.StrCmpLogicalW("", "a"));
            Assert.AreEqual(-1, NaturalComparer.Instance.Compare("", "a"));
            Assert.LessOrEqual(string.CompareOrdinal("", "a"), -1);

            Assert.AreEqual(1, NaturalComparer.StrCmpLogicalW("a", ""));
            Assert.AreEqual(1, NaturalComparer.Instance.Compare("a", ""));
            Assert.GreaterOrEqual(string.CompareOrdinal("a", ""), 1);

            Assert.AreEqual(-1, NaturalComparer.StrCmpLogicalW("a", "b"));
            Assert.AreEqual(-1, NaturalComparer.Instance.Compare("a", "b"));
            Assert.AreEqual(-1, string.CompareOrdinal("a", "b"));

            Assert.AreEqual(0, NaturalComparer.StrCmpLogicalW("a", "a"));
            Assert.AreEqual(0, NaturalComparer.Instance.Compare("a", "a"));
            Assert.AreEqual(0, string.CompareOrdinal("a", "a"));

            Assert.AreEqual(1, NaturalComparer.StrCmpLogicalW("b", "a"));
            Assert.AreEqual(1, NaturalComparer.Instance.Compare("b", "a"));
            Assert.AreEqual(1, string.CompareOrdinal("b", "a"));
        }

        [Test]
        public void StrCmpLogicalW_NumbersInTexts_HappyPath()
        {
            Assert.AreEqual(-1, NaturalComparer.StrCmpLogicalW("a 2", "a 10"));
            Assert.AreEqual(-1, NaturalComparer.Instance.Compare("a 2", "a 10"));
            Assert.AreEqual(1, string.CompareOrdinal("a 2", "a 10"));

            List<string> list = new List<string>(6);
            NaturalSortCore(list, new[] { "a1", "a10", "a11", "a2" }, new[] { "a1", "a2", "a10", "a11" });
            NaturalSortCore(list, new[] { "0", "1", "10", "100", "101", "11" }, new[] { "0", "1", "10", "11", "100", "101" });
            NaturalSortCore(list, new[] { "zb", "aaa", "aba", "zc" }, new[] { "aaa", "aba", "zb", "zc" });
            NaturalSortCore(list, "nat_116_a,nat_101_b,nat_1_z,nat_22_c,nat_2_y,nat_5_m".Split(','), new[] { "nat_1_z", "nat_2_y", "nat_5_m", "nat_22_c", "nat_101_b", "nat_116_a" });
        }

        [Test]
        public void StrCmpLogicalW_ErrorCases_ReturnsWithErrorCode()
        {
            Assert.AreEqual(-2, NaturalComparer.StrCmpLogicalW(null, null));
            Assert.AreEqual(-2, NaturalComparer.StrCmpLogicalW(null, "a"));
            Assert.AreEqual(-2, NaturalComparer.StrCmpLogicalW("a", null));
        }

        private static void NaturalSortCore(List<string> list, string[] items, string[] expectedOrder)
        {
            list.Clear();
            list.AddRange(items);
            list.Sort(NaturalComparer.StrCmpLogicalW);
            CollectionAssert.AreEqual(expectedOrder, list);

            list.Clear();
            list.AddRange(items);
            list.Sort(NaturalComparer.Instance);
            CollectionAssert.AreEqual(expectedOrder, list);
        }
    }
}
