using System;
using RAIN.Utility;

namespace RAIN.Representation
{
	[Obsolete("Use the Expression.Evaluate<T> instead of using EvaluateValue objects")]
	public class ExpressionValue
	{
		private Type _type;

		private object _value;

		public ExpressionValue(object aValue)
		{
			if (aValue != null)
			{
				_type = aValue.GetType();
			}
			if (_type == typeof(short))
			{
				_type = typeof(int);
			}
			_value = aValue;
			if (_type != null && _type != typeof(string) && !_type.IsValueType)
			{
				_type = typeof(object);
			}
		}

		public bool IsUnassigned()
		{
			return _type == null;
		}

		public bool IsType<T>()
		{
			return _type == typeof(T);
		}

		public bool IsTypeEqual(ExpressionValue aValue)
		{
			if (_type != aValue._type && _type != typeof(object))
			{
				return aValue._type == typeof(object);
			}
			return true;
		}

		public T GetValue<T>()
		{
			if (_type == null)
			{
				if (typeof(T) == typeof(string))
				{
					return (T)(object)"";
				}
				return default(T);
			}
			return TypeConvert.ConvertValue<T>(_value);
		}
	}
}
