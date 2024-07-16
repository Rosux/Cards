using System.Text.RegularExpressions;
using System.Diagnostics;



/// <summary>
/// This is a debug class.
/// </summary>
public static class Spore
{
    private static readonly string _colorSplitPattern = @"(<Black>|<DarkBlue>|<DarkGreen>|<DarkCyan>|<DarkRed>|<DarkMagenta>|<DarkYellow>|<Gray>|<DarkGray>|<Blue>|<Green>|<Cyan>|<Red>|<Magenta>|<Yellow>|<White>|<Previous>|<Reset>)";

    /// <summary>
    /// Prints a line with color to print.
    /// </summary>
    /// <param name="value">The message to print.</param>
    public static void ColorLine(string value)
    {
        Spore.Color(value+"\n");
    }

    /// <summary>
    /// Prints a line with color to print.
    /// </summary>
    /// <param name="value">The message to print.</param>
    public static void Color(string value)
    {
        List<(string Color, string Characters)> stringTextParts = new List<(string Color, string Characters)>();

        string[] parts = Regex.Split(value, _colorSplitPattern, RegexOptions.Compiled);

        SporeTraceColor baseColor = SporeTraceColor.White;
        SporeTraceColor currentColor = baseColor;
        Stack<SporeTraceColor> colorStack = new Stack<SporeTraceColor>();
        colorStack.Push(currentColor);

        int lineLength = 0;
        int consoleWidth = Console.WindowWidth;

        foreach(string s in parts){
            SporeTraceColor? color = StringToSporeTraceColor(s);
            if(color.HasValue)
            {
                if(color == SporeTraceColor.Reset)
                {
                    currentColor = baseColor;
                    colorStack.Clear();
                    colorStack.Push(currentColor);
                }
                else if(color == SporeTraceColor.Previous)
                {
                    if(colorStack.Count > 1)
                    {
                        colorStack.Pop();
                        currentColor = colorStack.Peek();
                    }
                }
                else
                {
                    currentColor = (SporeTraceColor)color;
                    colorStack.Push(currentColor);
                }
            }
            else
            {
                Console.ForegroundColor = (ConsoleColor)currentColor;
                foreach(char c in s)
                {
                    if(c=='\n')
                    {
                        Console.Write(new string(' ', consoleWidth - lineLength));
                        Console.Write(c);
                        lineLength = 0;
                    }
                    else
                    {
                        Console.Write(c);
                        lineLength++;
                    }
                }
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
    }

    /// <summary>
    /// Convert a string to SporeTraceColor.
    /// </summary>
    /// <param name="color">The color to convert.</param>
    /// <returns>A SporeTraceColor of the color or null.</returns>
    private static SporeTraceColor? StringToSporeTraceColor(string color)
    {
        switch(color)
        {
            case "<Black>":
                return SporeTraceColor.Black;
            case "<DarkBlue>":
                return SporeTraceColor.DarkBlue;
            case "<DarkGreen>":
                return SporeTraceColor.DarkGreen;
            case "<DarkCyan>":
                return SporeTraceColor.DarkCyan;
            case "<DarkRed>":
                return SporeTraceColor.DarkRed;
            case "<DarkMagenta>":
                return SporeTraceColor.DarkMagenta;
            case "<DarkYellow>":
                return SporeTraceColor.DarkYellow;
            case "<Gray>":
                return SporeTraceColor.Gray;
            case "<DarkGray>":
                return SporeTraceColor.DarkGray;
            case "<Blue>":
                return SporeTraceColor.Blue;
            case "<Green>":
                return SporeTraceColor.Green;
            case "<Cyan>":
                return SporeTraceColor.Cyan;
            case "<Red>":
                return SporeTraceColor.Red;
            case "<Magenta>":
                return SporeTraceColor.Magenta;
            case "<Yellow>":
                return SporeTraceColor.Yellow;
            case "<White>":
                return SporeTraceColor.White;
            case "<Previous>":
                return SporeTraceColor.Previous;
            case "<Reset>":
                return SporeTraceColor.Reset;
            default:
                return null;
        }
    }

    public static void Clear()
    {
        Console.Clear();
    }

}
