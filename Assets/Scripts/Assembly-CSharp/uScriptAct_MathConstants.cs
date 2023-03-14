using System;

[FriendlyName("Get Math Constants", "Get various math constants.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#MathConstants")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Get various math constants.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Math/Float")]
public class uScriptAct_MathConstants : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Degrees To Radians", "The constant for calculating radians from degrees.")] out float deg2Rad, [FriendlyName("Radians To Degrees", "The constant for calculating degrees from radians.")] out float rad2Deg, [FriendlyName("Infinity", "The constant representing infinity.")] out float infinity, [FriendlyName("Negative Infinity", "The constant representing negative infinity.")] out float nInfinity, [FriendlyName("PI", "The constant PI.")] out float pi)
	{
		deg2Rad = (float)Math.PI / 180f;
		infinity = float.PositiveInfinity;
		nInfinity = float.NegativeInfinity;
		pi = (float)Math.PI;
		rad2Deg = 57.29578f;
	}
}
