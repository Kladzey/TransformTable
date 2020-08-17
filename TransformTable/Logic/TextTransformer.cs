using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class TextTransformer : ITextTransformer
    {
        private readonly ITableLoader tableLoader;
        private readonly ITableSaver tableSaver;
        private readonly ITableTransformer tableTransformer;

        public TextTransformer(
            ITableLoader tableLoader,
            ITableSaver tableSaver,
            ITableTransformer tableTransformer)
        {
            this.tableLoader = tableLoader ?? throw new ArgumentNullException(nameof(tableLoader));
            this.tableSaver = tableSaver ?? throw new ArgumentNullException(nameof(tableSaver));
            this.tableTransformer = tableTransformer ?? throw new ArgumentNullException(nameof(tableTransformer));
        }

        public void Transform(TextReader input, TextWriter output, string query)
        {
            var inputTable = tableLoader.Load(input);
            var result = tableTransformer.Transform(inputTable, query);
            tableSaver.Save(result, output);
        }
    }
}