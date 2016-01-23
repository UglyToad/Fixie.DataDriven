namespace UglyToad.Fixie.DataDriven.Tests
{
    using System;

    public static class Assert
    {
        public static bool IsTrue(bool value)
        {
            if (!value)
            {
                throw new Exception("Test failed");
            }

            return true;
        }

        public static bool IsEqual<T>(T expected, T actual)
        {
            if (!expected.Equals(actual))
            {
                throw new Exception("Test failed");
            }

            return true;
        }
    }
}
