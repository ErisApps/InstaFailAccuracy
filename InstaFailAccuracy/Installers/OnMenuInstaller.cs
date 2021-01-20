using InstaFailAccuracy.UI;
using InstaFailAccuracy.UI.Controllers;

using Zenject;


namespace InstaFailAccuracy.Installers {


    internal class OnMenuInstaller : Installer {
        public override void InstallBindings() {
            Container.BindInterfacesAndSelfTo<InstaFailAccuracyGameplaySetup>().AsSingle();
            Container.BindInterfacesTo<MenuButtonManager>().AsSingle();
        }
    }
}
