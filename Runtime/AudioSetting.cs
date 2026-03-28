using UnityEngine;
using UnityEngine.Audio;

namespace TWS.Settings
{
	/// <summary>
	/// Audio Settings Klasse für Lautstärke-Regelung
	/// Mappt 0-100 Werte zu -80 bis 0 dB für AudioMixer
	/// </summary>
	public class AudioSetting : MonoBehaviour, ISetting
	{
		public enum AudioSliderMode { Log10, Linear80, Linear100 }

		[SerializeField] protected string playerPrefKey;
		[SerializeField] protected AudioSliderMode sliderMode = AudioSliderMode.Log10;

        [Header("Audio Settings")]
		[SerializeField] private AudioMixer audioMixer;

		[SerializeField] private int defaultValue = 0;
		
		private const int DB_MIN = -80;
		private const int DB_MAX = 0;
		private const int UI_MIN = 0;
		private const int UI_MAX = 100;

		private int value;

		private IntValueUI ui;

		void Awake()
		{
			ui = GetComponent<IntValueUI>();
			value = PlayerPrefs.GetInt(playerPrefKey, defaultValue);
		}

		public void InitValue()
		{
            value = PlayerPrefs.GetInt(playerPrefKey, defaultValue);
            audioMixer.SetFloat(playerPrefKey, MapValue(value));
		}

		void OnEnable()
		{
			ui.OnValueChanged.AddListener(ValueChanged);
			ui.Formatter = value => string.Format("{0}%", value);
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
            audioMixer.SetFloat(playerPrefKey, MapValue(value));
			PlayerPrefs.SetInt(playerPrefKey, value);
		}

		protected virtual float MapValue(int sliderValue)
		{
			switch(sliderMode)
			{
				case AudioSliderMode.Log10: return Mathf.Log10(Mathf.Max(value / 100f, 0.0001f)) * 20;
                case AudioSliderMode.Linear80: return -80 + value * 0.8f;
                case AudioSliderMode.Linear100:
                default: return -80 + value;
            }
		}
	}
} 