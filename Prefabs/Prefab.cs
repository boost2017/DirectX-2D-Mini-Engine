using SlimDX;
using SlimDX.Direct2D;

namespace SlimDX.Common
{
	public abstract class Prefab
	{
		#region Public Interface

		public DirectXApp App;

		protected virtual DeviceContext2D Device { get { return App.Context2D; } }

		public virtual int X { get; set; }

		public virtual int Y { get; set; }

		public virtual Color4 Color { get; set; }

		public void Render()
		{
			if(Device == null)
				throw new System.ArgumentNullException("Device");
			
			_render();
		}

		protected abstract void _render();

		public abstract Vector2 Measure();
		
		#endregion
	}
}
