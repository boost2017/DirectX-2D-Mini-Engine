/**
 * Defines the interface required to specify an element's visual representation
 **/ 

using SlimDX;

namespace SlimDX.Common
{
	public interface IElementVisual
	{
		/**
		 *  Measures the element, returning the size (in pixels) it would
		 *  occupy if rendered with the specified renderer
		 **/
		Vector2 Measure(UIRenderer uIRenderer, Element element);

		/**
		 *  Renders the element using the specified renderer
		 **/
		void Render(UIRenderer uIRenderer, Element element, int x, int y, int width, int height);
	}
}
