using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using RAIN.Core;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Serialization
{
	[Serializable]
	public class FieldSerializer
	{
		[Serializable]
		public class CustomSerializedData
		{
			[SerializeField]
			private byte[] _data = new byte[0];

			[SerializeField]
			private string _stringData = "";

			public byte[] Data
			{
				get
				{
					if (_data.Length > 0)
					{
						_stringData = Convert.ToBase64String(_data);
						_data = new byte[0];
					}
					return Convert.FromBase64String(_stringData);
				}
				set
				{
					_stringData = Convert.ToBase64String(value);
				}
			}

			public string StringData
			{
				get
				{
					return _stringData;
				}
				set
				{
					_stringData = value;
				}
			}

			public CustomSerializedData(byte[] aData)
			{
				_stringData = Convert.ToBase64String(aData);
			}
		}

		private const string _serializerVersion = "1.2";

		[NonSerialized]
		private ObjectDocument _currentDocument = ObjectDocument.CreateFieldDocument(FieldDocumentType.Xml);

		[NonSerialized]
		private List<object> _currentReferences = new List<object>();

		[NonSerialized]
		private List<UnityEngine.Object> _currentGameObjects = new List<UnityEngine.Object>();

		[NonSerialized]
		private List<CustomSerializedData> _currentCustomData = new List<CustomSerializedData>();

		[SerializeField]
		private string _serializedData = "";

		[SerializeField]
		private List<UnityEngine.Object> _serializedGameObjects = new List<UnityEngine.Object>();

		[SerializeField]
		private List<CustomSerializedData> _serializedCustomData = new List<CustomSerializedData>();

		[NonSerialized]
		private bool _holdReferences;

		[NonSerialized]
		private bool _headerProcessed;

		[NonSerialized]
		private bool _documentProcessed;

		[NonSerialized]
		private bool _needsReserialization;

		[NonSerialized]
		private LinkedList<int> _recursiveReferences = new LinkedList<int>();

		[NonSerialized]
		private SimpleProfiler _profiler = SimpleProfiler.GetProfiler("RAINSerialization");

		public string SerializerVersion
		{
			get
			{
				return "1.2";
			}
		}

		public ObjectDocument CurrentDocument
		{
			get
			{
				return _currentDocument;
			}
		}

		public string SerializedData
		{
			get
			{
				return _serializedData;
			}
		}

		public IList<UnityEngine.Object> SerializedGameObjects
		{
			get
			{
				return _serializedGameObjects.AsReadOnly();
			}
		}

		public IList<CustomSerializedData> SerializedCustomData
		{
			get
			{
				return _serializedCustomData.AsReadOnly();
			}
		}

		public bool NeedsSerialization
		{
			get
			{
				return _serializedData == "";
			}
		}

		public bool NeedsDeserialization
		{
			get
			{
				return !_documentProcessed;
			}
		}

		public bool HoldReferences
		{
			get
			{
				return _holdReferences;
			}
			set
			{
				_holdReferences = value;
			}
		}

		public FieldSerializer()
		{
		}

		public FieldSerializer(string aDocument, IList<UnityEngine.Object> aSerializedGameObjects, IList<CustomSerializedData> aSerializedCustomData)
			: this()
		{
			_serializedData = aDocument;
			_serializedGameObjects = new List<UnityEngine.Object>(aSerializedGameObjects);
			_serializedCustomData = new List<CustomSerializedData>(aSerializedCustomData);
		}

		public Type GetComponentType()
		{
			DeserializeRAINObjectHeader(false);
			Type aObject;
			if (!DeserializeObjectFromString<Type>(_currentDocument.DocumentElement.GetAttribute("type"), out aObject))
			{
				return null;
			}
			return aObject;
		}

		public string GetComponentVersion()
		{
			DeserializeRAINObjectHeader(false);
			return _currentDocument.DocumentElement.GetAttribute("version");
		}

		public void SerializeRAINObject(object aComponent)
		{
			_currentDocument.ClearDocument();
			_currentReferences.Clear();
			_currentGameObjects.Clear();
			_currentCustomData.Clear();
			ObjectElement objectElement = ((aComponent is RAINComponent) ? _currentDocument.CreateElement("component") : ((!(aComponent is RAINScriptableObject)) ? _currentDocument.CreateElement("object") : _currentDocument.CreateElement("scriptableobject")));
			objectElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aComponent.GetType()));
			objectElement.SetAttribute("version", "1.2");
			_currentDocument.SetDocument(objectElement);
			ObjectElement objectElement2 = _currentDocument.CreateElement("fields");
			objectElement.AddChild(objectElement2);
			ObjectElement aElement = _currentDocument.CreateElement("references");
			objectElement.AddChild(aElement);
			SerializeClassToElement(objectElement2, aComponent, false);
			_serializedData = _currentDocument.GetDocumentAsString();
			_serializedGameObjects = new List<UnityEngine.Object>(_currentGameObjects);
			_serializedCustomData = new List<CustomSerializedData>(_currentCustomData);
		}

		public void DeserializeRAINObjectHeader(bool aForce)
		{
			if (!_headerProcessed || aForce)
			{
				_currentDocument.SetDocument(_serializedData);
				_currentReferences.Clear();
				_currentGameObjects = new List<UnityEngine.Object>(_serializedGameObjects);
				_currentCustomData = new List<CustomSerializedData>(_serializedCustomData);
				_headerProcessed = true;
				_documentProcessed = false;
			}
		}

		public bool DeserializeRAINObject(object aComponent)
		{
			DeserializeRAINObjectHeader(false);
			_needsReserialization = false;
			bool flag = DeserializeClassFromElement(_currentDocument.DocumentElement.GetChild(0), aComponent);
			_documentProcessed = true;
			if (flag)
			{
				return !_needsReserialization;
			}
			return false;
		}

		public void ApplySerializationChanges()
		{
			_serializedData = _currentDocument.GetDocumentAsString();
			_serializedGameObjects = new List<UnityEngine.Object>(_currentGameObjects);
			_serializedCustomData = new List<CustomSerializedData>(_currentCustomData);
		}

		public void SerializeFieldToElement(ObjectElement aParent, string aFieldName, Type aFieldType, object aFieldValue, bool aReserialize)
		{
			ObjectElement objectElement = null;
			if (aReserialize)
			{
				objectElement = aParent.GetChildByID(aFieldName);
			}
			if (objectElement == null)
			{
				aReserialize = false;
				objectElement = _currentDocument.CreateElement("field");
				aParent.AddChild(objectElement);
			}
			objectElement.SetAttribute("id", aFieldName);
			SerializeObjectToElement(objectElement, aFieldType, aFieldValue, aReserialize);
		}

		public bool DeserializeFieldFromElement(ObjectElement aParent, string aFieldName, string[] aOldFieldNames, Type aFieldType, object aFieldValue, out object aFieldResult)
		{
			ObjectElement childByID = aParent.GetChildByID(aFieldName);
			if (childByID == null && aFieldName.StartsWith("_"))
			{
				string aID = aFieldName.Substring(1);
				childByID = aParent.GetChildByID(aID);
				if (childByID != null)
				{
					_needsReserialization = true;
				}
			}
			if (childByID == null)
			{
				for (int i = 0; i < aOldFieldNames.Length; i++)
				{
					childByID = aParent.GetChildByID(aOldFieldNames[i]);
					if (childByID != null)
					{
						_needsReserialization = true;
						break;
					}
				}
			}
			if (childByID == null)
			{
				aFieldResult = null;
				return false;
			}
			return DeserializeObjectFromElement(childByID, aFieldType, aFieldValue, out aFieldResult);
		}

		public void SerializeObjectToElement(ObjectElement aElement, Type aType, object aValue, bool aReserialize)
		{
			if (aValue != null)
			{
				aType = aValue.GetType();
			}
			if ((aType.IsGenericType && aType.GetGenericTypeDefinition() == typeof(List<>)) || aType.IsArray)
			{
				Type type = ((!aType.IsArray) ? aType.GetGenericArguments()[0] : aType.GetElementType());
				aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aType));
				aElement.SetAttribute("elementtype", SerializeObjectToString(typeof(Type), type));
				if (aValue != null)
				{
					IList list = aValue as IList;
					for (int i = 0; i < list.Count; i++)
					{
						if (list[i] == null)
						{
							SerializeFieldToElement(aElement, "element" + i, type, null, aReserialize);
						}
						else
						{
							SerializeFieldToElement(aElement, "element" + i, list[i].GetType(), list[i], aReserialize);
						}
					}
					while (aElement.ChildCount > list.Count)
					{
						RemoveFieldElement(aElement, aElement.GetChild(aElement.ChildCount - 1));
					}
				}
				else
				{
					while (aElement.ChildCount > 0)
					{
						RemoveFieldElement(aElement, aElement.GetChild(aElement.ChildCount - 1));
					}
				}
				return;
			}
			if (aType == typeof(AnimationCurve))
			{
				aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aType));
				if (aValue == null)
				{
					aElement.SetAttribute("prewrapmode", SerializeObjectToString(typeof(WrapMode), WrapMode.Default));
					aElement.SetAttribute("postwrapmode", SerializeObjectToString(typeof(WrapMode), WrapMode.Default));
				}
				else
				{
					AnimationCurve animationCurve = aValue as AnimationCurve;
					aElement.SetAttribute("prewrapmode", SerializeObjectToString(typeof(WrapMode), animationCurve.preWrapMode));
					aElement.SetAttribute("postwrapmode", SerializeObjectToString(typeof(WrapMode), animationCurve.postWrapMode));
					Keyframe[] keys = animationCurve.keys;
					for (int j = 0; j < keys.Length; j++)
					{
						SerializeFieldToElement(aElement, "keyframe" + j, typeof(Keyframe), keys[j], aReserialize);
					}
				}
				while (aElement.ChildCount > 0)
				{
					RemoveFieldElement(aElement, aElement.GetChild(aElement.ChildCount - 1));
				}
				return;
			}
			if (aType == typeof(UnityEngine.Object) || aType.IsSubclassOf(typeof(UnityEngine.Object)))
			{
				aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aType));
				if (aReserialize && aElement.HasAttribute("gameobject"))
				{
					int aObject;
					DeserializeObjectFromString<int>(aElement.GetAttribute("gameobject"), out aObject);
					if (aObject >= 0)
					{
						_currentGameObjects[aObject] = null;
					}
				}
				if ((UnityEngine.Object)aValue == null)
				{
					aElement.SetAttribute("gameobject", "-1");
					return;
				}
				int num = -1;
				for (int k = 0; k < _currentGameObjects.Count; k++)
				{
					if ((object)_currentGameObjects[k] == null)
					{
						num = k;
						break;
					}
				}
				if (num < 0)
				{
					num = _currentGameObjects.Count;
					_currentGameObjects.Add(null);
				}
				aElement.SetAttribute("gameobject", num.ToString());
				_currentGameObjects[num] = aValue as UnityEngine.Object;
				return;
			}
			if (aType.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length > 0)
			{
				int aObject2 = -1;
				if (aReserialize && aElement.HasAttribute("reference"))
				{
					DeserializeObjectFromString<int>(aElement.GetAttribute("reference"), out aObject2);
				}
				if (aValue == null)
				{
					aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aType));
					aElement.SetAttribute("reference", "-1");
				}
				else
				{
					aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aValue.GetType()));
					aElement.SetAttribute("reference", SerializeReferenceToElement(aType, aValue, aObject2).ToString());
				}
				return;
			}
			aElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aType));
			try
			{
				aElement.SetAttribute("value", SerializeObjectToString(aType, aValue));
			}
			catch (Exception ex)
			{
				Debug.LogWarning(ex.Message + "\n" + ex.StackTrace);
			}
		}

		public bool DeserializeObjectFromElement(ObjectElement aElement, Type aType, object aValue, out object aResult)
		{
			Type type = aType;
			Type type2 = null;
			if (type.IsGenericType)
			{
				type2 = type.GetGenericArguments()[0];
				type = type.GetGenericTypeDefinition();
			}
			else if (type.IsArray)
			{
				type2 = type.GetElementType();
				type = typeof(Array);
			}
			bool result = true;
			Type aObject;
			if (!DeserializeObjectFromString<Type>(aElement.GetAttribute("type"), out aObject))
			{
				aResult = null;
				result = false;
			}
			else if (aObject != type && !aObject.IsSubclassOf(type) && aObject.GetInterface(type.Name, true) == null)
			{
				aResult = null;
				result = false;
			}
			else if (aObject == typeof(List<>) || aObject == typeof(Array))
			{
				Type aObject2;
				if (!DeserializeObjectFromString<Type>(aElement.GetAttribute("elementtype"), out aObject2))
				{
					aResult = null;
					result = false;
				}
				else if (type2 != null && aObject2 != type2 && !aObject2.IsSubclassOf(type2) && aObject2.GetInterface(type2.Name, true) == null)
				{
					aResult = null;
					result = false;
				}
				else
				{
					IList list = aValue as IList;
					if (list == null)
					{
						list = ((aObject != typeof(List<>)) ? (Activator.CreateInstance(aObject2.MakeArrayType(), 0) as IList) : (Activator.CreateInstance(typeof(List<>).MakeGenericType(aObject2)) as IList));
					}
					if (aObject == typeof(Array))
					{
						IList list2 = Activator.CreateInstance(aObject2.MakeArrayType(), aElement.ChildCount) as IList;
						for (int i = 0; i < list.Count && i < list2.Count; i++)
						{
							list2[i] = list[i];
						}
						for (int j = list.Count; j < list2.Count; j++)
						{
							list2[j] = CreateEmpty(aObject2);
						}
						list = list2;
					}
					else if (aObject == typeof(List<>))
					{
						while (list.Count > aElement.ChildCount)
						{
							list.RemoveAt(list.Count - 1);
						}
						while (list.Count < aElement.ChildCount)
						{
							list.Add(CreateEmpty(aObject2));
						}
					}
					for (int k = 0; k < aElement.ChildCount; k++)
					{
						object aResult2;
						if (DeserializeObjectFromElement(aElement.GetChild(k), aObject2, list[k], out aResult2))
						{
							list[k] = aResult2;
						}
						else
						{
							result = false;
						}
					}
					aResult = list;
				}
			}
			else if (aObject == typeof(AnimationCurve))
			{
				AnimationCurve animationCurve = new AnimationCurve();
				WrapMode aObject3;
				if (DeserializeObjectFromString<WrapMode>(aElement.GetAttribute("prewrapmode"), out aObject3))
				{
					animationCurve.preWrapMode = aObject3;
				}
				else
				{
					result = false;
				}
				if (DeserializeObjectFromString<WrapMode>(aElement.GetAttribute("postwrapmode"), out aObject3))
				{
					animationCurve.postWrapMode = aObject3;
				}
				else
				{
					result = false;
				}
				List<Keyframe> list3 = new List<Keyframe>();
				for (int l = 0; l < aElement.ChildCount; l++)
				{
					object aResult3;
					if (DeserializeObjectFromElement(aElement.GetChild(l), typeof(Keyframe), null, out aResult3))
					{
						list3.Add((Keyframe)aResult3);
					}
					else
					{
						result = false;
					}
				}
				animationCurve.keys = list3.ToArray();
				aResult = animationCurve;
			}
			else if (aObject == typeof(UnityEngine.Object) || aObject.IsSubclassOf(typeof(UnityEngine.Object)))
			{
				int aObject4;
				if (!DeserializeObjectFromString<int>(aElement.GetAttribute("gameobject"), out aObject4))
				{
					result = false;
					aObject4 = -1;
				}
				if (aObject4 < 0)
				{
					aResult = null;
				}
				else if (aObject4 >= _currentGameObjects.Count)
				{
					aResult = null;
					result = false;
				}
				else
				{
					if (_currentGameObjects[aObject4] == null)
					{
						result = false;
					}
					aResult = _currentGameObjects[aObject4];
				}
			}
			else if (aObject.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length > 0)
			{
				int aObject5;
				if (!DeserializeObjectFromString<int>(aElement.GetAttribute("reference"), out aObject5))
				{
					result = false;
					aObject5 = -1;
				}
				if (aObject5 < 0)
				{
					aResult = null;
				}
				else
				{
					if (aValue == null || aValue.GetType() != aObject)
					{
						aValue = CreateInstance(aObject);
					}
					if (aValue != null)
					{
						result = DeserializeReferenceFromElement(aObject5, aObject, aValue, out aResult);
					}
					else
					{
						aResult = null;
					}
				}
			}
			else if (aElement.HasAttribute("value"))
			{
				if (!DeserializeObjectFromString(aObject, aElement.GetAttribute("value"), out aResult))
				{
					result = false;
				}
			}
			else
			{
				aResult = CreateInstance(aObject);
				result = aResult != null;
			}
			return result;
		}

		public string SerializeObjectToString(Type aType, object aObject)
		{
			if (aType == typeof(Type) || aType.IsSubclassOf(typeof(Type)))
			{
				string text = aObject.ToString();
				if (text == "System.MonoType")
				{
					return "System.Type";
				}
				if (text.EndsWith("[]"))
				{
					text = "System.Array";
				}
				int num = text.IndexOf("`");
				if (num >= 0)
				{
					text = text.Substring(0, num);
				}
				return text;
			}
			if (aType == typeof(Vector2))
			{
				return SerializeVector2((Vector2)aObject);
			}
			if (aType == typeof(Vector3))
			{
				return SerializeVector3((Vector3)aObject);
			}
			if (aType == typeof(Vector4))
			{
				return SerializeVector4((Vector4)aObject);
			}
			if (aType == typeof(Color))
			{
				return SerializeColor((Color)aObject);
			}
			if (aType == typeof(Keyframe))
			{
				return SerializeKeyframe((Keyframe)aObject);
			}
			if (aType == typeof(LayerMask))
			{
				return (string)Convert.ChangeType((int)(LayerMask)aObject, typeof(string));
			}
			if (aType.IsEnum)
			{
				return Enum.GetName(aType, aObject);
			}
			if (aType == typeof(string))
			{
				if (aObject == null)
				{
					return "";
				}
				return (string)aObject;
			}
			if (aType.IsPrimitive)
			{
				if (aObject == null)
				{
					return (string)Convert.ChangeType(Activator.CreateInstance(aType), typeof(string));
				}
				return (string)Convert.ChangeType(aObject, typeof(string));
			}
			if (aType == typeof(object))
			{
				return null;
			}
			throw new Exception("Couldn't serialize type: " + aType.ToString());
		}

		public bool DeserializeObjectFromString<T>(string aData, out T aObject)
		{
			object aObject2;
			bool result = DeserializeObjectFromString(typeof(T), aData, out aObject2);
			aObject = (T)aObject2;
			return result;
		}

		public bool DeserializeObjectFromString(Type aType, string aData, out object aObject)
		{
			if (aType == typeof(Type))
			{
				if (aData == "System.Collections.Generic.List")
				{
					aObject = typeof(List<>);
					return true;
				}
				if (aData == "")
				{
					Debug.Log("Problem");
				}
				aObject = Type.GetType(aData);
				if (aObject == null)
				{
					aObject = typeof(UnityEngine.Object).Assembly.GetType(aData);
				}
				if (aObject == null)
				{
					Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
					Assembly[] array = assemblies;
					foreach (Assembly assembly in array)
					{
						try
						{
							aObject = assembly.GetType(aData);
							if (aObject != null)
							{
								return true;
							}
						}
						catch (Exception ex)
						{
							Debug.LogWarning(ex.Message + "\n" + ex.StackTrace);
						}
					}
				}
				if (aObject == null)
				{
					Debug.LogWarning("FieldSerializer: Couldn't deserialize type: " + aType.ToString() + " (" + aData + ")");
					return false;
				}
				return true;
			}
			if (aType == typeof(Vector2))
			{
				aObject = DeserializeVector2(aData);
			}
			else if (aType == typeof(Vector3))
			{
				aObject = DeserializeVector3(aData);
			}
			else if (aType == typeof(Vector4))
			{
				aObject = DeserializeVector4(aData);
			}
			else if (aType == typeof(Color))
			{
				aObject = DeserializeColor(aData);
			}
			else if (aType == typeof(Keyframe))
			{
				aObject = DeserializeKeyframe(aData);
			}
			else if (aType == typeof(LayerMask))
			{
				try
				{
					aObject = (LayerMask)(int)Convert.ChangeType(aData, typeof(int));
				}
				catch
				{
					Debug.LogWarning("FieldSerializer: Couldn't convert " + aData + " to " + aType.ToString());
					aObject = default(LayerMask);
					return false;
				}
			}
			else if (aType.IsEnum)
			{
				List<string> list = new List<string>(Enum.GetNames(aType));
				Array values = Enum.GetValues(aType);
				int num = list.IndexOf(aData);
				if (num < 0)
				{
					aObject = values.GetValue(0);
				}
				else
				{
					aObject = values.GetValue(num);
				}
			}
			else if (aType == typeof(string))
			{
				if (aData == null)
				{
					aObject = "";
				}
				else
				{
					aObject = aData;
				}
			}
			else
			{
				if (!aType.IsPrimitive)
				{
					if (aType == typeof(object))
					{
						aObject = null;
						return true;
					}
					Debug.LogWarning("FieldSerializer: Couldn't deserialize type: " + aType.ToString());
					aObject = null;
					return false;
				}
				try
				{
					aObject = Convert.ChangeType(aData, aType);
				}
				catch
				{
					Debug.LogWarning("FieldSerializer: Couldn't convert " + aData + " to " + aType.ToString());
					aObject = Activator.CreateInstance(aType);
					return false;
				}
			}
			return true;
		}

		public bool SetArrayCount(ObjectElement aElement, IList aList, int aCount, out IList aResult)
		{
			Type aObject;
			if (!DeserializeObjectFromString<Type>(aElement.GetAttribute("elementtype"), out aObject))
			{
				aResult = aList;
				return false;
			}
			if (aList.IsFixedSize)
			{
				IList list = Activator.CreateInstance(aObject.MakeArrayType(), aCount) as IList;
				for (int i = 0; i < aList.Count && i < list.Count; i++)
				{
					list[i] = aList[i];
				}
				for (int j = aList.Count; j < list.Count; j++)
				{
					list[j] = CreateEmpty(aObject);
				}
				aList = list;
			}
			else
			{
				while (aList.Count > aCount)
				{
					aList.RemoveAt(aList.Count - 1);
				}
				while (aList.Count < aCount)
				{
					aList.Add(CreateEmpty(aObject));
				}
			}
			while (aElement.ChildCount > aCount)
			{
				RemoveFieldElement(aElement, aElement.GetChild(aElement.ChildCount - 1));
			}
			while (aElement.ChildCount < aCount)
			{
				SerializeFieldToElement(aElement, "element" + aElement.ChildCount, aObject, aList[aElement.ChildCount], false);
			}
			aResult = aList;
			return true;
		}

		public bool InsertArrayElement(ObjectElement aElement, IList aList, int aIndex, object aValue, out IList aResult)
		{
			Type aObject;
			if (!DeserializeObjectFromString<Type>(aElement.GetAttribute("elementtype"), out aObject))
			{
				aResult = aList;
				return false;
			}
			if (aList.IsFixedSize)
			{
				IList list = Activator.CreateInstance(aObject.MakeArrayType(), aList.Count + 1) as IList;
				int num = 0;
				for (int i = 0; i < aList.Count; i++)
				{
					if (i == aIndex)
					{
						list[num++] = aValue;
					}
					list[num++] = aList[i];
				}
				aList = list;
			}
			else
			{
				aList.Insert(aIndex, aValue);
			}
			ObjectElement objectElement = _currentDocument.CreateElement("field");
			aElement.InsertChild(aIndex, objectElement);
			objectElement.SetAttribute("id", "element" + aIndex);
			SerializeObjectToElement(objectElement, aObject, aValue, false);
			for (int j = aIndex + 1; j < aElement.ChildCount; j++)
			{
				aElement.GetChild(j).SetAttribute("id", "element" + j);
			}
			aResult = aList;
			return true;
		}

		public bool DeleteArrayElement(ObjectElement aElement, IList aList, int aIndex, out IList aResult)
		{
			Type aObject;
			if (!DeserializeObjectFromString<Type>(aElement.GetAttribute("elementtype"), out aObject))
			{
				aResult = aList;
				return false;
			}
			if (aList.IsFixedSize)
			{
				IList list = Activator.CreateInstance(aObject.MakeArrayType(), aList.Count - 1) as IList;
				int num = 0;
				for (int i = 0; i < aList.Count; i++)
				{
					if (i != aIndex)
					{
						list[num++] = aList[i];
					}
				}
				aList = list;
			}
			else
			{
				aList.RemoveAt(aIndex);
			}
			RemoveFieldElement(aElement, aElement.GetChild(aIndex));
			for (int j = aIndex; j < aElement.ChildCount; j++)
			{
				aElement.GetChild(j).SetAttribute("id", "element" + j);
			}
			aResult = aList;
			return true;
		}

		public void ChangeAllTypes(string aFromType, string aToType)
		{
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			if (child.ChildCount == 0)
			{
				return;
			}
			for (ObjectElement objectElement = child.GetChild(0); objectElement != null; objectElement = ((objectElement.ChildCount <= 0) ? ((objectElement.NextSibling != null) ? objectElement.NextSibling : objectElement.Parent.NextSibling) : objectElement.GetChild(0)))
			{
				if (objectElement.GetAttribute("type") == aFromType)
				{
					objectElement.SetAttribute("type", aToType);
				}
				if (objectElement.GetAttribute("elementtype") == aFromType)
				{
					objectElement.SetAttribute("elementtype", aToType);
				}
			}
		}

		public List<ObjectElement> GetFieldElementsForType(string aType)
		{
			List<ObjectElement> list = new List<ObjectElement>();
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			if (child.ChildCount == 0)
			{
				return list;
			}
			for (ObjectElement objectElement = child.GetChild(0); objectElement != null; objectElement = ((objectElement.ChildCount <= 0) ? ((objectElement.NextSibling != null) ? objectElement.NextSibling : objectElement.Parent.NextSibling) : objectElement.GetChild(0)))
			{
				if (objectElement.Name == "field" && objectElement.GetAttribute("type") == aType)
				{
					list.Add(objectElement);
				}
			}
			return list;
		}

		private void SerializeClassToElement(ObjectElement aParent, object aObject, bool aReserialize)
		{
			Type type = aObject.GetType();
			if ((!type.IsSubclassOf(typeof(RAINComponent)) && !type.IsSubclassOf(typeof(RAINScriptableObject)) && type.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length == 0) || type.IsAbstract)
			{
				return;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false);
			if (customAttributes.Length > 0 && (customAttributes[0] as RAINSerializableClassAttribute).OverrideSerialization)
			{
				if (aObject == null)
				{
					return;
				}
				try
				{
					SerializeClassDelegate serializeClassDelegate = Delegate.CreateDelegate(typeof(SerializeClassDelegate), aObject, (customAttributes[0] as RAINSerializableClassAttribute).SerializeClassCallback) as SerializeClassDelegate;
					SerializeOverriddenClass(aParent, serializeClassDelegate(), aReserialize);
					return;
				}
				catch (Exception ex)
				{
					Debug.LogWarning(ex.Message + "\n" + ex.StackTrace);
					return;
				}
			}
			Stack<Type> stack = new Stack<Type>();
			while (type != null && type != typeof(RAINComponent) && type != typeof(RAINScriptableObject) && type != typeof(RAINElement))
			{
				stack.Push(type);
				type = type.BaseType;
			}
			while (stack.Count > 0)
			{
				type = stack.Pop();
				FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < fields.Length; i++)
				{
					if (fields[i].GetCustomAttributes(typeof(RAINNonSerializableFieldAttribute), false).Length > 0 || (!fields[i].IsPublic && fields[i].GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false).Length == 0))
					{
						continue;
					}
					object[] customAttributes2 = fields[i].GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false);
					if (customAttributes2.Length > 0 && (customAttributes2[0] as RAINSerializableFieldAttribute).OverrideSerialization)
					{
						try
						{
							SerializeFieldDelegate serializeFieldDelegate = Delegate.CreateDelegate(typeof(SerializeFieldDelegate), aObject, (customAttributes2[0] as RAINSerializableFieldAttribute).SerializeFieldCallback) as SerializeFieldDelegate;
							SerializeOverriddenField(aParent, fields[i].Name, fields[i].FieldType, serializeFieldDelegate(), aReserialize);
						}
						catch (Exception ex2)
						{
							Debug.LogWarning(ex2.Message + "\n" + ex2.StackTrace);
						}
						continue;
					}
					object value = fields[i].GetValue(aObject);
					if (fields[i].FieldType == typeof(string) && value == null)
					{
						fields[i].SetValue(aObject, "");
						value = fields[i].GetValue(aObject);
					}
					SerializeFieldToElement(aParent, fields[i].Name, fields[i].FieldType, value, aReserialize);
				}
			}
		}

		private bool DeserializeClassFromElement(ObjectElement aParent, object aObject)
		{
			Type type = aObject.GetType();
			if (!type.IsSubclassOf(typeof(RAINComponent)) && !type.IsSubclassOf(typeof(RAINScriptableObject)) && type.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length == 0)
			{
				return true;
			}
			object[] customAttributes = type.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false);
			if (customAttributes.Length > 0 && (customAttributes[0] as RAINSerializableClassAttribute).OverrideSerialization)
			{
				byte[] aData;
				if (DeserializeOverriddenClass(aParent, out aData))
				{
					try
					{
						DeserializeClassDelegate deserializeClassDelegate = Delegate.CreateDelegate(typeof(DeserializeClassDelegate), aObject, (customAttributes[0] as RAINSerializableClassAttribute).DeserializeClassCallback) as DeserializeClassDelegate;
						deserializeClassDelegate(aData);
						return true;
					}
					catch (Exception ex)
					{
						Debug.LogWarning(ex.Message + "\n" + ex.StackTrace);
					}
				}
				return false;
			}
			Stack<Type> stack = new Stack<Type>();
			while (type != null && type != typeof(RAINComponent) && type != typeof(RAINScriptableObject) && type != typeof(RAINElement))
			{
				stack.Push(type);
				type = type.BaseType;
			}
			if (stack.Count == 0)
			{
				return true;
			}
			bool result = true;
			while (stack.Count > 0)
			{
				type = stack.Pop();
				FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < fields.Length; i++)
				{
					if (fields[i].GetCustomAttributes(typeof(RAINNonSerializableFieldAttribute), false).Length > 0 || (!fields[i].IsPublic && fields[i].GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false).Length == 0))
					{
						continue;
					}
					object[] customAttributes2 = fields[i].GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false);
					if (customAttributes2.Length > 0 && (customAttributes2[0] as RAINSerializableFieldAttribute).OverrideSerialization)
					{
						try
						{
							byte[] aData2;
							if (DeserializeOverriddenField(aParent, fields[i].Name, fields[i].FieldType, out aData2))
							{
								DeserializeFieldDelegate deserializeFieldDelegate = Delegate.CreateDelegate(typeof(DeserializeFieldDelegate), aObject, (customAttributes2[0] as RAINSerializableFieldAttribute).DeserializeFieldCallback) as DeserializeFieldDelegate;
								deserializeFieldDelegate(aData2);
							}
							else
							{
								result = false;
							}
						}
						catch (Exception ex2)
						{
							Debug.LogWarning(ex2.Message + "\n" + ex2.StackTrace);
							result = false;
						}
					}
					else
					{
						string[] aOldFieldNames = new string[0];
						if (customAttributes2.Length > 0)
						{
							aOldFieldNames = (customAttributes2[0] as RAINSerializableFieldAttribute).OldFieldNames;
						}
						object aFieldResult;
						if (DeserializeFieldFromElement(aParent, fields[i].Name, aOldFieldNames, fields[i].FieldType, fields[i].GetValue(aObject), out aFieldResult))
						{
							fields[i].SetValue(aObject, aFieldResult);
						}
						else
						{
							result = false;
						}
					}
				}
			}
			return result;
		}

		private void SerializeOverriddenClass(ObjectElement aClass, byte[] aData, bool aReserialize)
		{
			int aObject = -1;
			if (aReserialize && aClass.HasAttribute("customdata"))
			{
				DeserializeObjectFromString<int>(aClass.GetAttribute("customdata"), out aObject);
			}
			if (aObject < 0)
			{
				aClass.SetAttribute("customdata", _currentCustomData.Count.ToString());
				_currentCustomData.Add(new CustomSerializedData(aData));
			}
			else
			{
				_currentCustomData[aObject].Data = aData;
			}
		}

		private bool DeserializeOverriddenClass(ObjectElement aClass, out byte[] aData)
		{
			int aObject;
			if (!DeserializeObjectFromString<int>(aClass.GetAttribute("customdata"), out aObject))
			{
				Debug.LogWarning("FieldSerializer: Couldn't deserialize custom data for overridden class: " + aClass.GetAttribute("type"));
				aData = new byte[0];
				return false;
			}
			if (aObject >= _currentCustomData.Count)
			{
				aData = new byte[0];
				return false;
			}
			aData = _currentCustomData[aObject].Data;
			return true;
		}

		private void SerializeOverriddenField(ObjectElement aParent, string aFieldName, Type aFieldType, byte[] aData, bool aReserialize)
		{
			ObjectElement objectElement = aParent.GetChildByID(aFieldName);
			if (objectElement == null)
			{
				objectElement = _currentDocument.CreateElement("field");
				aParent.AddChild(objectElement);
			}
			int aObject = -1;
			if (aReserialize && objectElement.HasAttribute("customdata") && !DeserializeObjectFromString<int>(objectElement.GetAttribute("customdata"), out aObject))
			{
				aObject = -1;
			}
			objectElement.SetAttribute("id", aFieldName);
			objectElement.SetAttribute("type", SerializeObjectToString(typeof(Type), aFieldType));
			if (aObject < 0)
			{
				objectElement.SetAttribute("customdata", _currentCustomData.Count.ToString());
				_currentCustomData.Add(new CustomSerializedData(aData));
			}
			else
			{
				_currentCustomData[aObject].Data = aData;
			}
		}

		private bool DeserializeOverriddenField(ObjectElement aParent, string aFieldName, Type aFieldType, out byte[] aData)
		{
			ObjectElement childByID = aParent.GetChildByID(aFieldName);
			if (childByID == null)
			{
				Debug.LogWarning("FieldSerializer: Couldn't find overridden field: " + aFieldName);
				aData = new byte[0];
				return false;
			}
			int aObject;
			if (!DeserializeObjectFromString<int>(childByID.GetAttribute("customdata"), out aObject))
			{
				Debug.LogWarning("FieldSerializer: Couldn't deserialize custom data for overridden field: " + aFieldName);
				aData = new byte[0];
				return false;
			}
			aData = _currentCustomData[aObject].Data;
			return true;
		}

		private int SerializeReferenceToElement(Type aType, object aValue, int aRefIndex)
		{
			if (aRefIndex >= 0 && _currentReferences[aRefIndex] != aValue)
			{
				RemoveReference(aRefIndex);
				aRefIndex = -1;
			}
			if (aRefIndex < 0)
			{
				aRefIndex = GetReference(aValue);
			}
			bool aReserialize = false;
			if (aRefIndex < 0)
			{
				aRefIndex = AddReference(aValue);
			}
			else
			{
				aReserialize = true;
			}
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			ObjectElement child2 = child.GetChild(aRefIndex);
			child2.SetAttribute("id", aRefIndex.ToString());
			child2.SetAttribute("type", SerializeObjectToString(typeof(Type), aValue.GetType()));
			if (!_recursiveReferences.Contains(aRefIndex))
			{
				_recursiveReferences.AddLast(aRefIndex);
				SerializeClassToElement(child2, aValue, aReserialize);
				_recursiveReferences.RemoveLast();
			}
			return aRefIndex;
		}

		private bool DeserializeReferenceFromElement(int aRefIndex, Type aType, object aValue, out object aResult)
		{
			while (_currentReferences.Count <= aRefIndex)
			{
				_currentReferences.Add(null);
			}
			if (_currentReferences[aRefIndex] == null)
			{
				_currentReferences[aRefIndex] = aValue;
				aResult = _currentReferences[aRefIndex];
				ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
				ObjectElement child2 = child.GetChild(aRefIndex);
				if (!DeserializeClassFromElement(child2, _currentReferences[aRefIndex]))
				{
					_needsReserialization = true;
				}
			}
			else
			{
				aResult = _currentReferences[aRefIndex];
			}
			return true;
		}

		private int GetReference(object aValue)
		{
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			int num = _currentReferences.IndexOf(aValue);
			if (num >= 0)
			{
				ObjectElement child2 = child.GetChild(num);
				int aObject;
				DeserializeObjectFromString<int>(child2.GetAttribute("refcount"), out aObject);
				child2.SetAttribute("refcount", (aObject + 1).ToString());
			}
			return num;
		}

		private int AddReference(object aValue)
		{
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			int num;
			if (_holdReferences)
			{
				while (_currentReferences.Count < child.ChildCount)
				{
					_currentReferences.Add(null);
				}
				num = _currentReferences.Count;
				_currentReferences.Add(aValue);
			}
			else
			{
				num = _currentReferences.IndexOf(null);
				if (num < 0)
				{
					num = _currentReferences.Count;
					_currentReferences.Add(aValue);
				}
				else
				{
					_currentReferences[num] = aValue;
				}
			}
			ObjectElement objectElement = _currentDocument.CreateElement("reference");
			objectElement.SetAttribute("refcount", "1");
			if (num == child.ChildCount)
			{
				child.AddChild(objectElement);
			}
			else
			{
				child.ReplaceChild(num, objectElement);
			}
			return num;
		}

		private void RemoveReference(int aRefIndex)
		{
			ObjectElement child = _currentDocument.DocumentElement.GetChild(1);
			ObjectElement child2 = child.GetChild(aRefIndex);
			int aObject;
			DeserializeObjectFromString<int>(child2.GetAttribute("refcount"), out aObject);
			aObject--;
			child2.SetAttribute("refcount", aObject.ToString());
			if (aObject != 0)
			{
				return;
			}
			if (aRefIndex < _currentReferences.Count)
			{
				_currentReferences[aRefIndex] = null;
			}
			if (!_holdReferences)
			{
				while (_currentReferences.Count > 0 && _currentReferences[_currentReferences.Count - 1] == null)
				{
					_currentReferences.RemoveAt(_currentReferences.Count - 1);
				}
			}
			while (child2.ChildCount > 0)
			{
				RemoveFieldElement(child2, child2.GetChild(0));
			}
		}

		private void RemoveFieldElement(ObjectElement aParent, ObjectElement aChild)
		{
			Type aObject;
			DeserializeObjectFromString<Type>(aChild.GetAttribute("type"), out aObject);
			if (aObject == typeof(UnityEngine.Object) || aObject.IsSubclassOf(typeof(UnityEngine.Object)))
			{
				int aObject2;
				DeserializeObjectFromString<int>(aChild.GetAttribute("gameobject"), out aObject2);
				if (aObject2 >= 0)
				{
					_currentGameObjects[aObject2] = null;
				}
				while (_currentGameObjects.Count > 0 && _currentGameObjects[_currentGameObjects.Count - 1] == null)
				{
					_currentGameObjects.RemoveAt(_currentGameObjects.Count - 1);
				}
			}
			else if (aObject.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length > 0)
			{
				int aObject3;
				DeserializeObjectFromString<int>(aChild.GetAttribute("reference"), out aObject3);
				if (aObject3 >= 0)
				{
					RemoveReference(aObject3);
				}
			}
			aParent.RemoveChild(aChild);
		}

		private string SerializeVector2(Vector2 aVector)
		{
			return aVector.x + "," + aVector.y;
		}

		private Vector2 DeserializeVector2(string aVector)
		{
			try
			{
				string[] array = aVector.Split(',');
				Vector2 result = default(Vector2);
				result.x = float.Parse(array[0]);
				result.y = float.Parse(array[1]);
				return result;
			}
			catch
			{
				Debug.LogWarning("FieldSerializer: Problem deserializing Vector2");
				return Vector2.zero;
			}
		}

		private string SerializeVector3(Vector3 aVector)
		{
			return aVector.x + "," + aVector.y + "," + aVector.z;
		}

		private Vector3 DeserializeVector3(string aVector)
		{
			try
			{
				string[] array = aVector.Split(',');
				Vector3 result = default(Vector3);
				result.x = float.Parse(array[0]);
				result.y = float.Parse(array[1]);
				result.z = float.Parse(array[2]);
				return result;
			}
			catch
			{
				Debug.LogWarning("FieldSerializer: Problem deserializing Vector3");
				return Vector3.zero;
			}
		}

		private string SerializeVector4(Vector4 aVector)
		{
			return aVector.x + "," + aVector.y + "," + aVector.z + "," + aVector.w;
		}

		private Vector4 DeserializeVector4(string aVector)
		{
			try
			{
				string[] array = aVector.Split(',');
				Vector4 result = default(Vector4);
				result[0] = float.Parse(array[0]);
				result[1] = float.Parse(array[1]);
				result[2] = float.Parse(array[2]);
				result[3] = float.Parse(array[3]);
				return result;
			}
			catch
			{
				Debug.LogWarning("FieldSerializer: Problem deserializing Vector4");
				return Vector4.zero;
			}
		}

		private string SerializeColor(Color aColor)
		{
			return aColor.r + "," + aColor.g + "," + aColor.b + "," + aColor.a;
		}

		private Color DeserializeColor(string aColor)
		{
			try
			{
				string[] array = aColor.Split(',');
				Color result = default(Color);
				result[0] = float.Parse(array[0]);
				result[1] = float.Parse(array[1]);
				result[2] = float.Parse(array[2]);
				result[3] = float.Parse(array[3]);
				return result;
			}
			catch
			{
				Debug.LogWarning("FieldSerializer: Problem deserializing Color");
				return Color.black;
			}
		}

		private string SerializeKeyframe(Keyframe aKeyframe)
		{
			return aKeyframe.time + "," + aKeyframe.value + "," + aKeyframe.inTangent + "," + aKeyframe.outTangent;
		}

		private Keyframe DeserializeKeyframe(string aKeyframe)
		{
			try
			{
				string[] array = aKeyframe.Split(',');
				return new Keyframe(float.Parse(array[0]), float.Parse(array[1]), float.Parse(array[2]), float.Parse(array[3]));
			}
			catch
			{
				Debug.LogWarning("FieldSerializer: Problem deserializing Keyframe");
				return default(Keyframe);
			}
		}

		private object CreateEmpty(Type aType)
		{
			if (aType == typeof(string))
			{
				return "";
			}
			if (aType.IsPrimitive || aType.IsEnum)
			{
				return Activator.CreateInstance(aType);
			}
			return null;
		}

		private object CreateInstance(Type aType)
		{
			if (aType == typeof(string))
			{
				return "";
			}
			if (aType.IsPrimitive || aType.IsEnum)
			{
				return Activator.CreateInstance(aType);
			}
			ConstructorInfo constructor = aType.GetConstructor(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, new Type[0], new ParameterModifier[0]);
			if (constructor == null)
			{
				Debug.LogWarning("FieldSerializer: Could not find public or private parameterless constructor for type: " + aType.ToString());
				return null;
			}
			return constructor.Invoke(new object[0]);
		}
	}
}
