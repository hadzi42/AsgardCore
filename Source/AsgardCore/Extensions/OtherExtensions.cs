using System;
using System.Globalization;

namespace AsgardCore
{
    public static partial class Extensions
    {
        /// <summary>
        /// Determines whether the given array is null or empty.
        /// </summary>
        public static bool IsNullOrEmpty<T>(this T[] array)
        {
            return array == null || array.Length == 0;
        }

        /// <summary>
        /// Converts the value of this instance to its equivalent culture-independent string representation (either "True" or "False").
        /// </summary>
        public static string ToStringInvariant(this bool b)
        {
            return b.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this short s)
        {
            return s.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this ushort u)
        {
            return u.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this int i)
        {
            return i.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this long i)
        {
            return i.ToString(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance rounded to 2 digits, to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this double d)
        {
            d = Math.Round(d, 2, MidpointRounding.AwayFromZero);
            return d.ToString("F2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the numeric value of this instance rounded to 2 digits, to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this float f)
        {
            f = MathF.Round(f, 2, MidpointRounding.AwayFromZero);
            return f.ToString("F2", CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Converts the value of the current System.DateTime object to its equivalent culture-independent string representation.
        /// </summary>
        public static string ToStringInvariant(this DateTime d)
        {
            return d.ToString(CultureInfo.InvariantCulture);
        }
    }
}
