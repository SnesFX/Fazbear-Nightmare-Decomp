using System;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when an accelerometer event happens.")]
[FriendlyName("Accelerometer Events", "Fires an event signal when an accelerometer event happens.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAutoAssignMasterInstance(true)]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Accelerometer_Events")]
[NodePath("Events/Input Events")]
public class uScript_Accelerometer : uScriptEvent
{
	public class AccelerometerEventArgs : EventArgs
	{
		private AccelerationEvent m_AccelEvent;

		[FriendlyName("Acceleration", "The accelerometer value of this event.")]
		public Vector3 Acceleration
		{
			get
			{
				return m_AccelEvent.acceleration;
			}
		}

		[FriendlyName("Delta Time", "Amount of time (in seconds) that has passed since the last acceleraton measurement.")]
		[SocketState(false, false)]
		public float DeltaTime
		{
			get
			{
				return m_AccelEvent.deltaTime;
			}
		}

		public AccelerometerEventArgs(AccelerationEvent accelEvent)
		{
			m_AccelEvent = accelEvent;
		}
	}

	public delegate void uScriptEventHandler(object sender, AccelerometerEventArgs args);

	[FriendlyName("On Acceleration")]
	public event uScriptEventHandler OnAccelerationEvent;

	private void Update()
	{
		AccelerationEvent[] accelerationEvents = Input.accelerationEvents;
		foreach (AccelerationEvent accelEvent in accelerationEvents)
		{
			if (this.OnAccelerationEvent != null)
			{
				this.OnAccelerationEvent(this, new AccelerometerEventArgs(accelEvent));
			}
		}
	}
}
