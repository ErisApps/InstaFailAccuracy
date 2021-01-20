using IPA.Config.Stores;

using System.Runtime.CompilerServices;


[assembly: InternalsVisibleTo(GeneratedStore.AssemblyVisibilityTarget)]
namespace InstaFailAccuracy.Configuration {


    internal class PluginConfig {
        public static PluginConfig Instance { get; set; }
        public virtual bool EnableInstaFailAcc { get; set; }
        public virtual float FailThresholdValue { get; set; } = 80f;
    }
}
