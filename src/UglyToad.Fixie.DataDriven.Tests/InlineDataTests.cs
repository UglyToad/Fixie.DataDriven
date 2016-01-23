namespace UglyToad.Fixie.DataDriven.Tests
{
    public class InlineDataTests
    {
        [InlineData(23, 2, 25)]
        [InlineData(16, 9, 25)]
        public void AddInline(int a, int b, int result)
        {
            Assert.IsEqual(a + b, result);
        }
    }
}
