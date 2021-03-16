using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Tools.Extensions
{
    /// <summary>
    /// DisplayExtensions
    /// </summary>
    public static class DisplayExtensions
    {
        /// <summary>
        /// 获取Display
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static DisplayAttribute Display(this PropertyInfo propertyInfo)
        {
            DisplayAttribute[] attrs =
                propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];
            return attrs.Length > 0 ? attrs[0] : new DisplayAttribute();
        }
        /// <summary>
        /// 获取Display
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static DisplayAttribute Display(this Enum enumValue)
        {
            FieldInfo fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
            DisplayAttribute[] attrs =
                fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            return attrs.Length > 0 ? attrs[0] : new DisplayAttribute();
        }
        /// <summary>
        /// 获取DisplayName
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string DisplayName(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Display().Name;
        }
        /// <summary>
        /// 获取DisplayName
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string DisplayName(this Enum enumValue)
        {
            return enumValue.Display().Name;
        }
        /// <summary>
        /// 获取DisplayDescription
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string DisplayDescription(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Display().Description;
        }
        /// <summary>
        /// 获取DisplayDescription
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string DisplayDescription(this Enum enumValue)
        {
            return enumValue.Display().Description;
        }
        /// <summary>
        /// 获取DisplayGroupName
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string DisplayGroupName(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Display().GroupName;
        }
        /// <summary>
        /// 获取DisplayGroupName
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string DisplayGroupName(this Enum enumValue)
        {
            return enumValue.Display().GroupName;
        }
        /// <summary>
        /// 获取DisplayShortName
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static string DisplayShortName(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Display().ShortName;
        }
        /// <summary>
        /// 获取DisplayShortName
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static string DisplayShortName(this Enum enumValue)
        {
            return enumValue.Display().ShortName;
        }
        /// <summary>
        /// 获取DisplayOrder
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public static int DisplayOrder(this PropertyInfo propertyInfo)
        {
            return propertyInfo.Display().Order;
        }
        /// <summary>
        /// 获取DisplayOrder
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public static int DisplayOrder(this Enum enumValue)
        {
            return enumValue.Display().Order;
        }
    }
}
