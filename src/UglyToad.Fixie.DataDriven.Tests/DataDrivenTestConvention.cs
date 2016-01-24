namespace UglyToad.Fixie.DataDriven.Tests
{
    using global::Fixie;

    public class DataDrivenTestConvention : Convention
    {
        public DataDrivenTestConvention()
        {
            Classes.NameEndsWith("Tests");

            Methods.Where(method => method.IsVoid());

            Parameters
                .Add<ProvideTestDataFromInlineData>()
                .Add<ProvideTestDataFromMemberData>();
        }
    }
}
