using UnityEngine;

[FriendlyName("Web Texture", "Download texture data from the specified URL.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#WWW")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Download texture data from the specified URL.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Web/Download")]
public class uScriptAct_WebTexture : uScriptLogic
{
	private bool _Out;

	private bool _OutFinished;

	private bool _OutError;

	private WWW _www;

	[FriendlyName("Out")]
	public bool Out
	{
		get
		{
			return _Out;
		}
	}

	[FriendlyName("Finished")]
	public bool OutFinished
	{
		get
		{
			return _OutFinished;
		}
	}

	[FriendlyName("Error")]
	public bool OutError
	{
		get
		{
			return _OutError;
		}
	}

	public void In([FriendlyName("URL", "The url to download")] string URL, [FriendlyName("Form Data", "A WWWForm instance containing the form data to post.")][SocketState(false, false)] WWWForm Form, [FriendlyName("Result", "The downloaded data.")] out Texture2D Result, [FriendlyName("Error", "Returns an error message if there was an error during the download.")][SocketState(false, false)] out string Error)
	{
		_Out = true;
		_OutFinished = false;
		_OutError = false;
		if (Form == null)
		{
			_www = new WWW(URL);
		}
		else
		{
			_www = new WWW(URL, Form);
		}
		Result = null;
		Error = string.Empty;
	}

	[Driven]
	public bool Driven(out Texture2D Result, out string Error)
	{
		_Out = false;
		Result = null;
		Error = string.Empty;
		if (!_OutFinished)
		{
			if (_www.isDone)
			{
				Result = _www.texture;
				_OutFinished = true;
				if (_www.error != null)
				{
					Error = _www.error;
					_OutError = true;
				}
			}
			return true;
		}
		return false;
	}
}
