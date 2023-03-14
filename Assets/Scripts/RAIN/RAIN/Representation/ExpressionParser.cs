using System;
using System.Collections.Generic;
using UnityEngine;

namespace RAIN.Representation
{
	public static class ExpressionParser
	{
		public static Expression Parse(string aExpression)
		{
			if (aExpression == null)
			{
				aExpression = "";
			}
			string aExpression2 = aExpression.Trim();
			Expression expression = ParseExpression(ref aExpression2);
			if (expression == null || aExpression2.Length != 0)
			{
				expression = new Expression();
			}
			expression.SimplifyConstants();
			expression.ExpressionAsEntered = aExpression;
			return expression;
		}

		internal static Expression ParseExpression(ref string aExpression)
		{
			if (aExpression.Length == 0)
			{
				return null;
			}
			Expression.OperatorType operatorType = ParseUnaryOperator(ref aExpression);
			Expression expression = ParseNumber(ref aExpression);
			if (expression == null)
			{
				expression = ParseString(ref aExpression);
			}
			if (expression == null)
			{
				expression = ParseVector(ref aExpression);
			}
			if (expression == null)
			{
				expression = ParseFunction(ref aExpression);
			}
			if (expression == null)
			{
				expression = ParseVariableOrKeyword(ref aExpression);
			}
			if (expression == null)
			{
				expression = ParseTerm(ref aExpression);
			}
			if (expression == null)
			{
				return null;
			}
			if (operatorType != Expression.OperatorType.NoOp)
			{
				Expression expression2 = new Expression();
				expression2.SetUnary(operatorType, expression);
				expression = expression2;
			}
			if (aExpression.Length == 0)
			{
				return expression;
			}
			if (aExpression[0] == ',')
			{
				return expression;
			}
			if (aExpression[0] == ')')
			{
				return expression;
			}
			Expression.OperatorType operatorType2 = ParseBinaryOperator(ref aExpression);
			if (operatorType2 == Expression.OperatorType.NoOp)
			{
				return null;
			}
			Expression expression3 = ParseExpression(ref aExpression);
			if (expression3 == null)
			{
				if (operatorType2 == Expression.OperatorType.Separator)
				{
					return expression;
				}
				return null;
			}
			Expression expression4 = new Expression();
			expression4.SetBinary(expression, operatorType2, expression3);
			expression4.FixOperatorPrecedence();
			aExpression = aExpression.Trim();
			return expression4;
		}

		private static Expression ParseNumber(ref string aExpression)
		{
			string text = aExpression;
			bool flag = false;
			int num = 0;
			while (num < text.Length)
			{
				if (text[num] == '.')
				{
					if (flag)
					{
						return null;
					}
					flag = true;
					num++;
				}
				else
				{
					if (!char.IsDigit(text[num]))
					{
						break;
					}
					num++;
				}
			}
			if (num == 0)
			{
				return null;
			}
			string text2 = text.Substring(0, num);
			text = text.Substring(num).Trim();
			if (text2[0] == '.')
			{
				text2 = "0" + text2;
			}
			if (text2[text2.Length - 1] == '.')
			{
				text2 += "0";
			}
			Expression expression = new Expression();
			int result2;
			if (flag)
			{
				float result;
				if (float.TryParse(text2, out result))
				{
					expression.SetConstant(result);
				}
				else
				{
					expression.SetConstant(double.Parse(text2));
				}
			}
			else if (int.TryParse(text2, out result2))
			{
				expression.SetConstant(result2);
			}
			else
			{
				expression.SetConstant(long.Parse(text2));
			}
			aExpression = text;
			return expression;
		}

		private static Expression ParseString(ref string aExpression)
		{
			if (aExpression.Length < 2 || aExpression[0] != '"')
			{
				return null;
			}
			string text = aExpression;
			text = text.Substring(1);
			int num = 0;
			do
			{
				num = text.IndexOf('"', num);
				if (num < 0)
				{
					return null;
				}
			}
			while (num != 0 && text[num - 1] == '\\');
			string constant = text.Substring(0, num);
			text = text.Substring(num + 1).Trim();
			Expression expression = new Expression();
			expression.SetConstant(constant);
			aExpression = text;
			return expression;
		}

		private static Expression ParseVector(ref string aExpression)
		{
			if (aExpression.Length == 0)
			{
				return null;
			}
			string text = aExpression;
			if (text[0] != '(')
			{
				return null;
			}
			text = text.Substring(1).Trim();
			List<float> list = new List<float>();
			while (true)
			{
				Expression.OperatorType operatorType = ParseUnaryOperator(ref text);
				Expression expression = ParseNumber(ref text);
				if (expression == null || text.Length == 0)
				{
					return null;
				}
				float num = expression.Evaluate<float>(0f, null);
				if (operatorType == Expression.OperatorType.Negate)
				{
					num = 0f - num;
				}
				list.Add(num);
				if (text[0] != ',')
				{
					break;
				}
				text = text.Substring(1).Trim();
			}
			if (text[0] == ')')
			{
				text = text.Substring(1).Trim();
				Expression expression2 = new Expression();
				if (list.Count == 2)
				{
					expression2.SetConstant(new Vector2(list[0], list[1]));
				}
				else if (list.Count == 3)
				{
					expression2.SetConstant(new Vector3(list[0], list[1], list[2]));
				}
				else
				{
					if (list.Count != 4)
					{
						return null;
					}
					expression2.SetConstant(new Vector4(list[0], list[1], list[2], list[3]));
				}
				aExpression = text;
				return expression2;
			}
			return null;
		}

		private static Expression ParseFunction(ref string aExpression)
		{
			if (aExpression.Length == 0 || (!char.IsLetter(aExpression[0]) && aExpression[0] != '_'))
			{
				return null;
			}
			string text = aExpression;
			int i;
			for (i = 0; i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'); i++)
			{
			}
			if (i == 0)
			{
				return null;
			}
			string text2 = text.Substring(0, i);
			text = text.Substring(i);
			if (text.Length == 0 || text[0] != '(')
			{
				return null;
			}
			text = text.Substring(1).Trim();
			List<Expression> list = new List<Expression>();
			while (text.Length > 0 && text[0] != ')')
			{
				Expression expression = ParseExpression(ref text);
				if (expression == null)
				{
					return null;
				}
				list.Add(expression);
				if (text.Length > 0 && text[0] == ',')
				{
					text = text.Substring(1).Trim();
				}
			}
			if (text.Length == 0)
			{
				return null;
			}
			text = text.Substring(1).Trim();
			Expression expression2 = new Expression();
			if (text2.Equals(Expression.Keywords.DeltaTimeFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.DeltaTimeFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.CurrentTimeFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.CurrentTimeFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.ClampFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.ClampFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.MinFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.MinFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.MaxFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.MaxFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.DebugFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.DebugFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.RandomFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.RandomFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.TargetFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.TargetFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.WaypointFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.WaypointFunction, list.ToArray());
			}
			else if (text2.Equals(Expression.Keywords.GameObjectFunction, StringComparison.OrdinalIgnoreCase))
			{
				expression2.SetFunction(Expression.FunctionType.GameObjectFunction, list.ToArray());
			}
			else
			{
				if (!text2.Equals(Expression.Keywords.PositionFunction, StringComparison.OrdinalIgnoreCase))
				{
					return null;
				}
				expression2.SetFunction(Expression.FunctionType.PositionFunction, list.ToArray());
			}
			aExpression = text;
			return expression2;
		}

		private static Expression ParseVariableOrKeyword(ref string aExpression)
		{
			if (aExpression.Length == 0 || (!char.IsLetter(aExpression[0]) && aExpression[0] != '_'))
			{
				return null;
			}
			string text = aExpression;
			int i;
			for (i = 0; i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'); i++)
			{
			}
			if (i == 0)
			{
				return null;
			}
			string text2 = text.Substring(0, i);
			text = text.Substring(i).Trim();
			Expression expression = new Expression();
			if (text2.Equals(Expression.Keywords.Null, StringComparison.OrdinalIgnoreCase))
			{
				expression.SetNull();
			}
			else if (text2.Equals(Expression.Keywords.True, StringComparison.OrdinalIgnoreCase) || text2.Equals(Expression.Keywords.False, StringComparison.OrdinalIgnoreCase))
			{
				expression.SetConstant(bool.Parse(text2));
			}
			else
			{
				expression.SetVariable(text2);
			}
			aExpression = text;
			return expression;
		}

		private static Expression ParseTerm(ref string aExpression)
		{
			if (aExpression.Length == 0)
			{
				return null;
			}
			string text = aExpression;
			if (text[0] != '(')
			{
				return null;
			}
			text = text.Substring(1).Trim();
			Expression expression = ParseExpression(ref text);
			if (expression == null)
			{
				return null;
			}
			expression.SetAtomic(true);
			if (text.Length == 0 || text[0] != ')')
			{
				return null;
			}
			text = text.Substring(1).Trim();
			aExpression = text;
			return expression;
		}

		internal static Expression.OperatorType ParseUnaryOperator(ref string aExpression)
		{
			Expression.OperatorType operatorType = Expression.OperatorType.NoOp;
			if (aExpression.Length > 1)
			{
				string text = aExpression.Substring(0, 2);
				if (text == "++")
				{
					operatorType = Expression.OperatorType.Increment;
				}
				else if (text == "--")
				{
					operatorType = Expression.OperatorType.Decrement;
				}
				if (operatorType != Expression.OperatorType.NoOp)
				{
					aExpression = aExpression.Substring(2).Trim();
					return operatorType;
				}
			}
			if (aExpression.Length > 0)
			{
				switch (aExpression[0])
				{
				case '!':
					operatorType = Expression.OperatorType.Bang;
					break;
				case '-':
					operatorType = Expression.OperatorType.Negate;
					break;
				}
				if (operatorType != Expression.OperatorType.NoOp)
				{
					aExpression = aExpression.Substring(1).Trim();
					return operatorType;
				}
			}
			return operatorType;
		}

		private static Expression.OperatorType ParseBinaryOperator(ref string aExpression)
		{
			Expression.OperatorType operatorType = Expression.OperatorType.NoOp;
			if (aExpression.Length > 1)
			{
				switch (aExpression.Substring(0, 2))
				{
				case "&&":
					operatorType = Expression.OperatorType.And;
					break;
				case "||":
					operatorType = Expression.OperatorType.Or;
					break;
				case ">=":
					operatorType = Expression.OperatorType.GreaterEqual;
					break;
				case "<=":
					operatorType = Expression.OperatorType.LessEqual;
					break;
				case "==":
					operatorType = Expression.OperatorType.Equal;
					break;
				case "!=":
					operatorType = Expression.OperatorType.NotEqual;
					break;
				case "~~":
					operatorType = Expression.OperatorType.Approx;
					break;
				case "!~":
					operatorType = Expression.OperatorType.NotApprox;
					break;
				case "+=":
					operatorType = Expression.OperatorType.AssignAddition;
					break;
				case "-=":
					operatorType = Expression.OperatorType.AssignSubtraction;
					break;
				case "*=":
					operatorType = Expression.OperatorType.AssignMultiplication;
					break;
				case "/=":
					operatorType = Expression.OperatorType.AssignDivision;
					break;
				}
				if (operatorType != Expression.OperatorType.NoOp)
				{
					aExpression = aExpression.Substring(2).Trim();
					return operatorType;
				}
			}
			if (aExpression.Length > 0)
			{
				switch (aExpression[0])
				{
				case ';':
					operatorType = Expression.OperatorType.Separator;
					break;
				case '.':
					operatorType = Expression.OperatorType.Dot;
					break;
				case '+':
					operatorType = Expression.OperatorType.Addition;
					break;
				case '-':
					operatorType = Expression.OperatorType.Subtraction;
					break;
				case '/':
					operatorType = Expression.OperatorType.Division;
					break;
				case '*':
					operatorType = Expression.OperatorType.Multiplication;
					break;
				case '^':
					operatorType = Expression.OperatorType.Xor;
					break;
				case '>':
					operatorType = Expression.OperatorType.Greater;
					break;
				case '<':
					operatorType = Expression.OperatorType.Less;
					break;
				case '=':
					operatorType = Expression.OperatorType.Assign;
					break;
				}
				if (operatorType != Expression.OperatorType.NoOp)
				{
					aExpression = aExpression.Substring(1).Trim();
					return operatorType;
				}
			}
			return operatorType;
		}
	}
}
