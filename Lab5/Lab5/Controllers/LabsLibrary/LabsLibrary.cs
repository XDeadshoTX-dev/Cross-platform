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
        public LabsLibrary(string lab)
        {
            this.lab = lab;
        }
        private static void RunCommand(string command)
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                ExecuteCommand("cmd.exe", $"/c {command}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                ExecuteCommand("/bin/bash", $"-c \"{command}\"");
            }
            else
            {
                Console.WriteLine("Unsupported operating system");
            }
        }

        private static void ExecuteCommand(string shell, string arguments)
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

            using (var process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine(output);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                }
            }
        }
        public void Build()
        {
            Console.WriteLine($"Building {lab}...");
            RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Build");
        }
        public void Test()
        {
            Console.WriteLine($"Testing {lab}...");
            RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Test");
        }
        public void Run()
        {
            Console.WriteLine($"Starting {lab}...");

            RunCommand($"dotnet build ../../Build.proj -p:Solution={lab} -t:Run");
        }
    }
}
