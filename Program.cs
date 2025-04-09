using System;
using System.Collections.Generic;
using Extensions;

public enum Style
{
    Horizontal, 
    Vertical,
    HorizontalMirror,
    VerticalMirror,
    TrueFalse,
    HorizontalTrueFalse,
    VerticalTrueFalse,
    Random, 
    Corner, 
    Biangular, 
    Quadrangle, 
    FillOutIn, 
    FillInOut,
}

public struct Vector2
{
    public int X { get; set; }
    public int Y { get; set; }

    public Vector2(int x, int y)
    {
        X = x;
        Y = y;
    }

    public Vector2(Vector2 vector)
    {
        X = vector.X;
        Y = vector.Y;
    }
}

public struct Pixel
{
    public Vector2 Position { get; set; }
    public char Value { get; set; }

    public Pixel(Vector2 pos, char val)
    {
        Position = pos;
        Value = val;
    }
}

public struct Letter
{
    public int LineLength { get; set; }
    public List<string> Value;
}

class Program
{
	static void Main(string[] args)
    {
		Data.ReadDataFromFile();

        Data.GetInput();

        string[] arr = Data.Input.Split(' ');

        Vector2 vector = new Vector2();
        vector.Y = Console.WindowHeight * 2 / 7;
        vector.X = (Console.WindowWidth - Data.GetPixelsLength(Data.Input)) / 2;

        foreach (string word in arr) {
            Data.GetPixels(word);
            
            Printer.Print(Data.Paint, Data.LineLength, 5, vector, (Style)Data.Mode);
            vector.X += Data.GetPixelsLength(word) + Data.Space;
        }

        Console.Write("Finished, You can press enter to quit the program");
        Console.ReadLine();
    }
}