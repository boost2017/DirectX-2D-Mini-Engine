/**
 * Add and retrieve elements to/from the UI
 */

using System.Collections.Generic;
using System.Collections;

namespace SlimDX.Common
{
	public class ElementContainer : IEnumerable<Element>
	{
		#region Public Interface

		public void Add(Element element)
		{
			elements.Add(element);
		}

		public void Update()
		{
			foreach (Element element in elements)
			{
				element.Update();
			}
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			throw new System.NotImplementedException();
		}

		IEnumerator<Element> IEnumerable<Element>.GetEnumerator()
		{
			foreach (Element element in elements)
				yield return element;
		}

		#endregion

		#region Implementation Detail

		List<Element> elements = new List<Element>();

		#endregion
	}
}
