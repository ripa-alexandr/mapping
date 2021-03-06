﻿using System;
using System.Reflection;

namespace Mapping.Core.Extensions
{
	internal static class MemberInfoExtensions
	{
		internal static object GetValue (this MemberInfo memberInfo, object obj)
		{
			switch (memberInfo.MemberType)
			{
				case MemberTypes.Field:
					return ((FieldInfo)memberInfo).GetValue(obj);

				case MemberTypes.Property:
					return ((PropertyInfo)memberInfo).GetValue(obj);

				default:
					throw new NotImplementedException();
			}
		}

		internal static void SetValue (this MemberInfo memberInfo, object obj, object value)
		{
			switch (memberInfo.MemberType)
			{
				case MemberTypes.Field:
					((FieldInfo)memberInfo).SetValue(obj, value);
					break;

				case MemberTypes.Property:
					((PropertyInfo)memberInfo).SetValue(obj, value);
					break;

				default:
					throw new NotImplementedException();
			}
		}

		internal static Type GetValueType (this MemberInfo memberInfo)
		{
			switch (memberInfo.MemberType)
			{
				case MemberTypes.Field:
					return ((FieldInfo)memberInfo).FieldType;

				case MemberTypes.Property:
					return  ((PropertyInfo)memberInfo).PropertyType;

				default:
					throw new NotImplementedException();
			}
		}
	}
}
