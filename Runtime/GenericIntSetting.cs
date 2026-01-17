using UnityEngine;

namespace TWS.Settings
{
	/// <summary>
	/// Generische Settings Klasse für Integer.
	/// Speichert den Wert in PlayerPrefs und aktualisiert die UI.
	/// </summary>
	public class GenericIntSetting : MonoBehaviour
	{
		public string playerPrefKey;

		public int defaultValue = 10;
		
		private int value;

		private IntValueUI ui;

		public int Value => value;

		public IntValueUI UI
		{
			get 
			{
				if (ui == null) Awake();
				return ui;
			}
		}

		void Awake()
		{
			ui = GetComponent<IntValueUI>();
			value = PlayerPrefs.GetInt(playerPrefKey, defaultValue);
		}

		void OnEnable()
		{
			ui.OnValueChanged.AddListener(ValueChanged);
			ui.SetValue(value);
			ValueChanged(value);
		}

		void OnDisable()
		{
			ui.OnValueChanged.RemoveListener(ValueChanged);
		}

		public void ValueChanged(int value)
		{
			this.value = value;
			PlayerPrefs.SetInt(playerPrefKey, value);
		}
	}
}
