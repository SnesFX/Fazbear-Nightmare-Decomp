using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Attach_To_GameObject")]
[FriendlyName("Attach To GameObject", "Attaches one GameObject to another, setting the Target as the parent of the Attachment.")]
[NodeToolTip("Attaches one GameObject to another, setting the Target as the parent of the Attachment.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/GameObjects")]
public class uScriptAct_AttachToGameObject : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "GameObject to attach to.")] GameObject Target, [FriendlyName("Attachment", "GameObject to attach to the Target.")] GameObject Attachment, [SocketState(false, false)][FriendlyName("Use Relative Offset", "Whether or not to use the relative offset.")] bool UseRelativeOffset, [FriendlyName("Relative Offset", "The relative offset to use.")][SocketState(false, false)] Vector3 RelativeOffset, [FriendlyName("Use Relative Rotation", "Whether or not to use the relative rotation.")][SocketState(false, false)] bool UseRelativeRotation, [FriendlyName("Relative Rotation", "The relative rotation to use.")][SocketState(false, false)] Vector3 RelativeRotation)
	{
		if (Attachment != null && Target != null)
		{
			Attachment.transform.parent = Target.transform;
			if (UseRelativeOffset)
			{
				Attachment.transform.localPosition = RelativeOffset;
			}
			if (UseRelativeRotation)
			{
				Attachment.transform.Rotate(RelativeRotation, Space.Self);
			}
		}
	}
}
