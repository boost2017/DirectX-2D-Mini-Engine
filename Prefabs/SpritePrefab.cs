using SlimDX.Direct2D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SlimDX.Common
{
	/**
	 * Once set, this object cannot change.
	 * It is pre-rendered at initialization.
	 */
	public class SpritePrefab
	{
		#region Public Interface

		public SpritePrefab(DeviceContext2D deviceContext2D,
		                    System.Drawing.Bitmap tileSheetBitmap,
		                    Rectangle tileRect,
		                    Point drawToPosition)
		{
			this._device = deviceContext2D;

			// Load the texture
			var bitmapData = tileSheetBitmap.LockBits(
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

			this._d2dBitmap = new SlimDX.Direct2D.Bitmap(
				_device.RenderTarget,
				tileRect.Size,
				dataStream,
				bitmapData.Stride,
				d2dBitmapProperties
			);
			tileSheetBitmap.UnlockBits(bitmapData);
			
			this._renderRectangle = new Rectangle(
				drawToPosition,
				new Size(tileRect.Width, tileRect.Height)
			);
		}
		
		public void Draw()
		{
			// Render to the 2D context
			_device.RenderTarget.DrawBitmap(
				this._d2dBitmap,
				this._renderRectangle
			);
		}
		#endregion

		#region Implementation Details
		protected DeviceContext2D _device;
		public Point _drawToPosition;
		private  Direct2D.Bitmap _d2dBitmap;
		private  Rectangle _renderRectangle;
		#endregion
	}
}
