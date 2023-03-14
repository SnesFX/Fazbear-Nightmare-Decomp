using System;
using System.Collections.Generic;
using System.Threading;
using RAIN.Core;
using RAIN.Navigation.NavMesh.RecastNodes;
using RAIN.Navigation.NavMesh.RecastProcess;
using RAIN.Serialization;
using RAIN.Utility;
using UnityEngine;

namespace RAIN.Navigation.NavMesh
{
	[RAINSerializableClass]
	public class NavMesh : RAINElement
	{
		public enum DisplayModeEnum
		{
			None = 0,
			GridLines = 1,
			NavigationMesh = 2
		}

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Graph Name")]
		private string _graphName = "";

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Size of the area covered by the NavMesh")]
		private float _size = 10f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Automatically determine grid size for parallel recast")]
		private bool _automaticGridSize = true;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Gridding size for parallel recast")]
		private int _gridSize = 4;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization mesh color")]
		private Color _meshColor = Color.green;

		[RAINSerializableField(Visibility = FieldVisibility.Hide, ToolTip = "Visualization outline color offset")]
		private float _outlineColorOffset = -0.5f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Visualization height")]
		private float _visualHeightOffset = 0.25f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Current Visualization")]
		private DisplayModeEnum _displayMode;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Layers included in Recast")]
		private LayerMask _includedLayers = -1;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Tags ignored during Recast")]
		private List<string> _ignoredTags = new List<string>();

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Tags marking unwalkable objects")]
		private List<string> _unwalkableTags = new List<string>();

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Tags for matching Navigators to Graphs")]
		private List<string> _graphTags = new List<string>();

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Max walkable slope angle")]
		private float _maxSlope = 45f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Vertical clearance needed")]
		private float _walkableHeight = 2f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Distance to stay away from objects")]
		private float _walkableRadius = 0.55f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Max step up height")]
		private float _stepHeight = 0.75f;

		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Mesh generation granularity")]
		private float _cellSize = 0.1f;

		[RAINSerializableField(Visibility = FieldVisibility.ShowAdvanced, ToolTip = "Maximum vertex smoothing")]
		private float _maxVertexError = 0.2f;

		[RAINSerializableField(Visibility = FieldVisibility.Hide)]
		private NavMeshPathGraph _graph = new NavMeshPathGraph();

		private ContourCreator _creator;

		private bool _creatingContours;

		private float _creatingProgress;

		public string GraphName
		{
			get
			{
				return _graphName;
			}
			set
			{
				_graphName = value;
				_graph.graphName = value;
			}
		}

		public float Size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
			}
		}

		public bool AutomaticGridSize
		{
			get
			{
				return _automaticGridSize;
			}
			set
			{
				_automaticGridSize = value;
			}
		}

		public int GridSize
		{
			get
			{
				return _gridSize;
			}
			set
			{
				_gridSize = value;
			}
		}

		public Color MeshColor
		{
			get
			{
				return _meshColor;
			}
			set
			{
				_meshColor = value;
			}
		}

		public float OutlineColorOffset
		{
			get
			{
				return _outlineColorOffset;
			}
			set
			{
				_outlineColorOffset = value;
			}
		}

		public float VisualHeightOffset
		{
			get
			{
				return _visualHeightOffset;
			}
			set
			{
				_visualHeightOffset = value;
			}
		}

		public DisplayModeEnum DisplayMode
		{
			get
			{
				return _displayMode;
			}
			set
			{
				_displayMode = value;
			}
		}

		public LayerMask IncludedLayers
		{
			get
			{
				return _includedLayers;
			}
			set
			{
				_includedLayers = value;
			}
		}

		public List<string> IgnoredTags
		{
			get
			{
				return _ignoredTags;
			}
			set
			{
				_ignoredTags = value;
			}
		}

		public List<string> UnwalkableTags
		{
			get
			{
				return _unwalkableTags;
			}
			set
			{
				_unwalkableTags = value;
			}
		}

		public IList<string> GraphTags
		{
			get
			{
				return _graphTags.AsReadOnly();
			}
		}

		public float MaxSlope
		{
			get
			{
				return _maxSlope;
			}
			set
			{
				_maxSlope = value;
			}
		}

		public float WalkableHeight
		{
			get
			{
				return _walkableHeight;
			}
			set
			{
				_walkableHeight = value;
			}
		}

		public float WalkableRadius
		{
			get
			{
				return _walkableRadius;
			}
			set
			{
				_walkableRadius = value;
			}
		}

		public float StepHeight
		{
			get
			{
				return _stepHeight;
			}
			set
			{
				_stepHeight = value;
			}
		}

		public float CellSize
		{
			get
			{
				return _cellSize;
			}
			set
			{
				_cellSize = value;
			}
		}

		public float MaxVertexError
		{
			get
			{
				return _maxVertexError;
			}
			set
			{
				_maxVertexError = value;
			}
		}

		public NavMeshPathGraph Graph
		{
			get
			{
				return _graph;
			}
		}

		public bool Creating
		{
			get
			{
				return _creatingContours;
			}
		}

		public float CreatingProgress
		{
			get
			{
				return _creatingProgress;
			}
		}

		public void RegisterNavigationGraph()
		{
			if (_graph.Size == 0)
			{
				Debug.LogWarning("NavMesh: Navigation graph is empty.");
				return;
			}
			_graph.graphName = _graphName;
			_graph.tags = _graphTags;
			_graph.SetParams(_cellSize, _stepHeight, _walkableHeight);
			_graph.InitPolyBuckets();
			NavigationManager.Instance.Register(_graph);
		}

		public void UnregisterNavigationGraph()
		{
			NavigationManager.Instance.Unregister(_graph);
		}

		public void ClearNavigationGraph()
		{
			UnregisterNavigationGraph();
			_graph = new NavMeshPathGraph();
		}

		public int DetermineGridSize(RAINComponent aComponent)
		{
			return DetermineGridSize(aComponent.transform.localScale);
		}

		public int DetermineGridSize(Vector3 aScale)
		{
			if (_automaticGridSize)
			{
				return Mathf.Max(1, Mathf.FloorToInt(_size * Mathf.Max(aScale.x, aScale.z) / (_cellSize * 250f)));
			}
			return _gridSize;
		}

		public void StartCreatingContours(RAINComponent aComponent, int aThreadCount)
		{
			if (aThreadCount == -1)
			{
				aThreadCount = Mathf.Max(1, SystemInfo.processorCount / 2);
			}
			StartCreatingContours(aComponent.gameObject.transform.position, aComponent.gameObject.transform.localScale, aThreadCount);
		}

		public void StartCreatingContours(Vector3 aPosition, Vector3 aScale, int aThreadCount)
		{
			ClearNavigationGraph();
			_creator = new ContourCreator(new Bounds(aPosition, aScale * _size), _includedLayers, _ignoredTags, _unwalkableTags, _maxSlope, _cellSize, DetermineGridSize(aScale), _cellSize * 2f, _walkableHeight, _walkableRadius, _walkableRadius, _stepHeight, 0f, 0f, _maxVertexError, 0f, aThreadCount);
			_creatingContours = true;
			_creatingProgress = 0f;
		}

		public void CreateContours()
		{
			if (_creatingContours)
			{
				List<ContourMeshData> aMeshes;
				_creatingContours = _creator.CreateContours(out aMeshes);
				_creatingProgress = _creator.Progress;
				for (int i = 0; i < aMeshes.Count; i++)
				{
					_graph.AddContourVertices(aMeshes[i]);
				}
			}
			if (!_creatingContours)
			{
				SimpleProfiler.GetProfiler("RAINNavMesh");
				_creator.FinishCreation();
				_creator = null;
				_graph.CompactNavMesh();
				GC.Collect();
			}
		}

		public void CreateAllContours()
		{
			while (_creatingContours)
			{
				List<ContourMeshData> aMeshes;
				_creatingContours = _creator.CreateContours(out aMeshes);
				_creatingProgress = _creator.Progress;
				for (int i = 0; i < aMeshes.Count; i++)
				{
					_graph.AddContourVertices(aMeshes[i]);
				}
				if (_creator.Asynchronous)
				{
					Thread.Sleep(10);
				}
			}
			if (!_creatingContours)
			{
				SimpleProfiler.GetProfiler("RAINNavMesh");
				_creator.FinishCreation();
				_creator = null;
				_graph.CompactNavMesh();
				GC.Collect();
			}
		}

		public void CancelCreatingContours()
		{
			ClearNavigationGraph();
			_creator.CancelCreatingContours();
			_creator = null;
			GC.Collect();
			_creatingContours = false;
			_creatingProgress = 0f;
		}
	}
}
