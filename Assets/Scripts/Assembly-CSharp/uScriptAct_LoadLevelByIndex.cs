using System;
using UnityEngine;

[NodeToolTip("Loads a level by its index value as defined in Unity's Build Settings.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Level")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Load Level By Index", "Loads a level by its index value as defined in Unity's Build Settings.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
public class uScriptAct_LoadLevelByIndex : uScriptLogic
{
	public delegate void uScriptEventHandler(object sender, EventArgs args);

	private bool m_IsLoading;

	private AsyncOperation m_Async;

	public bool Out
	{
		get
		{
			return true;
		}
	}

	[FriendlyName("Loaded")]
	public event uScriptEventHandler Loaded;

	public void In([FriendlyName("Level Index", "The level index to load (make sure it's been added to Unity through File->Build Settings...).")] int index, [FriendlyName("Unload Others", "Whether or not to destroy the other objects in the scene.")][DefaultValue(true)][SocketState(false, false)] bool destroyOtherObjects, [FriendlyName("Block Until Loaded", "Halt execution of the game until the level has loaded.  (Requires Unity Pro if set to false).")][SocketState(false, false)][DefaultValue(true)] bool blockUntilLoaded)
	{
		if (blockUntilLoaded)
		{
			if (destroyOtherObjects)
			{
				Application.LoadLevel(index);
			}
			else
			{
				Application.LoadLevelAdditive(index);
			}
			if (this.Loaded != null)
			{
				this.Loaded(this, EventArgs.Empty);
			}
		}
		else
		{
			m_IsLoading = true;
			if (destroyOtherObjects)
			{
				m_Async = Application.LoadLevelAsync(index);
			}
			else
			{
				m_Async = Application.LoadLevelAdditiveAsync(index);
			}
		}
	}

	public void Update()
	{
		if (m_IsLoading && m_Async.isDone)
		{
			m_IsLoading = false;
			if (this.Loaded != null)
			{
				this.Loaded(this, EventArgs.Empty);
			}
		}
	}
}
