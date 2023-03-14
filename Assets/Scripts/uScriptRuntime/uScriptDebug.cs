using UnityEngine;

public class uScriptDebug : MonoBehaviour
{
	public enum Type
	{
		Message = 0,
		Warning = 1,
		Error = 2,
		Debug = 3
	}

	public static void Log(string msgString)
	{
		Log(msgString, Type.Message);
	}

	public static void Log(string msgString, Type msgType)
	{
		string text = "uScript: ";
		string message = text + msgString + "\n";
		switch (msgType)
		{
		case Type.Message:
			Debug.Log(message);
			break;
		case Type.Warning:
			Debug.LogWarning(message);
			break;
		case Type.Error:
			Debug.LogError(message);
			break;
		default:
			Debug.Log(message);
			break;
		case Type.Debug:
			break;
		}
	}
}
