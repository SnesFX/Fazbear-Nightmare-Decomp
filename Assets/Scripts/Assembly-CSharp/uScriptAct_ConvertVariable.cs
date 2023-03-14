using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Convert_Variable")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Conversions")]
[NodeToolTip("Converts a variable into another type.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Convert Variable", "Converts a variable into another type.")]
public class uScriptAct_ConvertVariable : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target variable to be converted.")] object Target, [FriendlyName("Int Value", "The Target variable represented as an integer.")] out int IntValue, [FriendlyName("Int 64 Value", "The Target variable represented as a System.Int64.")] out long Int64Value, [FriendlyName("Float Value", "The Target variable represented as a floating-point value.")] out float FloatValue, [FriendlyName("String Value", "The Target variable represented as a string.")] out string StringValue, [FriendlyName("Boolean Value", "The Target variable represented as a Boolean (true/false) value.")] out bool BooleanValue, [FriendlyName("Vector3 Value", "The Target variable represented as a Vector3 value.")] out Vector3 Vector3Value)
	{
		int num = 0;
		long num2 = 0L;
		float num3 = 0f;
		string text = Target.ToString();
		bool flag = false;
		Vector3 vector = Vector3.zero;
		if (typeof(GameObject) == Target.GetType())
		{
			GameObject gameObject = (GameObject)Target;
			if (gameObject != null)
			{
				num = 1;
				num2 = 1L;
				num3 = 1f;
				flag = true;
				text = gameObject.name;
			}
			else
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = "null";
			}
		}
		else if (Target is Enum)
		{
			num = (int)Target;
			num2 = (long)Target;
			num3 = num;
			flag = ((num != 0) ? true : false);
			text = Target.ToString();
		}
		else if (typeof(Vector2) == Target.GetType())
		{
			Vector3 vector2 = (Vector2)Target;
			if (vector2.ToString() == "(0.0, 0.0)")
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = vector2.ToString();
			}
			else
			{
				num = 1;
				num2 = 1L;
				num3 = 1f;
				flag = true;
				text = vector2.ToString();
			}
		}
		else if (typeof(Vector3) == Target.GetType())
		{
			Vector3 vector3 = (Vector3)Target;
			vector = vector3;
			if (vector3.ToString() == "(0.0, 0.0, 0.0)")
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = vector3.ToString();
			}
			else
			{
				num = 1;
				num2 = 1L;
				num3 = 1f;
				flag = true;
				text = vector3.ToString();
			}
		}
		else if (typeof(Vector4) == Target.GetType())
		{
			Vector4 vector4 = (Vector4)Target;
			if (vector4.ToString() == "(0.0, 0.0, 0.0, 0.0)")
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = vector4.ToString();
			}
			else
			{
				num = 1;
				num2 = 1L;
				num3 = 1f;
				flag = true;
				text = vector4.ToString();
			}
		}
		else if (typeof(string) == Target.GetType())
		{
			string text2 = (string)Target;
			if (text2 == string.Empty)
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = text2;
			}
			else
			{
				string[] array = text2.Split(',');
				if (array.Length >= 3)
				{
					float.TryParse(array[0], out vector.x);
					float.TryParse(array[1], out vector.y);
					float.TryParse(array[2], out vector.z);
				}
				int result = 1;
				num = ((!int.TryParse(text2, out result)) ? 1 : result);
				long result2 = 1L;
				num2 = ((!long.TryParse(text2, out result2)) ? 1 : result2);
				float result3 = 1f;
				num3 = ((!float.TryParse(text2, out result3)) ? 1f : result3);
				flag = true;
				text = text2;
			}
		}
		else if (typeof(TextAsset) == Target.GetType())
		{
			TextAsset textAsset = (TextAsset)Target;
			string text3 = textAsset.text;
			if (text3 == string.Empty)
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = text3;
			}
			else
			{
				int result4 = 1;
				num = ((!int.TryParse(text3, out result4)) ? 1 : result4);
				long result5 = 1L;
				num2 = ((!long.TryParse(text3, out result5)) ? 1 : result5);
				float result6 = 1f;
				num3 = ((!float.TryParse(text3, out result6)) ? 1f : result6);
				flag = true;
				text = text3;
			}
		}
		else if (typeof(int) == Target.GetType())
		{
			int num4 = (int)Target;
			if (num4 == 0)
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = num4.ToString();
			}
			else
			{
				num = num4;
				num2 = Convert.ToInt64(num4);
				num3 = Convert.ToSingle(num4);
				flag = true;
				text = num4.ToString();
			}
		}
		else if (typeof(long) == Target.GetType())
		{
			long num5 = (long)Target;
			if (num5 == 0L)
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = num5.ToString();
			}
			else
			{
				num = Convert.ToInt32(num5);
				num2 = num5;
				num3 = Convert.ToSingle(num5);
				flag = true;
				text = num5.ToString();
			}
		}
		else if (typeof(float) == Target.GetType())
		{
			float num6 = (float)Target;
			if (num6 == 0f)
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = num6.ToString();
			}
			else
			{
				num = Convert.ToInt32(num6);
				num2 = Convert.ToInt64(num6);
				num3 = num6;
				flag = true;
				text = num6.ToString();
			}
		}
		else if (typeof(bool) == Target.GetType())
		{
			if ((bool)Target)
			{
				num = 1;
				num2 = 1L;
				num3 = 1f;
				flag = true;
				text = "True";
			}
			else
			{
				num = 0;
				num2 = 0L;
				num3 = 0f;
				flag = false;
				text = "False";
			}
		}
		IntValue = num;
		Int64Value = num2;
		FloatValue = num3;
		StringValue = text;
		BooleanValue = flag;
		Vector3Value = vector;
	}
}
