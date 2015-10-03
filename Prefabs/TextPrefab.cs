using SlimDX;
using SlimDX.Direct2D;
using System.Drawing;
using SlimDX.DirectWrite;

namespace SlimDX.Common
{
	public class TextPrefab : Prefab
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

		public string Typeface
		{
			get { return _typeface; }
			set { _typeface = value; }
		}

		public FontWeight Fontweight
		{
			get { return _fontweight; }
			set { _fontweight = value; }
		}

		public DirectWrite.FontStyle Fontstyle
		{
			get { return _fontstyle; }
			set { _fontstyle = value; }
		}

		public FontStretch Fontstretch
		{
			get { return _fontstretch; }
			set { _fontstretch = value; }
		}

		public float Fontsize
		{
			get { return _fontsize; }
			set { _fontsize = value; }
		}

		public virtual string String
		{
			get { return _string; }
			set { _string = value; }
		}

		public virtual DirectWrite.Factory Factory { get; set; }

		public override Vector2 Measure()
		{
			throw new System.NotImplementedException();
		}

		protected override void _render()
		{
			if (_brush == null)
				_brush = new SolidColorBrush(Device.RenderTarget, Color);

			if (Factory == null)
				Factory = new DirectWrite.Factory();

			if (_font == null)
				_font = new SlimDX.DirectWrite.TextFormat(
					this.Factory,
					this.Typeface,
					this.Fontweight,
					this.Fontstyle,
					this.Fontstretch,
					this.Fontsize,
					"en-us"
				);

			Device.RenderTarget.DrawText(String, _font, _rectangle, _brush);
		}

		protected TextFormat _font;
		protected SolidColorBrush _brush;
		protected Rectangle _rectangle;
		private string _typeface                 = "Arial";
		private FontWeight _fontweight           = FontWeight.Regular;
		private DirectWrite.FontStyle _fontstyle = DirectWrite.FontStyle.Normal;
		private FontStretch _fontstretch         = FontStretch.Normal;
		private float _fontsize                  = 18.0f;
		private string _string = "";

	}
}
