using System.Linq;

using BS_Utils.Utilities;

using InstaFailAccuracy.Configuration;

using UnityEngine;


namespace InstaFailAccuracy {
    public class InstaFailAccuracyController : MonoBehaviour {
        public enum GameStatus {
            Unknown,
            Menu,
            Game
        }

        public GameStatus CurrentGameStatus { get; private set; } = GameStatus.Unknown;
        private RelativeScoreAndImmediateRankCounter _rankCounter;
        private StandardLevelFailedController _standardLevelFailedController;
        private bool _alreadyFailed;

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void Awake() {
            DontDestroyOnLoad(this);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void OnEnable() {
            BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded += OnGameSceneLoaded;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "Called by Unity")]
        private void OnDisable() {
            BSEvents.menuSceneLoaded -= OnMenuSceneLoaded;
            BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
        }

        private void OnMenuSceneLoaded() {
            if (CurrentGameStatus != GameStatus.Unknown && PluginConfig.Instance.EnableInstaFailAcc) {
                _rankCounter.relativeScoreOrImmediateRankDidChangeEvent -= OnRelativeScoreOrImmediateRankDidChangeEvent;
            }

            CurrentGameStatus = GameStatus.Menu;
        }

        private void OnGameSceneLoaded() {
            if (PluginConfig.Instance.EnableInstaFailAcc) {
                _alreadyFailed = false;
                _rankCounter = Resources.FindObjectsOfTypeAll<RelativeScoreAndImmediateRankCounter>().First();
                _standardLevelFailedController = Resources.FindObjectsOfTypeAll<StandardLevelFailedController>().First();
                _rankCounter.relativeScoreOrImmediateRankDidChangeEvent += OnRelativeScoreOrImmediateRankDidChangeEvent;
            }

            CurrentGameStatus = GameStatus.Game;
        }

        private void OnRelativeScoreOrImmediateRankDidChangeEvent() {
            float? currentAcc = _rankCounter?.relativeScore * 100;
            if (!_alreadyFailed && _rankCounter != null &&
                currentAcc < PluginConfig.Instance.FailThresholdValue) {
                _alreadyFailed = true;
                _standardLevelFailedController.HandleLevelFailed();
            }
        }
    }
}