using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;

namespace InstaFailAccuracy.UI.Controllers
{
    internal class InstaFailAccuracyGameplaySetup : NotifiableSingleton<InstaFailAccuracyGameplaySetup>
    {
        [UIValue("enable-instafailacc")] public bool EnableInstaFailAcc;
        [UIValue("fail-threshold-value")] public float FailThresholdValue = 80f;

        [UIAction("fail-threshold-formatter")]
        public string InstaFailFormatter(float nb)
        {
            return $"{nb:F1}%";
        }
    }
}
