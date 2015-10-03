/**
 * Contains the elements of a user interface
 **/

namespace SlimDX.Common
{
	public class UI
	{
		public UI()
		{
			Container = new ElementContainer();
		}

		public ElementContainer Container { get; set; }
	}
}
