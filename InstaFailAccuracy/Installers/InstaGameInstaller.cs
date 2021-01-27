using InstaFailAccuracy.Configuration;
using Zenject;

namespace InstaFailAccuracy.Installers
{
	internal class InstaGameInstaller : Installer<InstaGameInstaller>
	{
		private readonly PluginConfig _config;
		private readonly GameplayCoreSceneSetupData _gameplayCoreSceneSetupData;

		public InstaGameInstaller(PluginConfig config, GameplayCoreSceneSetupData gameplayCoreSceneSetupData)
		{
			_config = config;
			_gameplayCoreSceneSetupData = gameplayCoreSceneSetupData;
		}

		public override void InstallBindings()
		{
			if (_config.EnableInstaFailAcc && !_gameplayCoreSceneSetupData.gameplayModifiers.noFailOn0Energy)
			{
				Container.BindInterfacesTo<InstaFailAccuracyGameController>().AsSingle();
			}
		}
	}
}