using System;
using System.Collections.Generic;
using System.IO;

namespace Extensions
{
	public abstract class Data
	{
		private static char[] alphaPet = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
		public static Dictionary<char, Letter> dict = new Dictionary<char, Letter>();
		public static string[] Paint = new string[LinesCount];

		public static int LineLength { get; set; }
		public static int LinesCount { get; set; } = 13;
		public static int Space { get; } = 2;

		public static string Input { get; private set; }
		public static int Mode { get; private set; }

		public static void ReadDataFromFile()
		{
			string lettersFolderPath = Directory.GetCurrentDirectory() + @"\Letters";
			int i = 0;

			foreach (string file in Directory.GetFiles(lettersFolderPath))
			{
				StreamReader reader = new StreamReader(file);
				string line = reader.ReadLine();

				Letter letter = new Letter();
				letter.LineLength = line.Length;
				letter.Value = new List<string>();

				while (line != null)
				{
					letter.Value.Add(line);
					line = reader.ReadLine();
				}

				dict.Add(alphaPet[i], letter);
				i++;
			}
		}

		public static void GetInput()
		{
			Console.Write("Enter a sentence: ");
			Input = Console.ReadLine().ToUpper();
			Console.Clear();

			for (int i = 0; i < Input.Length; i++)
			{
				if (char.IsLetter(Input[i]) == false && Input[i] != ' ')
				{
					Console.WriteLine(Input[i] + " is invalid");
					GetInput();
				}
			}

			if (GetPixelsLength(Input) > Console.WindowWidth)
			{
				Console.WriteLine("Sentence is too long");
				GetInput();
			}
			else 
			{
				start:
				Console.Write("Enter a number between to choose a style (0 - 12): ");
				string tmp = Console.ReadLine();
				
				if (int.TryParse(tmp, out int num) == false || num < 0 || num > 12)
				{
					Console.Clear();
					Console.WriteLine("Invalid value");
					goto start;
				}

				Mode = num;
			}

			Console.Clear();
		}

		public static void GetPixels(string input)
		{
			Paint = new string[LinesCount];
			LineLength = 0;

			foreach (char ch in input)
			{
				LineLength += dict[ch].LineLength;

				for (int i = 0; i < dict[ch].Value.Count; i++)
				{
					Paint[i] += dict[ch].Value[i];
				}
			}
		}

		public static int GetPixelsLength(string str)
		{
			int result = 0;

			for (int i = 0; i < str.Length; i++)
			{
				if (str[i] == ' ') result += Space;
				else result += dict[str[i]].Value[0].Length;
			}

			return result;
		}
	}
}
