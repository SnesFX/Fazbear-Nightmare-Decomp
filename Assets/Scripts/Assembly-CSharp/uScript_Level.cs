using System;

[FriendlyName("Level Load", "Fires an event signal when a level is finished loading.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide#Level_Load")]
[NodeAutoAssignMasterInstance(true)]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Fires an event signal when a level is finished loading.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Events/Game Events")]
public class uScript_Level : uScriptEvent
{
	public class LevelWasLoadedEventArgs : EventArgs
	{
		private int m_Level;

		[FriendlyName("Level Index", "The index of the level that was loaded.")]
		public int Level
		{
			get
			{
				return m_Level;
			}
		}

		public LevelWasLoadedEventArgs(int level)
		{
			m_Level = level;
		}
	}

	public delegate void uScriptEventHandler(object sender, LevelWasLoadedEventArgs args);

	[FriendlyName("On Level Was Loaded")]
	public event uScriptEventHandler LevelWasLoaded;

	private void OnLevelWasLoaded(int level)
	{
		if (this.LevelWasLoaded != null)
		{
			this.LevelWasLoaded(this, new LevelWasLoadedEventArgs(level));
		}
	}
}
