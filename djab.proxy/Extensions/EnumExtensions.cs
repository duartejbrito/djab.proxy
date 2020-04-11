using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace djab.proxy.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(
                value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Single(x => x.GetValue(null).Equals(value)),
                typeof(DescriptionAttribute)))?.Description ?? value.ToString();
        }
    }

    public class Enum<T> where T : struct, IConvertible
    {
        public static T TryParseByDescription(string value)
        {
            T toReturn = default;
            if (!typeof(T).IsEnum)
                throw new ArgumentException("T must be an enumerated type");

            var enumValues = Enum.GetValues(typeof(T));
            foreach (var item in enumValues)
            {
                Enum enumValue = (Enum)Enum.Parse(typeof(T), item.ToString());
                string description = enumValue.GetDescription();

                if (description == value)
                {
                    toReturn = (T)item;
                }
            }

            return toReturn;
        }
    }
}
