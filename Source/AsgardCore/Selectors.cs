using System;

namespace AsgardCore
{
    /// <summary>
    /// Common selector delegate functions for LINQ and other lambdas.
    /// </summary>
    public static class Selectors
    {
        /// <summary>
        /// A selector returning itself.
        /// </summary>
        public static T SelfSelector<T>(T s)
        {
            return s;
        }

        /// <summary>
        /// Always returns True.
        /// Can be used as a <see cref="Func{Boolean}"/> method.
        /// </summary>
        public static bool True()
        {
            return true;
        }

        /// <summary>
        /// Always returns False.
        /// Can be used as a <see cref="Func{Boolean}"/> method.
        /// </summary>
        public static bool False()
        {
            return false;
        }

        /// <summary>
        /// Always returns False.
        /// Can be used as a <see cref="Func{Boolean}"/> method.
        /// </summary>
        public static bool False<T>(T _)
        {
            return false;
        }

        /// <summary>
        /// An empty <see cref="Action"/> doing nothing.
        /// </summary>
        public static void NoAction()
        { }

        /// <summary>
        /// An empty <see cref="Action"/> with an unused parameter, doing nothing.
        /// </summary>
        public static void NoAction<T>(T _)
        { }
    }
}
