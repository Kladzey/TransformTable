using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Kladzey.TransformTable.Logic
{
    public class CsvTableSaver : ITableSaver
    {
        public static readonly CsvTableSaver Instance = new CsvTableSaver();

        private CsvTableSaver()
        {
        }

        public void Save(Table table, TextWriter textWriter)
        {
            if (textWriter == null)
            {
                throw new ArgumentNullException(nameof(textWriter));
            }
            using var csvWriter = new CsvWriter(textWriter, CultureInfo.InvariantCulture);
            foreach (var row in table.Rows.Prepend(table.Header))
            {
                foreach (var value in row)
                {
                    csvWriter.WriteField(value);
                }

                csvWriter.NextRecord();
            }
        }
    }
}