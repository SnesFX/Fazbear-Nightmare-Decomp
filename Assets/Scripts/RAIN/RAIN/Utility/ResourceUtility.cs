using System;
using System.IO;
using UnityEngine;

namespace RAIN.Utility
{
	public static class ResourceUtility
	{
		public static Stream OpenResourceAsStream(string aFilePath)
		{
			try
			{
				int num = aFilePath.IndexOf("resources", StringComparison.OrdinalIgnoreCase);
				if (num >= 0)
				{
					aFilePath = aFilePath.Substring(num + "resources/".Length);
				}
				aFilePath = aFilePath.Replace('\\', '/');
				aFilePath = aFilePath.Trim();
				if (aFilePath == "")
				{
					return null;
				}
				TextAsset textAsset = (TextAsset)Resources.Load(aFilePath, typeof(TextAsset));
				if (textAsset == null)
				{
					return null;
				}
				return new MemoryStream(textAsset.bytes);
			}
			catch
			{
			}
			return null;
		}
	}
}
