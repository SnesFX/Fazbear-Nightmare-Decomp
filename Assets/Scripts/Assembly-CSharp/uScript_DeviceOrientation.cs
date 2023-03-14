using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Device_Orientation_Events")]
[NodeToolTip("Fires an event signal when the screen orientation of a device happens.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeAutoAssignMasterInstance(true)]
[FriendlyName("Device Orientation Events", "Fires an event signal when the screen orientation of a device happens.\n\nSupported events: Portrait, Portrait Upside-Down, Landscape Left, Landscape Right, Face Up, Face Down.")]
[NodePath("Events/Input Events")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
public class uScript_DeviceOrientation : uScriptEvent
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private DeviceOrientation m_LastOrientation;

	[FriendlyName("On Portrait")]
	public event uScriptEventHandler OnDevicePortrait;

	[FriendlyName("On Portrait Upside-Down")]
	public event uScriptEventHandler OnDevicePortraitUpsideDown;

	[FriendlyName("On Landscape Left")]
	public event uScriptEventHandler OnDeviceLandscapeLeft;

	[FriendlyName("On Landscape Right")]
	public event uScriptEventHandler OnDeviceLandscapeRight;

	[FriendlyName("On Face Up")]
	public event uScriptEventHandler OnDeviceFaceUp;

	[FriendlyName("On Face Down")]
	public event uScriptEventHandler OnDeviceFaceDown;

	private void Update()
	{
		if (Input.deviceOrientation == DeviceOrientation.FaceDown && m_LastOrientation != DeviceOrientation.FaceDown && this.OnDeviceFaceDown != null)
		{
			this.OnDeviceFaceDown(this, new EventArgs());
		}
		if (Input.deviceOrientation == DeviceOrientation.FaceUp && m_LastOrientation != DeviceOrientation.FaceUp && this.OnDeviceFaceUp != null)
		{
			this.OnDeviceFaceUp(this, new EventArgs());
		}
		if (Input.deviceOrientation == DeviceOrientation.LandscapeLeft && m_LastOrientation != DeviceOrientation.LandscapeLeft && this.OnDeviceLandscapeLeft != null)
		{
			this.OnDeviceLandscapeLeft(this, new EventArgs());
		}
		if (Input.deviceOrientation == DeviceOrientation.LandscapeRight && m_LastOrientation != DeviceOrientation.LandscapeRight && this.OnDeviceLandscapeRight != null)
		{
			this.OnDeviceLandscapeRight(this, new EventArgs());
		}
		if (Input.deviceOrientation == DeviceOrientation.Portrait && m_LastOrientation != DeviceOrientation.Portrait && this.OnDevicePortrait != null)
		{
			this.OnDevicePortrait(this, new EventArgs());
		}
		if (Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown && m_LastOrientation != DeviceOrientation.PortraitUpsideDown && this.OnDevicePortraitUpsideDown != null)
		{
			this.OnDevicePortraitUpsideDown(this, new EventArgs());
		}
		m_LastOrientation = Input.deviceOrientation;
	}
}
