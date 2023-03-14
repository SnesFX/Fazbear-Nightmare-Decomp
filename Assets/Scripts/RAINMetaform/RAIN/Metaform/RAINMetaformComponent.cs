using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Metaform
{
	[Serializable]
	public class RAINMetaformComponent : MonoBehaviour
	{
		[SerializeField]
		public string serializationString;

		[SerializeField]
		public List<MetaformByteArray> customData = new List<MetaformByteArray>();

		[SerializeField]
		public string prefab;

		[SerializeField]
		public string inflatePath;

		[SerializeField]
		public List<UnityEngine.Object> linkedObjects = new List<UnityEngine.Object>();
	}
}
