namespace RAIN.Representation
{
	public class PartialExpression
	{
		private string variableName;

		private Expression.OperatorType operatorType = Expression.OperatorType.NoOp;

		private string expressionAsEntered;

		private Expression expr2;

		public string VariableName
		{
			get
			{
				return variableName;
			}
			set
			{
				variableName = value;
			}
		}

		public Expression.OperatorType OperatorType
		{
			get
			{
				return operatorType;
			}
			set
			{
				operatorType = value;
			}
		}

		public string ExpressionAsEntered
		{
			get
			{
				return expressionAsEntered;
			}
			set
			{
				expressionAsEntered = value;
			}
		}

		public Expression RHS
		{
			get
			{
				return expr2;
			}
			set
			{
				expr2 = value;
			}
		}

		public void Clear()
		{
			variableName = null;
			operatorType = Expression.OperatorType.NoOp;
			expr2 = null;
			expressionAsEntered = null;
		}

		public void RenameVariable(string originalName, string newName)
		{
			if (variableName == originalName)
			{
				variableName = newName;
			}
			if (expr2 != null)
			{
				expr2.RenameVariable(originalName, newName);
			}
		}
	}
}
