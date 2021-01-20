using BeatSaberMarkupLanguage.GameplaySetup;

using InstaFailAccuracy.UI.Controllers;

using System;

using Zenject;


namespace InstaFailAccuracy.UI {


    internal class MenuButtonManager : IInitializable, IDisposable {

        private readonly InstaFailAccuracyGameplaySetup _host;

        public MenuButtonManager(InstaFailAccuracyGameplaySetup host) {
            _host = host;
        }

        public void Initialize() {
            GameplaySetup.instance.AddTab("InstaFail Accuracy", "InstaFailAccuracy.UI.Views.InstaFailAccuracyGameplaySetupView.bsml", _host);
        }

        public void Dispose() {
            GameplaySetup.instance.RemoveTab("InstaFail Accuracy");
        }
    }
}
