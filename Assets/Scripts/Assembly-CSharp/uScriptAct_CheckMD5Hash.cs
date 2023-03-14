using System;
using System.Security.Cryptography;
using System.Text;

[NodePath("Actions/Security")]
[FriendlyName("Check MD5 Hash", "Checks to see if the Key string is a match for the provided MD5 Hash string.")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeToolTip("Checks to see if the Key string generates the provided MD5 Hash string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
public class uScriptAct_CheckMD5Hash : uScriptLogic
{
	private bool m_GoodHash;

	public bool Good
	{
		get
		{
			return m_GoodHash;
		}
	}

	public bool Bad
	{
		get
		{
			return !m_GoodHash;
		}
	}

	public void In([FriendlyName("Key", "The string to be used to check against the provided MD5 hash.")] string Key, [FriendlyName("MD5 Hash", "The MD5 Hash to check the key against.")] string Hash)
	{
		if (Key != string.Empty && Hash != string.Empty)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			byte[] bytes = uTF8Encoding.GetBytes(Key);
			MD5CryptoServiceProvider mD5CryptoServiceProvider = new MD5CryptoServiceProvider();
			byte[] array = mD5CryptoServiceProvider.ComputeHash(bytes);
			string text = string.Empty;
			for (int i = 0; i < array.Length; i++)
			{
				text += Convert.ToString(array[i], 16).PadLeft(2, '0');
			}
			string text2 = text.PadLeft(32, '0');
			if (text2 == Hash)
			{
				m_GoodHash = true;
			}
		}
	}
}
