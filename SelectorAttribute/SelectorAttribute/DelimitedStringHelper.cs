using System;
using System.Collections.Generic;
using System.Linq;

namespace SelectorAttribute
{
    public static class DelimitedStringHelper
    {
        public static string DefaultDelimiter = ", ";

        /// <summary>
        /// Convert a sequence of items to a delimited string. By default, ToString() will be called on each item in the sequence to formulate the result. The default delimiter of ', ' will be used
        /// </summary>
        public static string ToDelimitedString<T>(this IEnumerable<T> source)
        {
            return source.ToDelimitedString(x => x.ToString(), DefaultDelimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string. By default, ToString() will be called on each item in the sequence to formulate the result
        /// </summary>
        /// <param name="delimiter">The delimiter to separate each item with</param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, string delimiter)
        {
            return source.ToDelimitedString(x => x.ToString(), delimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string. The default delimiter of ', ' will be used
        /// </summary>
        /// <param name="selector">A lambda expression to select a string property of <typeparamref name="T"/></param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> selector)
        {
            return source.ToDelimitedString(selector, DefaultDelimiter);
        }

        /// <summary>
        /// Convert a sequence of items to a delimited string.
        /// </summary>
        /// <param name="selector">A lambda expression to select a string property of <typeparamref name="T"/></param>
        /// <param name="delimiter">The delimiter to separate each item with</param>
        public static string ToDelimitedString<T>(this IEnumerable<T> source, Func<T, string> selector, string delimiter)
        {
            if (source == null)
                return string.Empty;

            if (selector == null)
                throw new ArgumentNullException("selector", "Must provide a valid property selector");

            if (string.IsNullOrEmpty(delimiter))
                delimiter = DefaultDelimiter;

            return string.Join(delimiter, source.Select(selector).ToArray());
        }
    }
}
