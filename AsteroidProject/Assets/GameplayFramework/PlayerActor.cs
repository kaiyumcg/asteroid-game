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
        public UnityEvent<int> OnUpdateScore;
        [SerializeField] bool useDeviceStorageForScore = false;
        [SerializeField] int initialScore;
        [SerializeField] int currentScore;
        [SerializeReference] [SerializeReferenceButton] IPlayerController playerController;
        public IPlayerController PlayerController { get { return playerController; } set { playerController = value; } }
        public int Score { get { return currentScore; } }
       
        const string scoreIdentifier = "_Player_Score_";
        protected virtual void OnDisableActor() { }

        private void OnDisable()
        {
            OnDisableActor();
            if (playerController != null)
            {
                playerController.OnEndController(this);
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

            if (playerController != null)
            {
                playerController.OnStartController(this);   
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
            if (playerController != null)
            {
                playerController.ControlInUpdate();
            }
        }

        protected override void UpdateActorPhysics()
        {
            if (playerController != null)
            {
                playerController.ControlInPhysicsUpdate();
            }
        }
    }
}