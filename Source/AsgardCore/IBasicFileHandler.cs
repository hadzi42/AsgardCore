using System;
using System.IO;
using System.Text;
using AsgardCore.Exceptions;

namespace AsgardCore
{
    /// <summary>
    /// Collection of common file-operations, like reading, writing and compression.
    /// </summary>
    public interface IBasicFileHandler
    {
        /// <summary>
        /// Executes the load operation with <see cref="Encoding.UTF8"/> from the file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="loadAction">The contents to read.</param>
        void LoadFromBinaryStream(string path, Action<BinaryReader> loadAction);

        /// <summary>
        /// Executes the load operation with <see cref="Encoding.UTF8"/> from the file at the specified path.
        /// Verifies that the given Magic Number matches the bytes at the beginning of the file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="loadAction">The contents to read.</param>
        /// <param name="magicNumber">The Magic Number of the file.</param>
        /// <exception cref="MagicNumberException">If the Magic Numbers are different.</exception>
        void LoadFromBinaryStream(string path, Action<BinaryReader> loadAction, byte[] magicNumber);

        /// <summary>
        /// Executes the save operation with <see cref="Encoding.UTF8"/> to a new file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="saveAction">The contents to save.</param>
        void SaveToBinaryStream(string path, Action<BinaryWriter> saveAction);

        /// <summary>
        /// Executes the save operation with <see cref="Encoding.UTF8"/> to a new file at the specified path.
        /// If Magic Number is provided, it will be at the beginning of the created file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="saveAction">The contents to save.</param>
        /// <param name="magicNumber">The Magic Number of the file.</param>
        void SaveToBinaryStream(string path, Action<BinaryWriter> saveAction, byte[] magicNumber);
    }
}
