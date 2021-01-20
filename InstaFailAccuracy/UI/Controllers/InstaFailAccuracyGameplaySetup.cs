using BeatSaberMarkupLanguage.Attributes;

using InstaFailAccuracy.Configuration;


namespace InstaFailAccuracy.UI.Controllers {


    internal class InstaFailAccuracyGameplaySetup {
        [UIValue("enable-instafailacc")]
        public bool EnableInstaFailAcc {
            get => PluginConfig.Instance.EnableInstaFailAcc;
            set => PluginConfig.Instance.EnableInstaFailAcc = value;
        }

        [UIValue("fail-threshold-value")]
        public float FailThresholdValue {
            get => PluginConfig.Instance.FailThresholdValue;
            set => PluginConfig.Instance.FailThresholdValue = value;
        }

        [UIAction("fail-threshold-formatter")]
        public string InstaFailFormatter(float nb) {
            return $"{nb:F1}%";
        }
    }
}
