using System;
using InstaFailAccuracy.Configuration;
using Zenject;

namespace InstaFailAccuracy
{
	internal class InstaFailAccuracyGameController : IInitializable, IDisposable
	{
		private readonly PluginConfig _config;
		private readonly ILevelEndActions _levelEndActions;
		private readonly ScoreController _scoreController;
		private readonly StandardLevelFailedController _standardLevelFailedController;

		private bool _failed;

		public InstaFailAccuracyGameController(PluginConfig config, ILevelEndActions levelEndActions, ScoreController scoreController, StandardLevelFailedController standardLevelFailedController)
		{
			_config = config;
			_levelEndActions = levelEndActions;
			_scoreController = scoreController;
			_standardLevelFailedController = standardLevelFailedController;
		}

		public void Initialize()
		{
			_levelEndActions.levelFailedEvent += OnLevelFailed;
			_scoreController.immediateMaxPossibleScoreDidChangeEvent += HandleScoreControllerImmediateMaxPossibleScoreDidChange;
		}

		public void Dispose()
		{
			_scoreController.immediateMaxPossibleScoreDidChangeEvent -= HandleScoreControllerImmediateMaxPossibleScoreDidChange;
			_levelEndActions.levelFailedEvent -= OnLevelFailed;
		}

		private void OnLevelFailed()
		{
			_failed = true;
		}

		private void HandleScoreControllerImmediateMaxPossibleScoreDidChange(int immediateMaxPossibleScore, int _)
		{
			var accuracy = _scoreController.prevFrameRawScore * 100f / immediateMaxPossibleScore;
			if (!_failed && accuracy < _config.FailThresholdValue)
			{
				_failed = true;
				_standardLevelFailedController.HandleLevelFailed();
			}
		}
	}
}