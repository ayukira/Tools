using System;
using System.ComponentModel;
using System.Reflection;

namespace Tools.Extensions
{
    /// <summary>
    /// DescriptionExtensions
    /// </summary>
    public static class DescriptionExtensions
    {
        /// <summary>
        /// DescriptionAttr
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static DescriptionAttribute DescriptionAttr(this PropertyInfo propertyInfo)
        {
            DescriptionAttribute[] attrs =
                propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];
            return attrs.Length > 0 ? attrs[0] : new DescriptionAttribute();
        }
        /// <summary>
        /// DescriptionAttr
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static DescriptionAttribute DescriptionAttr(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DescriptionAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            return attrs.Length > 0 ? attrs[0] : new DescriptionAttribute();
        }
        /// <summary>
        /// Description
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string Description(this PropertyInfo propertyInfo)
        {
            return propertyInfo.DescriptionAttr().Description;
        }
        /// <summary>
        /// Description
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string Description(this Enum enumValue)
        {
            return enumValue.DescriptionAttr().Description;
        }
    }
}
