using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Load_Level")]
[NodeToolTip("Loads a level by its scene name.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodePath("Actions/Level")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[FriendlyName("Load Level", "Loads a level by its scene name.")]
public class uScriptAct_LoadLevel : uScriptLogic
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

	public void In([FriendlyName("Level Name", "The level to load (make sure it's been added to Unity through File->Build Settings...).")] string name, [SocketState(false, false)][DefaultValue(true)][FriendlyName("Unload Others", "Whether or not to destroy the other objects in the scene.")] bool destroyOtherObjects, [SocketState(false, false)][FriendlyName("Block Until Loaded", "Halt execution of the game until the level has loaded.  (Requires Unity Pro if set to false).")][DefaultValue(true)] bool blockUntilLoaded)
	{
		if (blockUntilLoaded)
		{
			if (destroyOtherObjects)
			{
				Application.LoadLevel(name);
			}
			else
			{
				Application.LoadLevelAdditive(name);
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
				m_Async = Application.LoadLevelAsync(name);
			}
			else
			{
				m_Async = Application.LoadLevelAdditiveAsync(name);
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
