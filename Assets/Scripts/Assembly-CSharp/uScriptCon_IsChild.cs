using UnityEngine;

[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Outputs True if the GameObject is a child of the Parent.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Is Child", "Outputs True if the GameObject is a child of the Parent.")]
public class uScriptCon_IsChild : uScriptLogic
{
	private bool m_IsChild;

	[FriendlyName("Is Child")]
	public bool IsChild
	{
		get
		{
			return m_IsChild;
		}
	}

	[FriendlyName("Is Not Child")]
	public bool IsNotChild
	{
		get
		{
			return !m_IsChild;
		}
	}

	public void In([FriendlyName("Child", "The GameObject you wish to see is a child of the Parent.")] GameObject Child, [FriendlyName("Parent", "The potential parent GameObject.")] GameObject Parent)
	{
		if (null != Child && null != Parent)
		{
			m_IsChild = Child.transform.IsChildOf(Parent.transform);
		}
	}
}
