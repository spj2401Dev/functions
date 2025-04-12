using System.ComponentModel;
using System.Reflection;

namespace Functions.Shared.Extensions
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description of an enum field with DescriptionAttribute
        /// </summary>
        /// <param name="value">Enum field</param>
        /// <returns>Description or the enum name if no description is found</returns>
        public static string GetDescription(this System.Enum value)
        {
            Type type = value.GetType();
            string? name = System.Enum.GetName(type, value);

            if (name == null)
                return value.ToString();

            FieldInfo? field = type.GetField(name);

            if (field == null)
                return name;

            var attributes = field.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return ((DescriptionAttribute)attributes[0]).Description;

            return name;
        }
    }
}