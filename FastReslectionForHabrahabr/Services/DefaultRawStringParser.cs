using FastReslectionForHabrahabr.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FastReslectionForHabrahabr.Services
{
    public class DefaultRawStringParser : IRawStringParser
    {
        private static readonly string _unrecognizedKey = "Unrecognized";

        public Dictionary<string, string> ParseWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
            => rawData?.Split(pairDelimiter)
            .Select(x => x.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries))
            .Select(x => x.Length == 2 ? new { Key = x[0].Trim(), Value = x[1].Trim() } : new { Key = _unrecognizedKey, Value = x[0].Trim() })
            .ToDictionary(x => x.Key, x => x.Value)
            ?? new Dictionary<string, string>();

        public Dictionary<string, string> ParseWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";")
        {
            var result = new Dictionary<string, string>();
            var splitted = rawData?.Split(pairDelimiter);
            foreach (var item in splitted)
            {
                var pair = item.Split(keyValueDelimiter, StringSplitOptions.RemoveEmptyEntries);
                if (pair.Length == 2)
                    result.Add(pair[0].Trim(), pair[1].Trim());
                else
                    result.Add(_unrecognizedKey, pair[0].Trim());
            }
            return result;
        }
    }
}
