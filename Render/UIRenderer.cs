/**
 * Render the user interface
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;

using SlimDX;

namespace SlimDX.Common
{
	public abstract class UIRenderer : IDisposable
	{
		#region Public Interface

		/**
		 * Renders the specified user interface
		 **/
		internal void Render(UI userInterface)
		{
			if (userInterface == null)
				throw new ArgumentNullException("userInterface");

			int y = 0;
			foreach (Element element in userInterface.Container)
			{
				IElementVisual visual = new DefaultVisual();
				Vector2 size = visual.Measure(this, element);
				visual.Render(this, element, 0, y, (int)size.X, (int)size.Y);
				y += (int)size.Y;
			}

			Flush();
		}

		~UIRenderer()
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
			// ...
		}

		/**
		 * Flush all pending rendering commands
		 */
		protected abstract void Flush();

		/**
		 * Gets the size of a string
		 */
		internal abstract Vector2 MeasureString(string text);

		/**
		 * Draws some text
		 */
		internal abstract void RenderString(string text, int x, int y, Color4 color);

		/**
		 * Draws a line
		 */
		internal abstract void RenderLine(int x0, int y0, Color4 color0, int x1, int y1, Color4 color1);

		/**
		 * Draws a rectangle
		 */
		internal void RenderRectangle(int x, int y, int width, int height, Color4 color)
		{
			RenderLine(x, y, color, x + width, y, color);
			RenderLine(x + width, y, color, x + width, y + height, color);
			RenderLine(x + width, y + height, color, x, y + height, color);
			RenderLine(x, y + height, color, x, y, color);
		}

		#endregion
	}
}
