using System;
using System.Collections.Generic;
using System.Linq;

namespace TeoriaInformacjiProjekt;

public static class ShannonFano
{
    public static List<SFNode> StartAlgo(Dictionary<char, double> charProbabilities)
    {
        var result = new List<SFNode>();

        foreach (var charProb in charProbabilities)
        {
            result.Add(new SFNode(charProb.Key, charProb.Value));
        }

        result = result.OrderByDescending(r => r.Probability).ToList();

        return Divide(result);
    }

    public static List<SFNode> Divide(List<SFNode> nodes)
    {
        if (nodes.Count <= 1)
        {
            return nodes;
        }

        var totalProbability = nodes.Sum(n => n.Probability);
        var halfProbab = totalProbability / 2.0;

        var tempProb = 0.0;
        var index = 0;

        for (int i = 0; i < nodes.Count; i++)
        {
            tempProb += nodes[i].Probability;
            if (tempProb >= halfProbab)
            {
                if (Math.Abs(halfProbab - tempProb) < Math.Abs(halfProbab - (tempProb - nodes[i].Probability)))
                {
                    index = i;
                }
                break;
            }
            index = i;
        }

        var lowerGroup = nodes.GetRange(0, index + 1);
        var upperGroup = nodes.GetRange(index + 1, (nodes.Count - (index + 1)));

        foreach (var node in lowerGroup)
        {
            node.Code += "0";
        }
        foreach (var node in upperGroup)
        {
            node.Code += "1";
        }

        var result = new List<SFNode>();

        result.AddRange(Divide(lowerGroup));
        result.AddRange(Divide(upperGroup));

        return result;
    }
}

public class SFNode
{
    public char Symbol { get; set; }
    public double Probability { get; set; }
    public string Code { get; set; }

    public SFNode(char symbol, double probability)
    {
        Symbol = symbol;
        Probability = probability;
        Code = string.Empty;
    }
}