using InstaFailAccuracy.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using UnityEngine;
using Logger = IPA.Logging.Logger;

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
		}

		[OnStart]
		public void OnApplicationStart()
		{
			new GameObject("InstaFailAccuracyController").AddComponent<InstaFailAccuracyController>();
		}

		[OnExit]
		public void OnApplicationQuit()
		{
		}
	}
}