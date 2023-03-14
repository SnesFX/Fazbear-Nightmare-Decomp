using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Perform various floating-point trigonometric functions.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Trigonometry")]
[FriendlyName("Trigonometry", "Perform various floating-point trigonometric functions.")]
[NodePath("Actions/Math/Float")]
public class uScriptAct_Trigonometry : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Acos(float input, out float output)
	{
		output = Mathf.Acos(input);
	}

	public void Asin(float input, out float output)
	{
		output = Mathf.Asin(input);
	}

	public void Atan(float input, out float output)
	{
		output = Mathf.Atan(input);
	}

	public void Cos(float input, out float output)
	{
		output = Mathf.Cos(input);
	}

	public void Sin(float input, out float output)
	{
		output = Mathf.Sin(input);
	}

	public void Tan([FriendlyName("Input", "The input value to the trigonometric function.")] float input, [FriendlyName("Output", "The output value of the trigonometric function.")] out float output)
	{
		output = Mathf.Tan(input);
	}
}
