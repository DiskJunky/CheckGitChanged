using System.ComponentModel;
using System.Diagnostics;
using LibGit2Sharp;
using Spectre.Console;

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
    public static void Main(string[]? args)
    {
        // var command = "git";
        // var arguments = "status --porcelain=v2 -uno";

        var currentFolder = Directory.GetCurrentDirectory();
        AnsiConsole.MarkupLine($"[gray]Current folder:[/]       [yellow]{currentFolder}[/]");
        
        var workingFolder = currentFolder;
        if (args != null && args.Length > 0)
        {
            workingFolder = args[0];
        }
        AnsiConsole.MarkupLine($"[gray]Repo folder supplied:[/] [yellow]{workingFolder}[/]");

        string output = string.Empty;
        string? error = string.Empty;
        int result = 0;
        AnsiConsole.Status()
            .Spinner(Spinner.Known.Dots10)
                   .Start($"[gray]Querying git...[/]",
                          _ =>
                          {
                              // result = Run(command, 
                              //              out output, 
                              //              out error,
                              //              arguments);

                              var list = GetFilesChanged(workingFolder,
                                                         out error);
                              output = string.Join(Environment.NewLine, list);
                              result = list.Count > 0 ? 0 : sbyte.MinValue;

                              Thread.Sleep(3000);
                          });
        

        Console.WriteLine();
        AnsiConsole.MarkupLine("[white underline bold]Git results:[/]");
        if (result == 0) // success
        {
            AnsiConsole.MarkupLine($"[cyan]{output}[/]");
        }
        else
        {
            if (string.IsNullOrWhiteSpace(error)) error = null;
            AnsiConsole.MarkupLine($"[red]{error ?? output}[/]");
        }
    }

    #region Helper Methods
    private static List<string> GetFilesChanged(string workingFolder,
                                                out string error)
    {
        error = string.Empty;
        var changed = new List<string>();

        try
        {
            using (var repo = new Repository(workingFolder))
            {
                foreach (TreeEntryChanges c in repo.Diff.Compare<TreeChanges>(repo.Head.Tip?.Tree,
                             DiffTargets.Index | DiffTargets.WorkingDirectory))
                {
                    changed.Add(c.Path ?? string.Empty);
                }
            }
        }
        catch (Exception e)
        {
            error = e.ToString();
        }
        
        return changed;
    }
    
    
    /// <summary>
    /// This runs the specified command with the arguments supplied and returns the error code
    /// and process output.
    /// </summary>
    /// <param name="command">The command to execute.</param>
    /// <param name="output">The program's output.</param>
    /// <param name="error">The program's error output.</param>
    /// <param name="arguments">The arguments to supply with the command.</param>
    /// <returns>The process's exit code.</returns>
    // ReSharper disable once UnusedMember.Local
    private static int Run(string command, 
                           out string output, 
                           out string error,
                           params string[] arguments)
    {
        output = string.Empty;
        error = string.Empty;

        var process = new Process();
        var startInfo = process.StartInfo;
        try
        {
            startInfo.FileName = command;
            startInfo.Arguments = string.Join(' ', arguments);
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

