using System;
using System.Threading;

namespace AsgardCore
{
    /// <summary>
    /// Implements a read-lock.
    /// Allows reading from multiple threads, but prevents writing to the locked object.
    /// </summary>
    public struct ReadLock : IDisposable
    {
        private ReaderWriterLockSlim _Locker;

        /// <summary>
        /// Creates a new <see cref="ReadLock"/> instance and enters into read-lock.
        /// </summary>
        /// <param name="locker">The lock controller.</param>
        public ReadLock(ReaderWriterLockSlim locker)
        {
            _Locker = locker;
            locker.EnterReadLock();
        }

        /// <summary>
        /// Releases the read-lock.
        /// </summary>
        public void Dispose()
        {
            if (_Locker != null)
            {
                _Locker.ExitReadLock();
                _Locker = null;
            }
        }
    }
}
