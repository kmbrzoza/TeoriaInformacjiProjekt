using System.Collections.Generic;

namespace TeoriaInformacjiProjekt;

public static class Aritmetic
{
    public static Dictionary<char, double[]> GetAritmeticCodeProbabilities(Dictionary<char, double> charProbabilities)
    {
        var aritmeticCodeProbabilities = new Dictionary<char, double[]>();

        var tempSum = 0.0;

        foreach (var probOfChar in charProbabilities)
        {
            aritmeticCodeProbabilities.Add(probOfChar.Key, [tempSum, tempSum + probOfChar.Value]);
            tempSum += probOfChar.Value;
        }

        return aritmeticCodeProbabilities;
    }

    public static double Encode(Dictionary<char, double[]> probabilities, string message)
    {
        var low = 0.0;
        var high = 1.0;

        foreach (var symbol in message)
        {
            var symbolRange = high - low;
            high = low + symbolRange * probabilities[symbol][1];
            low = low + symbolRange * probabilities[symbol][0];
        }

        return low;
    }

    public static string Decode(Dictionary<char, double[]> probabilities, double code, int length)
    {
        var low = 0.0;
        var high = 1.0;
        var result = string.Empty;

        for (var _i = 0; _i < length; _i++)
        {
            foreach (var symbol in probabilities)
            {
                var currentRange = high - low;
                var value = (code - low) / currentRange;

                if (symbol.Value[0] <= value && value < symbol.Value[1])
                {
                    result += symbol.Key;
                    high = low + currentRange * symbol.Value[1];
                    low = low + currentRange * symbol.Value[0];
                    break;
                }
            }
        }

        return result;
    }
}
