using System;
using System.Collections.Generic;
using System.IO;

namespace AsgardCore
{
    /// <summary>
    /// Thread-safe ID generating class.
    /// </summary>
    public sealed class IdManager : IIdManager
    {
        private readonly object _Locker = new object();
        private readonly List<int> _FreeIDs;
        private int _NextID;
        private readonly int _FirstID;
        private int _LastID;

        /// <summary>
        /// The first ID to be returned.
        /// </summary>
        public int FirstID
        {
            get { return _FirstID; }
        }

        /// <summary>
        /// Preview of the next ID to be returned.
        /// Use <see cref="GenerateID"/> to actually acquire it.
        /// </summary>
        public int NextID
        {
            get { return _FreeIDs.Count > 0 ? _FreeIDs[0] : _NextID; }
        }

        /// <summary>
        /// Creates a new <see cref="IdManager"/> instance with 0 as the first ID to be returned.
        /// </summary>
        public IdManager()
        {
            _FreeIDs = new List<int>();
        }

        /// <summary>
        /// Creates a new <see cref="IdManager"/> instance with the given, non-negative ID as the first ID to be returned.
        /// </summary>
        /// <param name="firstID">The first ID to return.</param>
        public IdManager(int firstID)
            : this()
        {
            if (firstID < 0)
                throw new ArgumentException("The ID cannot be negative: " + firstID);

            _NextID = firstID;
            _FirstID = firstID;
            _LastID = firstID;
        }

        /// <summary>
        /// Restores a serialized <see cref="IdManager"/> instance.
        /// </summary>
        public IdManager(BinaryReader br)
        {
            _FirstID = br.ReadInt32();
            _LastID = br.ReadInt32();
            _NextID = br.ReadInt32();
            _FreeIDs = Extensions.DeserializeIntList32(br);
        }

        /// <summary>
        /// Resets IDManager.
        /// </summary>
        public void Clear()
        {
            lock (_Locker)
            {
                _NextID = _FirstID;
                _LastID = _FirstID;
                _FreeIDs.Clear();
            }
        }

        /// <summary>
        /// Creating new ID (returning NextID in most cases) in a thread-safe manner.
        /// </summary>
        public int GenerateID()
        {
            int id;
            lock (_Locker)
            {
                if (_FreeIDs.Count > 0)
                {
                    id = _FreeIDs[0];
                    _FreeIDs.RemoveAt(0);
                }
                else
                {
                    id = _NextID;
                    _LastID = id;
                    _NextID++;
                }
            }
            return id;
        }

        /// <summary>
        /// Makes the given ID to be re-assignable.
        /// </summary>
        public void RecycleID(int id)
        {
            if (id < _FirstID || id > _LastID || id == _NextID)
                throw new ArgumentOutOfRangeException(id.ToStringInvariant());

            lock (_Locker)
            {
                if (!_FreeIDs.Contains(id))
                    _FreeIDs.Add(id);
                _FreeIDs.Sort();
            }
        }

        /// <summary>
        /// Makes the given ID to be re-assignable.
        /// </summary>
        public void RecycleID(params int[] ids)
        {
            if (ids == null)
                throw new ArgumentNullException(nameof(ids));

            lock (_Locker)
            {
                foreach (int id in ids)
                {
                    if (id < _FirstID || id > _LastID || id == _NextID)
                        throw new ArgumentOutOfRangeException(id.ToStringInvariant());

                    if (!_FreeIDs.Contains(id))
                        _FreeIDs.Add(id);
                }
                _FreeIDs.Sort();
            }
        }

        /// <inheritdoc />
        public void Serialize(BinaryWriter bw)
        {
            lock (_Locker)
            {
                bw.Write(_FirstID);
                bw.Write(_LastID);
                bw.Write(_NextID);
                _FreeIDs.Serialize32(bw);
            }
        }
    }
}
