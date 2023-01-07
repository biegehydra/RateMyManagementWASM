using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace RateMyManagementWASM.Shared
{
    public static class ExtensionMethods
    {
        public static List<string> GetEnumNamesCorrected<TEnum>(bool withNull = false) where TEnum : Enum
        {
            var correctedNames = new List<string>();
            foreach (var enumName in Enum.GetNames(typeof(TEnum)))
            {
                var wordsInType =
                    Regex.Matches(enumName.ToString(), @"([A-Z][a-z]+)")
                        .Select(m => m.Value);

                var withSpaces = string.Join(" ", wordsInType);
                if (!withNull && withSpaces == "Null") continue;
                correctedNames.Add(withSpaces);
            }
            return correctedNames;
        }
        public static string ToStringCorrected(this Enum enumType)
        {
            var regex = new Regex(@"([A-Z][a-z]+)");
            var words = regex.Split(enumType.ToString());
            return string.Join(" ", words);
        }
        public static bool MatchesEnumItem<TEnum>(this string str, out TEnum value) where TEnum : struct, Enum
        {
            value = default;
            var words = str.Split(' ');
            var wordString = string.Join("", words);

            if (Enum.TryParse(wordString, true, out TEnum result))
            {
                var lowercaseResult = result.ToString().ToLowerInvariant();
                value = result;
                return true;
            }

            return false;
        }
        public static Uri Combine(this Uri baseUri, Uri relativeUri)
        {
            if (baseUri == null)
            {
                throw new ArgumentNullException(nameof(baseUri));
            }
            if (relativeUri == null)
            {
                throw new ArgumentNullException(nameof(relativeUri));
            }
            var baseUriString = baseUri.ToString();
            if (!baseUriString.EndsWith("/"))
            {
                baseUriString += "/";
            }
            var relativeUriString = relativeUri.ToString();
            if (relativeUriString.StartsWith("/"))
            {
                relativeUriString = relativeUriString.Substring(1);
            }
            var combinedUriString = baseUriString + relativeUriString;
            return new Uri(combinedUriString);
        }
    }
}
