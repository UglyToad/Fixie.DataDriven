namespace UglyToad.Fixie.DataDriven
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using global::Fixie;

    public class ProvideTestDataFromInlineData : ParameterSource
    {
        public IEnumerable<object[]> GetParameters(MethodInfo method)
        {
            return method.GetCustomAttributes<InlineDataAttribute>(true).Select(input => input.Data);
        }
    }
}
