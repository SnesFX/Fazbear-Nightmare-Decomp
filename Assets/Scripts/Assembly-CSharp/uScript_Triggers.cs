using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Trigger_Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Trigger Events", "Fires an event signal when a GameObject enters, exits, or stays in a trigger. The Instance GameObject must have a collider component on it set to be a trigger. Also, only other Gameobjects with a rigidbody component will trigger this event (the instigators).")]
[NodeToolTip("Fires an event signal when a GameObject enters, exits, or stays in a trigger.")]
[NodePropertiesPath("Properties/Triggers")]
[NodePath("Events")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScript_Triggers : uScriptEvent
{
	public class TriggerEventArgs : EventArgs
	{
		private GameObject m_GameObject;

		[FriendlyName("Instigator", "The GameObject that interacted with this GameObject's (the Instance) collider component. ")]
		[SocketState(false, false)]
		public GameObject GameObject
		{
			get
			{
				return m_GameObject;
			}
		}

		public TriggerEventArgs(GameObject gameObject)
		{
			m_GameObject = gameObject;
		}
	}

	public delegate void uScriptEventHandler(object sender, TriggerEventArgs args);

	private bool m_AlwaysTrigger;

	private int m_TimesToTrigger;

	[FriendlyName("Times to Trigger", "How many times this trigger should fire before it deactivates.")]
	public int TimesToTrigger
	{
		set
		{
			m_TimesToTrigger = value;
			if (m_TimesToTrigger == 0)
			{
				m_AlwaysTrigger = true;
			}
		}
	}

	public event uScriptEventHandler OnEnterTrigger;

	public event uScriptEventHandler OnExitTrigger;

	public event uScriptEventHandler WhileInsideTrigger;

	private void OnTriggerEnter(Collider other)
	{
		if (m_TimesToTrigger != 0 || m_AlwaysTrigger)
		{
			m_TimesToTrigger--;
			if (this.OnEnterTrigger != null)
			{
				this.OnEnterTrigger(this, new TriggerEventArgs(other.gameObject));
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (m_TimesToTrigger != 0 || m_AlwaysTrigger)
		{
			m_TimesToTrigger--;
			if (this.OnExitTrigger != null)
			{
				this.OnExitTrigger(this, new TriggerEventArgs(other.gameObject));
			}
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (m_TimesToTrigger != 0 || m_AlwaysTrigger)
		{
			m_TimesToTrigger--;
			if (this.WhileInsideTrigger != null)
			{
				this.WhileInsideTrigger(this, new TriggerEventArgs(other.gameObject));
			}
		}
	}
}
