using FluentAssertions;
using FluentAssertions.Execution;
using Kladzey.TransformTable.Logic;
using Xunit;

namespace Kladzey.TransformCsv.Tests.Logic
{
    public class SqliteTransformerTests
    {
        private static readonly SqliteTableTransformer sut = SqliteTableTransformer.Instance;

        [Fact]
        public void ShouldTransform()
        {
            // Given
            var inputTable = new Table(
                new[]
                {
                    "col1", "col2",
                },
                new[]
                {
                    new[] {"1", "-2"},
                    new[] {"3", "4"},
                    new[] {"2", "3"},
                });

            // When
            var result = sut.Transform(
                inputTable,
                "select col1 + col2 sum from data where col2 > 0 order by col1");

            // Then
            using (new AssertionScope())
            {
                result.Header.Should().Equal("sum");
                result.Rows.Should().BeEquivalentTo(new[] {"5"}, new[] {"7"});
            }
        }
    }
}