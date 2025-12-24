using System.ComponentModel;
using System.Diagnostics;
using CheckGitChanged;
using static CheckGitChanged.ColorConsole;

namespace CheckGitChanged;

/// <summary>
/// This program will check if there are any pending git changes (full files, not partial),
/// and ensure they're still part of the added set pre-commit after the FixCopyrightHeaders
/// application has been run.
/// </summary>
public static class Program
{
    /// <summary>
    /// Main program entry point.
    /// </summary>
    /// <param name="args">Optional, any arguments passed to the application.</param>
    public static void Main(string[] args)
    {
        var command = "gitf";
        var arguments = "--help";

        int result = Run(command, 
                         out string output, 
                         out string error,
                         arguments);

        WriteLine("");
        WriteLine("Captured output:", ConsoleColor.White, ConsoleDecoration.Underline);
        if (result == 0) // success
        {
            WriteLine(output, ConsoleColor.Cyan);
        }
        else
        {
            WriteLine(error ?? output, ConsoleColor.Red);
        }
    }

    #region Helper Methods
    /// <summary>
    /// This runs the specified command with the arguments supplied and returns the error code
    /// and process output.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="output">The program's output.</param>
    /// <param name="error">The program's error output.</param>
    /// <param name="arguments">The arguments to supply with the command.</param>
    /// <returns>The process's exit code.</returns>
    private static int Run(string command, 
                           out string output, 
                           out string error,
                           params string[] arguments)
    {
        output = string.Empty;
        error = string.Empty;

        var args = string.Join(" ", arguments);
        var commandline = $"{command} {args}".TrimEnd();


        Write("Running command: '", ConsoleColor.Gray);
        Write(commandline, ConsoleColor.DarkYellow, ConsoleDecoration.Italics);
        WriteLine("'...", ConsoleColor.Gray);

        var process = new Process();
        var startInfo = process.StartInfo;
        try
        {
            startInfo.FileName = command;
            startInfo.Arguments = args;
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.UseShellExecute = false;

            process.Start();

            output = process.StandardOutput.ReadToEnd();
            error = process.StandardError.ReadToEnd();
        }
        catch (Win32Exception e)
        {
            error = e.ToString();
            return sbyte.MinValue;
        }

        return process.ExitCode;
    }

    // #capture process output

    // #capture git output specifically

    // capture git porcelain v2 status...

    // filter by files fully added (no partially added files)

    // run FixCopyrightHeaders...

    // capture git porcelain v2 status again...

    // identify any files that WERE fully added but now are not

    // re-add any newly partially-added files to git...
    // output files affected

    // display status

    #endregion
}

