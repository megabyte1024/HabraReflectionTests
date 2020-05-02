using System.Collections.Generic;

namespace FastReslectionForHabrahabr.Interfaces
{
    public interface IRawStringParser
    {
        Dictionary<string, string> ParseWithLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";");
        Dictionary<string, string> ParseWithoutLinq(string rawData, string keyValueDelimiter = ":", string pairDelimiter = ";");        
    }
}