using InstaFailAccuracy.Installers;

using IPA;
using IPA.Config;
using IPA.Config.Stores;

using SiraUtil.Zenject;

using UnityEngine;

using IPALogger = IPA.Logging.Logger;


namespace InstaFailAccuracy {

    [Plugin(RuntimeOptions.SingleStartInit)]
    public class Plugin {
        internal static IPALogger Logger { get; private set; }

        [Init]
        public void Init(IPALogger logger) {
            Logger = logger;
        }

        [Init]
        public void InitWithConfig(Config conf) {
            Configuration.PluginConfig.Instance = conf.Generated<Configuration.PluginConfig>();
        }

        [Init]
        public void InitWithZenjector(Zenjector zenjector) {
            zenjector.OnMenu<OnMenuInstaller>();
        }

        [OnStart]
        public void OnApplicationStart() {
            new GameObject("InstaFailAccuracyController").AddComponent<InstaFailAccuracyController>();
        }

        [OnExit]
        public void OnApplicationQuit() {
            
        }
    }
}
