/**
 * App's configuration descriptions
 **/

namespace SlimDX.Common
{
	public class DirectXAppConfiguration
	{
		#region Public Interface

		/**
		 * Default values for the window
		 */
		public DirectXAppConfiguration(int Width = 800, int Height = 600)
		{
			WindowTitle = "App";
			WindowWidth = Width;
			WindowHeight = Height;
		}

		public string WindowTitle { get; set; }

		public int WindowWidth { get; set; }

		public int WindowHeight { get; set; }

		#endregion
	}
}
