/**
 * Very basic interface component
 **/

using System.Collections.Generic;

namespace SlimDX.Common
{
	public class Element
	{
		#region Public Interface
		public string Label { get; set; }

		public void SetBinding(string targetName, object source)
		{
			// A single target shouldn't have several bindings.
			// A new binding overrides the previous entry
			bindings[targetName] = new Binding(targetName, source);
		}

		public void Update()
		{
			foreach (Binding binding in bindings.Values)
				binding.Update(this);
		}
		#endregion

		#region Implementation Detail
		private Dictionary<string, Binding> bindings = new Dictionary<string, Binding>();
		#endregion
	}
}
