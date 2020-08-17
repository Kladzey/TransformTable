using CommandLine;
using Kladzey.TransformTable.Logic;

namespace Kladzey.TransformTable
{
    public static class Program
    {
        public static int Main(string[] args)
        {
            if (!(Parser.Default.ParseArguments<Options>(args) is Parsed<Options> parsed))
            {
                return 1;
            }

            Execute(parsed.Value);
            return 0;
        }

        public static void Execute(Options options)
        {
            var application = new Application(
                new TextTransformer(
                    CsvTableLoader.Instance,
                    CsvTableSaver.Instance,
                    SqliteTableTransformer.Instance),
                options.InputFile != null
                    ? (IInputFactory) new FileInputFactory(options.InputFile)
                    : ConsoleInputFactory.Instance,
                options.OutputFile != null
                    ? (IOutputFactory) new FileOutputFactory(options.OutputFile)
                    : ConsoleOutputFactory.Instance);

            application.Execute(options.Query);
        }
    }
}