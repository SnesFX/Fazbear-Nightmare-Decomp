using UnityEngine;

[NodePath("Actions/GameObjects")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Isometric Character Controller.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[FriendlyName("Isometric Character Controller", "Simple character controller.  Character always moves forward and backwards along its forward vector.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Isometric_Character_Controller")]
public class uScriptAct_IsometricCharacterController : uScriptLogic
{
	private enum Direction
	{
		None = 0,
		Forward = 1,
		Backward = 2,
		Right = 3,
		Left = 4
	}

	private GameObject m_Target;

	private Direction m_Translate;

	private Direction m_Rotate;

	private Direction m_Strafe;

	private float m_TranslateSpeed;

	private float m_RotateSpeed;

	private CharacterController m_Controller;

	private float m_LastTranslateSpeed;

	private float m_LastRotateSpeed;

	private bool m_FilterTranslation;

	private bool m_FilterRotation;

	private float m_TranslationFilterConstant = 0.7f;

	private float m_RotationFilterConstant = 0.1f;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Move Local Forward")]
	public void MoveForward(GameObject target, float translation, float rotation, bool filterTranslation, float translationFilterConstant, bool filterRotation, float rotationFilterConstant)
	{
		m_Translate = Direction.Forward;
		m_Target = target;
		m_TranslateSpeed = (m_LastTranslateSpeed = translation);
		m_RotateSpeed = rotation;
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	[FriendlyName("Move Local Backward")]
	public void MoveBackward(GameObject target, float translation, float rotation, bool filterTranslation, float translationFilterConstant, bool filterRotation, float rotationFilterConstant)
	{
		m_Translate = Direction.Backward;
		m_Target = target;
		m_TranslateSpeed = (m_LastTranslateSpeed = translation);
		m_RotateSpeed = rotation;
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	[FriendlyName("Strafe Local Right")]
	public void StrafeRight(GameObject target, float translation, float rotation, bool filterTranslation, float translationFilterConstant, bool filterRotation, float rotationFilterConstant)
	{
		m_Strafe = Direction.Right;
		m_Target = target;
		m_TranslateSpeed = (m_LastTranslateSpeed = translation);
		m_RotateSpeed = rotation;
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	[FriendlyName("Strafe Local Left")]
	public void StrafeLeft(GameObject target, float translation, float rotation, bool filterTranslation, float translationFilterConstant, bool filterRotation, float rotationFilterConstant)
	{
		m_Strafe = Direction.Left;
		m_Target = target;
		m_TranslateSpeed = (m_LastTranslateSpeed = translation);
		m_RotateSpeed = rotation;
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	[FriendlyName("Rotate Local Right")]
	public void RotateRight(GameObject target, float translation, float rotation, bool filterTranslation, float translationFilterConstant, bool filterRotation, float rotationFilterConstant)
	{
		m_Rotate = Direction.Right;
		m_Target = target;
		m_TranslateSpeed = translation;
		m_RotateSpeed = (m_LastRotateSpeed = rotation);
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	[FriendlyName("Rotate Local Left")]
	public void RotateLeft([FriendlyName("Target", "The character to control.")] GameObject target, [FriendlyName("Translation Units Per Second", "How many units to move per second when the forward/backward keys are pressed.")] float translation, [FriendlyName("Rotation Units Per Second", "How many units to rotate per second when the left/right keys are pressed.")][DefaultValue(1.5f)] float rotation, [FriendlyName("Filter Translation", "If True, the object's translation will be filtered.")][SocketState(false, false)][DefaultValue(false)] bool filterTranslation, [FriendlyName("Translation Filter Constant", "The strength of the translation filter (lower numbers mean more filtering, i.e. slower).")][DefaultValue(0.7f)][SocketState(false, false)] float translationFilterConstant, [FriendlyName("Filter Rotation", "If True, the object's rotation will be filtered.")][SocketState(false, false)][DefaultValue(false)] bool filterRotation, [DefaultValue(0.1f)][FriendlyName("Rotation Filter Constant", "The strength of the rotation filter (lower numbers mean more filtering, i.e. slower).")][SocketState(false, false)] float rotationFilterConstant)
	{
		m_Rotate = Direction.Left;
		m_Target = target;
		m_TranslateSpeed = translation;
		m_RotateSpeed = (m_LastRotateSpeed = rotation);
		m_FilterTranslation = filterTranslation;
		m_FilterRotation = filterRotation;
		m_TranslationFilterConstant = translationFilterConstant;
		m_RotationFilterConstant = rotationFilterConstant;
		if (null != target)
		{
			m_Controller = target.GetComponent<CharacterController>();
		}
	}

	public void Update()
	{
		if (!(null == m_Target))
		{
			float num = m_RotateSpeed;
			float num2 = m_TranslateSpeed;
			if (m_FilterTranslation)
			{
				num2 = (m_LastTranslateSpeed *= 1f - m_TranslationFilterConstant);
			}
			if (m_FilterRotation)
			{
				num = (m_LastRotateSpeed *= 1f - m_RotationFilterConstant);
			}
			if (m_Rotate == Direction.Left)
			{
				m_Target.transform.RotateAroundLocal(Vector3.up, (0f - num) * Time.deltaTime);
			}
			else if (m_Rotate == Direction.Right)
			{
				m_Target.transform.RotateAroundLocal(Vector3.up, num * Time.deltaTime);
			}
			Vector3 vector = Vector3.zero;
			if (m_Strafe == Direction.Left)
			{
				vector = m_Target.transform.right * (0f - num2) * Time.deltaTime;
			}
			else if (m_Strafe == Direction.Right)
			{
				vector = m_Target.transform.right * num2 * Time.deltaTime;
			}
			if (m_Translate == Direction.Forward)
			{
				vector = m_Target.transform.forward * num2 * Time.deltaTime;
			}
			else if (m_Translate == Direction.Backward)
			{
				vector = m_Target.transform.forward * (0f - num2) * Time.deltaTime;
			}
			if (null == m_Controller)
			{
				m_Target.transform.position += vector;
			}
			else
			{
				m_Controller.Move(vector);
			}
			if (!m_FilterTranslation || (double)Mathf.Abs(num2) <= 0.01)
			{
				m_Translate = Direction.None;
				m_Strafe = Direction.None;
			}
			if (!m_FilterRotation || (double)Mathf.Abs(num) <= 0.01)
			{
				m_Rotate = Direction.None;
			}
			if (m_Rotate == Direction.None && m_Translate == Direction.None && m_Strafe == Direction.None)
			{
				m_Target = null;
			}
		}
	}
}
