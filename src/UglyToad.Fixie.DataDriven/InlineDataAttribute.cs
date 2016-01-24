namespace UglyToad.Fixie.DataDriven
{
    using System;

    /// <summary>
    /// Provides data from the attribute itself. One test per InlineData attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineDataAttribute : Attribute
    {
        /// <summary>
        /// The data provided to the test method.
        /// </summary>
        public object[] Data { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineDataAttribute"/>.
        /// </summary>
        /// <param name="data">The data to pass to the test method.</param>
        public InlineDataAttribute(params object[] data)
        {
            Data = data;
        }
    }
}
