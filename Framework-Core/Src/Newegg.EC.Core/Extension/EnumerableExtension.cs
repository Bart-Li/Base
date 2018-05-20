using System;
using System.Collections.Generic;
using System.Linq;

namespace Newegg.EC.Core
{
    /// <summary>
    /// Enumerable extension.
    /// </summary>
    public static class EnumerableExtension
    {
        /// <summary>
        /// Check whether a collection is null or empty.
        /// </summary>
        /// <typeparam name="TSource">IEnumerable T Class.</typeparam>
        /// <param name="source">IEnumerable Object.</param>
        /// <returns>Return True or False.</returns>
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source == null || !source.Any();
        }

        /// <summary>
        /// Do action for each item.
        /// </summary>
        /// <typeparam name="TSource">T Class.</typeparam>
        /// <param name="source">Current IEnumerable.</param>
        /// <param name="action">Action for each item.</param>
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException("Enumerable foreach function");
            }

            if (!source.IsNullOrEmpty())
            {
                foreach (TSource item in source)
                {
                    action(item);
                }
            }
        }
    }
}
