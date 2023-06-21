using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace AsgardCore
{
    /// <summary>
    /// ONLY WORKS ON WINDOWS! Uses native shlwapi.dll.
    /// Implements sorting used in Windows Explorer.
    /// This considers numbers in file names, so items with increasing numbers will follow each other.
    /// </summary>
    public sealed class NaturalComparer : IComparer<string>
    {
        /// <summary>
        /// A global instance of <see cref="NaturalComparer"/>.
        /// </summary>
        public static readonly NaturalComparer Instance = new NaturalComparer();

        /// <summary>
        /// Do not let others create new instances. Everybody should use <see cref="Instance"/>.
        /// </summary>
        private NaturalComparer()
        { }

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>A signed integer that indicates the relative values of x and y.</returns>
        [DllImport("shlwapi.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        public static extern int StrCmpLogicalW(string x, string y);

        /// <summary>
        /// Compares two objects and returns a value indicating whether one is less than, equal to, or greater than the other.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>A signed integer that indicates the relative values of x and y.</returns>
        public int Compare(string x, string y)
        {
            if (x == y)
                return 0;
            if (x == null)
                return -1;
            if (y == null)
                return 1;
            return StrCmpLogicalW(x, y);
        }
    }
}
