namespace CheckGitChanged;

/// <summary>
/// These are the text formatting options available for rendering text on the console.
/// </summary>
public enum ConsoleDecoration
{
    None = 0,
    Bold,
    Italics,
    Underline,
    Strikethrough
}

/// <summary>
/// This is used to map the <see cref="ConsoleDecoration"/> value to a Unicode value to
/// send to the console window.
/// </summary>
public static class DecorationCodes
{
    public const string RESET = "\x1B[0m";
    public const string BOLD = "\x1B[1m";
    public const string ITALICS = "\x1B[3m";
    public const string UNDERLINE = "\x1B[4m";
    public const string STRIKETROUGH = "\x1B[9m";
}


/// <summary>
/// This class wraps functionality for writing text formatting to the console
/// window.
/// </summary>
public static class ColorConsole
{
    /// <summary>
    /// This maps the <see cref="DecorationCodes"/> to a specific Unicode to write to the console.
    /// </summary>
    /// <param name="decoration">The decoration effect to get a code for.</param>
    /// <returns>The Unicode for the specified decoration.</returns>
    /// <exception cref="IndexOutOfRangeException">Thrown if there is no decoration for the specified code.</exception>
    private static string MapDecoration(ConsoleDecoration decoration)
    {
        switch (decoration)
        {
            case ConsoleDecoration.None: return string.Empty;
            case ConsoleDecoration.Bold: return DecorationCodes.BOLD;
            case ConsoleDecoration.Italics: return DecorationCodes.ITALICS;
            case ConsoleDecoration.Underline: return DecorationCodes.UNDERLINE;
            case ConsoleDecoration.Strikethrough: return DecorationCodes.STRIKETROUGH;
            default: throw new IndexOutOfRangeException($"Unable to find a console code for {decoration}.");
        }
    }

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="backColor">The background color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void WriteLine(string message = "",
                                 ConsoleColor? foreColor = null,
                                 ConsoleColor? backColor = null,
                                 ConsoleDecoration decoration = ConsoleDecoration.None)
        => Write(Console.WriteLine, message, foreColor, backColor, decoration);

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void WriteLine(string message = "",
                                 ConsoleColor? foreColor = null,
                                 ConsoleDecoration decoration = ConsoleDecoration.None)
        => Write(Console.WriteLine, message, foreColor, backColor: null, decoration);

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void WriteLine(string message,
                                 ConsoleDecoration decoration)
        => Write(Console.WriteLine, message, foreColor: null, backColor: null, decoration);

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    public static void WriteLine(string message = "",
                                 ConsoleColor? foreColor = null)
        => Write(Console.WriteLine, message, foreColor, backColor: null, decoration: ConsoleDecoration.None);

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void WriteLine(string message)
        => Write(Console.WriteLine, message, foreColor: null, backColor: null, decoration: ConsoleDecoration.None);

    /// <summary>
    /// This writes text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="backColor">The background color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void Write(string message = "",
                             ConsoleColor? foreColor = null,
                             ConsoleColor? backColor = null,
                             ConsoleDecoration decoration = ConsoleDecoration.None)
        => Write(Console.Write, message, foreColor, backColor, decoration);

    /// <summary>
    /// This writes text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void Write(string message = "",
                             ConsoleColor? foreColor = null,
                             ConsoleDecoration decoration = ConsoleDecoration.None)
        => Write(Console.Write, message, foreColor, backColor: null, decoration);

    /// <summary>
    /// This writes text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    public static void Write(string message = "",
                             ConsoleColor? foreColor = null)
        => Write(Console.Write, message, foreColor, backColor: null, decoration: ConsoleDecoration.None);

    /// <summary>
    /// This writes text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    public static void Write(string message = "")
        => Write(Console.Write, message, foreColor: null, backColor: null, decoration: ConsoleDecoration.None);

    /// <summary>
    /// This writes text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="message">The message to write.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void Write(string message,
                             ConsoleDecoration decoration)
        => Write(Console.Write, message, foreColor: null, backColor: null, decoration);

    /// <summary>
    /// This writes a line of text to the console using the specified <paramref name="message"/>
    /// and text formatting.
    /// </summary>
    /// <param name="write">The write method to send the message and formatting codes to.</param>
    /// <param name="message">The message to write.</param>
    /// <param name="foreColor">The foreground color to use.</param>
    /// <param name="backColor">The background color to use.</param>
    /// <param name="decoration">The text formatting to use.</param>
    public static void Write(Action<string> write,
                             string message = "",
                             ConsoleColor? foreColor = null,
                             ConsoleColor? backColor = null,
                             ConsoleDecoration decoration = ConsoleDecoration.None)
    {
        var oldFore = Console.ForegroundColor;
        var oldBack = Console.BackgroundColor;

        if (foreColor != null) Console.ForegroundColor = foreColor.Value;
        if (backColor != null) Console.BackgroundColor = backColor.Value;

        var prefix = MapDecoration(decoration);
        var suffix = decoration == ConsoleDecoration.None ? string.Empty : DecorationCodes.RESET;

        write($"{prefix}{message}{suffix}");

        Console.ForegroundColor = oldFore;
        Console.BackgroundColor = oldBack;
    }
}
