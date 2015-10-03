using SlimDX;
using SlimDX.Direct2D;
using System.Drawing;

namespace SlimDX.Common
{
	public class RectanglePrefab : Prefab
	{
		public override int X
		{
			set { _rectangle.X = value; }
		}

		public override int Y
		{
			set { _rectangle.Y = value; }
		}

		public virtual int Width
		{
			set { _rectangle.Width = value; }
		}

		public virtual int Height
		{
			set { _rectangle.Height = value; }
		}

		public virtual System.Drawing.Bitmap Texture
		{
			set {
				if (!value.Equals(_bitmap))
					_d2dBitmap = null;

				if ((_bitmap = value) == null)
					_d2dBitmap = null;
			}
		}

		public RectanglePrefab()
		{
			_rectangle = new Rectangle();
		}

		public override Vector2 Measure()
		{
			throw new System.NotImplementedException();
		}

		/**
		 * Render with or without texture
		 **/
		protected override void _render()
		{
			if (this._bitmap == null)
			{
				// Render in solid color
				if (_brush == null)
					_brush = new SolidColorBrush(Device.RenderTarget, Color);

				Device.RenderTarget.FillRectangle(_brush, _rectangle);

				return;
			}

			if (this._d2dBitmap == null)
			{
				// Load the texture
				var bitmapData = this._bitmap.LockBits(
					new Rectangle(new Point(0, 0), this._bitmap.Size),
					System.Drawing.Imaging.ImageLockMode.ReadOnly,
					System.Drawing.Imaging.PixelFormat.Format32bppPArgb
				);
				var dataStream = new SlimDX.DataStream(
					bitmapData.Scan0,
					bitmapData.Stride * bitmapData.Height,
					true,
					false
				);
				var d2dPixelFormat = new SlimDX.Direct2D.PixelFormat(
					SlimDX.DXGI.Format.B8G8R8A8_UNorm,
					SlimDX.Direct2D.AlphaMode.Premultiplied
				);
				var d2dBitmapProperties = new SlimDX.Direct2D.BitmapProperties();
				d2dBitmapProperties.PixelFormat = d2dPixelFormat;

				_d2dBitmap = new SlimDX.Direct2D.Bitmap(
					Device.RenderTarget,
					new Size(this._bitmap.Width, this._bitmap.Height),
					dataStream,
					bitmapData.Stride,
					d2dBitmapProperties
				);
				this._bitmap.UnlockBits(bitmapData);
			}

			// Render the texture
			Device.RenderTarget.DrawBitmap(_d2dBitmap, _rectangle);
		}

		protected SolidColorBrush _brush;
		protected Rectangle _rectangle;
		private System.Drawing.Bitmap _bitmap;
		private Direct2D.Bitmap _d2dBitmap;

	}
}
