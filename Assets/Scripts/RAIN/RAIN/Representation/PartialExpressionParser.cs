using System;

namespace RAIN.Representation
{
	public static class PartialExpressionParser
	{
		private class PartialExpressionParseException : Exception
		{
		}

		public static PartialExpression Parse(string aExpression)
		{
			if (aExpression == null)
			{
				aExpression = "";
			}
			PartialExpression partialExpression = null;
			try
			{
				string aExpression2 = aExpression.Trim();
				partialExpression = ParseExpression(ref aExpression2);
				if (partialExpression == null || aExpression2.Length != 0)
				{
					throw new PartialExpressionParseException();
				}
			}
			catch (Exception)
			{
				partialExpression = new PartialExpression();
			}
			partialExpression.ExpressionAsEntered = aExpression;
			return partialExpression;
		}

		private static PartialExpression ParseExpression(ref string aExpression)
		{
			PartialExpression partialExpression = new PartialExpression();
			if (aExpression.Length == 0)
			{
				return null;
			}
			Expression.OperatorType operatorType = ExpressionParser.ParseUnaryOperator(ref aExpression);
			if (operatorType != Expression.OperatorType.NoOp)
			{
				return null;
			}
			string text = ParseVariable(ref aExpression);
			if (text == null)
			{
				return null;
			}
			partialExpression.VariableName = text;
			if (aExpression.Length == 0)
			{
				return partialExpression;
			}
			Expression.OperatorType operatorType2 = ParseComparisonOperator(ref aExpression);
			if (operatorType2 == Expression.OperatorType.NoOp)
			{
				return null;
			}
			partialExpression.OperatorType = operatorType2;
			if (aExpression.Length == 0)
			{
				return partialExpression;
			}
			Expression expression = ExpressionParser.ParseExpression(ref aExpression);
			if (expression == null)
			{
				return null;
			}
			partialExpression.RHS = expression;
			return partialExpression;
		}

		private static string ParseVariable(ref string aExpression)
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
			if (text2.Equals(Expression.Keywords.Null, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			if (text2.Equals(Expression.Keywords.True, StringComparison.OrdinalIgnoreCase) || text2.Equals(Expression.Keywords.False, StringComparison.OrdinalIgnoreCase))
			{
				return null;
			}
			aExpression = text;
			return text2;
		}

		private static Expression.OperatorType ParseComparisonOperator(ref string aExpression)
		{
			Expression.OperatorType operatorType = Expression.OperatorType.NoOp;
			if (aExpression.Length > 1)
			{
				switch (aExpression.Substring(0, 2))
				{
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
				case '>':
					operatorType = Expression.OperatorType.Greater;
					break;
				case '<':
					operatorType = Expression.OperatorType.Less;
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
