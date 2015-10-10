using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace JinTong.Jyrj.Common
{
    /// <summary>
    /// 枚举帮助类
    /// </summary>
    public class EnumHelper
    {
    //    public static string GetDescription(this Enum value)
    //    {
    //        var fieldInfo = value.GetType().GetField(value.ToString());
    //        var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(
    //                                                     typeof(DescriptionAttribute),
    //                                                     false);
    //        return attributes.Length > 0
    //                   ? attributes[0].Description
    //                   : null;
    //    }

    //    /// <summary>
    //    /// Gets the enum value by description.
    //    /// </summary>
    //    /// <typeparam name="EnumType">The enum type.</typeparam>
    //    /// <param name="description">The description.</param>
    //    /// <returns>The enum value.</returns>
    //    public static EnumType GetValueByDescription<EnumType>(string description)
    //    {
    //        var type = typeof(EnumType);
    //        if (!type.IsEnum)
    //            throw new ArgumentException("This method is destinated for enum types only.");
    //        foreach (var enumName in Enum.GetNames(type))
    //        {
    //            var enumValue = Enum.Parse(type, enumName);
    //            if (description == ((Enum)enumValue).GetDescription())
    //                return (EnumType)enumValue;
    //        }
    //        throw new ArgumentException("There is no value with this description among specified enum type values.");
    //    }

    }
}
