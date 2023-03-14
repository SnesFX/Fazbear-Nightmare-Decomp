using System;

namespace RAIN.Navigation.Graph
{
	public class NavigationGraphException : Exception
	{
		public NavigationGraphException()
		{
		}

		public NavigationGraphException(string message)
			: base(message)
		{
		}

		public NavigationGraphException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
