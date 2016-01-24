# Fixie - Data Driven #

[![Build status](https://ci.appveyor.com/api/projects/status/fa6k4yw9h5fmg1i5?svg=true)](https://ci.appveyor.com/project/EliotJones/fixie-datadriven)

[NuGet][NuGet]

This projects adds xUnit style attributes to provide test data for Fixie unit tests.

It adds the following attributes:

## Inline Data ##

Provide data from the attribute itself.

	[InlineData(16, 9, 25)]
	public void Add(int a, int b, int result)
	{
	    Assert.IsEqual(a + b, result);
	} 

## Member Data ##

Provide data from a field, property or method of either the test class or any other class:

    public static IEnumerable<object[]> Property => new[]
    {
        new object[] {16, 9, 25}
    };

    [MemberData("Property")]
    public void Add(int a, int b, int result)
    {
        var actual = a + b;

        Assert.IsEqual(result, actual);
    }

Note that the member data property, field or method has to be static.

## Configuration ##

Add a custom Fixie configuration class and add the following lines to the parameters configuration:

    Parameters
        .Add<ProvideTestDataFromInlineData>()
        .Add<ProvideTestDataFromMemberData>();

For example a full configuration would look like this:

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
    
[NuGet]: https://www.nuget.org/packages/UglyToad.Fixie.DataDriven/
