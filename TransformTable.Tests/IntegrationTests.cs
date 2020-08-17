using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;

namespace Kladzey.TransformCsv.Tests
{
    public class IntegrationTests
    {
        private const string inputCsv = @"col1,col2
1,-2
3,4
2,3";

        private const string query = "select col1 + col2 sum from data where col2 > 0 order by col1";

        private const string expectedOutput = @"sum
5
7
";

        private readonly ITestOutputHelper testOutputHelper;

        public IntegrationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldTransformFromStandardInputToStandardOutput()
        {
            // When
            using var process = Process.Start(new ProcessStartInfo("dotnet")
            {
                WorkingDirectory = Path.Combine(GetSolutionPath(), "TransformTable"),
                UseShellExecute = false,
                RedirectStandardError = true,
                RedirectStandardInput = true,
                RedirectStandardOutput = true,
                ArgumentList =
                {
                    "run",
                    "--",
                    "--query",
                    query,
                },
            });

            process.StandardInput.Write(inputCsv);
            process.StandardInput.Close();

            process.WaitForExit();

            // Then
            testOutputHelper.WriteLine(process.StandardError.ReadToEnd());
            process.ExitCode.Should().Be(0);
            process.StandardOutput.ReadToEnd().Should().Be(expectedOutput);
        }

        private string GetSolutionPath()
        {
            var directoryInfo = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory!);
            while (!directoryInfo.EnumerateFiles("TransformTable.sln", SearchOption.TopDirectoryOnly).Any())
            {
                directoryInfo = directoryInfo.Parent ??
                                throw new InvalidOperationException("Solution directory is not found.");
            }

            return directoryInfo.ToString();
        }
    }
}