using BeatSaberMarkupLanguage.Attributes;
using InstaFailAccuracy.Configuration;

namespace InstaFailAccuracy.UI.Controllers
{
	internal class InstaFailAccuracyGameplaySetup
	{
		private readonly PluginConfig _config;

		public InstaFailAccuracyGameplaySetup(PluginConfig config)
		{
			_config = config;
		}

		[UIValue("enable-instafailacc")]
		public bool EnableInstaFailAcc
		{
			get => _config.EnableInstaFailAcc;
			set => _config.EnableInstaFailAcc = value;
		}

		[UIValue("fail-threshold-value")]
		public float FailThresholdValue
		{
			get => _config.FailThresholdValue;
			set => _config.FailThresholdValue = value;
		}

		[UIAction("fail-threshold-formatter")]
		public string InstaFailFormatter(float nb) => $"{nb:F1}%";
	}
}