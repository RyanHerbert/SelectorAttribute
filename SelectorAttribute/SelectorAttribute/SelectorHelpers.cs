using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SelectorAttribute
{
    public static class SelectorHelpers
    {
        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable data)
        {
            return new SelectList(data);
        }

        public static IEnumerable<SelectListItem> ToSelectList(this IEnumerable data, string dataValueField, string dataTextField)
        {
            return new SelectList(data, dataValueField, dataTextField);
        }

        public static IEnumerable<SelectListItem> ToSelectList<T>(this IEnumerable<T> data, Expression<Func<T, object>> dataValueFieldSelector, Expression<Func<T, string>> dataTextFieldSelector)
        {
            var dataValueField = dataValueFieldSelector.ToPropertyInfo().Name;
            var dataTextField = dataTextFieldSelector.ToPropertyInfo().Name;
            return ToSelectList(data, dataValueField, dataTextField);
        }

        public static PropertyInfo ToPropertyInfo(this LambdaExpression expression)
        {
            MemberExpression memberExpression;

            var unaryExpression = expression.Body as UnaryExpression;
            if (unaryExpression != null)
            {
                memberExpression = unaryExpression.Operand as MemberExpression;
            }
            else
            {
                memberExpression = expression.Body as MemberExpression;
            }

            return (PropertyInfo)memberExpression.Member;
        }

        public static string SplitCamelCaseWords(this string camelCaseWord)
        {
            // if the word is all upper, just return it 
            if (!Regex.IsMatch(camelCaseWord, "[a-z]"))
                return camelCaseWord;

            return string.Join(" ", Regex.Split(camelCaseWord, @"(?<!^)(?=[A-Z])"));
        }

        public static IEnumerable<SelectListItem> CreateItemsFromEnum<T>(T selectedValue = default(T)) where T : struct
        {
            return from name in Enum.GetNames(typeof(T))
                   let enumValue = Enum.Parse(typeof(T), name, true)
                   let enumValueAsText = Convert.ToString(enumValue)
                   select new SelectListItem
                   {
                       Text = GetLabel((Enum)enumValue),
                       Value = enumValueAsText,
                       Selected = enumValue.Equals(selectedValue)
                   };
        }

        private static string GetLabel(Enum value)
        {
            if (value == null) return null;

            var enumType = value.GetType();

            var enumString = Enum.GetName(enumType, value);

            var attributes = (DescriptionAttribute[])enumType.GetField(enumString).GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 ? attributes[0].Description : SplitCamelCaseWords(enumString);
        }
    }
}
