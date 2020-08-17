using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class ConsoleOutputFactory : IOutputFactory
    {
        public static readonly ConsoleOutputFactory Instance = new ConsoleOutputFactory();

        private ConsoleOutputFactory()
        {
        }

        public Owned<TextWriter> Create()
        {
            return new Owned<TextWriter>(Console.Out, null);
        }
    }
}