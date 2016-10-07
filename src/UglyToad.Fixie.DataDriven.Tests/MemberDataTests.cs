namespace UglyToad.Fixie.DataDriven.Tests
{
    using System.Collections.Generic;

    public class MemberDataTests : TestBase
    {
        public static IEnumerable<object[]> Property => new[]
        {
            new object[] {1, 2, 3},
            new object[] {16, 9, 25}
        };

        public static IEnumerable<object[]> Field = new[]
        {
            new object[] {1, 2, 2},
            new object[] {3, 5, 15}
        };

        protected static IEnumerable<object[]> Method()
        {
            return new[]
            {
                new object[] {5, 2, 3},
                new object[] {5, 3, 2}
            };
        }

        [MemberData("Property")]
        public void AdditionMember(int a, int b, int result)
        {
            var actual = a + b;

            Assert.IsEqual(result, actual);
        }

        [MemberData("BaseProperty")]
        public void AdditionBaseMember(int a, int b, int expected)
        {
            var result = a + b;

            Assert.IsEqual(expected, result);
        }

        [MemberData("Field")]
        public void MultiplicationField(int a, int b, int expected)
        {
            var result = a*b;

            Assert.IsEqual(expected, result);
        }

        [MemberData("BaseField")]
        public void MultiplicationBaseField(int a, int b, int expected)
        {
            var result = a * b;

            Assert.IsEqual(expected, result);
        }

        [MemberData("Method")]
        public void SubtractionMethod(int a, int b, int expected)
        {
            var result = a - b;

            Assert.IsEqual(expected, result);
        }

        [MemberData("BaseMethod")]
        public void SubtractionBaseMethod(int a, int b, int expected)
        {
            var result = a - b;

            Assert.IsEqual(expected, result);
        }

        [MemberData("TestData", Type = typeof(DataProvider))]
        public void SequenceExternalProperty(int a, int b, int expected)
        {
            var result = b + (b - a);

            Assert.IsEqual(expected, result);
        }

        [MemberData("Method")]
        [MemberData("BaseMethod")]
        public void MultipleMemberData(int a, int b, int expected)
        {
            var result = a - b;

            Assert.IsEqual(expected, result);
        }
    }
}
