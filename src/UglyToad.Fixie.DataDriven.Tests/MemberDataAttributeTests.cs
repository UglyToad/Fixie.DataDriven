namespace UglyToad.Fixie.DataDriven.Tests
{
    using System;

    public class MemberDataAttributeTests
    {
        [InlineData("NotATest")]
        [InlineData("AnotherNotATest")]
        public void CannotFindMemberDataAttribute_DoesNotGetStuck(string methodName)
        {
            try
            {
                MemberDataAttribute.GetData(typeof(NotATestClass).GetMethod(methodName));
            }
            catch (ArgumentException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Cannot locate a field, property"));
            }
        }
    }

    public class NotATestClass
    {
        [MemberData("Jim")]
        public void NotATest(int a)
        {
            Assert.IsEqual(a, a);
        }

        [MemberData("Bob", Type = typeof(MemberDataAttributeTests))]
        public void AnotherNotATest(int a)
        {
            Assert.IsEqual(a, a);
        }
    }
}
