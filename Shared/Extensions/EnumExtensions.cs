using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Functions.Shared.Extensions
{    /// <summary>
     /// Extends enumfields with custom functions
     /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of an enumfield with DescriptionAttribute
        /// </summary>
        /// <param name="value">Enum field</param>
        /// <returns>Desciption</returns>
        public static string Description(this System.Enum value)
        {
            var attribute = value.GetAttribute<DescriptionAttribute>();
            return attribute != default ? attribute.Description : "Description Not Found";
        }


        public static string GetDescription(this System.Enum GenericEnum) //Hint: Change the method signature and input paramter to use the type parameter T
        {
            Type genericEnumType = GenericEnum.GetType();
            MemberInfo[] memberInfo = genericEnumType.GetMember(GenericEnum.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                var _Attribs = memberInfo[0]
                    .GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (_Attribs != null && _Attribs.Count() > 0)
                {
                    return ((DescriptionAttribute)_Attribs.ElementAt(0)).Description;
                }
            }

            return GenericEnum.ToString();
        }

        public static TEnum ToEnum<TEnum>(this int value, TEnum defaultValue) where TEnum : System.Enum
        {
            var info = typeof(TEnum);
            if (info.IsEnum)
            {
                if (System.Enum.IsDefined(info, value))
                {
                    TEnum result = (TEnum)System.Enum.Parse(info, value.ToString(), true);
                    return result;
                }
            }
            return defaultValue;
        }

        /// <summary>
        /// Gets the attribute of an enum field based on its type
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type</typeparam>
        /// <param name="value">Enum field</param>
        /// <returns>First Attribute of TAttribute</returns>
        public static TAttribute GetAttribute<TAttribute>(this System.Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = System.Enum.GetName(type, value);

            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>()
                .FirstOrDefault();
        }
        /// <summary>
        /// Gets the attributes of an enum field based on their type
        /// </summary>
        /// <typeparam name="TAttribute">Attribute type</typeparam>
        /// <param name="value">Enum field</param>
        /// <returns>Attributes of TAttribute</returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(this System.Enum value)
            where TAttribute : Attribute
        {
            var type = value.GetType();
            var name = System.Enum.GetName(type, value);

            return type.GetField(name)
                .GetCustomAttributes(false)
                .OfType<TAttribute>();
        }
    }
}
