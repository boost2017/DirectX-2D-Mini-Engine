/**
 * A clock.
 **/

using System.Diagnostics;
using SlimDX;

namespace SlimDX.Common
{
	class Clock
	{
		#region Public Interface

		public Clock()
		{
			frequency = Stopwatch.Frequency;
		}

		public void Start()
		{
			count = Stopwatch.GetTimestamp();
			isRunning = true;
		}

		/**
		 * Returns how many seconds since the last call to this method
		 **/
		public float Update()
		{
			float result = 0.0f;
			if (isRunning)
			{
				long last = count;
				count = Stopwatch.GetTimestamp();
				result = (float)(count - last) / frequency;
			}

			return result;
		}

		#endregion

		#region Implementation Detail

		private bool isRunning;
		private readonly long frequency;
		private long count;

		#endregion
	}
}
