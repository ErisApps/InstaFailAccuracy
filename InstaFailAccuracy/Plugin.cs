using InstaFailAccuracy.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using IPA.Logging;
using SiraUtil.Zenject;

namespace InstaFailAccuracy
{
	[Plugin(RuntimeOptions.DynamicInit)]
	public class Plugin
	{
		[Init]
		public void Init(Logger logger, Config config, Zenjector zenject)
		{
			zenject.OnApp<InstaAppInstaller>().WithParameters(logger, config.Generated<Configuration.PluginConfig>());
			zenject.OnMenu<InstaMenuInstaller>();
			zenject.OnGame<InstaGameInstaller>().OnlyForStandard();
		}

		[OnEnable, OnDisable]
		public void OnStateChanged()
		{
			// Zenject is poggers
		}
	}
}