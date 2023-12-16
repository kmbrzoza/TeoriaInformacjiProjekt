using System;
using System.Collections.Generic;
using System.Linq;

namespace TeoriaInformacjiProjekt;

public static class CharCounter
{
    public static Dictionary<char, long> CountChars(string fileText)
    {
        var result = new Dictionary<char, long>();

        foreach (var character in fileText)
        {
            if (result.ContainsKey(character))
            {
                result[character]++;
            }
            else
            {
                result.Add(character, 1);
            }
        }

        return result;
    }

    public static Dictionary<char, double> CountProbabilityOfChars(Dictionary<char, long> charCounts)
    {
        var sumCount = charCounts.Sum(c => c.Value);

        var result = new Dictionary<char, double>();

        foreach (var charCount in charCounts)
        {
            result.Add(charCount.Key, Convert.ToDouble(charCount.Value) / Convert.ToDouble(sumCount));
        }

        return result;
    }
}
