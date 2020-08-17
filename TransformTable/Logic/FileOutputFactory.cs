using System;
using System.IO;

namespace Kladzey.TransformTable.Logic
{
    public class FileOutputFactory : IOutputFactory
    {
        private readonly string path;

        public FileOutputFactory(string path)
        {
            this.path = path ?? throw new ArgumentNullException(nameof(path));
        }

        public Owned<TextWriter> Create()
        {
            return Owned.From<TextWriter>(new StreamWriter(path));
        }
    }
}