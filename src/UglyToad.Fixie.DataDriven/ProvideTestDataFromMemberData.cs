namespace UglyToad.Fixie.DataDriven
{
    using System.Collections.Generic;
    using System.Reflection;
    using global::Fixie;

    public class ProvideTestDataFromMemberData : ParameterSource
    {
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            return MemberDataAttribute.GetData(method);
        }
    }
}
