using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Toggle")]
[FriendlyName("Toggle", "Toggles the active state of a GameObject.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Toggle")]
[NodeToolTip("Toggles the active state of a GameObject.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_Toggle : uScriptLogic
{
	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Turned On")]
	public event EventHandler OnOut;

	[FriendlyName("Turned Off")]
	public event EventHandler OffOut;

	[FriendlyName("Toggled")]
	public event EventHandler ToggleOut;

	[FriendlyName("Turn On")]
	public void TurnOn(GameObject[] Target, bool IgnoreChildren)
	{
		foreach (GameObject gameObject in Target)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			if (IgnoreChildren)
			{
				if (!CheckIfActive(gameObject))
				{
					SetActiveState(gameObject, true, IgnoreChildren);
				}
			}
			else if (!CheckIfActive(gameObject))
			{
				SetActiveState(gameObject, true, IgnoreChildren);
			}
		}
		if (this.OnOut != null)
		{
			this.OnOut(this, new EventArgs());
		}
	}

	[FriendlyName("Turn Off")]
	public void TurnOff(GameObject[] Target, bool IgnoreChildren)
	{
		foreach (GameObject gameObject in Target)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			if (IgnoreChildren)
			{
				if (CheckIfActive(gameObject))
				{
					SetActiveState(gameObject, false, IgnoreChildren);
				}
			}
			else if (CheckIfActive(gameObject))
			{
				SetActiveState(gameObject, false, IgnoreChildren);
			}
		}
		if (this.OffOut != null)
		{
			this.OffOut(this, new EventArgs());
		}
	}

	[FriendlyName("Toggle")]
	public void Toggle([FriendlyName("Target", "The Target GameObject(s) to toggle state on.")] GameObject[] Target, [SocketState(false, false)][FriendlyName("Ignore Children", "If True, the state change will not affect the Target's children. However, the children will still not render if their parent has been disabled.")] bool IgnoreChildren)
	{
		foreach (GameObject gameObject in Target)
		{
			if (!(gameObject != null))
			{
				continue;
			}
			if (IgnoreChildren)
			{
				if (CheckIfActive(gameObject))
				{
					SetActiveState(gameObject, false, IgnoreChildren);
				}
				else
				{
					SetActiveState(gameObject, true, IgnoreChildren);
				}
			}
			else if (CheckIfActive(gameObject))
			{
				SetActiveState(gameObject, false, IgnoreChildren);
			}
			else
			{
				SetActiveState(gameObject, true, IgnoreChildren);
			}
		}
		if (this.ToggleOut != null)
		{
			this.ToggleOut(this, new EventArgs());
		}
	}

	private bool CheckIfActive(GameObject go)
	{
		return go.activeInHierarchy;
	}

	private void SetActiveState(GameObject go, bool State, bool IgnoreChildren)
	{
		if (IgnoreChildren)
		{
			go.SetActive(State);
		}
		else
		{
			SetAllChildren(go, State);
		}
	}

	private void SetAllChildren(GameObject go, bool State)
	{
		foreach (Transform item in go.transform)
		{
			item.gameObject.SetActive(State);
			SetAllChildren(item.gameObject, State);
		}
		go.SetActive(State);
	}
}
