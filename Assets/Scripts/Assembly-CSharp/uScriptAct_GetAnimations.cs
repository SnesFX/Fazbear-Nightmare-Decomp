using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Get Animations", "Get the list of animation names on the target GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Get the list of animation names on the target GameObject.")]
[NodePath("Actions/Animation")]
public class uScriptAct_GetAnimations : uScriptLogic
{
	private string[] m_Animations;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The target GameObject with the animations.")] GameObject Target, [FriendlyName("Filter", "(optional) A string used to filter the returned animations to ones that contain this string.")][SocketState(false, false)] string Filter, [FriendlyName("Animations", "The list of animations as a String List variable.")] out string[] Animations)
	{
		Animation animation = (Animation)Target.GetComponent("Animation");
		int num = 0;
		if (animation != null)
		{
			if (Filter == string.Empty)
			{
				m_Animations = new string[animation.GetClipCount()];
			}
			else
			{
				foreach (AnimationState item in animation)
				{
					if (item.name.Contains(Filter))
					{
						num++;
					}
				}
				m_Animations = new string[num];
			}
			int num2 = 0;
			foreach (AnimationState item2 in animation)
			{
				if (Filter != string.Empty)
				{
					if (item2.name.Contains(Filter))
					{
						m_Animations[num2] = item2.name;
						num2++;
					}
				}
				else
				{
					m_Animations[num2] = item2.name;
					num2++;
				}
			}
			Animations = m_Animations;
		}
		else
		{
			m_Animations = new string[1];
			m_Animations[0] = string.Empty;
			Animations = m_Animations;
		}
	}
}
