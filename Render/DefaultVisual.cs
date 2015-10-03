/**
 * Provides a default visual representation of an element, to be used when no
 * element-specific visual representation is available
 **/

using System.Windows.Forms;
using System.Drawing;
using SlimDX;

namespace SlimDX.Common
{
	public class DefaultVisual : IElementVisual
	{
		/**
		 *  Measures the element, returning the size (in pixels) it would
		 *  occupy if rendered with the specified renderer
		 **/
		public virtual Vector2 Measure(UIRenderer uIRenderer, Element element)
		{
			return uIRenderer.MeasureString(element.Label);
		}

		/**
		 *  Renders the element using the specified renderer
		 **/
		public virtual void Render(UIRenderer uIRenderer, Element element, int x, int y, int width, int height)
		{
			Color4 color = new Color4(1.0f, 1.0f, 1.0f);
			uIRenderer.RenderRectangle(x, y, width, height, color);
			uIRenderer.RenderString(element.Label, 0, 0, new Color4(1.0f, 0.0f, 0.0f));
		}
	}
}
