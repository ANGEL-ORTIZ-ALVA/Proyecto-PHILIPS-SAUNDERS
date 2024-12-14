using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.ComponentModel;
namespace waSysColegio.Models
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum value)
        {
            FieldInfo field = value.GetType().GetField(value.ToString());
            DescriptionAttribute attribute = field.GetCustomAttribute<DescriptionAttribute>();

            return attribute != null ? attribute.Description : value.ToString();
        }
    }
}