using System.Collections.Generic;
using System.IO;
using RAIN.Navigation.Graph;

namespace RAIN.Navigation.NavMesh
{
	public class NavMeshNode : NavigationGraphNode
	{
		public NavMeshNode(RAINNavigationGraph aGraph)
			: base(aGraph)
		{
		}

		public virtual void RemapVertices(int[] aVertexMap)
		{
		}

		public virtual void Serialize(Dictionary<NavMeshNode, int> aNodeLookup, Stream aStream)
		{
		}

		public virtual void Deserialize(NavMeshNode[] aIndexLookup, Stream aStream)
		{
		}
	}
}
