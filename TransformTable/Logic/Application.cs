using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class Application : IApplication
    {
        private readonly ITextTransformer textTransformer;
        private readonly IInputFactory inputFactory;
        private readonly IOutputFactory outputFactory;

        public Application(
            ITextTransformer textTransformer,
            IInputFactory inputFactory,
            IOutputFactory outputFactory)
        {
            this.textTransformer = textTransformer ?? throw new ArgumentNullException(nameof(textTransformer));
            this.inputFactory = inputFactory ?? throw new ArgumentNullException(nameof(inputFactory));
            this.outputFactory = outputFactory ?? throw new ArgumentNullException(nameof(outputFactory));
        }

        public void Execute(string query)
        {
            using var ownedInput = inputFactory.Create();
            using var tempOutput = new StringWriter();
            textTransformer.Transform(ownedInput.Value, tempOutput, query);
            using var ownedOutput = outputFactory.Create();
            ownedOutput.Value.Write(tempOutput);
        }
    }
}