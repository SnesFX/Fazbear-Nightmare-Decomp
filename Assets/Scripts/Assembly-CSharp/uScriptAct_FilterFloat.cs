using UnityEngine;

[FriendlyName("Filter Float", "Takes any float and outputs a low-pass filtered version.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Takes any float and outputs a low-pass filtered version.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Filter_Float")]
[NodePath("Actions/Math/Float")]
public class uScriptAct_FilterFloat : uScriptLogic
{
	private float m_PreviousValue;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Reset(float Target, float FilterConstant, out float Value)
	{
		m_PreviousValue = (Value = Target);
	}

	public void Filter([FriendlyName("Target", "Value to filter.")] float Target, [DefaultValue(0.1f)][FriendlyName("Filter Constant", "The strength of the filter (lower numbers mean more filtering, i.e. slower - default value = 0.1).")] float FilterConstant, [FriendlyName("Value", "Filtered value.")] out float Value)
	{
		m_PreviousValue = (Value = Target * FilterConstant + m_PreviousValue * (1f - FilterConstant));
		if ((double)Mathf.Abs(m_PreviousValue - Target) < 0.001)
		{
			m_PreviousValue = (Value = Target);
		}
	}
}
