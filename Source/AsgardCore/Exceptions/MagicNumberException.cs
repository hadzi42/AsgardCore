using System;
using System.Collections.Generic;
using System.IO;

namespace AsgardCore.Exceptions
{
    public sealed class MagicNumberException : IOException
    {
        public IReadOnlyList<byte> ExpectedMagicNumber { get; }
        public IReadOnlyList<byte> ActualMagicNumber { get; }

        public MagicNumberException()
        { }

        public MagicNumberException(string message)
            : base(message)
        { }

        public MagicNumberException(string message, Exception innerException)
            : base(message, innerException)
        { }

        public MagicNumberException(byte[] expectedMagicNumber, byte[] actualMagicNumber)
            : this(expectedMagicNumber, actualMagicNumber, null)
        { }

        public MagicNumberException(byte[] expectedMagicNumber, byte[] actualMagicNumber, string message)
            : base(message)
        {
            ExpectedMagicNumber = expectedMagicNumber;
            ActualMagicNumber = actualMagicNumber;
        }
    }
}
