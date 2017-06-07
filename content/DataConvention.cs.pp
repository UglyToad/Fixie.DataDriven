namespace $rootnamespace$
{
    using Fixie;
    using UglyToad.Fixie.DataDriven;

    public class DataDrivenTestConvention : Convention
    {
        public DataDrivenTestConvention()
        {
            Classes.NameEndsWith("Tests");

            Methods.Where(method => method.IsVoid() || method.IsAsync());

            Parameters
                .Add<ProvideTestDataFromInlineData>()
                .Add<ProvideTestDataFromMemberData>();
        }
    }
}
