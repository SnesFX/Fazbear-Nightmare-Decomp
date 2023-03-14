using System;
using RAIN.Navigation.Graph;

namespace RAIN.Navigation.Pathfinding
{
	public class LPAPathNodeHelper : PathNodeHelper
	{
		public float rhs;

		public float g;

		protected NavigationGraphNode _startNode;

		public LPAPathNodeHelper(NavigationGraphNode aNode, NavigationGraphNode aStartNode)
			: base(aNode)
		{
			rhs = float.PositiveInfinity;
			g = float.PositiveInfinity;
			_startNode = aStartNode;
		}

		public override int CompareTo(PathNodeHelper h)
		{
			LPAPathNodeHelper lPAPathNodeHelper = (LPAPathNodeHelper)h;
			if (lPAPathNodeHelper == null)
			{
				return 1;
			}
			float num = K1();
			float num2 = lPAPathNodeHelper.K1();
			if (num == num2)
			{
				num = K2();
				num2 = lPAPathNodeHelper.K2();
			}
			if (num < num2)
			{
				return -1;
			}
			if (num > num2)
			{
				return 1;
			}
			return 0;
		}

		private float K1()
		{
			return Math.Min(g, rhs + HeuristicCostFrom(_startNode));
		}

		private float K2()
		{
			return Math.Min(g, rhs);
		}
	}
}
