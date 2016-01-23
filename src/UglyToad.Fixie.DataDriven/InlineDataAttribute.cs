namespace UglyToad.Fixie.DataDriven
{
    using System;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class InlineDataAttribute : Attribute
    {
        public object[] Data { get; }

        public InlineDataAttribute(params object[] data)
        {
            Data = data;
        }
    }
}
