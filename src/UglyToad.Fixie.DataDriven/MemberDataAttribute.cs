namespace UglyToad.Fixie.DataDriven
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    [AttributeUsage(AttributeTargets.Method)]
    public class MemberDataAttribute : Attribute
    {
        public string MemberName { get; }

        public Type Type { get; set; }

        public MemberDataAttribute(string memberName)
        {
            MemberName = memberName;
        }

        internal static IEnumerable<object[]> GetData(MethodInfo methodInfo)
        {
            var attribute = methodInfo.GetCustomAttribute<MemberDataAttribute>(true);

            if (attribute == null)
            {
                return new object[][] { };
            }

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

            if (propInfo == null || propInfo.GetMethod == null || !propInfo.GetMethod.IsStatic)
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
