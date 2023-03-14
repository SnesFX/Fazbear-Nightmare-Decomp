using RAIN.Serialization;

namespace RAIN.Core
{
	[RAINSerializableClass]
	public abstract class RAINAIElement : RAINElement
	{
		private AI _ai;

		private bool _initialized;

		public virtual AI AI
		{
			get
			{
				return _ai;
			}
		}

		public virtual bool Initialized
		{
			get
			{
				return _initialized;
			}
		}

		public void AIInit(AI ai)
		{
			_ai = ai;
			_initialized = true;
			AIInit();
		}

		public virtual void AIInit()
		{
		}

		public virtual void BodyInit()
		{
		}

		public virtual void Start()
		{
		}
	}
}
