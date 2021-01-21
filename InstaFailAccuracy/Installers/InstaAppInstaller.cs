using InstaFailAccuracy.Configuration;
using IPA.Logging;
using SiraUtil;
using Zenject;

namespace InstaFailAccuracy.Installers
{
	internal class InstaAppInstaller : Installer<Logger, PluginConfig, InstaAppInstaller>
	{
		private readonly Logger _logger;
		private readonly PluginConfig _config;

		public InstaAppInstaller(Logger logger, PluginConfig config)
		{
			_logger = logger;
			_config = config;
		}

		public override void InstallBindings()
		{
			Container.BindLoggerAsSiraLogger(_logger);
			Container.BindInstance(_config).AsSingle();
		}
	}
}