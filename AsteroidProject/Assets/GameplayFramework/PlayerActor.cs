using KSaveDataMan;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// Player Actor class.
/// </summary>
namespace GameplayFramework
{
    public abstract class PlayerActor : GameActor
    {
        [Header("Player Actor Setting")]
        public UnityEvent<int> OnUpdateScore;
        [SerializeField] bool useDeviceStorageForScore = false;
        [SerializeField] int initialScore;
        [SerializeField] int currentScore;
        
        public abstract IPlayerController PlayerController { get; set; }
        public int Score { get { return currentScore; } }
        const string scoreIdentifier = "_Player_Score_";

        protected override void OnEditorUpdate()
        {
            base.OnEditorUpdate();
        }

        protected virtual void OnDisableActor() { }

        private void OnDisable()
        {
            OnDisableActor();
            if (PlayerController != null)
            {
                PlayerController.OnEndController(this);
            }
        }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            if (useDeviceStorageForScore)
            {
                var diskValue = SaveDataManager.LoadInt(scoreIdentifier, -1);
                if (diskValue < 0)
                {
                    currentScore = initialScore;
                    SaveDataManager.SaveInt(scoreIdentifier, currentScore);
                }
                else
                {
                    currentScore = diskValue;
                }
            }
            else
            {
                currentScore = initialScore;
            }

            if (PlayerController != null)
            {
                PlayerController.OnStartController(this);   
            }
        }

        public void AddScore(int score)
        {
            var sc = Mathf.Abs(score);
            this.currentScore += sc;
            OnUpdateScore?.Invoke(score);

            if (useDeviceStorageForScore)
            {
                SaveDataManager.SaveInt(scoreIdentifier, currentScore);
            }
        }

        protected override void UpdateActor()
        {
            if (PlayerController != null)
            {
                PlayerController.ControlInUpdate();
            }
        }

        protected override void UpdateActorPhysics()
        {
            if (PlayerController != null)
            {
                PlayerController.ControlInPhysicsUpdate();
            }
        }
    }
}