namespace AsgardCore
{
    /// <summary>
    /// Interface for managing IDS.
    /// </summary>
    public interface IIdManager : ISerializable
    {
        /// <summary>
        /// The first ID to be returned.
        /// </summary>
        int FirstID { get; }

        /// <summary>
        /// Preview of the next ID to be returned.
        /// Use <see cref="GenerateID"/> to actually acquire it.
        /// </summary>
        int NextID { get; }

        /// <summary>
        /// Resets IDManager.
        /// </summary>
        void Clear();

        /// <summary>
        /// Creating new ID (returning NextID in most cases) in a thread-safe manner.
        /// </summary>
        int GenerateID();

        /// <summary>
        /// Makes the given ID to be re-assignable.
        /// </summary>
        void RecycleID(int id);

        /// <summary>
        /// Makes the given IDs to be re-assignable.
        /// </summary>
        void RecycleID(params int[] ids);
    }
}
