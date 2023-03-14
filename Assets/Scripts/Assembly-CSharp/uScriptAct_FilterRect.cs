using UnityEngine;

[NodePath("Actions/Math/Rect")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Takes any Rect and outputs a low-pass filtered version.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Filter_Rect")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Filter Rect", "Takes any Rect and outputs a low-pass filtered version.")]
public class uScriptAct_FilterRect : uScriptLogic
{
	private Rect m_PreviousValue = new Rect(0f, 0f, 0f, 0f);

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Reset(Rect Target, float FilterConstant, out Rect Value)
	{
		Value = (m_PreviousValue = Target);
	}

	public void Filter([FriendlyName("Target", "Value to filter.")] Rect Target, [DefaultValue(0.1f)][FriendlyName("Filter Constant", "The strength of the filter (lower numbers mean more filtering, i.e. slower - default value = 0.1).")] float FilterConstant, [FriendlyName("Value", "Filtered value.")] out Rect Value)
	{
		float num = Target.x * FilterConstant + m_PreviousValue.x * (1f - FilterConstant);
		m_PreviousValue.x = num;
		float left = num;
		num = Target.y * FilterConstant + m_PreviousValue.y * (1f - FilterConstant);
		m_PreviousValue.y = num;
		float top = num;
		num = Target.width * FilterConstant + m_PreviousValue.width * (1f - FilterConstant);
		m_PreviousValue.width = num;
		float width = num;
		num = Target.height * FilterConstant + m_PreviousValue.height * (1f - FilterConstant);
		m_PreviousValue.height = num;
		float height = num;
		Value = new Rect(left, top, width, height);
		if ((double)Mathf.Abs(m_PreviousValue.x - Target.x) < 0.001 && (double)Mathf.Abs(m_PreviousValue.y - Target.y) < 0.001 && (double)Mathf.Abs(m_PreviousValue.width - Target.width) < 0.001 && (double)Mathf.Abs(m_PreviousValue.height - Target.height) < 0.001)
		{
			m_PreviousValue = (Value = Target);
		}
	}
}
