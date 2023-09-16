using System;
using System.IO;
using System.Linq;
using System.Text;
using AsgardCore.Exceptions;

namespace AsgardCore
{
    /// <summary>
    /// Collection of common file-operations, like reading, writing and compression.
    /// </summary>
    public class BasicFileHandler : IBasicFileHandler
    {
        /// <summary>
        /// Executes the load operation with <see cref="Encoding.UTF8"/> from the file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="loadAction">The contents to read.</param>
        public void LoadFromBinaryStream(string path, Action<BinaryReader> loadAction)
        {
            LoadFromBinaryStream(path, loadAction, null);
        }

        /// <summary>
        /// Executes the load operation with <see cref="Encoding.UTF8"/> from the file at the specified path.
        /// Verifies that the given Magic Number matches the bytes at the beginning of the file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="loadAction">The contents to read.</param>
        /// <param name="magicNumber">The Magic Number of the file.</param>
        /// <exception cref="MagicNumberException">If the Magic Numbers are different.</exception>
        public void LoadFromBinaryStream(string path, Action<BinaryReader> loadAction, byte[] magicNumber)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open))
            {
                using (BinaryReader br = new BinaryReader(fs, Encoding.UTF8))
                {
                    if (!magicNumber.IsNullOrEmpty())
                    {
                        byte[] actual = br.ReadBytes(magicNumber.Length);
                        if (!actual.SequenceEqual(magicNumber))
                            throw new MagicNumberException(magicNumber, actual);
                    }
                    loadAction(br);
                }
            }
        }

        /// <summary>
        /// Executes the save operation with <see cref="Encoding.UTF8"/> to a new file at the specified path.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="saveAction">The contents to save.</param>
        public void SaveToBinaryStream(string path, Action<BinaryWriter> saveAction)
        {
            SaveToBinaryStream(path, saveAction, null);
        }

        /// <summary>
        /// Executes the save operation with <see cref="Encoding.UTF8"/> to a new file at the specified path.
        /// If Magic Number is provided, it will be at the beginning of the created file.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <param name="saveAction">The contents to save.</param>
        /// <param name="magicNumber">The Magic Number of the file.</param>
        public void SaveToBinaryStream(string path, Action<BinaryWriter> saveAction, byte[] magicNumber)
        {
            using (FileStream fs = new FileStream(path, FileMode.Create))
            {
                using (BinaryWriter bw = new BinaryWriter(fs, Encoding.UTF8))
                {
                    if (!magicNumber.IsNullOrEmpty())
                        bw.Write(magicNumber);
                    saveAction(bw);
                }
            }
        }
    }
}
