using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.Serialization;
using ServiceStack.Common.Extensions;

namespace ServiceStack.Common.Text
{
	public class ParseStringTypeMethod
	{
		public static Func<string, object> GetParseMethod(Type type)
		{
			if (!type.IsClass) return null;

			var propertyInfos = type.GetProperties();
			if (propertyInfos.Length == 0) return null;


			var setterMap = new Dictionary<string, Action<object, object>>();
			var map = new Dictionary<string, Func<string, object>>();

			foreach (var propertyInfo in propertyInfos)
			{
				map[propertyInfo.Name] = ParseStringMethods.GetParseMethod(propertyInfo.PropertyType);
				setterMap[propertyInfo.Name] = GetSetPropertyMethod(type, propertyInfo);
			}

			return value => StringToType(value, type, setterMap, map);
		}

		private static object StringToType(string value, Type type,
			IDictionary<string, Action<object, object>> setterMap, IDictionary<string, Func<string, object>> parseStringFnMap)
		{
			if (value[0] != TextExtensions.TypeStartChar)
				throw new SerializationException(string.Format(
					"Type definitions should start with a '{0}'", TextExtensions.TypeStartChar));

			var instance = Activator.CreateInstance(type);

			for (var i=1; i < value.Length; i++)
			{
				var propertyName = EatPropertyName(value, ref i);
				i++;
				var propertyValueString = EatPropertyValue(value, ref i);

				var parseStringFn = parseStringFnMap[propertyName];
				var propertyValue = parseStringFn(propertyValueString);
				var setterFn = setterMap[propertyName];

				setterFn(instance, propertyValue);
			}

			return instance;
		}

		private static string EatPropertyName(string value, ref int i)
		{
			var tokenStartPos = i;
			while (value[++i] != TextExtensions.PropertyNameSeperator) { }
			return value.Substring(tokenStartPos, i - tokenStartPos);
		}

		private static string EatPropertyValue(string value, ref int i)
		{
			var tokenStartPos = i;
			var valueChar = value[i];
			if (i == value.Length
				|| valueChar == TextExtensions.PropertyItemSeperator
				|| valueChar == TextExtensions.TypeEndChar)
			{
				return null;
			}

			while (++i < value.Length
				&& value[i] != TextExtensions.PropertyItemSeperator
				&& value[i] != TextExtensions.TypeEndChar) { }
			return value.Substring(tokenStartPos, i - tokenStartPos);
		}

		private static Action<object, object> GetSetPropertyMethod(Type type, PropertyInfo propertyInfo)
		{
			var setMethodInfo = propertyInfo.GetSetMethod();
			if (setMethodInfo == null) return null;
			var oInstanceParam = Expression.Parameter(typeof(object), "oInstanceParam");
			var oValueParam = Expression.Parameter(typeof(object), "oValueParam");

			var instanceParam = Expression.Convert(oInstanceParam, type);
			var useType = propertyInfo.PropertyType;

			var valueParam = Expression.Convert(oValueParam, useType);
			var exprCallPropertySetFn = Expression.Call(instanceParam, setMethodInfo, valueParam);

			var propertySetFn = Expression.Lambda<Action<object, object>>
			(
				exprCallPropertySetFn,
				oInstanceParam,
				oValueParam
			).Compile();

			return propertySetFn;
		}
	}
}