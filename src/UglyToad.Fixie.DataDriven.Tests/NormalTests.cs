namespace UglyToad.Fixie.DataDriven.Tests
{
    public class NormalTests
    {
        public void Addition()
        {
            var result = 1 + 2;

            Assert.IsEqual(result, 3);
        }
        
        public void Modulus()
        {
            var result = 5 % 2;

            Assert.IsEqual(result, 1);
        }
    }
}
