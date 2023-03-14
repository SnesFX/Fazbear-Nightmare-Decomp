using System;
using UnityEngine;

namespace RAIN.Utility
{
	public class TypeConvert
	{
		public static T ConvertValue<T>(object aValue)
		{
			if (aValue is T)
			{
				return (T)aValue;
			}
			try
			{
				if (typeof(T) == typeof(short))
				{
					return (T)(object)Convert.ToInt16(aValue);
				}
				if (typeof(T) == typeof(int))
				{
					return (T)(object)Convert.ToInt32(aValue);
				}
				if (typeof(T) == typeof(long))
				{
					return (T)(object)Convert.ToInt64(aValue);
				}
				if (typeof(T) == typeof(float))
				{
					return (T)(object)Convert.ToSingle(aValue);
				}
				if (typeof(T) == typeof(double))
				{
					return (T)(object)Convert.ToDouble(aValue);
				}
				if (typeof(T) == typeof(bool))
				{
					return (T)(object)Convert.ToBoolean(aValue);
				}
				if (typeof(T) == typeof(string))
				{
					return (T)(object)Convert.ToString(aValue);
				}
				if (typeof(T) == typeof(Vector2))
				{
					if (aValue is Vector2)
					{
						return (T)aValue;
					}
					if (aValue is Vector3)
					{
						Vector3 vector = (Vector3)aValue;
						return (T)(object)(Vector2)vector;
					}
					if (aValue is Vector4)
					{
						Vector4 vector2 = (Vector4)aValue;
						return (T)(object)(Vector2)vector2;
					}
				}
				else if (typeof(T) == typeof(Vector3))
				{
					if (aValue is Vector2)
					{
						Vector2 vector3 = (Vector2)aValue;
						return (T)(object)(Vector3)vector3;
					}
					if (aValue is Vector3)
					{
						return (T)aValue;
					}
					if (aValue is Vector4)
					{
						Vector4 vector4 = (Vector4)aValue;
						return (T)(object)(Vector3)vector4;
					}
				}
				else if (typeof(T) == typeof(Vector4))
				{
					if (aValue is Vector2)
					{
						Vector2 vector5 = (Vector2)aValue;
						return (T)(object)(Vector4)vector5;
					}
					if (aValue is Vector3)
					{
						Vector3 vector6 = (Vector3)aValue;
						return (T)(object)(Vector4)vector6;
					}
					if (aValue is Vector4)
					{
						return (T)aValue;
					}
				}
				else
				{
					if (typeof(T) == typeof(Color))
					{
						if (aValue is Color32)
						{
							Color32 color = (Color32)aValue;
							return (T)(object)(Color)color;
						}
						return (T)aValue;
					}
					if (typeof(T) == typeof(Color32))
					{
						if (aValue is Color)
						{
							Color color2 = (Color)aValue;
							return (T)(object)(Color32)color2;
						}
						return (T)aValue;
					}
					if (typeof(T) == typeof(GameObject))
					{
						return (T)aValue;
					}
					if (typeof(T) == typeof(object))
					{
						return (T)aValue;
					}
				}
			}
			catch (Exception)
			{
			}
			return default(T);
		}
	}
}
