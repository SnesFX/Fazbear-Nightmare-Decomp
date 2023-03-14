using UnityEngine;

[AddComponentMenu("uScript/Graphs/lift_make_your_lift")]
public class lift_make_your_lift_Component : uScriptCode
{
	public lift_make_your_lift ExposedVariables = new lift_make_your_lift();

	public GameObject _Person_Controller
	{
		get
		{
			return ExposedVariables._Person_Controller;
		}
		set
		{
			ExposedVariables._Person_Controller = value;
		}
	}

	public KeyCode BOTTOM
	{
		get
		{
			return ExposedVariables.BOTTOM;
		}
		set
		{
			ExposedVariables.BOTTOM = value;
		}
	}

	public KeyCode TOP
	{
		get
		{
			return ExposedVariables.TOP;
		}
		set
		{
			ExposedVariables.TOP = value;
		}
	}

	private void Awake()
	{
		base.useGUILayout = false;
		ExposedVariables.Awake();
		ExposedVariables.SetParent(base.gameObject);
		if ("1.CMR" != uScript_MasterComponent.Version)
		{
			uScriptDebug.Log("The generated code is not compatible with your current uScript Runtime " + uScript_MasterComponent.Version, uScriptDebug.Type.Error);
			ExposedVariables = null;
			Debug.Break();
		}
	}

	private void Start()
	{
		ExposedVariables.Start();
	}

	private void OnEnable()
	{
		ExposedVariables.OnEnable();
	}

	private void OnDisable()
	{
		ExposedVariables.OnDisable();
	}

	private void Update()
	{
		ExposedVariables.Update();
	}

	private void OnDestroy()
	{
		ExposedVariables.OnDestroy();
	}
}
