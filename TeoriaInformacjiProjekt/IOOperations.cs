using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeoriaInformacjiProjekt;

public static class IOOperations
{
    public static async Task<string> ReadFileText(string inputFilePath)
    {
        using (var fileStream = File.OpenRead(inputFilePath))
        {
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
            {
                var fileText = await streamReader.ReadToEndAsync();

                fileText = fileText.Replace("\r", string.Empty);

                return fileText;
            }
        }
    }

    public static void SaveDictionaryToFile<T, Y>(string outputFilePath, Dictionary<T, Y> dict)
    {
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            foreach (var character in dict.OrderBy(k => k.Key))
            {
                outputFile.WriteLine($"'{character.Key}': {character.Value}");
            }
        }
    }

    public static void SaveDictionaryToFile<T>(string outputFilePath, Dictionary<T, double> dict)
    {
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            foreach (var character in dict.OrderBy(k => k.Key))
            {
                outputFile.WriteLine($"'{character.Key}': {(decimal)character.Value}");
            }
        }
    }

    public static void SaveValueToFile<T>(string outputFilePath, T value)
    {
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            outputFile.WriteLine($"{value}");
        }
    }

    public static void SaveValueToFile(string outputFilePath, List<SFNode> value)
    {
        using (StreamWriter outputFile = new StreamWriter(outputFilePath))
        {
            foreach (var node in value)
            {
                outputFile.WriteLine($"'{node.Symbol}': {node.Code}");
            }
        }
    }
}
