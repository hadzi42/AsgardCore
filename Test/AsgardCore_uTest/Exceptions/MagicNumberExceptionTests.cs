using System;
using System.Diagnostics.CodeAnalysis;
using AsgardCore.Exceptions;
using NUnit.Framework;

namespace AsgardCore_uTest.Exceptions
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    internal sealed class MagicNumberExceptionTests
    {
        [Test]
        public void Constructor_NoParameters_PropertiesAreNull()
        {
            MagicNumberException ex = new MagicNumberException();

            Assert.IsNull(ex.ExpectedMagicNumber);
            Assert.IsNull(ex.ActualMagicNumber);
        }

        [Test]
        public void Constructor_WithMessage_PropertiesAreNull()
        {
            const string message = "test";
            MagicNumberException ex = new MagicNumberException(message);

            Assert.AreEqual(message, ex.Message);
            Assert.IsNull(ex.ExpectedMagicNumber);
            Assert.IsNull(ex.ActualMagicNumber);
        }

        [Test]
        public void Constructor_WithMessageAndInnerException_PropertiesAreNull()
        {
            const string message = "test";
            ArgumentException innerException = new ArgumentException();
            MagicNumberException ex = new MagicNumberException(message, innerException);

            Assert.AreEqual(message, ex.Message);
            Assert.AreEqual(innerException, ex.InnerException);
            Assert.IsNull(ex.ExpectedMagicNumber);
            Assert.IsNull(ex.ActualMagicNumber);
        }

        [Test]
        public void Constructor_WithMagicNumbers_PropertiesAreFilled()
        {
            byte[] magic1 = new byte[] { 1, 2 };
            byte[] magic2 = new byte[] { 3, 4 };
            MagicNumberException ex = new MagicNumberException(magic1, magic2);

            // The same object instances are returned.
            Assert.AreEqual(magic1, ex.ExpectedMagicNumber);
            Assert.AreEqual(magic2, ex.ActualMagicNumber);
            // The contents are not changed.
            CollectionAssert.AreEqual(new byte[] { 1, 2 }, ex.ExpectedMagicNumber);
            CollectionAssert.AreEqual(new byte[] { 3, 4 }, ex.ActualMagicNumber);
        }

        [Test]
        public void Constructor_WithMessageAndMagicNumbers_PropertiesAreFilled()
        {
            byte[] magic1 = new byte[] { 1, 2 };
            byte[] magic2 = new byte[] { 3, 4 };
            const string message = "test";
            MagicNumberException ex = new MagicNumberException(magic1, magic2, message);

            Assert.AreEqual(message, ex.Message);
            // The same object instances are returned.
            Assert.AreEqual(magic1, ex.ExpectedMagicNumber);
            Assert.AreEqual(magic2, ex.ActualMagicNumber);
            // The contents are not changed.
            CollectionAssert.AreEqual(new byte[] { 1, 2 }, ex.ExpectedMagicNumber);
            CollectionAssert.AreEqual(new byte[] { 3, 4 }, ex.ActualMagicNumber);
        }
    }
}
