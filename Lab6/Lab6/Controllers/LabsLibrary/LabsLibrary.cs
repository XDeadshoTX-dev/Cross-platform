using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace Lab4
{
    public class LabsLibrary
    {
        private string lab;
        private string output = "";
        public string GetOutputConsole
        {
            get
            {
                return output;
            }
        }
        public LabsLibrary(string lab)
        {
            this.lab = lab;
        }
        private static string RunCommand(string command)
        {
            string outputExecute;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                outputExecute = ExecuteCommand("cmd.exe", $"/c {command}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                outputExecute = ExecuteCommand("/bin/bash", $"-c \"{command}\"");
            }
            else
            {
                outputExecute = "Unsupported operating system";
            }
            return outputExecute;
        }

        private static string ExecuteCommand(string shell, string arguments)
        {
            var processInfo = new ProcessStartInfo
            {
                FileName = shell,
                Arguments = arguments,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            string output, error;
            using (var process = Process.Start(processInfo))
            {
                output = process.StandardOutput.ReadToEnd();
                error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(output);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
            return output;
        }
        public void Build()
        {
            string text = $"Building {lab}...\n";
            Console.WriteLine(text);
            output += text;
            output += RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Build");
        }
        public void Test()
        {
            string text = $"Testing {lab}...\n";
            Console.WriteLine(text);
            output += text;
            output += RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Test");
        }
        public void Run()
        {
            string text = $"Starting {lab}...\n";
            Console.WriteLine(text);
            output += text;
            output += RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Run");
        }
    }
}
