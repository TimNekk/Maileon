using System;
using System.Collections.Generic;
using System.Linq;

namespace Maileon.Extensions
{
    /// <summary>
    /// Adds random pick functionality to enumerable
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Picks random item from list
        /// </summary>
        /// <param name="source">List of items</param>
        /// <typeparam name="T">Item type</typeparam>
        /// <returns>Random item</returns>
        public static T PickRandom<T>(this IEnumerable<T> source)
        {
            return source.PickRandom(1).Single();
        }

        /// <summary>
        /// Picks random items from list
        /// </summary>
        /// <param name="source">List of items</param>
        /// <param name="count">Amount of items to pick</param>
        /// <typeparam name="T">Item type</typeparam>
        /// <returns>Random items</returns>
        public static IEnumerable<T> PickRandom<T>(this IEnumerable<T> source, int count)
        {
            return source.Shuffle().Take(count);
        }

        /// <summary>
        /// Shuffles the list
        /// </summary>
        /// <param name="source">List of items</param>
        /// <typeparam name="T">Item type</typeparam>
        /// <returns>Shuffled list</returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => Guid.NewGuid());
        }
    }
}