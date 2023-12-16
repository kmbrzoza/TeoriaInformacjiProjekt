using System;
using System.Collections.Generic;

namespace TeoriaInformacjiProjekt;

public static class Entropia
{
    public static double CalculateEntropia(Dictionary<char, double> probabilityOfChars)
    {
        var result = 0d;

        foreach (var probabilityOfChar in probabilityOfChars)
        {
            result -= probabilityOfChar.Value * Math.Log2(probabilityOfChar.Value);
        }

        return result;
    }
}
