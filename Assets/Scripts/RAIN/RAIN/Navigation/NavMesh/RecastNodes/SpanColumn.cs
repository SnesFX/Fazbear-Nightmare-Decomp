using System.Collections.Generic;

namespace RAIN.Navigation.NavMesh.RecastNodes
{
	public class SpanColumn
	{
		private List<Span> _spans = new List<Span>();

		public List<Span> Spans
		{
			get
			{
				return _spans;
			}
			set
			{
				_spans = value;
			}
		}

		public void Add(Span aSpan)
		{
			for (int i = 0; i < _spans.Count; i++)
			{
				if (aSpan.MinHeight < _spans[i].MinHeight)
				{
					_spans.Insert(i, aSpan);
					return;
				}
			}
			_spans.Add(aSpan);
		}

		public Span GetSpan(int aSpanIndex)
		{
			return _spans[aSpanIndex];
		}

		public void RemoveSpan(int aSpanIndex)
		{
			_spans.RemoveAt(aSpanIndex);
		}

		public List<Span> GetAllSpans()
		{
			return _spans;
		}

		public int GetSpanCount()
		{
			return _spans.Count;
		}

		public int MergeVolumes()
		{
			int count = _spans.Count;
			for (int i = 0; i + 1 < _spans.Count; i++)
			{
				if (_spans[i].VolumeID <= 0)
				{
					continue;
				}
				int num = _spans.Count - 1;
				while (num > i && _spans[i].VolumeID != _spans[num].VolumeID)
				{
					num--;
				}
				while (num > i)
				{
					if (_spans[num].VolumeID == _spans[i].VolumeID)
					{
						if (_spans[i].MaxHeight <= _spans[num].MaxHeight)
						{
							_spans[i].Absorb(_spans[num]);
						}
						_spans.RemoveAt(num);
					}
					num--;
				}
			}
			return count - _spans.Count;
		}

		public int Merge()
		{
			int count = _spans.Count;
			int num = 0;
			int num2 = 1;
			while (num + 1 < _spans.Count)
			{
				if (num2 >= _spans.Count)
				{
					num++;
					num2 = num + 1;
				}
				else if (_spans[num].MaxHeight >= _spans[num2].MinHeight)
				{
					_spans[num].Absorb(_spans[num2]);
					_spans.RemoveAt(num2);
				}
				else
				{
					num2++;
				}
			}
			return count - _spans.Count;
		}

		public void MarkUnwalkableByHeight(int aWalkableHeight)
		{
			for (int i = 0; i < _spans.Count - 1; i++)
			{
				if (_spans[i + 1].MinHeight - _spans[i].MaxHeight < aWalkableHeight)
				{
					_spans[i].Walkable = false;
				}
			}
		}
	}
}
