/**
 * ...
 **/

namespace SlimDX.Common
{
	public class Bindable<T> : IBindable where T : struct
	{
		#region Public Interface
		public Bindable() : this(default(T))
		{
			// ...
		}

		public Bindable(T value)
		{
			this._value = value;
		}

		public T Value
		{
			get { return _value; }
			set { if (!object.Equals(this._value, value)) this._value = value; }
		}
		#endregion

		#region Implementation Detail
		private T _value;
		
		object IBindable.GetValue()
		{
			return Value;
		}
		#endregion

	}
}
