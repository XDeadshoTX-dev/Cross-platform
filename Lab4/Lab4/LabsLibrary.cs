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

        public static void RunCommand(string command)
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
        public class Lab1
        {
            public void Build()
            {
                Console.WriteLine("Building Lab1...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab1 -t:Build");
            }
            public void Test()
            {
                Console.WriteLine("Lab1 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab1 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Lab1 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab1 -t:Run");
            }
        }

        public class Lab2
        {
            public void Build()
            {
                Console.WriteLine("Building Lab2...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Run");
            }
            public void Test()
            {
                Console.WriteLine("Lab2 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Lab2 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Run");
            }
        }

        public class Lab3
        {
            public void Build()
            {
                Console.WriteLine("Building Lab3...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab3 -t:Build");
            }
            public void Test()
            {
                Console.WriteLine("Lab3 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab3 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Lab3 is starting...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab3 -t:Run");
            }
        }
    }
}
