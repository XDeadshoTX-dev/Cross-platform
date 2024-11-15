using System.Runtime.InteropServices;
using System;
using System.IO;
using McMaster.Extensions.CommandLineUtils;
using static Lab4.LabsLibrary.LabsLibrary;

namespace Lab4
{
    [Command(Name = "mylabs", Description = "Console application for running labs.")]
    [Subcommand(typeof(VersionCommand), typeof(RunCommand), typeof(SetPathCommand))]
    class Program
    {
        public static int Main(string[] args) => CommandLineApplication.Execute<Program>(args);

        private void OnExecute(CommandLineApplication app, IConsole console)
        {
            console.WriteLine("Specify a command to run (version, run, set-path).");
        }
    }

    [Command(Name = "version", Description = "Display version information.")]
    class VersionCommand
    {
        private void OnExecute(IConsole console)
        {
            console.WriteLine("Author: D.S");
            console.WriteLine("Version: 1.0.0");
        }
    }

    [Command(Name = "run", Description = "Run specified lab.")]
    class RunCommand
    {
        [Option("-I|--input", "Input file path", CommandOptionType.SingleValue)]
        public string InputFile { get; set; }

        [Option("-o|--output", "Output file path", CommandOptionType.SingleValue)]
        public string OutputFile { get; set; }

        [Argument(0, "lab", "Lab to run (lab1, lab2, lab3)")]
        public string Lab { get; set; }

        private void OnExecute(CommandLineApplication app, IConsole console)
        {
            if (string.IsNullOrEmpty(Lab))
            {
                console.WriteLine("Error: No lab specified.");
                app.ShowHelp();
                return;
            }

            string inputPath = string.Empty;
            string outputPath = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                inputPath = InputFile ?? Environment.GetEnvironmentVariable("LAB_PATH", EnvironmentVariableTarget.User) + "\\INPUT.TXT";
                outputPath = OutputFile ?? "OUTPUT.TXT";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                string EnvironmentVariableUnix = RunCommand("echo $LAB_PATH");
                inputPath = InputFile ?? $"{EnvironmentVariableUnix.Trim()}/INPUT.TXT";
                outputPath = OutputFile ?? "OUTPUT.TXT";
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                string pathMacOS = ReadFileContent("pathMacOS.txt");
                inputPath = InputFile ?? $"{pathMacOS.Trim()}/INPUT.TXT";
                outputPath = OutputFile ?? "OUTPUT.TXT";
            }

            Console.WriteLine($"[Debug] {inputPath} | {outputPath}");

            if (string.IsNullOrEmpty(inputPath) || inputPath == "\\INPUT.TXT" || inputPath == "/INPUT.TXT")
            {
                inputPath = "INPUT.TXT";
            }

            Console.WriteLine($"[Debug] {inputPath} | {outputPath}");

            if (!File.Exists(inputPath))
            {
                console.WriteLine($"Error: Input file not found at path '{inputPath}'");
                return;
            }

            switch (Lab.ToLower())
            {
                case "lab1":
                    Lab1 lab1 = new Lab1();
                    lab1.Build();
                    lab1.Test();
                    lab1.Run(inputPath, outputPath);
                    break;
                case "lab2":
                    Lab2 lab2 = new Lab2();
                    lab2.Build();
                    lab2.Test();
                    lab2.Run(inputPath, outputPath);
                    break;
                case "lab3":
                    Lab3 lab3 = new Lab3();
                    lab3.Build();
                    lab3.Test();
                    lab3.Run(inputPath, outputPath);
                    break;
                default:
                    console.WriteLine("Invalid lab specified. Use 'lab1', 'lab2', or 'lab3'.");
                    break;
            }
        }
    }

    [Command(Name = "set-path", Description = "Set input/output path.")]
    class SetPathCommand
    {
        [Option("-p|--path", "Set LAB_PATH environment variable", CommandOptionType.SingleValue)]
        public string Path { get; set; }

        private void OnExecute(IConsole console)
        {
            Console.WriteLine($"[Debug] {Path}");
            if (!string.IsNullOrEmpty(Path))
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    Environment.SetEnvironmentVariable("LAB_PATH", Path, EnvironmentVariableTarget.User);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    string addToBashrcCommand = $"echo 'export LAB_PATH={Path}' >> ~/.bashrc";
                    RunCommand(addToBashrcCommand);

                    string sourceBashrcCommand = "source ~/.bashrc";
                    RunCommand(sourceBashrcCommand);

                    string setInShellCommand = $"export LAB_PATH={Path} && echo $LAB_PATH";
                    RunCommand(setInShellCommand);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    string addToBashProfileCommand = $"echo 'export LAB_PATH={Path}' >> ~/.bash_profile";
                    RunCommand(addToBashProfileCommand);
                    string sourceBashProfileCommand = "source ~/.bash_profile";
                    RunCommand(sourceBashProfileCommand);
                    string setInShellCommand = $"export LAB_PATH={Path} && echo $LAB_PATH";
                    RunCommand(setInShellCommand);

                    CreateFileWithPath("pathMacOS.txt", Path);
                }
                else
                {
                    Console.WriteLine("Unsupported operating system");
                    return;
                }
                console.WriteLine($"LAB_PATH set to: {Path}");
            }
            else
            {
                console.WriteLine("Please specify a path.");
            }
        }
    }
}
