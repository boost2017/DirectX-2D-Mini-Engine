﻿using System.Reflection;

namespace SlimDX.Common
{
	class Binding
	{
		#region Implementation Detail
		private string targetName;
		private object source;
		#endregion

		#region Public Interface
		public Binding(string targetName, object source)
		{
			this.targetName = targetName;
			this.source = source;
		}

		public void Update(object target)
		{
			PropertyInfo property = target.GetType().GetProperty(targetName);
			if (property == null)
				return;

			object value = source;
			IBindable bindable = source as IBindable;
			if (bindable != null)
				value = bindable.GetValue();

			if (property.PropertyType.IsAssignableFrom(value.GetType()))
				property.SetValue(target, value, null);
			else if (property.PropertyType == typeof(string))
				property.SetValue(target, value.ToString(), null);
		}
		#endregion
	}
}
