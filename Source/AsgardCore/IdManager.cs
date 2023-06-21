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
        internal readonly object _Locker = new object();
        internal readonly List<int> _FreeIDs;
        internal readonly int _FirstId;
        internal int _NextId;
        internal int _LastId;

        /// <summary>
        /// The first ID to be returned.
        /// </summary>
        public int FirstID
        {
            get { return _FirstId; }
        }

        /// <summary>
        /// Preview of the next ID to be returned.
        /// Use <see cref="GenerateId"/> to actually acquire it.
        /// </summary>
        public int NextID
        {
            get { return _FreeIDs.Count > 0 ? _FreeIDs[0] : _NextId; }
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

            _NextId = firstID;
            _FirstId = firstID;
            _LastId = firstID;
        }

        /// <summary>
        /// Restores a serialized <see cref="IdManager"/> instance.
        /// </summary>
        public IdManager(BinaryReader br)
        {
            _FirstId = br.ReadInt32();
            _LastId = br.ReadInt32();
            _NextId = br.ReadInt32();
            _FreeIDs = Extensions.DeserializeIntList32(br);
        }

        /// <summary>
        /// Resets IDManager.
        /// </summary>
        public void Clear()
        {
            lock (_Locker)
            {
                _NextId = _FirstId;
                _LastId = _FirstId;
                _FreeIDs.Clear();
            }
        }

        /// <summary>
        /// Creates new ID (returning NextID in most cases) in a thread-safe manner.
        /// </summary>
        public int GenerateId()
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
                    id = _NextId;
                    _LastId = id;
                    _NextId++;
                }
            }
            return id;
        }

        /// <summary>
        /// Makes the given ID to be re-assignable.
        /// </summary>
        public void RecycleID(int id)
        {
            if (id < _FirstId || id > _LastId || id == _NextId)
                throw new ArgumentException(id.ToStringInvariant(), nameof(id));

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
                    if (id < _FirstId || id > _LastId || id == _NextId)
                        throw new ArgumentException(id.ToStringInvariant());

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
                bw.Write(_FirstId);
                bw.Write(_LastId);
                bw.Write(_NextId);
                _FreeIDs.Serialize32(bw);
            }
        }
    }
}
