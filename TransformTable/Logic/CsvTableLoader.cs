using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using CsvHelper;

namespace Kladzey.TransformTable.Logic
{
    public class CsvTableLoader : ITableLoader
    {
        public static readonly CsvTableLoader Instance = new CsvTableLoader();

        private CsvTableLoader()
        {

        }

        public Table Load(TextReader textReader)
        {
            if (textReader == null)
            {
                throw new ArgumentNullException(nameof(textReader));
            }

            using var csvReader = new CsvReader(textReader, CultureInfo.InvariantCulture);
            if (!csvReader.Read() || !csvReader.ReadHeader())
            {
                throw new InvalidOperationException("Header is required.");
            }

            var header = csvReader.Context.Record;
            var rows = new List<string[]>();
            while (csvReader.Read())
            {
                rows.Add(csvReader.Context.Record);
            }

            return new Table(header, rows.ToArray());
        }
    }
}