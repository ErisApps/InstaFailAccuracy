using InstaFailAccuracy.Installers;
using IPA;
using IPA.Config;
using IPA.Config.Stores;
using SiraUtil.Zenject;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;


namespace InstaFailAccuracy
{
	[Plugin(RuntimeOptions.SingleStartInit)]
	public class Plugin
	{
		internal static IPALogger Logger { get; private set; }

		[Init]
		public void Init(IPALogger logger, Config config, Zenjector zenject)
		{
			Logger = logger;
			Configuration.PluginConfig.Instance = config.Generated<Configuration.PluginConfig>();

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