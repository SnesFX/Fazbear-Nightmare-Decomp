using UnityEngine;

[FriendlyName("Control GameObject (Move)", "Moves a GameObject in the specified direction (local to the GameObject). Please note that this is a simple move node that brute-forces the movement of the GameObject's position-- it does not use the physics system. It is recomended you create your own game-specific character controller if you need more functionality.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/GameObjects/Movement")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Moves a GameObject in the specified direction.")]
public class uScriptAct_ControlGameObjectMove : uScriptLogic
{
	public enum Direction
	{
		Forward = 0,
		Backward = 1,
		Left = 2,
		Right = 3,
		Up = 4,
		Down = 5
	}

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("Target", "The Target GameObject to be moved.")] GameObject Target, [FriendlyName("Direction", "The direction to move the target.")][SocketState(false, false)] Direction moveDirection, [FriendlyName("Speed", "The speed you wish to move the target per tick. This uses a relativly small value for most cases.")][DefaultValue(0.01f)] float Speed, [FriendlyName("Use Local", "Move the GameObject in local coordinates. Not used if the GameObject is using a component called CharacterController.")][SocketState(false, false)][DefaultValue(false)] bool useLocal)
	{
		if (null != Target && Speed != 0f)
		{
			Vector3 vector = Vector3.zero;
			switch (moveDirection)
			{
			case Direction.Forward:
				vector = Vector3.forward * Speed;
				break;
			case Direction.Backward:
				vector = Vector3.back * Speed;
				break;
			case Direction.Left:
				vector = Vector3.left * Speed;
				break;
			case Direction.Right:
				vector = Vector3.right * Speed;
				break;
			case Direction.Up:
				vector = Vector3.up * Speed;
				break;
			case Direction.Down:
				vector = Vector3.down * Speed;
				break;
			}
			CharacterController component = Target.GetComponent<CharacterController>();
			if (null != component)
			{
				component.Move(vector);
			}
			else if (useLocal)
			{
				Target.transform.localPosition += vector;
			}
			else
			{
				Target.transform.position += vector;
			}
		}
	}
}
