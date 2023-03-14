using UnityEngine;

[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodeToolTip("Download text data from the specified URL.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#WWW")]
[NodePath("Actions/Web/Download")]
[FriendlyName("Web Text", "Download text data from the specified URL.")]
public class uScriptAct_WebText : uScriptLogic
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

	public void In([FriendlyName("URL", "The url to download")] string URL, [SocketState(false, false)][FriendlyName("Form Data", "A WWWForm instance containing the form data to post.")] WWWForm Form, [FriendlyName("Result", "The downloaded data.")] out string Result, [FriendlyName("Error", "Returns an error message if there was an error during the download.")][SocketState(false, false)] out string Error)
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
		Result = string.Empty;
		Error = string.Empty;
	}

	[Driven]
	public bool Driven(out string Result, out string Error)
	{
		_Out = false;
		Result = string.Empty;
		Error = string.Empty;
		if (!_OutFinished)
		{
			if (_www.isDone)
			{
				Result = _www.text;
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
