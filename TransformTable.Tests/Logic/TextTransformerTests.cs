using System.IO;
using FluentAssertions;
using Kladzey.TransformCsv.Tests.TestTools;
using Kladzey.TransformTable.Logic;
using Xunit;

namespace Kladzey.TransformCsv.Tests.Logic
{
    public class TextTransformerTests
    {
        [Fact]
        public void ShouldTransform()
        {
            // Given
            var sut = new TextTransformer(
                CsvTableLoader.Instance,
                CsvTableSaver.Instance,
                SqliteTableTransformer.Instance);

            var input = new StringReader(@"col1,col2
1,-2
3,4
2,3");

            var output = new StringWriter();

            // When
            sut.Transform(input, output, "select col1 + col2 sum from data where col2 > 0 order by col1");

            // Then
            output.ToString().NormalizeLineEndings().Should().Be(@"sum
5
7
".NormalizeLineEndings());
        }
    }
}