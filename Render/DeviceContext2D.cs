using System;
using System.Drawing;
using SlimDX.Direct2D;

namespace SlimDX.Common
{
	public class DeviceContext2D : IDisposable
	{

		public DeviceContext2D(IntPtr handle, DeviceSettings2D settings)
		{

			if (handle == null)
				throw new ArgumentNullException("handle");
			if (handle == IntPtr.Zero)
				throw new ArgumentException("Value must be a valid window handle.", "handle");
			if (settings == null)
				throw new ArgumentNullException("settings");

			this.settings = settings;

			factory = new Factory();
			RenderTarget = new WindowRenderTarget(factory, new WindowRenderTargetProperties
			{
				Handle = handle,
				PixelSize = new Size(settings.Width, settings.Height)
			});
		}

		~DeviceContext2D()
		{
			Dispose(false);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		protected virtual void Dispose(bool disposeManagedResources)
		{
			if (disposeManagedResources)
			{
				RenderTarget.Dispose();
				factory.Dispose();
			}
		}

		public WindowRenderTarget RenderTarget { get; private set; }

		#region Implementation Detail
		private DeviceSettings2D settings;
		private Factory factory;
		#endregion
	}
}
