using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Text.RegularExpressions;
using EnsureThat;

namespace System
{
    public static class StringExtensions
    {
        public static readonly string Elipses = "...";
        public static readonly char WhiteSpace = ' ';

        public static void WriteToFile(this string s, string path)
        {
            WriteToFile(s, path, Encoding.UTF8);
        }

        public static void WriteToFile(this string s, string path, Encoding encoding)
        {
            using var outfile = new StreamWriter(path, false, encoding);
            outfile.Write(s);
        }

        public static string ResetToDefaultIfWhiteSpace(this string s, string defaultValue = null)
        {
            return string.IsNullOrWhiteSpace(s) ? defaultValue : s;
        }
        public static string Truncate(this string s, int length)
        {
            Ensure.That(length, nameof(length)).IsGte(0);
            if (string.IsNullOrEmpty(s)) return string.Empty;
            return s.Length <= length ? s : s.Substring(0, length);
        }

        public static string TruncateWithElipses(this string s, int length)
        {
            Ensure.That(length, nameof(length)).IsGte(0);
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.Length <= length) return s;
            if (length <= Elipses.Length) return Truncate(s, length);

            return s.Substring(0, Math.Min(s.Length, length - Elipses.Length)) + Elipses;
        }

        public static string TruncateWithElipsesOnWordBoundary(this string s, int length)
        {
            Ensure.That(length, nameof(length)).IsGt(0);
            
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.Length <= length) return s;
            if (s.Length <= Elipses.Length) return Truncate(s, length);

            int lastSpace = s.Substring(0, length).LastIndexOf(WhiteSpace);
            return s.Substring(0, lastSpace) + Elipses; //Bug if lastSpace == -1, i.e. if first word lenght > length
        }

        public static string Remove(this string s, string toBeRemoved)
        {
            return s?.Replace(toBeRemoved, "");
        }

        public static string RemoveMany(this string s, params string[] toBeRemoved)
        {
            if (s == null)
                return null;

            toBeRemoved.ToList().ForEach(r => s = s.Replace(r, ""));
            return s;
        }

        public static string DefaultTo(this string s, string defaultValue)
        {
            return s ?? defaultValue;
        }

        public static string DefaultToEmpty(this string s)
        {
            return s.DefaultTo(string.Empty);
        }

        public static string DefaultIfEmpty(this string s, string defaultValue = "")
        {
            return string.IsNullOrEmpty(s) ? defaultValue : s;
        }

        public static string DefaultIfWhiteSpace(this string s, string defaultValue = "")
        {
            return string.IsNullOrWhiteSpace(s) ? defaultValue : s;
        }

        public static string NormalizeSpaces(this string s)
        {
            return Regex.Replace(s.DefaultToEmpty(), @"\s+", " ");
        }

        public static string TrimAndNormalizeSpaces(this string s)
        {
            return NormalizeSpaces(s.DefaultToEmpty().Trim());
        }

        /// <summary>
        /// Poor man's pluralization. Some better implementations sure exist.
        /// </summary>
        public static string Pluralize(this string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            if (s.EndsWith("y")) return s.TrimEnd('y') + "ies";
            if (s.EndsWith("sh") || s.EndsWith("ch")) return s + "es";
            return s + "s";
        }

        public static bool EndsWithVowel(this string s)
        {
            return !string.IsNullOrEmpty(s) && "aeiouy".ToCharArray().Contains(s[^1]);
        }

        public static string PrePend(this string s, string value)
        {
            return s.Insert(0, value);
        }

        public static string Append(this string s, string value)
        {
            return s.Insert(s.Length, value);
        }

        public static bool Intersects(this string s, string charSet)
        {
            return charSet.Any(s.Contains);
        }

        public static Stream ToStream(this string s)
        {
            using var stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public static string ToBase64(this string s)
        {
            if (string.IsNullOrEmpty(s)) return "";
            using var stream = new MemoryStream();
            new BinaryFormatter().Serialize(stream, s);
            string b64 = Convert.ToBase64String(stream.ToArray());
            return b64;
        }

        public static T ToEnum<T>(this string value, IDictionary<string, T> dictionary, T defaultValue) where T : struct
        {
            return dictionary.ContainsKey(value) ? dictionary[value] : defaultValue;
        }

        public static T ToEnum<T>(this int value, IDictionary<int, T> dictionary, T defaultValue) where T : struct
        {
            return dictionary.ContainsKey(value) ? dictionary[value] : defaultValue;
        }

        /// <summary>
        /// http://stackoverflow.com/questions/7343465/compression-decompression-string-with-c-sharp
        /// </summary>
        public static byte[] Zip(this string str)
        {
            var bytes = Encoding.UTF8.GetBytes(str);

            using var msi = new MemoryStream(bytes);
            using var mso = new MemoryStream();
            using (var gs = new GZipStream(mso, CompressionMode.Compress))
            {
                msi.CopyTo(gs);
            }

            return mso.ToArray();
        }

        public static byte[] ToByteArray(this string base64Str)
        {
            return Convert.FromBase64String(base64Str);
        }

        public static IEnumerable<string> Split(this string s, int chunkSize)
        {
            Ensure.That(chunkSize, nameof(chunkSize)).IsGte(1);
            
            if (string.IsNullOrEmpty(s) || s.Length <= chunkSize)
                return new List<string>() { s };

            var output = new List<string>();
            int len = s.Length;
            for (int i = 0; i < len; i += chunkSize)
            {
                if (i + chunkSize > len)
                    output.Add(s.Substring(i, len - i));
                else
                    output.Add(s.Substring(i, chunkSize));
            }
            return output;
        }

        public static IEnumerable<string> SplitOnWordBoundary(this string s, int chunkSize)
        {
            Ensure.That(chunkSize, nameof(chunkSize)).IsGte(1);
            
            if (string.IsNullOrEmpty(s) || s.Length <= chunkSize)
                return new List<string>() { s };

            var output = new List<string>();
            string current = s;
            while (current.Length > 0)
            {
                if (current.Length <= chunkSize)
                {
                    output.Add(current);
                    break;
                }

                int i = current.Substring(0, chunkSize).LastIndexOf(WhiteSpace);
                output.Add(current.Substring(0, i).Trim());
                current = current.Substring(i);
            }

            return output;
        }

        /// <summary>
        /// Transforms a non empty string using an optional mapping dictionnary
        /// </summary>
        public static string Map(this string s, IDictionary<string, string> mappings)
        {
            Ensure.That(s, nameof(s)).IsNotNullOrEmpty();
            if (mappings == null) return s;
            return mappings.ContainsKey(s) ? mappings[s] : s;
        }

        public static string Last(this string s, int length)
        {
            if (s == null) return null;
            if (s == string.Empty) return string.Empty;
            if (s.Length < length) return s;
            return s.Substring(s.Length - length, length);
        }

        public static bool In(this string s, params string[] list)
        {
            return list.Contains(s);
        }

        public static bool ContainsAny(this string s, params char[] list)
        {
            return list.Any(s.Contains);
        }

        public static TimeSpan ToTimeSpan(this string s)
        {
            if (string.IsNullOrEmpty(s)) return new TimeSpan();

            var components = s.Split(new char[] { ':' });
            int componentCount = components.Length;

            int day = componentCount - 4 >= 0 ? Convert.ToInt32(components[componentCount - 4]) : 0;
            int hour = componentCount - 3 >= 0 ? Convert.ToInt32(components[componentCount - 3]) : 0;
            int minutes = componentCount - 2 >= 0 ? Convert.ToInt32(components[componentCount - 2]) : 0;
            int seconds = componentCount - 1 >= 0 ? Convert.ToInt32(components[componentCount - 1]) : 0;

            return new TimeSpan(day, hour, minutes, seconds);
        }

        /// <summary>
        /// http://www.levibotelho.com/development/c-remove-diacritics-accents-from-a-string/
        /// </summary>
        public static string RemoveDiacritics(this string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return text;

            text = text.Normalize(NormalizationForm.FormD);
            var chars = text.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark).ToArray();
            return new string(chars).Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Index of the nth occurence of a pattern 
        /// </summary>
        public static int IndexOfNth(this string str, string pattern, int nth = 1)
        {
            Ensure.That(nth, nameof(nth)).IsGte(1);
            
            int offset = str.IndexOf(pattern, System.StringComparison.Ordinal);
            for (int i = 1; i < nth; i++)
            {
                if (offset == -1) return -1;
                offset = str.IndexOf(pattern, offset + 1, StringComparison.Ordinal);
            }
            return offset;
        }

        /// <summary>
        /// Substring upto the nth occurence of a pattern
        /// </summary>
        public static string SubstringUptoNth(this string str, string pattern, int from, int nth = 1)
        {
            Ensure.That(from, nameof(from), opts => opts.WithMessage("Can only find the nth pattern from 0 onwards")).IsGte(0);
            Ensure.That(nth, nameof(nth), opts => opts.WithMessage("Can only find the 1st, 2nd, 3rd... pattern in a string")).IsGte(1);
            var substring = str.Substring(from);
            return substring.Substring(0, substring.IndexOfNth(pattern, nth));
        }

        /// <summary>
        /// Substring after the nth occurence of a pattern
        /// </summary>
        public static string SubstringAfterNth(this string str, string pattern, int from, int nth = 1)
        {
            Ensure.That(from, nameof(from), opts => opts.WithMessage("Can only find the nth pattern from 0 onwards")).IsGte(0);
            Ensure.That(nth, nameof(nth), opts => opts.WithMessage("Can only find the 1st, 2nd, 3rd... pattern in a string")).IsGte(1);
            string substring = str.Substring(from);
            return substring.Substring(substring.IndexOfNth(pattern, nth) + 1);
        }

        public static string SafeJoin(char separator, params string[] strings)
        {
            return strings.Aggregate((x, y) => string.IsNullOrEmpty(y) ? x : string.Concat(x, separator, y));
        }

        public static string SafeJoin(params string[] strings)
        {
            return SafeJoin(' ', strings);
        }

        /// <remarks>
        /// Better with http://stackoverflow.com/questions/1479364/c-sharp-params-keyword-with-two-parameters-of-the-same-type. No better idea
        /// at this time. Having this method named SafeJoin "steals" calls to the previous SafeJoin(params string[] strings).
        /// Also, if an item is null or empty, it is not joined.
        /// Extra guard added if all strings are null or empty, returns ""
        /// </remarks>
        public static string SafeJoinWithSeparator(string separator, params string[] strings)
        {
            return strings.All(string.IsNullOrEmpty) 
                       ? string.Empty 
                       : strings.Where(s => !string.IsNullOrEmpty(s)).Aggregate((x, y) => string.Concat(x, separator, y));
        }

        public static string Repeat(this string s, int numRepetitions)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < numRepetitions; i++)
                sb.Append(s);
            return sb.ToString();
        }

        public static string OrIfNullOrWhiteSpace(this string s, string alternative)
        {
            return string.IsNullOrWhiteSpace(s) ? alternative : s;
        }
    }

    public static class Str
    {
        /// <summary>
        /// Replacement for String.Join that ignores empty or null strings
        /// </summary>
        public static string Join(string separator, params object[] objects)
        {
            return string.Join(separator, objects.Where(o => !string.IsNullOrWhiteSpace(o?.ToString()))
                                                 .Select(o => o.ToString()));
        }
    }
}
