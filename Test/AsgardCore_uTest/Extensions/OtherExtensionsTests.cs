using System;
using System.Diagnostics.CodeAnalysis;
using AsgardCore;
using NUnit.Framework;

namespace AsgardCore_uTest.Extensions
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class OtherExtensionsTests
    {
        [Test]
        public void IsNullOrEmpty_NullOrEmpty_ReturnsTrue()
        {
            byte[] array = null;
            Assert.IsTrue(array.IsNullOrEmpty());

            array = Array.Empty<byte>();
            Assert.IsTrue(array.IsNullOrEmpty());
        }

        [Test]
        public void IsNullOrEmpty_NotEmpty_ReturnsFalse()
        {
            byte[] array = new byte[] { 1 };
            Assert.IsFalse(array.IsNullOrEmpty());
        }
    }
}
