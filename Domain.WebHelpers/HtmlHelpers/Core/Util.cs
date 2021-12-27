using Domain.Infrastructure.Utilities.ExtensionMethods;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

namespace Domain.WebHelpers.HtmlHelpers.Core
{
    internal static class Util
    {
        internal static object GetPropertyValue(this object obj, string propertyName)
        {
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {
                
                var property = propertyValue.GetType().GetProperty(item).GetValue(propertyValue);
                propertyValue = property;
            }
            
            //if (property == null) throw new ArgumentNullException(String.Format("{0} geçersiz", propertyName));
            if (propertyValue == null)
                return "";
            else
                return propertyValue.ToString();// obj.GetValue(obj, null);
        }

        internal static object GetPropertyValueEx(this object obj, string propertyName, string nullDisplayText,string formatText)
        {

            /********************************************************************/
            Type type=null;
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {
                var property = propertyValue.GetType().GetProperty(item);
                propertyValue = property.GetValue(propertyValue);
                type = property.PropertyType;
            }

            var formattedModelValue = propertyValue;
            if (obj == null || (type==typeof(DateTime) && (DateTime) propertyValue==DateTime.MinValue))
            {
                if (!nullDisplayText.IsBlank())
                    formattedModelValue = nullDisplayText;
                else
                    formattedModelValue = "";
            }
            else if (!string.IsNullOrEmpty(formatText))
                {
                    formattedModelValue = string.Format(System.Globalization.CultureInfo.CurrentCulture, formatText, formattedModelValue);
                }
            
            return formattedModelValue;
            /********************************************************************/
            /*
            var columns = propertyName.Split('.');
            var propertyValue = obj;
            foreach (var item in columns)
            {

                var property = propertyValue.GetType().GetProperty(item).GetValue(propertyValue);
                propertyValue = property;
            }
            
            //if (property == null) throw new ArgumentNullException(String.Format("{0} geçersiz", propertyName));
            if (propertyValue == null)
                return "";
            else
                return propertyValue.ToString();// obj.GetValue(obj, null);
                */
        }

        internal static string GetHtmlAttributes2(object htmlAttributes)
        {
            string ret = string.Empty;

            if (htmlAttributes != null)
            {
                var attributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                foreach (var item in attributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }

        internal static string GetHtmlAttributes(IDictionary<string,object> htmlAttributes)
        {
            string ret = string.Empty;

            if (htmlAttributes != null)
            {
                
                foreach (var item in htmlAttributes)
                {
                    ret += " " + item.Key + "=" + "'" + item.Value + "'";
                }
            }

            return ret;
        }

        public static IDictionary<string, object> MergeHtmlAttributes(object htmlAttributesObject, object defaultHtmlAttributesObject)
        {
            var concatKeys = new string[] { "class" };

            var htmlAttributesDict = htmlAttributesObject as IDictionary<string, object>;
            var defaultHtmlAttributesDict = defaultHtmlAttributesObject as IDictionary<string, object>;

            RouteValueDictionary htmlAttributes = (htmlAttributesDict != null)
                ? new RouteValueDictionary(htmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributesObject);
            RouteValueDictionary defaultHtmlAttributes = (defaultHtmlAttributesDict != null)
                ? new RouteValueDictionary(defaultHtmlAttributesDict)
                : HtmlHelper.AnonymousObjectToHtmlAttributes(defaultHtmlAttributesObject);

            foreach (var item in htmlAttributes)
            {
                if (concatKeys.Contains(item.Key))
                {
                    defaultHtmlAttributes[item.Key] = (defaultHtmlAttributes[item.Key] != null)
                        ? $"{defaultHtmlAttributes[item.Key]} {item.Value}"
                        : item.Value;
                }
                else
                {
                    defaultHtmlAttributes[item.Key] = item.Value;
                }
            }

            return defaultHtmlAttributes;
        }
        
        internal static string GetDisplayName<T>(PropertyInfo p)
        {
            System.Type type = typeof(T);
            DisplayAttribute attr;
            attr = (DisplayAttribute)type.GetProperty(p.Name).GetCustomAttributes(typeof(DisplayAttribute), true).SingleOrDefault();

            if (attr == null)
            {
                MetadataTypeAttribute metadataType = (MetadataTypeAttribute)type.GetCustomAttributes(typeof(MetadataTypeAttribute), true).FirstOrDefault();
                if (metadataType != null)
                {
                    var property = metadataType.MetadataClassType.GetProperty(p.Name);
                    if (property != null)
                    {
                        attr = (DisplayAttribute)(property.GetCustomAttributes().Where(c => c.GetType().FullName.Contains("DisplayAttribute")).FirstOrDefault() as DisplayAttribute);
                    }
                }
            }

            if (attr != null)
            {
                return attr.GetName();
            }
            else
            {
                return p.Name;
            }
        }

        internal static PropertyInfo GetPropertyInfo<TSource, TProperty>(Expression<Func<TSource, TProperty>> propertyLambda)
        {
            System.Type type = typeof(TSource);
            LambdaExpression lambda = propertyLambda as LambdaExpression;
            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr =((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            /*
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' alanı özellik değil.",propertyLambda.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' özelliği {1} tipinde değil.", propertyLambda.ToString(), type));
            }*/

            return propInfo;
        }

        internal static PropertyInfo GetPropertyInfo<TSource>(Expression<Func<TSource, object>> propertyLambda)
        {
            System.Type type = typeof(TSource);
            LambdaExpression lambda = propertyLambda as LambdaExpression;
            MemberExpression memberExpr = null;

            if (lambda.Body.NodeType == ExpressionType.Convert)
            {
                memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
            }
            else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
            {
                memberExpr = lambda.Body as MemberExpression;
            }

            if (memberExpr == null)
            {
                throw new ArgumentException("method");
            }

            PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
            /*
            if (propInfo == null)
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' alanı özellik değil.",propertyLambda.ToString()));
            }

            if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
            {
                throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' özelliği {1} tipinde değil.", propertyLambda.ToString(), type));
            }*/

            return propInfo;
        }

        //internal static PropertyInfo GetPropertyInfo<TSource,TEnum>(Expression<Func<TSource, TEnum>> propertyLambda)
        //{
        //    System.Type type = typeof(TSource);
        //    LambdaExpression lambda = propertyLambda as LambdaExpression;
        //    MemberExpression memberExpr = null;

        //    if (lambda.Body.NodeType == ExpressionType.Convert)
        //    {
        //        memberExpr = ((UnaryExpression)lambda.Body).Operand as MemberExpression;
        //    }
        //    else if (lambda.Body.NodeType == ExpressionType.MemberAccess)
        //    {
        //        memberExpr = lambda.Body as MemberExpression;
        //    }

        //    if (memberExpr == null)
        //    {
        //        throw new ArgumentException("method");
        //    }

        //    PropertyInfo propInfo = memberExpr.Member as PropertyInfo;
        //    /*
        //    if (propInfo == null)
        //    {
        //        throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' alanı özellik değil.",propertyLambda.ToString()));
        //    }

        //    if (type != propInfo.ReflectedType && !type.IsSubclassOf(propInfo.ReflectedType))
        //    {
        //        throw new ArgumentException(string.Format("Lambda ifadesindeki '{0}' özelliği {1} tipinde değil.", propertyLambda.ToString(), type));
        //    }*/

        //    return propInfo;
        //}

        internal static string GetEnumDescription<TEnum>(TEnum value)
        {
            var field = value.GetType().GetField(value.ToString());
            var attributes = (DescriptionAttribute[])field.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }


        internal  static Type GetNonNullableModelType(ModelMetadata modelMetadata)
        {
            Type modelType = modelMetadata.ModelType;

            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            return modelType;
        }

        internal static Type GetNonNullableModelType(Type modelType)
        {
            Type underlyingType = Nullable.GetUnderlyingType(modelType);
            if (underlyingType != null)
            {
                modelType = underlyingType;
            }
            return modelType;
        }

        internal static bool IsNullable<T>(T obj)
        {
            if (obj == null) return true; // obvious
            Type type = typeof(T);
            if (!type.IsValueType) return true; // ref-type
            if (Nullable.GetUnderlyingType(type) != null) return true; // Nullable<T>
            return false; // value-type
        }

        internal static bool IsNullable(Type modelType)
        {
            bool result = false;
            if (Nullable.GetUnderlyingType(modelType) != null) { result = true; }
            return result; 
        }
        
        public static IDictionary<string, object> SetTooltipAttributes(this IDictionary<string, object> attributes, string tooltip)
        {
            if (!tooltip.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { title = tooltip });

            return attributes;
        }
        public static IDictionary<string, object> SetBindPropAttributes(this IDictionary<string, object> attributes, string bindProp)
        {
            if (!bindProp.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { data_bindprop = bindProp });

            return attributes;
        }
        public static IDictionary<string, object> SetRequiredAttributes(this IDictionary<string, object> attributes, string requiredSource, string requiredCondition)
        {
            if (!requiredSource.IsBlank())
            {
                attributes = Util.MergeHtmlAttributes(attributes, new
                {
                    data_custom_val_required = "true",
                    data_custom_val_required_source = requiredSource,
                    data_custom_val_required_condition = requiredCondition
                });
            }

            return attributes;
        }

        public static IDictionary<string, object> SetRequiredExpressionAttributes(this IDictionary<string, object> attributes, string requiredSource, string requiredExpression)
        {
            if (!requiredSource.IsBlank())
            {
                attributes = Util.MergeHtmlAttributes(attributes, new
                {
                    data_custom_val_required = "true",
                    data_custom_val_required_source_exp = requiredSource,
                    data_custom_val_required_expression = requiredExpression
                });
            }

            return attributes;
        }

        public static IDictionary<string, object> SetDisplayAttributes(this IDictionary<string, object> attributes, bool isParentDiv, string displaySource, string displayCondition)
        {
            if (!isParentDiv && !displaySource.IsBlank())
            {
                attributes = Util.MergeHtmlAttributes(attributes, new { data_display_source = displaySource, data_display_condition = displayCondition });
            }

            return attributes;
        }

        public static IDictionary<string, object> SetDisplayExpressionAttributes(this IDictionary<string, object> attributes, bool isParentDiv, string displaySource, string displayExpression)
        {
            if (!isParentDiv && !displaySource.IsBlank())
            {
                attributes = Util.MergeHtmlAttributes(attributes, new { data_display_source_exp = displaySource, data_display_expression = displayExpression });
            }

            return attributes;
        }

        public static IDictionary<string, object> SetDisableAttributes(this IDictionary<string, object> attributes, bool isDisabled)
        {
            if (isDisabled)
                attributes = Util.MergeHtmlAttributes(attributes, new { disabled = "disabled" });

            return attributes;
        }
        public static IDictionary<string, object> SetReadOnlyAttributes(this IDictionary<string, object> attributes, bool isReadOnly)
        {
            if (isReadOnly)
                attributes = Util.MergeHtmlAttributes(attributes, new { @readonly = "readonly" });

            return attributes;
        }
        public static IDictionary<string, object> SetPlaceHolderAttributes(this IDictionary<string, object> attributes, string placeholder)
        {
            if (!placeholder.IsBlank())
                attributes = Util.MergeHtmlAttributes(attributes, new { placeholder = placeholder });

            return attributes;
        }
    }

    /*
    public static class EnumHelper
    {
        static string GetName(Type enumType, string name)
        {
            var result = name;

            var attribute = enumType
                .GetField(name)
                .GetCustomAttributes(inherit: false)
                .OfType<DisplayAttribute>()
                .FirstOrDefault();

            if (attribute != null)
            {
                result = attribute.GetName();
            }

            return result;
        }

        public static IEnumerable<SelectListItem> GetItems(this Type enumType, int? selectedValue)
        {
            if (!typeof(Enum).IsAssignableFrom(enumType))
            {
                throw new ArgumentException("Type must be an enum!");
            }

            var names = Enum.GetNames(enumType);
            var values = Enum.GetValues(enumType).Cast<int>();

            var items = names.Zip(values, (name, value) =>
                new SelectListItem
                {
                    Text = GetName(enumType, name),
                    Value = value.ToString(),
                    Selected = value == selectedValue
                });

            return items;
        }

    

    }*/
}
