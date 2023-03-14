using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using UnityEngine;

namespace RAIN.Utility
{
	public class SimpleProfiler
	{
		private class TimerNode
		{
			private string _name = "";

			private long _individualTime;

			private long _totalTime;

			private TimerNode _parent;

			private List<TimerNode> _children = new List<TimerNode>();

			private TimerNode()
			{
			}

			public TimerNode(string aName)
			{
				_name = aName;
			}

			public TimerNode GetParent()
			{
				return _parent;
			}

			public TimerNode GetOrAddTimer(string aTimer)
			{
				for (int i = 0; i < _children.Count; i++)
				{
					if (_children[i]._name == aTimer)
					{
						return _children[i];
					}
				}
				TimerNode timerNode = new TimerNode();
				timerNode._name = aTimer;
				timerNode._parent = this;
				TimerNode timerNode2 = timerNode;
				_children.Add(timerNode2);
				return timerNode2;
			}

			public void AddIndividualTime(long aTime)
			{
				_individualTime += aTime;
				_totalTime += aTime;
				if (_parent != null)
				{
					_parent.AddTotalTime(aTime);
				}
			}

			public void AddTotalTime(long aTime)
			{
				_totalTime += aTime;
				if (_parent != null)
				{
					_parent.AddTotalTime(aTime);
				}
			}

			public void Reset()
			{
				_individualTime = 0L;
				_totalTime = 0L;
				_parent = null;
				_children.Clear();
			}

			public string PrintNode(int aTab)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(' ', aTab);
				stringBuilder.Append(_name);
				stringBuilder.Append(": ");
				stringBuilder.Append((double)_individualTime / (double)Stopwatch.Frequency);
				stringBuilder.Append(" ");
				stringBuilder.Append((double)_totalTime / (double)Stopwatch.Frequency);
				stringBuilder.Append("\n");
				for (int i = 0; i < _children.Count; i++)
				{
					stringBuilder.Append(_children[i].PrintNode(aTab + 1));
				}
				return stringBuilder.ToString();
			}
		}

		private static Dictionary<string, TimerNode> _profilerDictionary = new Dictionary<string, TimerNode>();

		private TimerNode _timerTree;

		private TimerNode _currentTimer;

		private Stack<string> _activeTimers = new Stack<string>();

		private Stack<Stopwatch> _activeWatches = new Stack<Stopwatch>();

		public static SimpleProfiler GetProfiler(string aProfilerName)
		{
			TimerNode value;
			lock (_profilerDictionary)
			{
				if (!_profilerDictionary.TryGetValue(aProfilerName, out value))
				{
					value = new TimerNode(aProfilerName);
					_profilerDictionary[aProfilerName] = value;
				}
			}
			return new SimpleProfiler(value);
		}

		[Conditional("DEBUG")]
		public static void PrintProfiler(string aProfilerName)
		{
			string message = aProfilerName;
			lock (_profilerDictionary)
			{
				TimerNode value;
				if (_profilerDictionary.TryGetValue(aProfilerName, out value))
				{
					message = value.PrintNode(2);
				}
			}
			UnityEngine.Debug.Log(message);
		}

		private SimpleProfiler(TimerNode aTimerCounts)
		{
			_timerTree = aTimerCounts;
			_currentTimer = _timerTree;
		}

		[Conditional("DEBUG")]
		public void Reset()
		{
			lock (_timerTree)
			{
				_timerTree.Reset();
				_currentTimer = _timerTree;
			}
		}

		[Conditional("DEBUG")]
		public void StartTimer(string aTimer)
		{
			if (_activeWatches.Count > 0)
			{
				_activeWatches.Peek().Stop();
			}
			lock (_timerTree)
			{
				_currentTimer = _currentTimer.GetOrAddTimer(aTimer);
			}
			_activeTimers.Push(aTimer);
			_activeWatches.Push(new Stopwatch());
			_activeWatches.Peek().Start();
		}

		[Conditional("DEBUG")]
		public void EndTimer(string aTimer)
		{
			string text = _activeTimers.Pop();
			if (text != aTimer)
			{
				throw new Exception("Mismatched StartTimer/EndTimer statements: " + text + " " + aTimer);
			}
			Stopwatch stopwatch = _activeWatches.Pop();
			stopwatch.Stop();
			lock (_timerTree)
			{
				_currentTimer.AddIndividualTime(stopwatch.ElapsedTicks);
				_currentTimer = _currentTimer.GetParent();
			}
			if (_activeWatches.Count > 0)
			{
				_activeWatches.Peek().Start();
			}
		}
	}
}
