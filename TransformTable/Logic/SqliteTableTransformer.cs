using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.Data.Sqlite;

namespace Kladzey.TransformTable.Logic
{
    public class SqliteTableTransformer : ITableTransformer
    {
        public static readonly SqliteTableTransformer Instance = new SqliteTableTransformer();

        private SqliteTableTransformer()
        {
        }

        public Table Transform(Table inputTable, string query)
        {
            using var connection = CreateConnection();
            connection.Open();

            CreateTable(connection, inputTable.Header);

            InsertData(connection, inputTable);

            return Transform(connection, query);
        }

        private static IDbConnection CreateConnection()
        {
            var connection = new SqliteConnection("Data Source=:memory:");
            connection.CreateFunction(
                "REGEXP",
                (string pattern, string input) => Regex.IsMatch(input, pattern));
            connection.CreateFunction(
                "CT_REGEXP_VALUE",
                (string pattern, string input) => Regex.Match(input, pattern).Value);
            connection.CreateFunction(
                "CT_PARSE_DATETIME",
                (string s, string format) =>
                    DateTime.TryParseExact(
                        s,
                        format,
                        CultureInfo.InvariantCulture,
                        DateTimeStyles.None,
                        out var result)
                        ? result.ToString("O")
                        : null);
            return connection;
        }

        private static Table Transform(IDbConnection connection, string query)
        {
            using var command = connection.CreateCommand();
            command.CommandText = query;
            using var reader = command.ExecuteReader();

            var header = new string[reader.FieldCount];
            var rows = new List<string[]>();

            for (var i = 0; i < header.Length; ++i)
            {
                header[i] = reader.GetName(i);
            }

            while (reader.Read())
            {
                var row = new string[header.Length];
                for (var i = 0; i < header.Length; ++i)
                {
                    row[i] = reader.GetValue(i)?.ToString() ?? string.Empty;
                }

                rows.Add(row);
            }

            return new Table(header, rows.ToArray());
        }

        private static void InsertData(IDbConnection connection, Table table)
        {
            using var transaction = connection.BeginTransaction();

            using var command = connection.CreateCommand();
            command.CommandText =
                $"insert into data values ({string.Join(", ", table.Header.Select((s, i) => "$p" + i))})";
            var parameters = table.Header
                .Select((s, i) =>
                {
                    var parameter = command.CreateParameter();
                    parameter.ParameterName = "p" + i;
                    return parameter;
                })
                .ToArray();
            foreach (var parameter in parameters)
            {
                command.Parameters.Add(parameter);
            }

            var emptyStrings = Enumerable.Repeat("", table.Header.Length);
            foreach (var row in table.Rows)
            {
                // ReSharper disable once PossibleMultipleEnumeration
                foreach (var (parameter, value) in parameters.Zip(row.Concat(emptyStrings), (p, v) => (p, v)))
                {
                    parameter.Value = value;
                }

                command.ExecuteNonQuery();
            }

            transaction.Commit();
        }

        private static void CreateTable(IDbConnection connection, string[] header)
        {
            var command = connection.CreateCommand();
            command.CommandText = new StringBuilder("create table data (")
                .AppendJoin(", ", header.Select(s => $"\"{s}\" TEXT NOT NULL"))
                .Append(")")
                .ToString();
            command.ExecuteNonQuery();
        }
    }
}