using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Filter_Vector")]
[NodeToolTip("Takes any Vector3 or Vector4 and outputs a low-pass filtered version.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Filter Vector", "Takes any Vector3 or Vector4 and outputs a low-pass filtered version.")]
[NodePath("Actions/Math/Vectors")]
public class uScriptAct_FilterVector : uScriptLogic
{
	private Vector4 m_PreviousValue = Vector4.zero;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void Reset(object Target, float FilterConstant, out Vector3 Value3, out Vector4 Value4)
	{
		if (typeof(Vector3) == Target.GetType())
		{
			Value3 = (m_PreviousValue = (Value4 = (Vector3)Target));
			return;
		}
		if (typeof(Vector4) == Target.GetType())
		{
			Value3 = (m_PreviousValue = (Value4 = (Vector4)Target));
			return;
		}
		Value3 = Vector3.zero;
		Value4 = Vector4.zero;
		uScriptDebug.Log("Invalid Target for Filter Vector node Reset() - must be a Vector3 or Vector4", uScriptDebug.Type.Warning);
	}

	public void Filter([FriendlyName("Target", "Value to filter.")] object Target, [DefaultValue(0.1f)][FriendlyName("Filter Constant", "The strength of the filter (lower numbers mean more filtering, i.e. slower - default value = 0.1).")] float FilterConstant, [FriendlyName("Value3", "Filtered value.")] out Vector3 Value3, [FriendlyName("Value4", "Filtered value.")] out Vector4 Value4)
	{
		if (typeof(Vector3) == Target.GetType() || typeof(Vector4) == Target.GetType())
		{
			Vector4 vector = ((typeof(Vector3) != Target.GetType()) ? ((Vector4)Target) : ((Vector4)(Vector3)Target));
			Value3 = (m_PreviousValue = (Value4 = vector * FilterConstant + m_PreviousValue * (1f - FilterConstant)));
			if (m_PreviousValue - vector == Vector4.zero)
			{
				Value3 = (m_PreviousValue = (Value4 = vector));
			}
		}
		else
		{
			Value3 = Vector3.zero;
			Value4 = Vector4.zero;
			uScriptDebug.Log("Invalid Target for Filter Vector node Filter() - must be a Vector3 or Vector4", uScriptDebug.Type.Warning);
		}
	}
}
