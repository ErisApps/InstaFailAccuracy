using System.Linq;
using InstaFailAccuracy.UI.Controllers;
using UnityEngine;

namespace InstaFailAccuracy
{
    /// <summary>
    /// Monobehaviours (scripts) are added to GameObjects.
    /// For a full list of Messages a Monobehaviour can receive from the game, see https://docs.unity3d.com/ScriptReference/MonoBehaviour.html.
    /// </summary>
    public class InstaFailAccuracyController : MonoBehaviour
    {
        public enum GameStatus
        {
            Unknown,
            Menu,
            Game
        }

        public static InstaFailAccuracyController instance { get; private set; }

        #region Properties

        public GameStatus CurrentGameStatus { get; private set; } = GameStatus.Unknown;
        private RelativeScoreAndImmediateRankCounter _rankCounter;
        private StandardLevelFailedController _standardLevelFailedController;
        private bool _alreadyFailed;

        #endregion

        #region Monobehaviour Messages

        /// <summary>
        /// Only ever called once, mainly used to initialize variables.
        /// </summary>
        private void Awake()
        {
            // For this particular MonoBehaviour, we only want one instance to exist at any time, so store a reference to it in a static property
            //   and destroy any that are created while one already exists.
            if (instance != null)
            {
                Logger.log?.Warn($"Instance of {this.GetType().Name} already exists, destroying.");
                GameObject.DestroyImmediate(this);
                return;
            }

            GameObject.DontDestroyOnLoad(this); // Don't destroy this object on scene changes
            instance = this;
            Logger.log?.Debug($"{name}: Awake()");
        }

        /// <summary>
        /// Called when the script becomes enabled and active
        /// </summary>
        private void OnEnable()
        {
            BS_Utils.Utilities.BSEvents.menuSceneLoaded += OnMenuSceneLoaded;
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += OnGameSceneLoaded;
        }

        /// <summary>
        /// Called when the script becomes disabled or when it is being destroyed.
        /// </summary>
        private void OnDisable()
        {
            BS_Utils.Utilities.BSEvents.menuSceneLoaded -= OnMenuSceneLoaded;
            BS_Utils.Utilities.BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
        }

        /// <summary>
        /// Called when the script is being destroyed.
        /// </summary>
        private void OnDestroy()
        {
            Logger.log?.Debug($"{name}: OnDestroy()");
            instance = null; // This MonoBehaviour is being destroyed, so set the static instance property to null.
        }

        #endregion

        #region Event methods

        private void OnMenuSceneLoaded()
        {
            if (CurrentGameStatus != GameStatus.Unknown && InstaFailAccuracyGameplaySetup.instance.EnableInstaFailAcc)
                _rankCounter.relativeScoreOrImmediateRankDidChangeEvent -= OnRelativeScoreOrImmediateRankDidChangeEvent;
            CurrentGameStatus = GameStatus.Menu;
        }

        private void OnGameSceneLoaded()
        {
            if (InstaFailAccuracyGameplaySetup.instance.EnableInstaFailAcc)
            {
                _alreadyFailed = false;
                _rankCounter = Resources.FindObjectsOfTypeAll<RelativeScoreAndImmediateRankCounter>().First();
                _standardLevelFailedController =
                    Resources.FindObjectsOfTypeAll<StandardLevelFailedController>().First();
                _rankCounter.relativeScoreOrImmediateRankDidChangeEvent += OnRelativeScoreOrImmediateRankDidChangeEvent;
            }

            CurrentGameStatus = GameStatus.Game;
        }

        private void OnRelativeScoreOrImmediateRankDidChangeEvent()
        {
            var currentAcc = _rankCounter?.relativeScore * 100;
            if (!_alreadyFailed && _rankCounter != null &&
                currentAcc * 100 < InstaFailAccuracyGameplaySetup.instance.FailThresholdValue)
            {
                Logger.log.Debug($"Current accuracy: {currentAcc}");
                _standardLevelFailedController.HandleLevelFailed();
            }
        }

        #endregion
    }
}