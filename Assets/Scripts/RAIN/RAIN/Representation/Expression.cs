using System;
using System.IO;
using RAIN.Memory;
using RAIN.Navigation;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Representation
{
	[RAINSerializableClass("SerializeExpression", "DeserializeExpression")]
	public class Expression
	{
		public enum FunctionType
		{
			DeltaTimeFunction = 0,
			CurrentTimeFunction = 1,
			ClampFunction = 2,
			MinFunction = 3,
			MaxFunction = 4,
			DebugFunction = 5,
			RandomFunction = 6,
			TargetFunction = 7,
			WaypointFunction = 8,
			GameObjectFunction = 9,
			PositionFunction = 10,
			NoOp = 11
		}

		public enum OperatorType
		{
			Separator = 0,
			Dot = 1,
			Bang = 2,
			Negate = 3,
			Increment = 4,
			Decrement = 5,
			Addition = 6,
			Subtraction = 7,
			Multiplication = 8,
			Division = 9,
			And = 10,
			Or = 11,
			Xor = 12,
			Greater = 13,
			GreaterEqual = 14,
			Less = 15,
			LessEqual = 16,
			Equal = 17,
			NotEqual = 18,
			Approx = 19,
			NotApprox = 20,
			Assign = 21,
			AssignAddition = 22,
			AssignSubtraction = 23,
			AssignMultiplication = 24,
			AssignDivision = 25,
			NoOp = 26
		}

		public class Keywords
		{
			public static readonly string Null = "null";

			public static readonly string True = "true";

			public static readonly string False = "false";

			public static readonly string DeltaTimeFunction = "deltaTime";

			public static readonly string CurrentTimeFunction = "currentTime";

			public static readonly string ClampFunction = "clamp";

			public static readonly string MinFunction = "min";

			public static readonly string MaxFunction = "max";

			public static readonly string DebugFunction = "debug";

			public static readonly string RandomFunction = "random";

			public static readonly string TargetFunction = "navigationtarget";

			public static readonly string WaypointFunction = "waypoints";

			public static readonly string GameObjectFunction = "gameobject";

			public static readonly string PositionFunction = "position";
		}

		public enum ExpressionType
		{
			Invalid = 0,
			Null = 1,
			Constant = 2,
			Variable = 3,
			Function = 4,
			Unary = 5,
			Binary = 6
		}

		private string _expressionAsEntered;

		private ExpressionType _expressionType;

		private object _constant;

		private string _variable;

		private FunctionType _function;

		private Expression[] _args;

		private OperatorType _operator;

		private Expression _expr1;

		private Expression _expr2;

		private bool _isAtomic;

		public bool IsValid
		{
			get
			{
				return _expressionType != ExpressionType.Invalid;
			}
		}

		public bool IsNull
		{
			get
			{
				return _expressionType == ExpressionType.Null;
			}
		}

		public bool IsConstant
		{
			get
			{
				return _expressionType == ExpressionType.Constant;
			}
		}

		public bool IsVariable
		{
			get
			{
				return _expressionType == ExpressionType.Variable;
			}
		}

		public bool IsFunction
		{
			get
			{
				return _expressionType == ExpressionType.Function;
			}
		}

		public bool IsUnary
		{
			get
			{
				return _expressionType == ExpressionType.Unary;
			}
		}

		public bool IsBinary
		{
			get
			{
				return _expressionType == ExpressionType.Binary;
			}
		}

		public string VariableName
		{
			get
			{
				if (IsVariable)
				{
					return _variable;
				}
				return null;
			}
		}

		public string ExpressionAsEntered
		{
			get
			{
				return _expressionAsEntered;
			}
			set
			{
				_expressionAsEntered = value;
			}
		}

		public Expression()
		{
			Clear();
		}

		public void Clear()
		{
			_expressionType = ExpressionType.Invalid;
			_constant = null;
			_variable = null;
			_function = FunctionType.NoOp;
			_args = new Expression[0];
			_operator = OperatorType.NoOp;
			_expr1 = null;
			_expr2 = null;
			_expressionAsEntered = null;
		}

		public void RenameVariable(string originalName, string newName)
		{
			if (_expressionType == ExpressionType.Variable)
			{
				if (_variable == originalName)
				{
					_variable = newName;
				}
			}
			else if (_expressionType == ExpressionType.Function)
			{
				for (int i = 0; i < _args.Length; i++)
				{
					_args[i].RenameVariable(originalName, newName);
				}
			}
			else if (_expressionType == ExpressionType.Binary)
			{
				_expr1.RenameVariable(originalName, newName);
				if (_expr2 != null)
				{
					_expr2.RenameVariable(originalName, newName);
				}
			}
		}

		public void SetInvalid()
		{
			Clear();
		}

		public void SetNull()
		{
			Clear();
			_expressionType = ExpressionType.Null;
		}

		public void SetConstant(object aConstant)
		{
			Clear();
			_expressionType = ExpressionType.Constant;
			_constant = aConstant;
		}

		public void SetVariable(string aVariable)
		{
			Clear();
			_expressionType = ExpressionType.Variable;
			_variable = aVariable;
		}

		public void SetFunction(FunctionType aFunctionType, params Expression[] aArgs)
		{
			Clear();
			_expressionType = ExpressionType.Function;
			_function = aFunctionType;
			_args = aArgs;
		}

		[Obsolete("Use SetUnary or SetBinary instead")]
		public void SetComplex(Expression aLeft, OperatorType aOperator, Expression aRight)
		{
			if (aRight == null)
			{
				SetUnary(aOperator, aLeft);
			}
			else
			{
				SetBinary(aLeft, aOperator, aRight);
			}
		}

		public void SetUnary(OperatorType aOperator, Expression aExpr)
		{
			if (aExpr == this)
			{
				throw new Exception("Invalid recursive expression");
			}
			if (aExpr == null)
			{
				throw new Exception("Invalid unary expression");
			}
			Clear();
			_expressionType = ExpressionType.Unary;
			_operator = aOperator;
			_expr1 = aExpr;
		}

		public void SetBinary(Expression aLeft, OperatorType aOperator, Expression aRight)
		{
			if (aLeft == this || aRight == this)
			{
				throw new Exception("Invalid recursive expression");
			}
			if (aLeft == null || aRight == null)
			{
				throw new Exception("Invalid binary expression");
			}
			Clear();
			_expressionType = ExpressionType.Binary;
			_operator = aOperator;
			_expr1 = aLeft;
			_expr2 = aRight;
		}

		[Obsolete("Use Evaluate<T> instead")]
		public ExpressionValue Evaluate(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			return new ExpressionValue(EvaluateObject(aDeltaTime, aWorkingMemory));
		}

		public T Evaluate<T>(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			return ConvertValue<T>(EvaluateObject(aDeltaTime, aWorkingMemory));
		}

		public void FixOperatorPrecedence()
		{
			if (_expressionType != ExpressionType.Binary)
			{
				return;
			}
			if (_expr1._expressionType == ExpressionType.Binary)
			{
				_expr1.FixOperatorPrecedence();
			}
			if (_expr2._expressionType == ExpressionType.Binary && !_expr2._isAtomic)
			{
				int opPrecedence = GetOpPrecedence();
				int opPrecedence2 = _expr2.GetOpPrecedence();
				if (opPrecedence < opPrecedence2)
				{
					_expr2.FixOperatorPrecedence();
					return;
				}
				Expression expression = new Expression();
				expression.SetBinary(_expr1, _operator, _expr2._expr1);
				_operator = _expr2._operator;
				_expr2 = _expr2._expr2;
				_expr1 = expression;
				FixOperatorPrecedence();
			}
		}

		public void SimplifyConstants()
		{
			if (_expressionType == ExpressionType.Unary)
			{
				_expr1.SimplifyConstants();
				if (_expr1._expressionType == ExpressionType.Constant)
				{
					SetConstant(EvaluateObject(0f, null));
				}
			}
			else if (_expressionType == ExpressionType.Binary)
			{
				_expr1.SimplifyConstants();
				_expr2.SimplifyConstants();
				if (_expr1._expressionType == ExpressionType.Constant && _expr2._expressionType == ExpressionType.Constant)
				{
					SetConstant(EvaluateObject(0f, null));
				}
			}
		}

		public void SetAtomic(bool isAtomic)
		{
			_isAtomic = isAtomic;
		}

		public override string ToString()
		{
			if (_expressionType == ExpressionType.Null)
			{
				return Keywords.Null;
			}
			if (_expressionType == ExpressionType.Constant)
			{
				return _constant.ToString();
			}
			if (_expressionType == ExpressionType.Variable)
			{
				return _variable;
			}
			if (_expressionType == ExpressionType.Function)
			{
				string text = "";
				switch (_function)
				{
				case FunctionType.DeltaTimeFunction:
					text = text + Keywords.DeltaTimeFunction + "(";
					break;
				case FunctionType.CurrentTimeFunction:
					text = text + Keywords.CurrentTimeFunction + "(";
					break;
				case FunctionType.ClampFunction:
					text = text + Keywords.ClampFunction + "(";
					break;
				case FunctionType.MinFunction:
					text = text + Keywords.MinFunction + "(";
					break;
				case FunctionType.MaxFunction:
					text = text + Keywords.MaxFunction + "(";
					break;
				case FunctionType.DebugFunction:
					text = text + Keywords.DebugFunction + "(";
					break;
				case FunctionType.RandomFunction:
					text = text + Keywords.RandomFunction + "(";
					break;
				case FunctionType.TargetFunction:
					text = text + Keywords.TargetFunction + "(";
					break;
				case FunctionType.WaypointFunction:
					text = text + Keywords.WaypointFunction + "(";
					break;
				case FunctionType.GameObjectFunction:
					text = text + Keywords.GameObjectFunction + "(";
					break;
				case FunctionType.PositionFunction:
					text = text + Keywords.PositionFunction + "(";
					break;
				}
				for (int i = 0; i < _args.Length; i++)
				{
					text += _args[i].ToString();
					if (i + 1 < _args.Length)
					{
						text += ", ";
					}
				}
				return text + ")";
			}
			if (_expressionType == ExpressionType.Unary)
			{
				string text2 = "";
				if (_isAtomic)
				{
					text2 = "(";
				}
				switch (_operator)
				{
				case OperatorType.Bang:
					text2 += "!";
					break;
				case OperatorType.Negate:
					text2 += "-";
					break;
				case OperatorType.Decrement:
					text2 += "--";
					break;
				case OperatorType.Increment:
					text2 += "++";
					break;
				}
				text2 += _expr1.ToString();
				if (_isAtomic)
				{
					text2 += ")";
				}
				return text2;
			}
			if (_expressionType == ExpressionType.Binary)
			{
				string text3 = "";
				if (_isAtomic)
				{
					text3 = "(";
				}
				text3 += _expr1.ToString();
				switch (_operator)
				{
				case OperatorType.Separator:
					text3 += "; ";
					break;
				case OperatorType.Dot:
					text3 += ".";
					break;
				case OperatorType.Addition:
					text3 += " + ";
					break;
				case OperatorType.Subtraction:
					text3 += " - ";
					break;
				case OperatorType.Multiplication:
					text3 += " * ";
					break;
				case OperatorType.Division:
					text3 += " / ";
					break;
				case OperatorType.And:
					text3 += " && ";
					break;
				case OperatorType.Or:
					text3 += " || ";
					break;
				case OperatorType.Xor:
					text3 += " ^ ";
					break;
				case OperatorType.Greater:
					text3 += " > ";
					break;
				case OperatorType.GreaterEqual:
					text3 += " >= ";
					break;
				case OperatorType.Less:
					text3 += " < ";
					break;
				case OperatorType.LessEqual:
					text3 += " <= ";
					break;
				case OperatorType.Equal:
					text3 += " == ";
					break;
				case OperatorType.NotEqual:
					text3 += " != ";
					break;
				case OperatorType.Approx:
					text3 += " ~~ ";
					break;
				case OperatorType.NotApprox:
					text3 += " !~ ";
					break;
				case OperatorType.Assign:
					text3 += " = ";
					break;
				case OperatorType.AssignAddition:
					text3 += " += ";
					break;
				case OperatorType.AssignSubtraction:
					text3 += " -= ";
					break;
				case OperatorType.AssignMultiplication:
					text3 += " *= ";
					break;
				case OperatorType.AssignDivision:
					text3 += " /= ";
					break;
				case OperatorType.NoOp:
					text3 += " ?? ";
					break;
				}
				text3 += _expr2.ToString();
				if (_isAtomic)
				{
					text3 += ")";
				}
				return text3;
			}
			return null;
		}

		private object EvaluateObject(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			switch (_expressionType)
			{
			case ExpressionType.Invalid:
			case ExpressionType.Null:
				return null;
			case ExpressionType.Constant:
				return _constant;
			case ExpressionType.Variable:
				return aWorkingMemory.GetItem<object>(_variable);
			case ExpressionType.Function:
				return EvaluateFunction(aDeltaTime, aWorkingMemory);
			case ExpressionType.Unary:
				return EvaluateUnary(aDeltaTime, aWorkingMemory);
			case ExpressionType.Binary:
				return EvaluateBinary(aDeltaTime, aWorkingMemory);
			default:
				throw new Exception("Failed to handle all expression types");
			}
		}

		private object EvaluateUnary(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			object obj = _expr1.EvaluateObject(aDeltaTime, aWorkingMemory);
			object result = obj;
			switch (_operator)
			{
			case OperatorType.Bang:
				if (obj == null)
				{
					result = true;
				}
				else if (obj is bool)
				{
					result = !ConvertValue<bool>(obj);
				}
				break;
			case OperatorType.Negate:
				if (obj == null)
				{
					result = 0;
				}
				else if (obj is int)
				{
					result = -ConvertValue<int>(obj);
				}
				else if (obj is long)
				{
					result = -ConvertValue<long>(obj);
				}
				else if (obj is float)
				{
					result = 0f - ConvertValue<float>(obj);
				}
				else if (obj is double)
				{
					result = 0.0 - ConvertValue<double>(obj);
				}
				break;
			case OperatorType.Increment:
				if (obj == null)
				{
					result = 1;
				}
				else if (obj is int)
				{
					result = ConvertValue<int>(obj) + 1;
				}
				else if (obj is long)
				{
					result = ConvertValue<long>(obj) + 1;
				}
				else if (obj is float)
				{
					result = ConvertValue<float>(obj) + 1f;
				}
				else if (obj is double)
				{
					result = ConvertValue<double>(obj) + 1.0;
				}
				break;
			case OperatorType.Decrement:
				if (obj == null)
				{
					result = -1;
				}
				else if (obj is int)
				{
					result = ConvertValue<int>(obj) - 1;
				}
				else if (obj is long)
				{
					result = ConvertValue<long>(obj) - 1;
				}
				else if (obj is float)
				{
					result = ConvertValue<float>(obj) - 1f;
				}
				else if (obj is double)
				{
					result = ConvertValue<double>(obj) - 1.0;
				}
				break;
			}
			return result;
		}

		private object EvaluateBinary(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			object obj = _expr1.EvaluateObject(aDeltaTime, aWorkingMemory);
			object obj2 = _expr2.EvaluateObject(aDeltaTime, aWorkingMemory);
			if (_operator != OperatorType.Assign && _operator != OperatorType.AssignAddition && _operator != OperatorType.AssignSubtraction && _operator != OperatorType.AssignMultiplication && _operator != OperatorType.AssignDivision && (obj != null || obj2 != null))
			{
				if (obj == null)
				{
					if (obj2 is int)
					{
						obj = 0;
					}
					else if (obj2 is long)
					{
						obj = 0L;
					}
					else if (obj2 is float)
					{
						obj = 0f;
					}
					else if (obj2 is double)
					{
						obj = 0.0;
					}
					else if (obj2 is bool)
					{
						obj = false;
					}
					else if (obj2 is string)
					{
						obj = null;
					}
					else if (obj2 is Vector2)
					{
						obj = default(Vector2);
					}
					else if (obj2 is Vector3)
					{
						obj = default(Vector3);
					}
					else if (obj2 is Vector4)
					{
						obj = default(Vector4);
					}
				}
				else if (obj2 == null)
				{
					if (obj is int)
					{
						obj2 = 0;
					}
					else if (obj is long)
					{
						obj2 = 0L;
					}
					else if (obj is float)
					{
						obj2 = 0f;
					}
					else if (obj is double)
					{
						obj2 = 0.0;
					}
					else if (obj is bool)
					{
						obj2 = false;
					}
					else if (obj is string)
					{
						obj2 = null;
					}
					else if (obj is Vector2)
					{
						obj2 = default(Vector2);
					}
					else if (obj is Vector3)
					{
						obj2 = default(Vector3);
					}
					else if (obj is Vector4)
					{
						obj2 = default(Vector4);
					}
				}
			}
			object obj3 = obj;
			switch (_operator)
			{
			case OperatorType.Dot:
				if ((!(obj is Vector4) && !(obj is Vector3) && !(obj is Vector2)) || !_expr2.IsVariable)
				{
					break;
				}
				if (_expr2.VariableName == "x")
				{
					obj3 = ConvertValue<Vector2>(obj).x;
				}
				else if (_expr2.VariableName == "y")
				{
					obj3 = ConvertValue<Vector2>(obj).y;
				}
				else if (!(obj is Vector2))
				{
					if (_expr2.VariableName == "z")
					{
						obj3 = ConvertValue<Vector3>(obj).z;
					}
					else if (!(obj is Vector3) && _expr2.VariableName == "w")
					{
						obj3 = ConvertValue<Vector4>(obj).w;
					}
				}
				break;
			case OperatorType.Addition:
			case OperatorType.AssignAddition:
				if (obj is string || obj2 is string)
				{
					obj3 = ConvertValue<string>(obj) + ConvertValue<string>(obj2);
				}
				else if ((obj is Vector4 || obj is Vector3 || obj is Vector2) && (obj2 is Vector4 || obj2 is Vector3 || obj2 is Vector2))
				{
					if (obj is Vector4 || obj2 is Vector4)
					{
						obj3 = ConvertValue<Vector4>(obj) + ConvertValue<Vector4>(obj2);
					}
					else if (obj is Vector3 || obj2 is Vector3)
					{
						obj3 = ConvertValue<Vector3>(obj) + ConvertValue<Vector3>(obj2);
					}
					else if (obj is Vector2 || obj2 is Vector2)
					{
						obj3 = ConvertValue<Vector2>(obj) + ConvertValue<Vector2>(obj2);
					}
				}
				else if ((obj is int || obj is long || obj is float || obj is double || obj is bool) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double || obj2 is bool))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) + ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) + ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) + ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) + ConvertValue<double>(obj2))));
				}
				if (_operator == OperatorType.AssignAddition && _expr1.IsVariable)
				{
					if (obj3 == null)
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, typeof(object));
					}
					else
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, obj3.GetType());
					}
				}
				break;
			case OperatorType.Subtraction:
			case OperatorType.AssignSubtraction:
				if ((obj is Vector4 || obj is Vector3 || obj is Vector2) && (obj2 is Vector4 || obj2 is Vector3 || obj2 is Vector2))
				{
					if (obj is Vector4 || obj2 is Vector4)
					{
						obj3 = ConvertValue<Vector4>(obj) - ConvertValue<Vector4>(obj2);
					}
					else if (obj is Vector3 || obj2 is Vector3)
					{
						obj3 = ConvertValue<Vector3>(obj) - ConvertValue<Vector3>(obj2);
					}
					else if (obj is Vector2 || obj2 is Vector2)
					{
						obj3 = ConvertValue<Vector2>(obj) - ConvertValue<Vector2>(obj2);
					}
				}
				else if ((obj is int || obj is long || obj is float || obj is double || obj is bool) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double || obj2 is bool))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) - ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) - ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) - ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) - ConvertValue<double>(obj2))));
				}
				if (_operator == OperatorType.AssignSubtraction && _expr1.IsVariable)
				{
					if (obj3 == null)
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, typeof(object));
					}
					else
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, obj3.GetType());
					}
				}
				break;
			case OperatorType.Multiplication:
			case OperatorType.AssignMultiplication:
				if ((obj is int || obj is long || obj is float || obj is double || obj is bool) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double || obj2 is bool))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) * ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) * ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) * ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) * ConvertValue<double>(obj2))));
				}
				if (_operator == OperatorType.AssignMultiplication && _expr1.IsVariable)
				{
					if (obj3 == null)
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, typeof(object));
					}
					else
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, obj3.GetType());
					}
				}
				break;
			case OperatorType.Division:
			case OperatorType.AssignDivision:
				if ((obj is int || obj is long || obj is float || obj is double || obj is bool) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double || obj2 is bool))
				{
					if (obj is double || obj2 is double)
					{
						obj3 = ConvertValue<double>(obj) / ConvertValue<double>(obj2);
					}
					else if (obj is float || obj2 is float)
					{
						obj3 = ConvertValue<float>(obj) / ConvertValue<float>(obj2);
					}
					else if (obj is long || obj2 is long)
					{
						long num = ConvertValue<long>(obj);
						long num2 = ConvertValue<long>(obj2);
						obj3 = ((num2 != 0) ? ((object)(num / num2)) : ((object)float.NaN));
					}
					else
					{
						int num3 = ConvertValue<int>(obj);
						int num4 = ConvertValue<int>(obj2);
						obj3 = ((num4 != 0) ? ((object)(num3 / num4)) : ((object)float.NaN));
					}
				}
				if (_operator == OperatorType.AssignDivision && _expr1.IsVariable)
				{
					if (obj3 == null)
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, typeof(object));
					}
					else
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj3, obj3.GetType());
					}
				}
				break;
			case OperatorType.And:
				if (obj is bool || obj is float || obj is double || obj is int || obj is long || obj2 is bool || obj2 is float || obj2 is double || obj2 is int || obj2 is long)
				{
					obj3 = ConvertValue<bool>(obj) && ConvertValue<bool>(obj2);
				}
				break;
			case OperatorType.Or:
				if (obj is bool || obj is float || obj is double || obj is int || obj is long || obj2 is bool || obj2 is float || obj2 is double || obj2 is int || obj2 is long)
				{
					obj3 = ConvertValue<bool>(obj) || ConvertValue<bool>(obj2);
				}
				break;
			case OperatorType.Xor:
				if (obj is bool || obj is float || obj is double || obj is int || obj is long || obj2 is bool || obj2 is float || obj2 is double || obj2 is int || obj2 is long)
				{
					obj3 = ConvertValue<bool>(obj) ^ ConvertValue<bool>(obj2);
				}
				break;
			case OperatorType.Greater:
				if (obj is string || obj2 is string)
				{
					obj3 = string.CompareOrdinal(ConvertValue<string>(obj), ConvertValue<string>(obj2)) > 0;
				}
				else if ((obj is int || obj is long || obj is float || obj is double) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) > ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) > ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) > ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) > ConvertValue<double>(obj2))));
				}
				break;
			case OperatorType.GreaterEqual:
				if (obj is string || obj2 is string)
				{
					obj3 = string.CompareOrdinal(ConvertValue<string>(obj), ConvertValue<string>(obj2)) >= 0;
				}
				else if ((obj is int || obj is long || obj is float || obj is double) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) >= ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) >= ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) >= ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) >= ConvertValue<double>(obj2))));
				}
				break;
			case OperatorType.Less:
				if (obj is string || obj2 is string)
				{
					obj3 = string.CompareOrdinal(ConvertValue<string>(obj), ConvertValue<string>(obj2)) < 0;
				}
				else if ((obj is int || obj is long || obj is float || obj is double) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) < ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) < ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) < ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) < ConvertValue<double>(obj2))));
				}
				break;
			case OperatorType.LessEqual:
				if (obj is string && obj2 is string)
				{
					obj3 = string.CompareOrdinal(ConvertValue<string>(obj), ConvertValue<string>(obj2)) <= 0;
				}
				else if ((obj is int || obj is long || obj is float || obj is double) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double))
				{
					obj3 = ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) <= ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) <= ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) <= ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) <= ConvertValue<double>(obj2))));
				}
				break;
			case OperatorType.Equal:
			case OperatorType.NotEqual:
				obj3 = ((!(obj is string) && !(obj2 is string)) ? ((((obj is int || obj is long || obj is float || obj is double) && obj2 is bool) || ((obj2 is int || obj2 is long || obj2 is float || obj2 is double) && obj is bool)) ? ((object)(ConvertValue<bool>(obj) == ConvertValue<bool>(obj2))) : (((obj is int || obj is long || obj is float || obj is double) && (obj2 is int || obj2 is long || obj2 is float || obj2 is double)) ? ((!(obj is double) && !(obj2 is double)) ? ((!(obj is float) && !(obj2 is float)) ? ((!(obj is long) && !(obj2 is long)) ? ((object)(ConvertValue<int>(obj) == ConvertValue<int>(obj2))) : ((object)(ConvertValue<long>(obj) == ConvertValue<long>(obj2)))) : ((object)(ConvertValue<float>(obj) == ConvertValue<float>(obj2)))) : ((object)(ConvertValue<double>(obj) == ConvertValue<double>(obj2)))) : ((obj != null && obj2 != null && obj.GetType().IsPrimitive && obj2.GetType().IsPrimitive && obj.GetType() != obj2.GetType()) ? ((object)false) : ((obj is int) ? ((object)(ConvertValue<int>(obj) == ConvertValue<int>(obj2))) : ((obj is float) ? ((object)(ConvertValue<float>(obj) == ConvertValue<float>(obj2))) : ((obj is bool) ? ((object)(ConvertValue<bool>(obj) == ConvertValue<bool>(obj2))) : ((obj is Vector2) ? ((object)(ConvertValue<Vector2>(obj) == ConvertValue<Vector2>(obj2))) : ((obj is Vector3) ? ((object)(ConvertValue<Vector3>(obj) == ConvertValue<Vector3>(obj2))) : ((!(obj is Vector4)) ? ((object)(ConvertValue<object>(obj) == ConvertValue<object>(obj2))) : ((object)(ConvertValue<Vector4>(obj) == ConvertValue<Vector4>(obj2)))))))))))) : ((object)ConvertValue<string>(obj).Equals(ConvertValue<string>(obj2))));
				if (_operator == OperatorType.NotEqual)
				{
					obj3 = !ConvertValue<bool>(obj3);
				}
				break;
			case OperatorType.Approx:
				if (obj is float || obj2 is float)
				{
					obj3 = Mathf.Approximately(ConvertValue<float>(obj), ConvertValue<float>(obj2));
				}
				break;
			case OperatorType.NotApprox:
				if (obj is float || obj2 is float)
				{
					obj3 = !Mathf.Approximately(ConvertValue<float>(obj), ConvertValue<float>(obj2));
				}
				break;
			case OperatorType.Assign:
				if (_expr1.IsVariable)
				{
					if (obj2 == null)
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj2, typeof(object));
					}
					else
					{
						aWorkingMemory.SetItem(_expr1.VariableName, obj2, obj2.GetType());
					}
					obj3 = obj2;
				}
				break;
			}
			return obj3;
		}

		private object EvaluateFunction(float aDeltaTime, RAINMemory aWorkingMemory)
		{
			object result = null;
			switch (_function)
			{
			case FunctionType.DeltaTimeFunction:
				result = aDeltaTime;
				break;
			case FunctionType.CurrentTimeFunction:
				result = Time.time;
				break;
			case FunctionType.ClampFunction:
				if (_args.Length == 3)
				{
					object obj3 = _args[0].EvaluateObject(aDeltaTime, aWorkingMemory);
					object obj4 = _args[1].EvaluateObject(aDeltaTime, aWorkingMemory);
					object obj5 = _args[2].EvaluateObject(aDeltaTime, aWorkingMemory);
					if (obj3 is double || obj4 is double || obj5 is double || obj3 is float || obj4 is float || obj5 is float)
					{
						result = Mathf.Clamp(ConvertValue<float>(obj3), ConvertValue<float>(obj4), ConvertValue<float>(obj5));
					}
					if (obj3 is long || obj4 is long || obj5 is long || obj3 is int || obj4 is int || obj5 is int)
					{
						result = Mathf.Clamp(ConvertValue<int>(obj3), ConvertValue<int>(obj4), ConvertValue<int>(obj5));
					}
				}
				break;
			case FunctionType.MinFunction:
				if (_args.Length == 2)
				{
					object obj6 = _args[0].EvaluateObject(aDeltaTime, aWorkingMemory);
					object obj7 = _args[1].EvaluateObject(aDeltaTime, aWorkingMemory);
					if (obj6 is double || obj7 is double || obj6 is float || obj7 is float)
					{
						result = Mathf.Min(ConvertValue<float>(obj6), ConvertValue<float>(obj7));
					}
					else if (obj6 is long || obj7 is long || obj6 is int || obj7 is int)
					{
						result = Mathf.Min(ConvertValue<int>(obj6), ConvertValue<int>(obj7));
					}
				}
				break;
			case FunctionType.MaxFunction:
				if (_args.Length == 2)
				{
					object obj = _args[0].EvaluateObject(aDeltaTime, aWorkingMemory);
					object obj2 = _args[1].EvaluateObject(aDeltaTime, aWorkingMemory);
					if (obj is double || obj2 is double || obj is float || obj2 is float)
					{
						result = Mathf.Max(ConvertValue<float>(obj), ConvertValue<float>(obj2));
					}
					else if (obj is long || obj2 is long || obj is int || obj2 is int)
					{
						result = Mathf.Max(ConvertValue<int>(obj), ConvertValue<int>(obj2));
					}
				}
				break;
			case FunctionType.DebugFunction:
				if (_args.Length == 1)
				{
					string text = _args[0].Evaluate<string>(aDeltaTime, aWorkingMemory);
					if (text == "")
					{
						Debug.Log("(null)");
					}
					else
					{
						Debug.Log(text);
					}
				}
				break;
			case FunctionType.RandomFunction:
				if (_args.Length == 0)
				{
					result = UnityEngine.Random.value;
				}
				else if (_args.Length == 2)
				{
					return UnityEngine.Random.Range(_args[0].Evaluate<float>(aDeltaTime, aWorkingMemory), _args[1].Evaluate<float>(aDeltaTime, aWorkingMemory));
				}
				break;
			case FunctionType.TargetFunction:
				if (_args.Length == 1)
				{
					string aTargetName = _args[0].Evaluate<string>(aDeltaTime, aWorkingMemory);
					result = NavigationManager.Instance.GetNavigationTarget(aTargetName);
				}
				break;
			case FunctionType.WaypointFunction:
				if (_args.Length == 1)
				{
					string aWaypointSetName = _args[0].Evaluate<string>(aDeltaTime, aWorkingMemory);
					result = NavigationManager.Instance.GetWaypointSet(aWaypointSetName);
				}
				break;
			case FunctionType.GameObjectFunction:
				if (_args.Length == 1)
				{
					string name = _args[0].Evaluate<string>(aDeltaTime, aWorkingMemory);
					result = GameObject.Find(name);
				}
				break;
			case FunctionType.PositionFunction:
				if (_args.Length == 1)
				{
					GameObject gameObject = _args[0].Evaluate<GameObject>(aDeltaTime, aWorkingMemory);
					if (gameObject != null)
					{
						result = gameObject.transform.position;
					}
				}
				break;
			}
			return result;
		}

		private int GetOpPrecedence()
		{
			switch (_operator)
			{
			case OperatorType.Separator:
				return 0;
			case OperatorType.Assign:
			case OperatorType.AssignAddition:
			case OperatorType.AssignSubtraction:
			case OperatorType.AssignMultiplication:
			case OperatorType.AssignDivision:
				return 1;
			case OperatorType.And:
			case OperatorType.Or:
			case OperatorType.Xor:
				return 2;
			case OperatorType.Equal:
			case OperatorType.NotEqual:
			case OperatorType.Approx:
			case OperatorType.NotApprox:
				return 3;
			case OperatorType.Greater:
			case OperatorType.GreaterEqual:
			case OperatorType.Less:
			case OperatorType.LessEqual:
				return 4;
			case OperatorType.Addition:
			case OperatorType.Subtraction:
				return 5;
			case OperatorType.Multiplication:
			case OperatorType.Division:
				return 6;
			case OperatorType.Bang:
			case OperatorType.Negate:
			case OperatorType.Increment:
			case OperatorType.Decrement:
				return 7;
			default:
				return 0;
			}
		}

		private T ConvertValue<T>(object aValue)
		{
			if (aValue == null)
			{
				if (typeof(T) == typeof(string))
				{
					return (T)(object)"";
				}
				return default(T);
			}
			return TypeConvert.ConvertValue<T>(aValue);
		}

		private byte[] SerializeExpression()
		{
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (StreamWriter streamWriter = new StreamWriter(memoryStream))
				{
					streamWriter.WriteLine(_expressionAsEntered);
					streamWriter.Flush();
					return memoryStream.ToArray();
				}
			}
		}

		private void DeserializeExpression(byte[] aSerializedData)
		{
			using (MemoryStream stream = new MemoryStream(aSerializedData))
			{
				using (StreamReader streamReader = new StreamReader(stream))
				{
					Expression expression = ExpressionParser.Parse(streamReader.ReadLine());
					_expressionAsEntered = expression._expressionAsEntered;
					_expressionType = expression._expressionType;
					_constant = expression._constant;
					_variable = expression._variable;
					_function = expression._function;
					_args = expression._args;
					_operator = expression._operator;
					_expr1 = expression._expr1;
					_expr2 = expression._expr2;
					_isAtomic = expression._isAtomic;
				}
			}
		}
	}
}
