using System;
using System.IO;
using System.Linq;
using TeoriaInformacjiProjekt;

var inputFilePath = "K:\\STUDIA MGR\\2 SEMESTR\\Teoria informacji\\TeoriaInformacjiProjekt\\test.txt";
if (!File.Exists(inputFilePath))
{
    throw new ArgumentException("Plik nie istnieje.");
}

var outputFileDirectory = "K:\\STUDIA MGR\\2 SEMESTR\\Teoria informacji\\TeoriaInformacjiProjekt\\results";

var parentDirectory = Path.GetDirectoryName(outputFileDirectory);
if (!Directory.Exists(parentDirectory))
{
    throw new ArgumentException("Folder wynikowy nie istnieje");
}


var inputFileText = await IOOperations.ReadFileText(inputFilePath);

var charCounts = CharCounter.CountChars(inputFileText);

var charCountsOutputFilePath = $"{outputFileDirectory}\\char_counts.txt";

IOOperations.SaveDictionaryToFile(charCountsOutputFilePath, charCounts);


var probabilityOfChars = CharCounter.CountProbabilityOfChars(charCounts);

var probabilityOfCharsOutputFilePath = $"{outputFileDirectory}\\char_probabilities.txt";

IOOperations.SaveDictionaryToFile(probabilityOfCharsOutputFilePath, probabilityOfChars);


var entropia = Entropia.CalculateEntropia(probabilityOfChars);

var entropiaOutputFilePath = $"{outputFileDirectory}\\entropia.txt";

IOOperations.SaveValueToFile(entropiaOutputFilePath, entropia);


var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var shannonFanoResult = ShannonFano.StartAlgo(probabilityOfChars);
watch.Stop();

var shannonFanoTimeOutputFilePath = $"{outputFileDirectory}\\Shannon-Fano_Time.txt";
IOOperations.SaveValueToFile(shannonFanoTimeOutputFilePath, $"{watch.ElapsedMilliseconds} ms");

var shannonFanoOutputFilePath = $"{outputFileDirectory}\\Shannon-Fano.txt";
IOOperations.SaveValueToFile(shannonFanoOutputFilePath, shannonFanoResult);


//var aritmeticCodeProbabilities = Aritmetic.GetAritmeticCodeProbabilities(probabilityOfChars);

//watch.Reset();
//watch.Start();
//var aritmeticEncoded = Aritmetic.Encode(aritmeticCodeProbabilities, inputFileText);
//watch.Stop();

//var aritmeticEncodeTimeOutputFilePath = $"{outputFileDirectory}\\aritmetic_encoded_time.txt";
//IOOperations.SaveValueToFile(aritmeticEncodeTimeOutputFilePath, $"{watch.ElapsedMilliseconds} ms");

//var aritmeticEncodedOutputFilePath = $"{outputFileDirectory}\\aritmetic_encoded.txt";
//IOOperations.SaveValueToFile(aritmeticEncodedOutputFilePath, aritmeticEncoded);


//var aritmeticDecoded = Aritmetic.Decode(aritmeticCodeProbabilities, aritmeticEncoded, inputFileText.Length);

//var aritmeticDecodedOutputFilePath = $"{outputFileDirectory}\\aritmetic_decoded.txt";
//IOOperations.SaveValueToFile(aritmeticDecodedOutputFilePath, aritmeticDecoded);


var L = shannonFanoResult.Sum(n => n.Code.Length * n.Probability); // Średnia długość słowa kodowego
var LOutputFilePath = $"{outputFileDirectory}\\ShannonFano-sr_dlugosc_slowa_kodowego.txt";
IOOperations.SaveValueToFile(LOutputFilePath, L);

var n = entropia / L; // sprawność
var nOutputFilePath = $"{outputFileDirectory}\\ShannonFano-sprawnosc.txt";
IOOperations.SaveValueToFile(nOutputFilePath, n);

var rozwleklosc = 1.0 - n;
var rozwlekloscOutputFilePath = $"{outputFileDirectory}\\ShannonFano-rozwleklosc.txt";
IOOperations.SaveValueToFile(rozwlekloscOutputFilePath, rozwleklosc);

var efektywnoscKodu = entropia / L * 100.0;
var efektywnoscKoduOutputFilePath = $"{outputFileDirectory}\\ShannonFano-efektywnosc_kodu.txt";
IOOperations.SaveValueToFile(efektywnoscKoduOutputFilePath, efektywnoscKodu);
