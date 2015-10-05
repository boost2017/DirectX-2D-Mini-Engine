using SlimDX.Direct2D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SlimDX.Common
{
	public class SpritePrefab
	{
		#region Public Interface
		public System.Drawing.Bitmap tileSheetBitmap;
		public Rectangle tileRect;
		public Point drawToPosition;

		public SpritePrefab(DeviceContext2D deviceContext2D)
		{
			this._device = deviceContext2D;
		}
		
		public void Draw()
		{
			// Load the texture
			var bitmapData = this.tileSheetBitmap.LockBits(
				tileRect,
				System.Drawing.Imaging.ImageLockMode.ReadOnly,
				System.Drawing.Imaging.PixelFormat.Format32bppPArgb
			);
			var dataStream = new DataStream(
				bitmapData.Scan0,
				bitmapData.Stride * bitmapData.Height,
				true,
				false
			);
			var d2dPixelFormat = new PixelFormat(
				SlimDX.DXGI.Format.B8G8R8A8_UNorm,
				SlimDX.Direct2D.AlphaMode.Premultiplied
			);
			var d2dBitmapProperties = new BitmapProperties();
			d2dBitmapProperties.PixelFormat = d2dPixelFormat;

			var d2dBitmap = new SlimDX.Direct2D.Bitmap(
				_device.RenderTarget,
				tileRect.Size,
				dataStream,
				bitmapData.Stride,
				d2dBitmapProperties
			);
			this.tileSheetBitmap.UnlockBits(bitmapData);

			// Render to the 2D context
			_device.RenderTarget.DrawBitmap(
				d2dBitmap,
				new Rectangle(
					this.drawToPosition,
					new Size(this.tileRect.Width, this.tileRect.Height)
				)
			);
		}
		#endregion

		#region Implementation Details
		protected DeviceContext2D _device;
		#endregion
	}
}
