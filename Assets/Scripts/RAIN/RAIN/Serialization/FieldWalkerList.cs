using System;
using System.Collections.Generic;
using RAIN.Core;

namespace RAIN.Serialization
{
	[Serializable]
	public class FieldWalkerList
	{
		private List<RAINComponent> _targets = new List<RAINComponent>();

		private List<FieldWalker> _walkers = new List<FieldWalker>();

		public IList<RAINComponent> Targets
		{
			get
			{
				return _targets.AsReadOnly();
			}
		}

		public IList<FieldWalker> Walkers
		{
			get
			{
				return _walkers.AsReadOnly();
			}
		}

		public FieldVisibility Visibility
		{
			get
			{
				return _walkers[0].Visibility;
			}
		}

		public string FieldName
		{
			get
			{
				return _walkers[0].FieldName;
			}
		}

		public string PrettyFieldName
		{
			get
			{
				return _walkers[0].PrettyFieldName;
			}
		}

		public string ToolTip
		{
			get
			{
				return _walkers[0].ToolTip;
			}
		}

		public Type FieldType
		{
			get
			{
				return _walkers[0].FieldType;
			}
		}

		public Type FieldValueType
		{
			get
			{
				return _walkers[0].FieldValueType;
			}
		}

		public bool IsFieldArray
		{
			get
			{
				return _walkers[0].IsFieldArray;
			}
		}

		public bool IsFieldValid
		{
			get
			{
				return _walkers[0].IsFieldValid;
			}
		}

		public bool IsFieldNull
		{
			get
			{
				return _walkers[0].IsFieldNull;
			}
		}

		public int ChildCount
		{
			get
			{
				return _walkers[0].ChildCount;
			}
		}

		public bool ChildrenVisible
		{
			get
			{
				return _walkers[0].ChildrenVisible;
			}
			set
			{
				for (int i = 0; i < _walkers.Count; i++)
				{
					_walkers[i].ChildrenVisible = value;
				}
			}
		}

		public bool IsFirstReference
		{
			get
			{
				return _walkers[0].IsFirstReference;
			}
		}

		public bool IsStartOfDocument
		{
			get
			{
				return _walkers[0].IsStartOfDocument;
			}
		}

		public bool AllHaveSiblings
		{
			get
			{
				for (int i = 0; i < _walkers.Count; i++)
				{
					if (!_walkers[i].HasSibling)
					{
						return false;
					}
				}
				return true;
			}
		}

		public bool HasDifferingValueTypes
		{
			get
			{
				for (int i = 0; i < _walkers.Count - 1; i++)
				{
					if (!_walkers[i].FieldValueType.Equals(_walkers[i + 1].FieldValueType))
					{
						return true;
					}
				}
				return false;
			}
		}

		public bool HasDifferingValues
		{
			get
			{
				for (int i = 0; i < _walkers.Count - 1; i++)
				{
					if (_walkers[i].GetFieldValue() != null || _walkers[i + 1].GetFieldValue() != null)
					{
						if (_walkers[i].GetFieldValue() == null || _walkers[i + 1].GetFieldValue() == null)
						{
							return true;
						}
						if (!_walkers[i].GetFieldValue().Equals(_walkers[i + 1].GetFieldValue()))
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		public bool HasDifferingChildCounts
		{
			get
			{
				for (int i = 0; i < _walkers.Count - 1; i++)
				{
					if (_walkers[i].ChildCount != _walkers[i + 1].ChildCount)
					{
						return true;
					}
				}
				return false;
			}
		}

		public string Path
		{
			get
			{
				return _walkers[0].Path;
			}
		}

		public FieldWalkerList()
		{
		}

		public FieldWalkerList(RAINComponent[] aTargets)
		{
			_targets = new List<RAINComponent>(aTargets);
			for (int i = 0; i < aTargets.Length; i++)
			{
				_walkers.Add(new FieldWalker(aTargets[i]));
			}
		}

		public void Reset()
		{
			for (int i = 0; i < _walkers.Count; i++)
			{
				_walkers[i].Reset();
			}
		}

		public FieldWalkerList Copy(bool aStartOver = false)
		{
			FieldWalkerList fieldWalkerList = new FieldWalkerList();
			for (int i = 0; i < _targets.Count; i++)
			{
				fieldWalkerList._targets.Add(_targets[i]);
			}
			for (int j = 0; j < _walkers.Count; j++)
			{
				fieldWalkerList._walkers.Add(_walkers[j].Copy(aStartOver));
			}
			return fieldWalkerList;
		}

		public bool FirstChild()
		{
			bool flag = true;
			if (IsStartOfDocument || (IsFieldArray && GetArrayCount() > 0) || ChildCount > 0)
			{
				for (int i = 0; i < _walkers.Count; i++)
				{
					flag &= _walkers[i].FirstChild();
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public bool NextSibling()
		{
			bool flag = true;
			if (!IsStartOfDocument && AllHaveSiblings)
			{
				for (int i = 0; i < _walkers.Count; i++)
				{
					flag &= _walkers[i].NextSibling();
				}
			}
			else
			{
				flag = false;
			}
			return flag;
		}

		public FieldWalkerList FindFieldInDocument(string aField)
		{
			FieldWalkerList fieldWalkerList = Copy(true);
			if (fieldWalkerList.GoToPath("field." + aField))
			{
				return fieldWalkerList;
			}
			return null;
		}

		public FieldWalkerList FindFieldInChildren(string aField, bool aRecursive = false)
		{
			FieldWalkerList fieldWalkerList = Copy();
			if (fieldWalkerList.GoToPath(aRecursive ? ("field." + aField) : (Path + "/field." + aField)))
			{
				return fieldWalkerList;
			}
			return null;
		}

		public void SetFieldValue(object aValue)
		{
			for (int i = 0; i < _walkers.Count; i++)
			{
				_walkers[i].SetFieldValue(aValue);
			}
		}

		public object GetFieldValue()
		{
			return _walkers[0].GetFieldValue();
		}

		public void SetArrayCount(int aCount)
		{
			for (int i = 0; i < _walkers.Count; i++)
			{
				_walkers[i].SetArrayCount(aCount);
			}
		}

		public int GetArrayCount()
		{
			int num = int.MaxValue;
			for (int i = 0; i < _walkers.Count; i++)
			{
				if (_walkers[i].GetArrayCount() < num)
				{
					num = _walkers[i].GetArrayCount();
				}
			}
			return num;
		}

		public FieldWalkerList GetArrayElement(int aIndex)
		{
			FieldWalkerList fieldWalkerList = Copy();
			if (fieldWalkerList.GoToPath(Path + "/field.element" + aIndex))
			{
				return fieldWalkerList;
			}
			return null;
		}

		public void DeleteArrayElement(int aIndex)
		{
			for (int i = 0; i < _walkers.Count; i++)
			{
				_walkers[i].DeleteArrayElement(aIndex);
			}
		}

		public void InsertArrayElement(int aIndex, object aValue)
		{
			for (int i = 0; i < _walkers.Count; i++)
			{
				_walkers[i].InsertArrayElement(aIndex, aValue);
			}
		}

		public bool GoToPath(string aPath)
		{
			bool result = false;
			for (int i = 0; i < _walkers.Count; i++)
			{
				result = _walkers[i].GoToPath(aPath);
			}
			return result;
		}
	}
}
