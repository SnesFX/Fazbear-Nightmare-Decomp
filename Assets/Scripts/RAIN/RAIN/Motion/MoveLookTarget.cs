using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Memory;
using RAIN.Navigation;
using RAIN.Navigation.Targets;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Motion
{
	[RAINSerializableClass]
	public class MoveLookTarget
	{
		public enum MoveLookTargetType
		{
			None = 0,
			Transform = 1,
			Kinematic = 2,
			Vector = 3,
			NavigationTarget = 4,
			Aspect = 5,
			Variable = 6
		}

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Target Type")]
		private MoveLookTargetType _targetType;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private Transform _transformTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private Kinematic _kinematicTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private Vector3 _vectorTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private NavigationTarget _navigationTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private RAINAspect _aspectTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show)]
		private string _variableTarget;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Target Close Enough Distance")]
		private float _closeEnoughDistance;

		private Kinematic _value = new Kinematic();

		private MoveLookTarget _savedTarget;

		private RAINMemory _variableMemory;

		public MoveLookTargetType TargetType
		{
			get
			{
				return _targetType;
			}
			set
			{
				_targetType = value;
			}
		}

		public Transform TransformTarget
		{
			get
			{
				return _transformTarget;
			}
			set
			{
				_transformTarget = value;
				if (_transformTarget != null)
				{
					_targetType = MoveLookTargetType.Transform;
				}
			}
		}

		public Kinematic KinematicTarget
		{
			get
			{
				return _kinematicTarget;
			}
			set
			{
				_kinematicTarget = value;
				if (_kinematicTarget != null)
				{
					_targetType = MoveLookTargetType.Kinematic;
				}
			}
		}

		public Vector3 VectorTarget
		{
			get
			{
				return _vectorTarget;
			}
			set
			{
				_vectorTarget = value;
				_targetType = MoveLookTargetType.Vector;
			}
		}

		public NavigationTarget NavigationTarget
		{
			get
			{
				return _navigationTarget;
			}
			set
			{
				_navigationTarget = value;
				_targetType = MoveLookTargetType.NavigationTarget;
				if (_navigationTarget != null)
				{
					_closeEnoughDistance = _navigationTarget.Range;
				}
			}
		}

		public RAINAspect AspectTarget
		{
			get
			{
				return _aspectTarget;
			}
			set
			{
				_aspectTarget = value;
				_targetType = MoveLookTargetType.Aspect;
			}
		}

		public string VariableTarget
		{
			get
			{
				return _variableTarget;
			}
			set
			{
				_variableTarget = value;
				_targetType = MoveLookTargetType.Variable;
			}
		}

		public object ObjectTarget
		{
			set
			{
				if (value is GameObject)
				{
					if ((GameObject)value == null)
					{
						TransformTarget = null;
						return;
					}
					GameObject gameObject = (GameObject)value;
					NavigationTargetRig component = gameObject.GetComponent<NavigationTargetRig>();
					if (component == null)
					{
						TransformTarget = gameObject.transform;
					}
					else
					{
						NavigationTarget = component.Target;
					}
				}
				else if (value is Vector3)
				{
					VectorTarget = (Vector3)value;
				}
				else if (value is Kinematic)
				{
					KinematicTarget = value as Kinematic;
				}
				else if (value is NavigationTarget)
				{
					NavigationTarget = value as NavigationTarget;
				}
				else if (value is RAINAspect)
				{
					AspectTarget = value as RAINAspect;
				}
				else if (value is string)
				{
					NavigationTarget navigationTarget = NavigationManager.Instance.GetNavigationTarget(value as string);
					if (navigationTarget != null)
					{
						NavigationTarget = navigationTarget;
					}
				}
				else
				{
					TargetType = MoveLookTargetType.None;
				}
			}
		}

		public RAINMemory VariableMemory
		{
			get
			{
				return _variableMemory;
			}
			set
			{
				_variableMemory = value;
			}
		}

		public bool IsValid
		{
			get
			{
				if (_targetType == MoveLookTargetType.Transform)
				{
					if (_transformTarget != null)
					{
						return true;
					}
				}
				else if (_targetType == MoveLookTargetType.Kinematic)
				{
					if (_kinematicTarget != null)
					{
						return true;
					}
				}
				else
				{
					if (_targetType == MoveLookTargetType.Vector)
					{
						return true;
					}
					if (_targetType == MoveLookTargetType.NavigationTarget)
					{
						if (_navigationTarget != null)
						{
							return true;
						}
					}
					else if (_targetType == MoveLookTargetType.Aspect)
					{
						if (_aspectTarget != null)
						{
							return true;
						}
					}
					else if (_targetType == MoveLookTargetType.Variable)
					{
						_savedTarget = GetTargetFromVariable(_variableMemory, _variableTarget, _closeEnoughDistance, _savedTarget);
						return _savedTarget.IsValid;
					}
				}
				return false;
			}
		}

		public Vector3 Position
		{
			get
			{
				if (_targetType == MoveLookTargetType.Transform)
				{
					if (_transformTarget != null)
					{
						return _transformTarget.position;
					}
				}
				else if (_targetType == MoveLookTargetType.Kinematic)
				{
					if (_kinematicTarget != null)
					{
						return _kinematicTarget.Position;
					}
				}
				else
				{
					if (_targetType == MoveLookTargetType.Vector)
					{
						return _vectorTarget;
					}
					if (_targetType == MoveLookTargetType.NavigationTarget)
					{
						if (_navigationTarget != null)
						{
							return _navigationTarget.Position;
						}
					}
					else if (_targetType == MoveLookTargetType.Aspect)
					{
						if (_aspectTarget != null)
						{
							return _aspectTarget.Position;
						}
					}
					else if (_targetType == MoveLookTargetType.Variable)
					{
						_savedTarget = GetTargetFromVariable(_variableMemory, _variableTarget, _closeEnoughDistance, _savedTarget);
						return _savedTarget.Position;
					}
				}
				return Vector3.zero;
			}
		}

		public Vector3 Orientation
		{
			get
			{
				if (_targetType == MoveLookTargetType.Transform)
				{
					if (_transformTarget != null)
					{
						return _transformTarget.rotation.eulerAngles;
					}
				}
				else if (_targetType == MoveLookTargetType.Kinematic)
				{
					if (_kinematicTarget != null)
					{
						return _kinematicTarget.Orientation;
					}
				}
				else
				{
					if (_targetType == MoveLookTargetType.Vector)
					{
						return Vector3.zero;
					}
					if (_targetType == MoveLookTargetType.NavigationTarget)
					{
						if (_navigationTarget != null)
						{
							return _navigationTarget.Orientation;
						}
					}
					else if (_targetType == MoveLookTargetType.Aspect)
					{
						if (_aspectTarget != null)
						{
							return _aspectTarget.Orientation;
						}
					}
					else if (_targetType == MoveLookTargetType.Variable)
					{
						_savedTarget = GetTargetFromVariable(_variableMemory, _variableTarget, _closeEnoughDistance, _savedTarget);
						return _savedTarget.Orientation;
					}
				}
				return Vector3.zero;
			}
		}

		public float CloseEnoughDistance
		{
			get
			{
				if (_targetType == MoveLookTargetType.Variable)
				{
					_savedTarget = GetTargetFromVariable(_variableMemory, _variableTarget, _closeEnoughDistance, _savedTarget);
					return _savedTarget.CloseEnoughDistance;
				}
				return _closeEnoughDistance;
			}
			set
			{
				_closeEnoughDistance = value;
				if (_targetType == MoveLookTargetType.Variable)
				{
					_savedTarget = GetTargetFromVariable(_variableMemory, _variableTarget, _closeEnoughDistance, _savedTarget);
					_savedTarget.CloseEnoughDistance = value;
				}
			}
		}

		public void SetVariableTarget(string aVariableName, RAINMemory aVariableMemory)
		{
			VariableTarget = aVariableName;
			VariableMemory = aVariableMemory;
		}

		public static MoveLookTarget GetTargetFromVariable(RAINMemory aMemory, string aVariableName, float aDefaultCloseEnoughDistance = 0f, MoveLookTarget aMoveLookTargetToReuse = null)
		{
			object item = aMemory.GetItem<object>(aVariableName);
			if (item is MoveLookTarget)
			{
				return item as MoveLookTarget;
			}
			MoveLookTarget moveLookTarget = aMoveLookTargetToReuse;
			if (moveLookTarget == null)
			{
				moveLookTarget = new MoveLookTarget();
			}
			moveLookTarget._closeEnoughDistance = aDefaultCloseEnoughDistance;
			moveLookTarget.ObjectTarget = item;
			return moveLookTarget;
		}
	}
}
