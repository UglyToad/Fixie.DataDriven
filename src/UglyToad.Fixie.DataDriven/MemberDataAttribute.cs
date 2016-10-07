namespace UglyToad.Fixie.DataDriven
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Get the test data from a member of this class. One of:
    /// Property
    /// Field
    /// Parameterless Method
    /// The member must be static and have type equivalent to IEnumberable&lt;object&gt;.
    /// The member can have any access modifier (public, internal, protected, private).
    /// The member can be on a base class and will automatically be located without specifying the type.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class MemberDataAttribute : Attribute
    {
        /// <summary>
        /// The name of the member to locate and use to provide the data.
        /// </summary>
        public string MemberName { get; }

        /// <summary>
        /// The type of the class holding the member. If not provided the current test class will be used.
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineDataAttribute"/>.
        /// </summary>
        /// <param name="memberName">The name of the member to use.</param>
        public MemberDataAttribute(string memberName)
        {
            MemberName = memberName;
        }

        internal static IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var attributes = methodInfo.GetCustomAttributes<MemberDataAttribute>(true).ToList();

            if (attributes.Count == 0)
            {
                return new object[][] { };
            }

            return attributes.SelectMany(x => GetSingleAttributeData(methodInfo, x));
        }

        private static IEnumerable<object[]> GetSingleAttributeData(MethodInfo methodInfo, MemberDataAttribute attribute)
        {
            var targetType = attribute.Type ?? methodInfo.DeclaringType;

            if (targetType == null)
            {
                throw new ArgumentException($"No type found for MemberDataAttribute on method {methodInfo.Name} with attribute value {attribute.MemberName}.");
            }

            var memberDataAccessor = GetFromProperty(attribute, targetType) 
                ?? GetFromField(attribute, targetType) 
                ?? GetFromMethod(attribute, targetType);

            if (memberDataAccessor == null)
            {
                    throw new ArgumentException($"Cannot locate a field, property or parameterless method with name {attribute.MemberName} on type {targetType.Name}. Did you forget to make it static?");
            }

            var data = memberDataAccessor() as IEnumerable<object[]>;

            if (data == null)
            {
                throw new ArgumentException($"The member data was not of a type convertible to IEnumerable<object[]> for member name {attribute.MemberName} on type {targetType.Name}.");
            }

            return data;
        } 

        private static Func<object> GetFromField(MemberDataAttribute memberDataAttribute, Type type)
        {
            var fieldInfo = GetMemberInfoByName(type, t => t.GetRuntimeFields(),
                f => f.Name.Equals(memberDataAttribute.MemberName, StringComparison.InvariantCultureIgnoreCase));

            if (fieldInfo == null || !fieldInfo.IsStatic)
            {
                return null;
            }

            return () => fieldInfo.GetValue(null);
        }

        private static Func<object> GetFromMethod(MemberDataAttribute memberDataAttribute, Type type)
        {
            var methodInfo = GetMemberInfoByName(type, t => t.GetRuntimeMethods(),
                m => m.Name.Equals(memberDataAttribute.MemberName, StringComparison.InvariantCultureIgnoreCase));

            if (methodInfo == null || !methodInfo.IsStatic)
            {
                return null;
            }

            return () => methodInfo.Invoke(null, null);
        }

        private static Func<object> GetFromProperty(MemberDataAttribute memberDataAttribute, Type type)
        {
            var propInfo = GetMemberInfoByName(type, t => t.GetRuntimeProperties(),
                p => p.Name.Equals(memberDataAttribute.MemberName, StringComparison.InvariantCultureIgnoreCase));

            if (propInfo?.GetMethod == null || !propInfo.GetMethod.IsStatic)
            {
                return null;
            }

            return () => propInfo.GetValue(null, null);
        }

        private static T GetMemberInfoByName<T>(Type type, Func<Type, IEnumerable<T>> getAllMemberInfos, Func<T, bool> filterStatement) where T : class
        {
            while (true)
            {
                if (type == typeof (object) || type == null)
                {
                    return null;
                }

                var propertyInfo = getAllMemberInfos(type).FirstOrDefault(filterStatement);

                if (propertyInfo != null)
                {
                    return propertyInfo;
                }

                type = type.BaseType;
            }
        }
    }
}
