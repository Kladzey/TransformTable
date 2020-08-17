using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class ConsoleInputFactory : IInputFactory
    {
        public static readonly ConsoleInputFactory Instance = new ConsoleInputFactory();

        private ConsoleInputFactory()
        {

        }

        public Owned<TextReader> Create()
        {
            return new Owned<TextReader>(Console.In, null);
        }
    }
}