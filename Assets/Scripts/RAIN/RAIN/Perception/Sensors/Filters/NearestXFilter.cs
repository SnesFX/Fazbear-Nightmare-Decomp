using System.Collections.Generic;
using RAIN.Core;
using RAIN.Entities.Aspects;
using RAIN.Serialization;

namespace RAIN.Perception.Sensors.Filters
{
	[RAINSerializableClass]
	[RAINElement("Nearest X Filter")]
	public class NearestXFilter : RAINSensorFilter
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "The number of Aspects to keep")]
		public int countX = 1;

		[RAINNonSerializableField]
		private LinkedList<KeyValuePair<RAINAspect, float>> items = new LinkedList<KeyValuePair<RAINAspect, float>>();

		[RAINNonSerializableField]
		private RAINAspect[] results = new RAINAspect[1];

		public override void Filter(RAINSensor aSensor, List<RAINAspect> aValues)
		{
			if (countX < 0)
			{
				countX = 0;
			}
			if (results.Length != countX)
			{
				results = new RAINAspect[countX];
			}
			items.Clear();
			LinkedListNode<KeyValuePair<RAINAspect, float>> linkedListNode = null;
			foreach (RAINAspect aValue in aValues)
			{
				if (aValue == null)
				{
					continue;
				}
				float magnitude = (aValue.Position - aSensor.Position).magnitude;
				linkedListNode = items.First;
				while (true)
				{
					if (linkedListNode == null)
					{
						items.AddLast(new KeyValuePair<RAINAspect, float>(aValue, magnitude));
						break;
					}
					if (linkedListNode.Value.Value > magnitude)
					{
						items.AddBefore(linkedListNode, new KeyValuePair<RAINAspect, float>(aValue, magnitude));
						break;
					}
					linkedListNode = linkedListNode.Next;
				}
				if (items.Count > countX)
				{
					items.RemoveLast();
				}
			}
			aValues.Clear();
			linkedListNode = items.First;
			for (int i = 0; i < results.Length; i++)
			{
				if (linkedListNode == null)
				{
					break;
				}
				aValues.Add(linkedListNode.Value.Key);
				linkedListNode = linkedListNode.Next;
			}
		}
	}
}
