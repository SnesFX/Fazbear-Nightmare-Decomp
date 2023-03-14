using System;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Fires an event signal when one of Instance's joints breaks.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Joint_Break")]
[FriendlyName("Joint Break", "Fires an event signal when one of Instance's joints breaks.")]
[NodePath("Events/Physics Events")]
public class uScript_Joint : uScriptEvent
{
	public class JointBreakEventArgs : EventArgs
	{
		private float m_BreakForce;

		[FriendlyName("Break Force", "The magnitude of the force that caused the joint break.")]
		public float BreakForce
		{
			get
			{
				return m_BreakForce;
			}
		}

		public JointBreakEventArgs(float force)
		{
			m_BreakForce = force;
		}
	}

	public delegate void uScriptEventHandler(object sender, JointBreakEventArgs args);

	[FriendlyName("On Joint Break")]
	public event uScriptEventHandler JointBreak;

	private void OnJointBreak(float force)
	{
		if (this.JointBreak != null)
		{
			this.JointBreak(this, new JointBreakEventArgs(force));
		}
	}
}
