using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonCharsInLines
{
    public class Program
    {
        static void Main(string[] args)
        {
        }

        /// <summary>
        /// Get the first char and compare it with all the chars in other lines.
        /// Naive but not that easy to put into place.
        /// Using char arithmetic only.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_Naive(List<string> lines)
        {
            List<char> result = new List<char>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int k = 0; k < lines[i].Length; k++)
                {
                    var kFoundInOtherLines = true;
                    for (int j = 0; j < lines.Count; j++)
                    {
                        if (j == i)
                            continue;
                        var foundInLineJ = false;
                        for (int m = 0; m < lines[j].Length; m++)
                        {
                            if (char.ToLower(lines[i][k]) == char.ToLower(lines[j][m]))
                            {
                                foundInLineJ = true;
                                break;
                            }
                        }
                        if (!foundInLineJ)
                        {
                            kFoundInOtherLines = false;
                            break;
                        }
                    }
                    if (kFoundInOtherLines)
                    {
                        result.Add(char.ToLower(lines[i][k]));
                    }
                }
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Naive again but instead of char arithmetic, string methods are used.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_Naive_Improved(List<string> lines)
        {
            List<char> result = new List<char>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int k = 0; k < lines[i].Length; k++)
                {
                    var kFoundInOtherLines = true;
                    for (int j = 0; j < lines.Count; j++)
                    {
                        if (j == i)
                            continue;
                        if (!lines[j].Contains(char.ToLower(lines[i][k])))
                        {
                            kFoundInOtherLines = false;
                            break;
                        }
                    }
                    if (kFoundInOtherLines)
                    {
                        result.Add(char.ToLower(lines[i][k]));
                    }
                }
            }

            return result.Distinct().ToList();
        }

        /// <summary>
        /// Solve by using a map of line number sets for each char.
        /// If the set value for a certain char is equal to the total line count, collect these chars.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_MapOfSets(List<string> lines)
        {
            List<char> result = new List<char>();

            Dictionary<char, HashSet<int>> data = new Dictionary<char, HashSet<int>>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char newChar = char.ToLower(lines[i][j]);
                    if (!data.ContainsKey(newChar))
                        data[newChar] = new HashSet<int>();
                    data[newChar].Add(i);
                }
            }

            foreach (var d in data)
            {
                if (d.Value.Count == lines.Count)
                {
                    result.Add(d.Key);
                }
            }

            return result;
        }

        /// <summary>
        /// Same as above but just collect the results using LINQ
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_MapOfSets_Condensed(List<string> lines)
        {
            Dictionary<char, HashSet<int>> data = new Dictionary<char, HashSet<int>>();

            for (int i = 0; i < lines.Count; i++)
            {
                for (int j = 0; j < lines[i].Length; j++)
                {
                    char newChar = char.ToLower(lines[i][j]);
                    if (!data.ContainsKey(newChar))
                        data[newChar] = new HashSet<int>();
                    data[newChar].Add(i);
                }
            }

            return data.Where(t => t.Value.Count == lines.Count).Select(p => p.Key).ToList();
        }

        /// <summary>
        ///  Create a list of chars from the first line and then filter out chars that do not exist in each other line.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_AddFirstLineThenFilterNonUsed(List<string> lines)
        {
            List<char> result = new List<char>();

            if (lines.Count > 0)
                result.AddRange(lines[0].ToCharArray().Select(x => char.ToLower(x)).Distinct());

            for (int i = 1; i < lines.Count; i++)
            {
                result.RemoveAll(item => !lines[i].ToLower().Contains(char.ToLower(item)));
            }

            return result;
        }


        /// <summary>
        /// No special treatment, like adding the first line's letters to the result first.
        /// But works only the initial listed letters.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_AddAllLettersThenFilterNonUsed(List<string> lines)
        {
            List<char> result = "abcdefghijklmnopqrstuvwxyz ".ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                result.RemoveAll(item => !lines[i].ToLower().Contains(item));
            }

            if (lines.Count == 0)
                return new List<char>();
            return result;
        }

        /// <summary>
        /// The same as above.
        /// But now works for all visible ASCII chars
        /// Two options to get all ASCII chars
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_AddAllASCIICharsThenFilterNonUsed(List<string> lines)
        {
            //List<char> result = Enumerable.Range(32, 33).Union(Enumerable.Range(91, 36)).Select(x => (char)x).ToList();
            List<char> result = Enumerable.Range(32, 95).Select(x => char.ToLower((char)x)).Distinct().ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                result.RemoveAll(item => !lines[i].ToLower().Contains(item));
            }

            if (lines.Count == 0)
                return new List<char>();
            return result;
        }

        /// <summary>
        /// Using more linq. Now result is first set again to the first line.
        /// Then, get the intersect of each line.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_AddFirstLineThenIntersectWithOtherLines(List<string> lines)
        {
            List<char> result = Enumerable.Range(32, 95).Select(x => char.ToLower((char)x)).Distinct().ToList();

            foreach (var line in lines)
            {
                result = result.Intersect(line.ToLower().ToCharArray().Distinct()).ToList();
            }

            if (lines.Count == 0)
                return new List<char>();
            return result;
        }

        /// <summary>
        /// Same as 4 but using a dictionary of trues and falses instead of removeAll
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_AddAllASCIICharsThenFilterButUsingDictionary(List<string> lines)
        {
            var result = Enumerable.Range(32, 95).Select(x => char.ToLower((char)x)).Distinct().ToDictionary(x => x, x => true);

            for (int i = 0; i < lines.Count; i++)
            {
                result = result.Select(item => lines[i].ToLower().Contains(item.Key) ? item : new KeyValuePair<char, bool>(item.Key, false)).ToDictionary(x => x.Key, x => x.Value);
            }

            if (lines.Count == 0)
                return new List<char>();
            return result.Where(x => x.Value == true).Select(x => x.Key).ToList();
        }


        /// <summary>
        /// Purely functional using linq. only special case of empty lines.
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static List<char> FindCommonCharsInLines_Functional(List<string> lines)
        {
            if (lines.Count == 0)
                return new List<char>();
            return lines.Select(line => line.ToCharArray().Select(x => x = char.ToLower(x)).Distinct()).Aggregate((cumulated, next) => cumulated = cumulated.Intersect(next).ToArray()).ToList();
        }

    }
}
