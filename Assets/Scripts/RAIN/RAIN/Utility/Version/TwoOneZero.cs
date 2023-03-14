using System;
using System.Collections.Generic;
using RAIN.Memory;
using RAIN.Motion;
using RAIN.Serialization;
using UnityEngine;

namespace RAIN.Utility.Version
{
	public class TwoOneZero
	{
		[RAINSerializableClass]
		public class InterimBasicMemory
		{
			[RAINSerializableField]
			public List<string> _memoryItems = new List<string>();

			[RAINSerializableField]
			public List<InterimMemoryObject> _memoryValues = new List<InterimMemoryObject>();

			public BasicMemory GetBasicMemory()
			{
				BasicMemory basicMemory = new BasicMemory();
				for (int i = 0; i < _memoryItems.Count; i++)
				{
					basicMemory.SetItem(_memoryItems[i], _memoryValues[i].GetValue<object>(), _memoryValues[i].GetValueType());
				}
				return basicMemory;
			}
		}

		[RAINSerializableClass]
		public class InterimMemoryObject
		{
			private Type _realType;

			private object _realValue;

			[RAINSerializableField]
			public string _serializedType = "";

			[RAINSerializableField]
			public string _serializedValue = "";

			[RAINSerializableField]
			public GameObject _serializedGameObject;

			public T GetValue<T>()
			{
				if (_realType == null)
				{
					ParseRealValue();
				}
				return TypeConvert.ConvertValue<T>(_realValue);
			}

			public Type GetValueType()
			{
				if (_realType == null)
				{
					ParseRealValue();
				}
				return _realType;
			}

			private void ParseRealValue()
			{
				_realType = Type.GetType(_serializedType);
				if (_realType == null)
				{
					switch (_serializedType)
					{
					case "UnityEngine.Vector2":
						_realType = typeof(Vector2);
						break;
					case "UnityEngine.Vector3":
						_realType = typeof(Vector3);
						break;
					case "UnityEngine.Vector4":
						_realType = typeof(Vector4);
						break;
					case "UnityEngine.GameObject":
						_realType = typeof(GameObject);
						break;
					}
				}
				if (_realType == null)
				{
					throw new Exception("MemoryObject - invalid type, couldn't parse type from: " + _serializedType);
				}
				if (_realType == typeof(GameObject))
				{
					_realValue = _serializedGameObject;
				}
				else if (_realType == typeof(double))
				{
					_realValue = double.Parse(_serializedValue);
				}
				else if (_realType == typeof(float))
				{
					_realValue = float.Parse(_serializedValue);
				}
				else if (_realType == typeof(long))
				{
					_realValue = long.Parse(_serializedValue);
				}
				else if (_realType == typeof(int))
				{
					_realValue = int.Parse(_serializedValue);
				}
				else if (_realType == typeof(bool))
				{
					_realValue = bool.Parse(_serializedValue);
				}
				else if (_realType == typeof(string))
				{
					_realValue = _serializedValue;
				}
				else if (_realType == typeof(Vector2))
				{
					string[] array = _serializedValue.Trim('(', ')').Split(',');
					Vector2 vector = default(Vector2);
					vector.x = float.Parse(array[0]);
					vector.y = float.Parse(array[1]);
					_realValue = vector;
				}
				else if (_realType == typeof(Vector3))
				{
					string[] array2 = _serializedValue.Trim('(', ')').Split(',');
					Vector3 vector2 = default(Vector3);
					vector2.x = float.Parse(array2[0]);
					vector2.y = float.Parse(array2[1]);
					vector2.z = float.Parse(array2[2]);
					_realValue = vector2;
				}
				else if (_realType == typeof(Vector4))
				{
					string[] array3 = _serializedValue.Trim('(', ')').Split(',');
					Vector4 vector3 = default(Vector4);
					vector3.x = float.Parse(array3[0]);
					vector3.y = float.Parse(array3[1]);
					vector3.z = float.Parse(array3[2]);
					vector3.w = float.Parse(array3[3]);
					_realValue = vector3;
				}
			}
		}

		[RAINSerializableClass]
		public class InterimMecanimMotor
		{
			[RAINSerializableField]
			public float defaultSpeed = 1f;

			[RAINSerializableField]
			public float defaultRotationSpeed = 180f;

			[RAINSerializableField]
			public float defaultCloseEnoughDistance = 0.1f;

			[RAINSerializableField]
			public float defaultCloseEnoughAngle = 0.001f;

			[RAINSerializableField]
			public float defaultFaceBeforeMoveAngle = 90f;

			[RAINSerializableField]
			public float defaultStepUpHeight = 0.5f;

			[RAINSerializableField]
			public bool allow3DMovement;

			[RAINSerializableField]
			public bool allow3DRotation;

			[RAINSerializableField]
			public bool validPathRequired = true;

			[RAINSerializableField]
			protected bool useRootMotion;

			[RAINSerializableField]
			protected bool overrideRootMotionRotation;

			[RAINSerializableField]
			public string paramSpeed;

			[RAINSerializableField]
			public string paramRotationSpeed;

			[RAINSerializableField]
			public string paramTurnAngle;

			[RAINSerializableField]
			public string paramVelocityX;

			[RAINSerializableField]
			public string paramVelocityY;

			[RAINSerializableField]
			public string paramVelocityZ;

			[RAINSerializableField]
			public string paramRotationX;

			[RAINSerializableField]
			public string paramRotationY;

			[RAINSerializableField]
			public string paramRotationZ;

			public MecanimMotor GetMecanimMotor()
			{
				MecanimMotor mecanimMotor = new MecanimMotor();
				mecanimMotor.DefaultSpeed = defaultSpeed;
				mecanimMotor.DefaultRotationSpeed = defaultRotationSpeed;
				mecanimMotor.DefaultCloseEnoughDistance = defaultCloseEnoughDistance;
				mecanimMotor.DefaultCloseEnoughAngle = defaultCloseEnoughAngle;
				mecanimMotor.DefaultFaceBeforeMoveAngle = defaultFaceBeforeMoveAngle;
				mecanimMotor.DefaultStepUpHeight = defaultStepUpHeight;
				mecanimMotor.Allow3DMovement = allow3DMovement;
				mecanimMotor.Allow3DRotation = allow3DRotation;
				mecanimMotor.AllowOffGraphMovement = validPathRequired;
				mecanimMotor.UseRootMotion = useRootMotion;
				mecanimMotor.OverrideRootMotionRotation = overrideRootMotionRotation;
				if (paramSpeed != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramSpeed,
						motorField = MecanimMotor.MotorField.Speed
					});
				}
				if (paramRotationSpeed != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramRotationSpeed,
						motorField = MecanimMotor.MotorField.RotationSpeed
					});
				}
				if (paramTurnAngle != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramTurnAngle,
						motorField = MecanimMotor.MotorField.TurnAngle
					});
				}
				if (paramVelocityX != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramVelocityX,
						motorField = MecanimMotor.MotorField.VelocityX
					});
				}
				if (paramVelocityY != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramVelocityY,
						motorField = MecanimMotor.MotorField.VelocityY
					});
				}
				if (paramVelocityZ != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramVelocityZ,
						motorField = MecanimMotor.MotorField.VelocityZ
					});
				}
				if (paramRotationX != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramRotationX,
						motorField = MecanimMotor.MotorField.RotationX
					});
				}
				if (paramRotationY != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramRotationY,
						motorField = MecanimMotor.MotorField.RotationY
					});
				}
				if (paramRotationZ != "")
				{
					mecanimMotor.AddForwardedParameter(new MecanimMotor.MotorParameter
					{
						parameterName = paramRotationZ,
						motorField = MecanimMotor.MotorField.RotationZ
					});
				}
				return mecanimMotor;
			}
		}

		public void UpgradeBasicMemory(FieldSerializer aRigSerializer)
		{
			aRigSerializer.ChangeAllTypes("RAIN.Memory.BasicMemory", typeof(InterimBasicMemory).ToString());
			aRigSerializer.ChangeAllTypes("RAIN.Memory.MemoryObject", typeof(InterimMemoryObject).ToString());
			List<ObjectElement> fieldElementsForType = aRigSerializer.GetFieldElementsForType(typeof(InterimBasicMemory).ToString());
			for (int i = 0; i < fieldElementsForType.Count; i++)
			{
				aRigSerializer.HoldReferences = true;
				object aResult;
				if (aRigSerializer.DeserializeObjectFromElement(fieldElementsForType[i], typeof(InterimBasicMemory), null, out aResult))
				{
					aRigSerializer.SerializeObjectToElement(fieldElementsForType[i], typeof(BasicMemory), ((InterimBasicMemory)aResult).GetBasicMemory(), true);
				}
				aRigSerializer.HoldReferences = false;
			}
		}

		public void UpgradeMecanimMotor(FieldSerializer aRigSerializer)
		{
			aRigSerializer.ChangeAllTypes("RAIN.Motion.MecanimMotor", typeof(InterimMecanimMotor).ToString());
			List<ObjectElement> fieldElementsForType = aRigSerializer.GetFieldElementsForType(typeof(InterimMecanimMotor).ToString());
			for (int i = 0; i < fieldElementsForType.Count; i++)
			{
				aRigSerializer.HoldReferences = true;
				object aResult;
				if (aRigSerializer.DeserializeObjectFromElement(fieldElementsForType[i], typeof(InterimMecanimMotor), null, out aResult))
				{
					aRigSerializer.SerializeObjectToElement(fieldElementsForType[i], typeof(MecanimMotor), ((InterimMecanimMotor)aResult).GetMecanimMotor(), true);
				}
				aRigSerializer.HoldReferences = false;
			}
		}
	}
}
