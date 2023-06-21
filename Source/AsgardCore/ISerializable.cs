using System.IO;

namespace AsgardCore
{
    /// <summary>
    /// Defines methods for AsgardCore's binary serialization.
    /// The type implementing this interface should have a constructor with a <see cref="BinaryReader"/> parameter to support deserialization.
    /// </summary>
    /// <remarks>
    /// AsgardCore's binary serialization does not use BinaryFormatter, only plain <see cref="BinaryReader"/> and <see cref="BinaryWriter"/> methods.
    /// This way the binary format is prone to breaking in case of any schema change, but the saving/loading is really fast.
    /// </remarks>
    public interface ISerializable
    {
        /// <summary>
        /// Saves this instance to the given binary stream.
        /// </summary>
        void Serialize(BinaryWriter bw);
    }
}
