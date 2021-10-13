using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameplayFramework
{
    public abstract class PlayerActor : GameActor
    {
        [SerializeField] bool useDeviceStorageForScore = false;
        [SerializeField] int initialScore;
        [SerializeField] int currentScore;
        [SerializeReference] [SerializeReferenceButton] IPlayerController Player_Con;
        public IPlayerController PlayerController { get { return Player_Con; } set { Player_Con = value; } }
        public int Score { get { return currentScore; } }
        public UnityEvent<int> OnUpdateScore;
        const string scoreIdentifier = "_Player_Score_";

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
            Player_Con.ControlInUpdate();
        }

        protected override void UpdateActorPhysics()
        {
            Player_Con.ControlInPhysicsUpdate();
        }
    }
}