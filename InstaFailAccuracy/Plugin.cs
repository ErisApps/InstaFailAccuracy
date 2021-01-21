using InstaFailAccuracy.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SiraUtil.Zenject;

namespace InstaFailAccuracy
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		[Init]
		public void Init(Logger logger, Config config, Zenjector zenject)
		{
			Configuration.PluginConfig.Instance = config.Generated<Configuration.PluginConfig>();

			zenject.OnApp<InstaAppInstaller>().WithParameters(logger, Configuration.PluginConfig.Instance);
			zenject.OnMenu<InstaMenuInstaller>();
			zenject.OnGame<InstaGameInstaller>().OnlyForStandard();
		}

		[OnStart]
		public void OnApplicationStart()
		{
		}

		[OnExit]
		public void OnApplicationQuit()
		{
		}
	}
}