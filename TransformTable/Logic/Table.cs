using System;

namespace Kladzey.TransformTable.Logic
{
    public readonly struct Table
    {
        public Table(string[] header, string[][] rows)
        {
            Header = header ?? throw new ArgumentNullException(nameof(header));
            Rows = rows ?? throw new ArgumentNullException(nameof(rows));
        }

        public string[] Header { get; }

        public string[][] Rows { get; }
    }
}