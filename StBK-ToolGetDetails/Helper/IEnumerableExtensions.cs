using HtmlAgilityPack;
using StBK_ToolGetDetails.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace StBK_ToolGetDetails.Helper
{
    public static class IEnumerableExtensions
    {
        private static readonly string[] tmpArr = StBKConst.ArrayAdelsTitel;

        /// <summary>
        /// Concatenates one or more instances of string from a collection and removes a &lt;br&gt; tag.
        /// </summary>
        /// <param name="list"><see cref="List{HtmlNode}"/></param>
        /// <returns>Concatenated <see cref="string"/></returns>
        public static string ConcatAddressFromColletion(this IEnumerable<HtmlNode> listOfNodes, string delimiter)
        {
            return listOfNodes
                .Select(n => n.InnerHtml
                    .Replace("<br>", " "))
                .Take(^2..)
                .StringJoin(delimiter)
                .Trim();
        }

        /// <summary>
        /// Concatenates the members of a collection of type <see cref="HtmlNode"/>, with the specified separator between each element. And removes a &lt;br&gt; tag.
        /// </summary>
        /// <param name="list"><see cref="List{HtmlNode}"/></param>
        /// <param name="delimiter"></param>
        /// <returns>Concatenated <see cref="string"/></returns>
        public static string SplitStringFromColletion(this IEnumerable<HtmlNode> listOfNodes, string delimiter)
        {
            return listOfNodes
                .Select(n => n.InnerHtml
                    .Replace("<br>", " "))
                .StringJoin(delimiter)
                .Trim();
        }

        /// <summary>
        /// Extract the last name from a string consisting of several first names and one last name.
        /// </summary>
        /// <param name="list"><see cref="List{HtmlNode}"/></param>
        /// <param name="delimiter"></param>
        /// <returns></returns>
        public static string SplitNachnameFromCollection(this IEnumerable<HtmlNode> selectedNodes, string delimiter)
        {
            string lastName = string.Empty;

            foreach (var node in selectedNodes)
            {
                var selectedNode = node.InnerHtml;

                foreach (var titel in tmpArr)
                {
                    if (RegExHelper.CheckIfStringContainsSubstring(selectedNode, titel))
                    {
                        lastName = Regex.Match(selectedNode, string.Format($"\\s+{titel}.*$"), RegexOptions.IgnoreCase).ToString().Trim();
                    }
                }
            }

            return lastName;

            //return listOfNodes
            //    .Select(n => n.InnerHtml)
            //    .Select(n => n.Split(" ")
            //        .Last())
            //    .StringJoin(delimiter)
            //    .Trim();
        }

        /// <summary>
        /// Extract the first name from a string consisting of several first names and a last name.
        /// </summary>
        /// <param name="list"><see cref="List{HtmlNode}"/></param>
        /// <param name="delimiter">A string that delimits the substrings in this string.</param>
        /// <returns></returns>
        public static string SplitVornameFromCollection(this IEnumerable<HtmlNode> listOfNodes, string delimiter)
        {
            return listOfNodes
                .Select(n => n.InnerHtml)
                .Select(n => n.Split(" ")
                .Reverse()
                .Skip(1)
                .Reverse())
                .SelectMany(n => n)
                .StringJoin(delimiter);
        }

        /// <summary>
        /// Determines whether the collection is null or contains no elements.
        /// </summary>
        /// <typeparam name="T">The IEnumerable type.</typeparam>
        /// <param name="enumerable">The enumerable, which may be null or empty.</param>
        /// <returns><c>true</c> if the IEnumerable is null or empty; otherwise, <c>false</c></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return !(enumerable?.Any()).GetValueOrDefault();
        }

        /// <summary>
        /// Concatenates the members of a collection of type <see cref="string"/>, with the specified separator between each element.
        /// </summary>
        /// <param name="list">A collection that contains the strings to concatenate.</param>
        /// <param name="seperator">The string to use as a separator is included in the returned string 
        /// only if values has more than one element.</param>
        /// <returns>A string that consists of the elements of values delimited by the separator string.</returns>
        private static string StringJoin(this IEnumerable<string> listOfNodes, string seperator)
        {
            return string.Join(seperator, listOfNodes);
        }
    }
}