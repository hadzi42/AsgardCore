using System;
using System.Threading;

namespace AsgardCore
{
    /// <summary>
    /// Implements a write-lock.
    /// Allows writing from this thread, and prevents reading of the locked object.
    /// </summary>
    public struct WriteLock : IDisposable
    {
        private ReaderWriterLockSlim _Locker;

        /// <summary>
        /// Creates a new <see cref="WriteLock"/> instance and enters into write-lock.
        /// </summary>
        /// <param name="locker">The lock controller.</param>
        public WriteLock(ReaderWriterLockSlim locker)
        {
            _Locker = locker;
            locker.EnterWriteLock();
        }

        /// <summary>
        /// Releases the write-lock.
        /// </summary>
        public void Dispose()
        {
            if (_Locker != null)
            {
                _Locker.ExitWriteLock();
                _Locker = null;
            }
        }
    }
}
