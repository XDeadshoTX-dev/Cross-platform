using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System;
using System.IO;

namespace Lab4.LabsLibrary
{
    public class LabsLibrary
    {
        public static void Main(string[] args)
        {

        }
        public static string RunCommand(string command)
        {
            string output = string.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                output = ExecuteCommand("cmd.exe", $"/c {command}");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                output = ExecuteCommand("/bin/bash", $"-c \"{command}\"");
            }
            else
            {
                Console.WriteLine("Unsupported operating system");
            }
            return output;
        }

        public static void CreateFileWithPath(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
                Console.WriteLine($"File {path} content: {content}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {path}: {ex.Message}");
            }
        }

        public static string ReadFileContent(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    string data = File.ReadAllText(fileName);
                    Console.WriteLine($"File {fileName} content: {data}");
                    return data;
                }
                else
                {
                    Console.WriteLine($"File {fileName} doesn't exist.");
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error {fileName}: {ex.Message}");
                return string.Empty;
            }
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

            using (var process = Process.Start(processInfo))
            {
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                Console.WriteLine("Result Execute: " + output);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine($"Error: {error}");
                }
                return output;
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
                Console.WriteLine("Testing Lab1...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab1 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Starting Lab1...");

                string destinationFilePath = "../../Lab1/INPUT.TXT";
                File.Copy(inputFile, destinationFilePath, overwrite: true);
                Console.WriteLine($"File copied successfully to: {destinationFilePath}");

                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab1 -t:Run");

                string resultFilePath = "../../Lab1/OUTPUT.TXT";
                File.Copy(resultFilePath, outputFile, overwrite: true);
                Console.WriteLine($"File copied successfully to: {outputFile}");
            }
        }

        public class Lab2
        {
            public void Build()
            {
                Console.WriteLine("Building Lab2...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Build");
            }
            public void Test()
            {
                Console.WriteLine("Testing Lab2...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Starting Lab2...");

                string destinationFilePath = "../../Lab2/INPUT.TXT";
                File.Copy(inputFile, destinationFilePath, overwrite: true);
                Console.WriteLine($"File copied successfully to: {destinationFilePath}");

                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab2 -t:Run");

                string resultFilePath = "../../Lab2/OUTPUT.TXT";
                File.Copy(resultFilePath, outputFile, overwrite: true);
                Console.WriteLine($"File copied successfully to: {outputFile}");
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
                Console.WriteLine("Testing Lab3...");
                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab3 -t:Test");
            }
            public void Run(string inputFile, string outputFile)
            {
                Console.WriteLine("Starting Lab3...");

                string destinationFilePath = "../../Lab3/INPUT.TXT";
                File.Copy(inputFile, destinationFilePath, overwrite: true);
                Console.WriteLine($"File copied successfully to: {destinationFilePath}");

                RunCommand("dotnet build ../../Build.proj -p:Solution=Lab3 -t:Run");

                string resultFilePath = "../../Lab3/OUTPUT.TXT";
                File.Copy(resultFilePath, outputFile, overwrite: true);
                Console.WriteLine($"File copied successfully to: {outputFile}");
            }
        }
    }
}
