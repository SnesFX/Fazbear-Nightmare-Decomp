using UnityEngine;

[FriendlyName("Exponent & Logarithmic Functions", "Perform various floating-point exponent and logarithmic functions.")]
[NodePath("Actions/Math/Float")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#ExponentLogarithm")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Perform various floating-point exponent and logarithmic functions.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_ExponentLogarithm : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Exp(float input, float Power, out float output)
	{
		output = Mathf.Exp(input);
	}

	public void Log(float input, float Power, out float output)
	{
		output = Mathf.Log(input);
	}

	public void Log10(float input, float Power, out float output)
	{
		output = Mathf.Log10(input);
	}

	public void Pow([FriendlyName("Input", "The input value to the function.")] float input, [FriendlyName("Power", "The power to use for the Pow function. (only used with the Pow function)")] float Power, [FriendlyName("Output", "The output value of the function.")] out float output)
	{
		output = Mathf.Pow(input, Power);
	}
}
