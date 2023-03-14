using System.IO;
using UnityEngine;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Capture_Screenshot")]
[NodeToolTip("Captures a screenshot as a PNG file.")]
[NodePath("Actions/Utilities")]
[FriendlyName("Capture Screenshot", "Captures a screenshot as a PNG file. If the file already exists, it will be overwritten. If no path is defined or a bad path is provided, the screenshot will be placed in the root folder of the project.\n\nNote: This node will not function when run from the web player or a Dashboard widget.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_CaptureScreenshot : uScriptLogic
{
	private int m_NumberCount;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	public void In([FriendlyName("File Name", "The name of the file. You do not need to provide the extension.")] string FileName, [FriendlyName("Path", "The path where you wish to save the screenshot file to.")] string Path, [FriendlyName("Relative To Data Folder", "Applies the project's root data folder path to the begining of the specified path (the same location as your project's Assets folder).")][DefaultValue(true)][SocketState(false, false)] bool RelativeToDataFolder, [DefaultValue(false)][SocketState(false, false)][FriendlyName("Append Number", "If true, this will append an incrementing number in the format of \"_#####\" to the end of the file name.")] bool AppendNumber, [SocketState(false, false)][FriendlyName("File Saved", "Outputs the full path and filename of the saved screenshot.")][DefaultValue(false)] out string FileSaved)
	{
		FileName = FileName.Replace("/", string.Empty);
		FileName = FileName.Replace("\\", string.Empty);
		if (Path != string.Empty)
		{
			Path = Path.Replace("\\", "/");
			if (Path.StartsWith("/"))
			{
				Path = Path.Remove(0, 1);
			}
			if (Path.EndsWith("/"))
			{
				int startIndex = Path.Length - 1;
				Path = Path.Remove(startIndex, 1);
			}
			if (RelativeToDataFolder)
			{
				Path = Application.dataPath + "/" + Path;
			}
			Path = ((!CheckFullPath(Path)) ? FileName : (Path + "/" + FileName + ".png"));
		}
		else
		{
			Path = FileName;
		}
		if (AppendNumber)
		{
			m_NumberCount++;
			string text = string.Empty;
			if (m_NumberCount < 10)
			{
				text = "0000";
			}
			if (m_NumberCount > 9 && m_NumberCount < 100)
			{
				text = "000";
			}
			if (m_NumberCount > 99 && m_NumberCount < 1000)
			{
				text = "00";
			}
			if (m_NumberCount > 999 && m_NumberCount < 10000)
			{
				text = "0";
			}
			if (m_NumberCount > 9999 && m_NumberCount < 100000)
			{
				text = string.Empty;
			}
			if (m_NumberCount > 100000)
			{
				text = "0000";
				m_NumberCount = 0;
			}
			Path = Path + "_" + text + m_NumberCount;
		}
		else
		{
			m_NumberCount = 0;
		}
		Path += ".png";
		ScreenCapture.CaptureScreenshot(Path);
		FileSaved = Path;
	}

	private bool CheckFullPath(string FullPath)
	{
		if (Directory.Exists(FullPath))
		{
			return true;
		}
		return false;
	}
}
