using RAIN.Serialization;

namespace RAIN.Core
{
	[RAINSerializableClass]
	public abstract class CustomAIElement : RAINAIElement
	{
		[RAINSerializableField(Visibility = FieldVisibility.Show, ToolTip = "Name", OldFieldNames = new string[] { "elementName" })]
		private string _name = "";

		public virtual string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}

		public virtual void Pre()
		{
		}

		public virtual void Sense()
		{
		}

		public virtual void Think()
		{
		}

		public virtual void Act()
		{
		}

		public virtual void Post()
		{
		}

		public virtual void RootMotion()
		{
		}

		public virtual void IK(int aLayerIndex)
		{
		}

		public virtual void Destroy()
		{
		}
	}
}
