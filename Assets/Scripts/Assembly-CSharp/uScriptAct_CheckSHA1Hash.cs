using System;
using System.Security.Cryptography;
using System.Text;

[NodeAuthor("Detox Studios LLC", "http://www.detoxstudios.com")]
[NodeHelp("http://www.uscript.net/docs/index.php?title=Node_Reference_Guide")]
[FriendlyName("Check SHA1 Hash", "Checks to see if the Key string is a match for the provided SHA1 Hash string.")]
[NodeCopyright("Copyright 2011 by Detox Studios LLC")]
[NodePath("Actions/Security")]
[NodeToolTip("Checks to see if the Key string generates the provided SHA1 Hash string.")]
public class uScriptAct_CheckSHA1Hash : uScriptLogic
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

	public void In([FriendlyName("Key", "The string to be used to check against the provided SHA1 hash.")] string Key, [FriendlyName("SHA1 Hash", "The SHA1 Hash to check the key against.")] string Hash)
	{
		if (Key != string.Empty && Hash != string.Empty)
		{
			UTF8Encoding uTF8Encoding = new UTF8Encoding();
			byte[] bytes = uTF8Encoding.GetBytes(Key);
			SHA1CryptoServiceProvider sHA1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] array = sHA1CryptoServiceProvider.ComputeHash(bytes);
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
