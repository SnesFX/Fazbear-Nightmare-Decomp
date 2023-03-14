using UnityEngine;

[NodePath("Actions/Math/Float")]
[NodeToolTip("Perform various floating-point functions.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Trigonometry")]
[FriendlyName("Floating Point Functions", "Perform various floating-point functions.")]
public class uScriptAct_FloatFunctions : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Abs(float input, out float output)
	{
		output = Mathf.Abs(input);
	}

	public void Ceiling(float input, out float output)
	{
		output = Mathf.Ceil(input);
	}

	public void Floor(float input, out float output)
	{
		output = Mathf.Floor(input);
	}

	public void Round(float input, out float output)
	{
		output = Mathf.Round(input);
	}

	public void Sign(float input, out float output)
	{
		output = Mathf.Sign(input);
	}

	public void Sqrt([FriendlyName("Input", "The input value to the function.")] float input, [FriendlyName("Output", "The output value of the function.")] out float output)
	{
		output = Mathf.Sqrt(input);
	}
}
