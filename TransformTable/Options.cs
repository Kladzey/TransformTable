using CommandLine;

namespace Kladzey.TransformTable
{
    public class Options
    {
        [Option( "input-file", Required = false, HelpText = "Input file to be processed.")]
        public string? InputFile { get; set; }

        [Option('q', "query", Required = true, HelpText = "Transform query.")]
        public string Query { get; set; } = null!;

        [Option("output-file", Required = false, HelpText = "Output file.")]
        public string? OutputFile { get; set; }
    }
}