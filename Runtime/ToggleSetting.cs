using UnityEngine;

namespace TWS.Settings
{
	public class ToggleSetting : MonoBehaviour
	{
		[SerializeField] protected string playerPrefKey;

		public TargetSetting targetSetting = TargetSetting.None;

		[SerializeField] private bool defaultValue = false;

		private bool value;
		private IntValueUI ui;

		void Awake()
		{
			ui = GetComponent<IntValueUI>();
			value = PlayerPrefs.GetInt(playerPrefKey, defaultValue ? 1 : 0) > 0;
		}

		public void InitValue()
		{
			value = PlayerPrefs.GetInt(playerPrefKey, defaultValue ? 1 : 0) > 0;
			Apply(value);
		}

		void OnEnable()
		{
			ui.OnValueChanged.AddListener(ValueChanged);
			ui.SetValue(value ? 1 : 0);
			ValueChanged(value ? 1 : 0);
		}

		void OnDisable()
		{
			ui.OnValueChanged.RemoveListener(ValueChanged);
		}

		public void ValueChanged(int value)
		{
			this.value = value > 0;
			Apply(this.value);
			PlayerPrefs.SetInt(playerPrefKey, value);
		}

		// Ändert die Einstellung für das TargetSetting.
		// Kann von Unterklassen überschrieben werden für eigene Implementierungen.
		protected virtual void Apply(bool value)
		{
			switch (targetSetting)
			{
				case TargetSetting.Vsync:
					QualitySettings.vSyncCount = value ? 1 : 0;
					break;
				case TargetSetting.Fullscreen:
					Screen.fullScreen = value;
					break;
			}
		}
	}

	public enum TargetSetting
	{
		None,
		Vsync,
		Fullscreen,
	}
}