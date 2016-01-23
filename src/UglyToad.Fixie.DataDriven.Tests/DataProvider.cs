namespace UglyToad.Fixie.DataDriven.Tests
{
    using System.Collections.Generic;

    internal class DataProvider
    {
        public static IEnumerable<object[]> TestData => new[]
        {
            new object[] {5, 7, 9},
            new object[] {2, 12, 22}
        };
    }
}
