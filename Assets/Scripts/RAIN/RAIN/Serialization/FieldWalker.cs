using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using RAIN.Core;
using UnityEngine;

namespace RAIN.Serialization
{
	[Serializable]
	public class FieldWalker
	{
		public static bool EnableToolTips = true;

		private RAINComponent _target;

		private ObjectElement _document;

		private ObjectElement _fieldGroup;

		private ObjectElement _referenceGroup;

		private Stack<ObjectElement> _currentElement = new Stack<ObjectElement>();

		private Stack<FieldInfo> _currentField = new Stack<FieldInfo>();

		private Stack<object> _currentObject = new Stack<object>();

		private ObjectElement _reference;

		private string _path = "";

		private static object _invalidValue = new object();

		private static object _endOfDocumentValue = new object();

		public RAINComponent Target
		{
			get
			{
				return _target;
			}
		}

		public FieldVisibility Visibility
		{
			get
			{
				if (_currentField.Peek() != null)
				{
					object[] customAttributes = _currentField.Peek().GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false);
					if (customAttributes.Length > 0)
					{
						return (customAttributes[0] as RAINSerializableFieldAttribute).Visibility;
					}
				}
				return FieldVisibility.Show;
			}
		}

		public string FieldName
		{
			get
			{
				return _currentElement.Peek().GetAttribute("id");
			}
		}

		public string PrettyFieldName
		{
			get
			{
				string fieldName = FieldName;
				fieldName = fieldName.Trim('_');
				if (fieldName.Length == 0)
				{
					return "";
				}
				fieldName = char.ToUpper(fieldName[0]) + fieldName.Substring(1);
				string text = fieldName[0].ToString();
				for (int i = 1; i < fieldName.Length; i++)
				{
					text = (((!char.IsLower(fieldName[i - 1]) || !char.IsUpper(fieldName[i])) && (!char.IsLetter(fieldName[i - 1]) || char.IsLetter(fieldName[i]))) ? ((fieldName[i] != '_') ? (text + fieldName[i]) : (text + " ")) : (text + " " + fieldName[i]));
				}
				text = text.Replace(" As ", " as ");
				text = text.Replace(" In ", " in ");
				text = text.Replace(" Of ", " of ");
				text = text.Replace(" By ", " by ");
				return text.Replace(" To ", " to ");
			}
		}

		public string ToolTip
		{
			get
			{
				if (!EnableToolTips)
				{
					return "";
				}
				if (_currentField.Peek() != null)
				{
					object[] customAttributes = _currentField.Peek().GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false);
					if (customAttributes.Length > 0)
					{
						return (customAttributes[0] as RAINSerializableFieldAttribute).ToolTip;
					}
				}
				return "";
			}
		}

		public Type FieldType
		{
			get
			{
				if (_currentField.Peek() == null)
				{
					object t = _currentObject.Pop();
					Type type = _currentObject.Peek().GetType();
					_currentObject.Push(t);
					if (type.IsArray)
					{
						return type.GetElementType();
					}
					return type.GetGenericArguments()[0];
				}
				return _currentField.Peek().FieldType;
			}
		}

		public Type FieldValueType
		{
			get
			{
				Type aObject;
				_target.DataSerializer.DeserializeObjectFromString<Type>(_currentElement.Peek().GetAttribute("type"), out aObject);
				if (aObject == typeof(List<>))
				{
					Type aObject2;
					_target.DataSerializer.DeserializeObjectFromString<Type>(_currentElement.Peek().GetAttribute("elementtype"), out aObject2);
					return aObject.MakeGenericType(aObject2);
				}
				if (aObject == typeof(Array))
				{
					Type aObject3;
					_target.DataSerializer.DeserializeObjectFromString<Type>(_currentElement.Peek().GetAttribute("elementtype"), out aObject3);
					return aObject3.MakeArrayType();
				}
				if (aObject == null)
				{
					throw new Exception("Unsupported type: " + _currentElement.Peek().GetAttribute("type"));
				}
				return aObject;
			}
		}

		public bool IsFieldArray
		{
			get
			{
				Type aObject;
				_target.DataSerializer.DeserializeObjectFromString<Type>(_currentElement.Peek().GetAttribute("type"), out aObject);
				if (aObject != typeof(List<>))
				{
					return aObject == typeof(Array);
				}
				return true;
			}
		}

		public bool IsFieldValid
		{
			get
			{
				return _currentObject.Peek() != _invalidValue;
			}
		}

		public bool IsFieldCustom
		{
			get
			{
				return _currentElement.Peek().HasAttribute("customdata");
			}
		}

		public bool IsFieldNull
		{
			get
			{
				return _currentObject.Peek() == null;
			}
		}

		public int ChildCount
		{
			get
			{
				if (_reference != null)
				{
					return _reference.ChildCount;
				}
				return _currentElement.Peek().ChildCount;
			}
		}

		public bool ChildrenVisible
		{
			get
			{
				return _currentElement.Peek().GetAttribute("childrenvisible") == "True";
			}
			set
			{
				if (value)
				{
					_currentElement.Peek().SetAttribute("childrenvisible", "True");
				}
				else
				{
					_currentElement.Peek().SetAttribute("childrenvisible", "False");
				}
			}
		}

		public bool HasSibling
		{
			get
			{
				return _currentElement.Peek().NextSibling != null;
			}
		}

		public bool IsFirstReference
		{
			get
			{
				if (_reference == null)
				{
					return true;
				}
				int num = 0;
				foreach (ObjectElement item in _currentElement)
				{
					ObjectElement reference = GetReference(item);
					if (reference != null && reference.GetAttribute("id") == _reference.GetAttribute("id"))
					{
						num++;
						if (num > 1)
						{
							return false;
						}
					}
				}
				return true;
			}
		}

		public bool IsStartOfDocument
		{
			get
			{
				return _currentObject.Peek() == _target;
			}
		}

		public bool IsEndOfDocument
		{
			get
			{
				return _currentObject.Peek() == _endOfDocumentValue;
			}
		}

		public string Path
		{
			get
			{
				return _path;
			}
		}

		public FieldWalker()
		{
		}

		public FieldWalker(RAINComponent aTarget)
		{
			_target = aTarget;
			Reset();
		}

		public void Reset()
		{
			_target.UpdateSerialization();
			_document = _target.DataSerializer.CurrentDocument.DocumentElement;
			_fieldGroup = _document.GetChild(0);
			_referenceGroup = _document.GetChild(1);
			_currentElement.Clear();
			_currentElement.Push(_fieldGroup);
			_currentField.Clear();
			_currentField.Push(null);
			_currentObject.Clear();
			_currentObject.Push(_target);
			_reference = null;
			_path = "/" + _document.Name + "/" + _fieldGroup.Name;
		}

		public FieldWalker Copy(bool aStartOver = false)
		{
			FieldWalker fieldWalker = new FieldWalker();
			fieldWalker._target = _target;
			if (aStartOver)
			{
				fieldWalker.Reset();
			}
			else
			{
				fieldWalker._document = _document;
				fieldWalker._fieldGroup = _fieldGroup;
				fieldWalker._referenceGroup = _referenceGroup;
				ObjectElement[] array = _currentElement.ToArray();
				for (int num = array.Length - 1; num >= 0; num--)
				{
					fieldWalker._currentElement.Push(array[num]);
				}
				FieldInfo[] array2 = _currentField.ToArray();
				for (int num2 = array2.Length - 1; num2 >= 0; num2--)
				{
					fieldWalker._currentField.Push(array2[num2]);
				}
				object[] array3 = _currentObject.ToArray();
				for (int num3 = array3.Length - 1; num3 >= 0; num3--)
				{
					fieldWalker._currentObject.Push(array3[num3]);
				}
				fieldWalker._reference = _reference;
				fieldWalker._path = _path;
			}
			return fieldWalker;
		}

		public bool FirstChild()
		{
			ValidateElements();
			if (IsEndOfDocument)
			{
				return false;
			}
			if (ChildCount == 0 || !IsFieldValid || IsFieldCustom || !IsFirstReference)
			{
				return false;
			}
			if (_reference != null)
			{
				_currentElement.Push(_reference.GetChild(0));
			}
			else
			{
				_currentElement.Push(_currentElement.Peek().GetChild(0));
			}
			_reference = GetReference(_currentElement.Peek());
			object obj = _currentObject.Peek();
			Type type = obj.GetType();
			if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
			{
				_currentField.Push(null);
				_currentObject.Push((obj as IList)[0]);
			}
			else
			{
				FieldInfo fieldInfo = GetFieldInfo(type, _currentElement.Peek().GetAttribute("id"));
				if (fieldInfo == null)
				{
					_currentField.Push(null);
					_currentObject.Push(_invalidValue);
				}
				else
				{
					_currentField.Push(fieldInfo);
					_currentObject.Push(fieldInfo.GetValue(obj));
				}
			}
			_path = ResolvePath(_currentElement.Peek());
			if (!IsFieldValid || IsFieldCustom)
			{
				return NextSibling();
			}
			return true;
		}

		public bool NextSibling()
		{
			ValidateElements();
			if (IsStartOfDocument || IsEndOfDocument)
			{
				return false;
			}
			if (_currentElement.Peek().NextSibling == null)
			{
				return false;
			}
			ObjectElement nextSibling = _currentElement.Peek().NextSibling;
			_currentElement.Pop();
			_currentField.Pop();
			_currentObject.Pop();
			_currentElement.Push(nextSibling);
			_reference = GetReference(nextSibling);
			object obj = _currentObject.Peek();
			Type type = obj.GetType();
			if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
			{
				int index = int.Parse(nextSibling.GetAttribute("id").Substring("element".Length));
				_currentField.Push(null);
				_currentObject.Push((obj as IList)[index]);
			}
			else
			{
				FieldInfo fieldInfo = GetFieldInfo(type, _currentElement.Peek().GetAttribute("id"));
				if (fieldInfo == null)
				{
					_currentField.Push(null);
					_currentObject.Push(_invalidValue);
				}
				else
				{
					_currentField.Push(fieldInfo);
					_currentObject.Push(fieldInfo.GetValue(obj));
				}
			}
			_path = ResolvePath(_currentElement.Peek());
			if (!IsFieldValid || IsFieldCustom)
			{
				return NextSibling();
			}
			return true;
		}

		public FieldWalker FindFieldInDocument(string aField)
		{
			FieldWalker fieldWalker = Copy(true);
			if (fieldWalker.GoToPath("field." + aField))
			{
				return fieldWalker;
			}
			return null;
		}

		public FieldWalker FindFieldInChildren(string aField, bool aRecursive = false)
		{
			FieldWalker fieldWalker = Copy();
			if (fieldWalker.GoToPath(aRecursive ? ("field." + aField) : (_path + "/field." + aField)))
			{
				return fieldWalker;
			}
			return null;
		}

		public void SetFieldValue(object aValue)
		{
			_target.DataSerializer.SerializeObjectToElement(_currentElement.Peek(), FieldValueType, aValue, true);
			_reference = GetReference(_currentElement.Peek());
			_currentObject.Pop();
			object obj = _currentObject.Peek();
			Type type = obj.GetType();
			if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
			{
				int index = int.Parse(_currentElement.Peek().GetAttribute("id").Substring("element".Length));
				(obj as IList)[index] = aValue;
				_currentObject.Push(aValue);
			}
			else
			{
				_currentField.Peek().SetValue(obj, aValue);
				_currentObject.Push(aValue);
			}
		}

		public object GetFieldValue()
		{
			_currentObject.Pop();
			object obj = _currentObject.Peek();
			Type type = obj.GetType();
			object obj2;
			if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
			{
				int index = int.Parse(_currentElement.Peek().GetAttribute("id").Substring("element".Length));
				obj2 = (obj as IList)[index];
			}
			else
			{
				obj2 = _currentField.Peek().GetValue(obj);
			}
			_currentObject.Push(obj2);
			return obj2;
		}

		public void SetArrayCount(int aCount)
		{
			if (!(_currentObject.Peek() is IList))
			{
				Debug.LogWarning("FieldWalker: Calling SetArrayCount on type: " + FieldValueType);
				return;
			}
			IList aResult = _currentObject.Pop() as IList;
			_target.DataSerializer.SetArrayCount(_currentElement.Peek(), aResult, aCount, out aResult);
			_currentField.Peek().SetValue(_currentObject.Peek(), aResult);
			_currentObject.Push(aResult);
		}

		public int GetArrayCount()
		{
			if (!(_currentObject.Peek() is IList))
			{
				Debug.LogWarning("FieldWalker: Calling GetArrayCount on type: " + FieldValueType);
				return 0;
			}
			return (_currentObject.Peek() as IList).Count;
		}

		public void DeleteArrayElement(int aIndex)
		{
			if (!(_currentObject.Peek() is IList))
			{
				Debug.LogWarning("FieldWalker: Calling DeleteArrayElement on type: " + FieldValueType);
				return;
			}
			IList aResult = _currentObject.Pop() as IList;
			_target.DataSerializer.DeleteArrayElement(_currentElement.Peek(), aResult, aIndex, out aResult);
			_currentField.Peek().SetValue(_currentObject.Peek(), aResult);
			_currentObject.Push(aResult);
			_reference = GetReference(_currentElement.Peek());
		}

		public void InsertArrayElement(int aIndex, object aValue)
		{
			if (!(_currentObject.Peek() is IList))
			{
				Debug.LogWarning("FieldWalker: Calling InsertArrayElement on type: " + FieldValueType);
				return;
			}
			IList aResult = _currentObject.Pop() as IList;
			_target.DataSerializer.InsertArrayElement(_currentElement.Peek(), aResult, aIndex, aValue, out aResult);
			_currentField.Peek().SetValue(_currentObject.Peek(), aResult);
			_currentObject.Push(aResult);
			_reference = GetReference(_currentElement.Peek());
		}

		public bool GoToPath(string aPath)
		{
			if (aPath.StartsWith("/"))
			{
				do
				{
					if (_path == aPath)
					{
						return true;
					}
				}
				while (Next(aPath.StartsWith(_path)));
			}
			else
			{
				do
				{
					if (_path.EndsWith(aPath))
					{
						return true;
					}
				}
				while (Next(true));
			}
			return false;
		}

		private bool Up()
		{
			if (IsStartOfDocument || IsEndOfDocument)
			{
				return false;
			}
			_currentElement.Pop();
			_currentField.Pop();
			_currentObject.Pop();
			if (_currentElement.Peek() == _fieldGroup)
			{
				_currentElement.Pop();
				_currentField.Pop();
				_currentObject.Pop();
			}
			if (_currentElement.Count == 0)
			{
				_currentElement.Push(null);
				_currentField.Push(null);
				_currentObject.Push(_endOfDocumentValue);
				_path = "";
				return false;
			}
			_path = ResolvePath(_currentElement.Peek());
			return true;
		}

		private bool Next(bool aFollowChildren)
		{
			ValidateElements();
			if (IsStartOfDocument)
			{
				aFollowChildren = true;
			}
			if (IsEndOfDocument)
			{
				return false;
			}
			if (aFollowChildren && ChildCount > 0 && IsFieldValid && !IsFieldCustom && IsFirstReference)
			{
				if (_reference != null)
				{
					_currentElement.Push(_reference.GetChild(0));
				}
				else
				{
					_currentElement.Push(_currentElement.Peek().GetChild(0));
				}
				_reference = GetReference(_currentElement.Peek());
				object obj = _currentObject.Peek();
				Type type = obj.GetType();
				if (type.IsArray || (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>)))
				{
					_currentField.Push(null);
					_currentObject.Push((obj as IList)[0]);
				}
				else
				{
					FieldInfo fieldInfo = GetFieldInfo(type, _currentElement.Peek().GetAttribute("id"));
					if (fieldInfo == null)
					{
						_currentField.Push(null);
						_currentObject.Push(_invalidValue);
					}
					else
					{
						_currentField.Push(fieldInfo);
						_currentObject.Push(fieldInfo.GetValue(obj));
					}
				}
			}
			else
			{
				while (_currentElement.Peek().NextSibling == null && Up())
				{
				}
				if (IsEndOfDocument)
				{
					return false;
				}
				NextSibling();
			}
			_path = ResolvePath(_currentElement.Peek());
			if (!IsFieldValid || IsFieldCustom)
			{
				return Next(false);
			}
			return true;
		}

		private void ValidateElements()
		{
			if (IsStartOfDocument || IsEndOfDocument || !IsFieldValid || IsFieldCustom)
			{
				return;
			}
			Type fieldValueType = FieldValueType;
			if (_currentObject.Peek() != null)
			{
				Type type = _currentObject.Peek().GetType();
				if (type != fieldValueType && !type.IsSubclassOf(fieldValueType))
				{
					_target.DataSerializer.SerializeObjectToElement(_currentElement.Peek(), fieldValueType, _currentObject.Peek(), true);
					return;
				}
			}
			if (fieldValueType.IsArray || (fieldValueType.IsGenericType && fieldValueType.GetGenericTypeDefinition() == typeof(List<>)))
			{
				IList list = _currentObject.Peek() as IList;
				if (list.Count != ChildCount)
				{
					SetArrayCount(list.Count);
				}
			}
		}

		private string ResolvePath(ObjectElement aElement)
		{
			string text = "";
			foreach (ObjectElement item in _currentElement)
			{
				text = ((!item.HasAttribute("id")) ? ("/" + item.Name + text) : ("/" + item.Name + "." + item.GetAttribute("id") + text));
			}
			return "/" + _document.Name + text;
		}

		private ObjectElement GetReference(ObjectElement aElement)
		{
			if (!aElement.HasAttribute("reference") || aElement.GetAttribute("reference") == "-1")
			{
				return null;
			}
			return _referenceGroup.GetChild(int.Parse(aElement.GetAttribute("reference")));
		}

		private FieldInfo GetFieldInfo(Type aType, string aField)
		{
			FieldInfo fieldInfo = null;
			Type type = aType;
			do
			{
				fieldInfo = type.GetField(aField, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				type = type.BaseType;
			}
			while (fieldInfo == null && type != null && (type.IsSubclassOf(typeof(RAINComponent)) || type.GetCustomAttributes(typeof(RAINSerializableClassAttribute), false).Length > 0));
			if (fieldInfo != null && fieldInfo.GetCustomAttributes(typeof(RAINNonSerializableFieldAttribute), false).Length > 0)
			{
				return null;
			}
			if (fieldInfo != null && fieldInfo.IsPrivate && fieldInfo.GetCustomAttributes(typeof(RAINSerializableFieldAttribute), false).Length == 0)
			{
				return null;
			}
			return fieldInfo;
		}
	}
}
