using UnityEngine;

[NodePath("Actions/Camera")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Allows you to fade to or from a color with the Target Camera.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Camera Fade", "Allows you to fade to or from a color with the Target Camera. This node works by creating a temporary primitive plane GameObject containing the specified material in front of the camera. The GameObject is removed after the fade is completed.")]
public class uScriptAct_CameraFade : uScriptLogic
{
	public enum FadeDirection
	{
		To = 0,
		From = 1
	}

	private float m_TimeToTrigger;

	private float m_TotalTime;

	private float m_HoldTime;

	private bool m_HoldFinished;

	private bool m_FadeTo = true;

	private Camera m_TargetCamera;

	private GameObject m_CameraPlane;

	private Color m_OriginalColor;

	private Material m_FadeMaterial;

	private bool m_ColorOverride;

	private Color m_StartColor;

	private bool m_ImmediateOut;

	private bool m_fadeFinished;

	[FriendlyName("Immediate Out")]
	public bool Immediate
	{
		get
		{
			return m_ImmediateOut;
		}
	}

	[FriendlyName("Finished")]
	public bool FadeFinished
	{
		get
		{
			return m_fadeFinished;
		}
	}

	public void In([FriendlyName("Camera", "The Camera you wish to apply the fade to.")] Camera TargetCamera, [FriendlyName("Direction", "The direction of the fade. To will fade in the color over the camera. From will start at full color and fade out.")] FadeDirection Direction, [FriendlyName("Material", "The material you wish to use for the fade. Note: You will need to use a material with a shader that supports transparency.")] Material FadeMaterial, [DefaultValue(1f)][FriendlyName("Fade Time", "The time period (in seconds) you wish the fade to occur.")] float FadeTime, [FriendlyName("Hold Time", "The time period (in seconds) you wish to hold the material in the camera before destroying the temporary plane. This is only used when the fade direction is set to \"To\". A value less than 0 will be ignored.")][SocketState(false, false)][DefaultValue(0f)] float HoldTime, [FriendlyName("Override Color", "Will override the material's main color with the one specified in the Color property.")][SocketState(false, false)] bool ColorOverride, [SocketState(false, false)][FriendlyName("Color", "The material color you wish to use when Color Override is set to true.")] Color FadeColor)
	{
		if (TargetCamera != null && FadeMaterial != null && FadeTime > 0f)
		{
			m_ImmediateOut = true;
			m_fadeFinished = false;
			m_TargetCamera = TargetCamera;
			m_FadeMaterial = FadeMaterial;
			m_ColorOverride = ColorOverride;
			if (ColorOverride)
			{
				m_OriginalColor = m_FadeMaterial.color;
				m_FadeMaterial.color = FadeColor;
			}
			if (Direction == FadeDirection.From)
			{
				m_FadeTo = false;
			}
			else
			{
				m_FadeMaterial.color = new Color(m_FadeMaterial.color.r, m_FadeMaterial.color.g, m_FadeMaterial.color.b, 0f);
			}
			m_CameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
			m_CameraPlane.GetComponent<Collider>().enabled = false;
			m_CameraPlane.name = "uScriptRuntimeGenerated_CameraFadePlane";
			m_CameraPlane.GetComponent<Renderer>().material = m_FadeMaterial;
			m_CameraPlane.transform.position = m_TargetCamera.transform.position;
			m_CameraPlane.transform.rotation = m_TargetCamera.transform.rotation;
			m_CameraPlane.transform.parent = m_TargetCamera.transform;
			m_CameraPlane.transform.localRotation = Quaternion.Euler(new Vector3(-90f, 0f, 0f));
			m_CameraPlane.transform.localPosition = new Vector3(0f, 0f, 0.5f);
			if (HoldTime < 0f)
			{
				HoldTime = 0f;
			}
			m_TimeToTrigger = FadeTime;
			m_TotalTime = FadeTime;
			m_HoldFinished = false;
			if (Direction != 0)
			{
				m_HoldTime = 0f;
			}
			else
			{
				m_HoldTime = HoldTime + m_TotalTime;
			}
		}
		else
		{
			uScriptDebug.Log("[Camera Fade] One or more of the sockets contains null data. Please check your specified Camera and Material to be sure they are not null. Also check to make sure your specified Time is greater than 0.", uScriptDebug.Type.Warning);
		}
	}

	[Driven]
	public bool DrivenFade()
	{
		m_ImmediateOut = false;
		m_fadeFinished = false;
		if (m_HoldTime > 0f)
		{
			m_HoldTime -= Time.deltaTime;
			if (m_HoldTime < 0f)
			{
				m_HoldTime = 0f;
			}
		}
		if (m_TimeToTrigger > 0f)
		{
			m_TimeToTrigger -= Time.deltaTime;
			if (m_TimeToTrigger < 0f)
			{
				m_TimeToTrigger = 0f;
			}
			float t = 1f - m_TimeToTrigger / m_TotalTime;
			if (m_FadeTo)
			{
				m_CameraPlane.GetComponent<Renderer>().material.color = new Color(m_FadeMaterial.color.r, m_FadeMaterial.color.g, m_FadeMaterial.color.b, Mathf.Lerp(0f, 1f, t));
			}
			else
			{
				m_CameraPlane.GetComponent<Renderer>().material.color = new Color(m_FadeMaterial.color.r, m_FadeMaterial.color.g, m_FadeMaterial.color.b, Mathf.Lerp(1f, 0f, t));
			}
			if (m_TimeToTrigger <= 0f)
			{
				m_TimeToTrigger = 0f;
			}
			return true;
		}
		if (m_TimeToTrigger <= 0f && !m_HoldFinished)
		{
			if (m_HoldTime <= 0f)
			{
				m_HoldFinished = true;
				if (m_ColorOverride)
				{
					m_FadeMaterial.color = m_OriginalColor;
				}
				Object.Destroy(m_CameraPlane);
				m_fadeFinished = true;
			}
			return true;
		}
		return false;
	}
}
