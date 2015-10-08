using SlimDX.Direct2D;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace SlimDX.Common
{
	public class SpritePrefab
	{
		#region Public Interface
		public Rectangle tileRect;
		public Point drawToPosition;
		public System.Drawing.Bitmap tileSheetBitmap
		{
			get { return this._tileSheetBitmap; }
			set {
				this._d2dbitmap = null;
				this._tileSheetBitmap = value;
			}
		}

		public SpritePrefab(DeviceContext2D deviceContext2D)
		{
			this._device = deviceContext2D;
		}
		
		public void Draw()
		{
			if (this._d2dbitmap == null)
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

				_d2dbitmap = new SlimDX.Direct2D.Bitmap(
					_device.RenderTarget,
					tileRect.Size,
					dataStream,
					bitmapData.Stride,
					d2dBitmapProperties
				);
				this.tileSheetBitmap.UnlockBits(bitmapData);
			}

			// Render to the 2D context
			_device.RenderTarget.DrawBitmap(
				_d2dbitmap,
				new Rectangle(
					this.drawToPosition,
					new Size(this.tileRect.Width, this.tileRect.Height)
				)
			);
		}
		#endregion

		#region Implementation Details
		protected DeviceContext2D _device;
		protected SlimDX.Direct2D.Bitmap _d2dbitmap;
		private System.Drawing.Bitmap _tileSheetBitmap;
		#endregion
	}
}
