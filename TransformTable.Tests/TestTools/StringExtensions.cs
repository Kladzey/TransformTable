using System;
using System.Text.RegularExpressions;

namespace Kladzey.TransformCsv.Tests.TestTools
{
    public static class StringExtensions
    {
        private static readonly Regex lineEndingsRegex = new Regex(@"\r\n|\n\r|\n|\r");

        public static string NormalizeLineEndings(this string s)
        {
            return lineEndingsRegex.Replace(s, Environment.NewLine);
        }
    }
}