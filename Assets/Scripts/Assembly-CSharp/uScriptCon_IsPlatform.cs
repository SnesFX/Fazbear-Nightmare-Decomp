using System.Collections.Generic;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Is_Platform")]
[NodeToolTip("Outputs true if the current platform is one of the attached platform variables.")]
[NodePath("Conditions/Comparison")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Is Platform", "Outputs True if the current platform is one of the attached platform variables.")]
public class uScriptCon_IsPlatform : uScriptLogic
{
	private bool m_IsPlatform;

	[FriendlyName("Is Platform")]
	public bool IsPlatform
	{
		get
		{
			return m_IsPlatform;
		}
	}

	[FriendlyName("Is Not Platform")]
	public bool IsNotPlatform
	{
		get
		{
			return !m_IsPlatform;
		}
	}

	public void In([FriendlyName("Valid Platforms", "All UnityEngine.RuntimePlaform variables attached will considerd to be valid platforms.")] RuntimePlatform[] ValidPlatforms)
	{
		List<RuntimePlatform> list = new List<RuntimePlatform>(ValidPlatforms);
		m_IsPlatform = list.Contains(Application.platform);
	}
}
