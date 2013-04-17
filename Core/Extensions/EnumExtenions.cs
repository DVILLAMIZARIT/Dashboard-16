using System;
using System.ComponentModel;
using System.Reflection;

namespace Core.Extensions
{
    public static class EnumExtenions
    {
        public static String GetDescription(this Enum e)
        {
            String enumAsString = e.ToString();
            Type type = e.GetType();
            MemberInfo[] members = type.GetMember(enumAsString);
            if (members != null && members.Length > 0)
            {
                Object[] attributes = members[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attributes != null && attributes.Length > 0)
                {
                    enumAsString = ((DescriptionAttribute)attributes[0]).Description;
                }
            }
            return enumAsString;
        }

        public static TEnum GetFromDescription<TEnum>(String description)
            where TEnum : struct, IConvertible // http://stackoverflow.com/a/79903/298053
        {
            if (!typeof(TEnum).IsEnum)
            {
                throw new InvalidOperationException();
            }
            foreach (FieldInfo field in typeof(TEnum).GetFields())
            {
                DescriptionAttribute attribute = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attribute != null)
                {
                    if (attribute.Description == description)
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        return (TEnum)field.GetValue(null);
                    }
                }
            }
            return default(TEnum);
        }
    }
}
