using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class FileInputFactory : IInputFactory
    {
        private readonly string path;

        public FileInputFactory(string path)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public Owned<TextReader> Create()
        {
            return Owned.From<TextReader>(new StreamReader(path));
        }
    }
}