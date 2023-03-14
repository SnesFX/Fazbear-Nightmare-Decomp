using System.Collections.Generic;
using RAIN.Navigation.NavMesh.RecastNodes;
using RAIN.Utility;

namespace RAIN.Navigation.NavMesh.Collectors
{
	public abstract class SpanMesh
	{
		public abstract Point3 Origin { get; }

		public abstract int Width { get; }

		public abstract int Length { get; }

		public SimpleProfiler Profiler { get; set; }

		public abstract List<Span>[] GetAllSpans(int aStartX, int aStartZ, int aEndX, int aEndZ);
	}
}
