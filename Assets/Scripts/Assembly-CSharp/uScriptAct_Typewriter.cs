using System;
using UnityEngine;

[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Typewriter", "Outputs each character of a string based on a time delay. Great for emulating a typewriter style output of text for UI.")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Outputs each character of a string based on a time delay.")]
[NodePath("Actions/Variables/String")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_Typewriter : uScriptLogic
{
	private bool m_Started;

	private float m_DelayTime;

	private float m_SkipDelayTime;

	private string m_TargetString;

	private bool m_Skipped;

	private bool m_CharTyped;

	private string m_CurrentCharacter;

	private string m_OutputString;

	private char[] m_CharacterArray;

	private float m_StartTime;

	private bool m_SkipSocketFired;

	private bool m_Finished;

	private int m_CurrentIndex;

	private bool m_IsRunning;

	public bool Started
	{
		get
		{
			return m_Started;
		}
	}

	public bool Skipped
	{
		get
		{
			return m_Skipped;
		}
	}

	[FriendlyName("Character Typed")]
	public bool CharTyped
	{
		get
		{
			return m_CharTyped;
		}
	}

	public event EventHandler Finished;

	public void In([FriendlyName("Target", "The string to be typed.")] string targetString, [DefaultValue(0.2f)][FriendlyName("Delay", "The time delay between characters in the target string.")] float delayTime, [DefaultValue(0.05f)][FriendlyName("Skip Delay", "The time delay between characters that will be used if the node receives a signal on its Skip input socket. Set to 0 if you want the string to finish typing instantly.")] float skipDelayTime, [FriendlyName("Output", "The new string containing the characters from the original target string. This string is updated as each character is 'typed'. Use this variable with the Character Typed output socket to update your UI as each character is added to this string variable.")] out string Output, [FriendlyName("Current Character", "The current character in the target string that the typewriter is currently on.")][SocketState(false, false)] out string currentCharacter)
	{
		m_Started = true;
		m_Skipped = false;
		m_SkipSocketFired = false;
		m_CharTyped = false;
		m_Finished = false;
		m_CurrentIndex = 0;
		m_OutputString = string.Empty;
		m_CurrentCharacter = string.Empty;
		Output = m_OutputString;
		currentCharacter = m_CurrentCharacter;
		m_DelayTime = delayTime;
		m_SkipDelayTime = skipDelayTime;
		m_TargetString = targetString;
		if (m_TargetString.Length > 0)
		{
			m_CharacterArray = m_TargetString.ToCharArray();
			m_StartTime = Time.time + m_DelayTime;
			m_IsRunning = true;
		}
		else if (this.Finished != null)
		{
			this.Finished(this, new EventArgs());
		}
	}

	public void Skip([FriendlyName("Target", "The string to be typed.")] string targetString, [FriendlyName("Delay", "The time delay between characters in the target string.")][DefaultValue(0.2f)] float delayTime, [DefaultValue(0.05f)][FriendlyName("Skip Delay", "The time delay between characters that will be used if the node receives a signal on its Skip input socket. Set to 0 if you want the string to finish typing instantly.")] float skipDelayTime, [FriendlyName("Output", "The new string containing the characters from the orginial target string. This string is updated as each character is 'typed'. Use this variable with the Character Typed output socket to update your UI as each character is added to this string variable.")] out string Output, [FriendlyName("Current Character", "The current character in the target string that the typewriter is currently on.")][SocketState(false, false)] out string currentCharacter)
	{
		m_SkipSocketFired = true;
		if (m_IsRunning)
		{
			m_Skipped = true;
			if (skipDelayTime <= 0f)
			{
				m_StartTime = Time.time + skipDelayTime;
				Output = targetString;
				currentCharacter = m_CharacterArray[m_CharacterArray.Length - 1].ToString();
			}
			else
			{
				Output = m_OutputString;
				currentCharacter = m_CurrentCharacter;
			}
		}
		else
		{
			Output = targetString;
			currentCharacter = targetString.Substring(targetString.Length - 1, 1);
			m_Skipped = true;
			if (this.Finished != null)
			{
				this.Finished(this, new EventArgs());
			}
		}
	}

	[Driven]
	public bool Driven(out string Output, out string currentCharacter)
	{
		m_Started = false;
		m_Skipped = false;
		m_CharTyped = false;
		bool flag = false;
		if (m_IsRunning)
		{
			if (!m_Finished)
			{
				if (!m_SkipSocketFired)
				{
					if (m_CurrentIndex == 0)
					{
						m_CurrentCharacter = m_CharacterArray[0].ToString();
						m_OutputString = m_CurrentCharacter;
						m_CurrentIndex++;
						m_CharTyped = true;
						if (m_CharacterArray.Length == 1)
						{
							m_Finished = true;
						}
						else
						{
							m_StartTime = Time.time + m_DelayTime;
						}
					}
					if (Time.time > m_StartTime && !m_Finished)
					{
						m_CurrentCharacter = m_CharacterArray[m_CurrentIndex].ToString();
						m_CurrentIndex++;
						m_OutputString += m_CurrentCharacter;
						m_CharTyped = true;
						if (m_CurrentIndex == m_CharacterArray.Length)
						{
							m_Finished = true;
						}
						else
						{
							m_StartTime = Time.time + m_DelayTime;
						}
					}
				}
				else if (m_SkipDelayTime > 0f)
				{
					if (m_CurrentIndex == 0)
					{
						m_CurrentCharacter = m_CharacterArray[0].ToString();
						m_OutputString = m_CurrentCharacter;
						m_CurrentIndex++;
						m_CharTyped = true;
						if (m_CharacterArray.Length == 1)
						{
							m_Finished = true;
						}
						else
						{
							m_StartTime = Time.time + m_SkipDelayTime;
						}
					}
					if (Time.time > m_StartTime && !m_Finished)
					{
						m_CurrentCharacter = m_CharacterArray[m_CurrentIndex].ToString();
						m_CurrentIndex++;
						m_OutputString += m_CurrentCharacter;
						m_CharTyped = true;
						if (m_CurrentIndex == m_CharacterArray.Length)
						{
							m_Finished = true;
						}
						else
						{
							m_StartTime = Time.time + m_SkipDelayTime;
						}
					}
				}
				else
				{
					flag = true;
					m_Finished = true;
				}
			}
			else
			{
				m_IsRunning = false;
				if (this.Finished != null)
				{
					this.Finished(this, new EventArgs());
				}
			}
		}
		if (flag)
		{
			Output = m_TargetString;
			currentCharacter = m_CharacterArray[m_CharacterArray.Length - 1].ToString();
			m_CharTyped = true;
		}
		else
		{
			Output = m_OutputString;
			currentCharacter = m_CurrentCharacter;
		}
		return m_IsRunning;
	}
}
