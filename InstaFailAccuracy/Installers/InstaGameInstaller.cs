using InstaFailAccuracy.Configuration;
using Zenject;

namespace InstaFailAccuracy.Installers
{
	internal class InstaGameInstaller : Installer<InstaGameInstaller>
	{
		private readonly PluginConfig _config;

		public InstaGameInstaller(PluginConfig config)
		{
			_config = config;
		}

		public override void InstallBindings()
		{
			if (_config.EnableInstaFailAcc)
			{
				Container.Bind<InstaFailAccuracyGameController>().AsSingle().NonLazy();
			}
		}
	}
}