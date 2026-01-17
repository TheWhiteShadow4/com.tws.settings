using UnityEngine.Events;
using System;


namespace TWS.Settings
{
	public interface IntValueUI
	{
		public int Value { get; }
		public UnityEvent<int> OnValueChanged { get; }
		public void SetValue(int value);
		public Func<int, string> Formatter { get; set; }
	}
}