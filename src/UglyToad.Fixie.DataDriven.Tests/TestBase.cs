namespace UglyToad.Fixie.DataDriven.Tests
{
    using System.Collections.Generic;

    public class TestBase
    {
        protected static IEnumerable<object[]> BaseProperty => new[]
        {
            new object[] {1, 2, 3},
            new object[] {16, 9, 25}
        };

        private static IEnumerable<object[]> baseField = new[]
        {
            new object[] {1, 16, 16},
            new object[] {3, 7, 21}
        };

        private static IEnumerable<object[]> BaseMethod()
        {
            return new[]
            {
                new object[] {27, 2, 25},
                new object[] {53, 1, 52}
            };
        }
    }
}
