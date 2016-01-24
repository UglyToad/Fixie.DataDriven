namespace UglyToad.Fixie.DataDriven
{
    using System.Collections.Generic;
    using System.Reflection;
    using global::Fixie;

    /// <summary>
    /// Use this to configure Fixie to look for <see cref="MemberDataAttribute"/>s on your test methods.
    /// </summary>
    public class ProvideTestDataFromMemberData : ParameterSource
    {
        /// <summary>
        /// Used by Fixie to provide test data to the engine.
        /// </summary>
        /// <param name="method">The test method currently running.</param>
        /// <returns>The data to provide to the test method.</returns>
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            return MemberDataAttribute.GetData(method);
        }
    }
}
