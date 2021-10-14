using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Manager;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Uses the description file and moves according to it after spawn. Lifecycle and reposition is also handled by that file.
    /// If it touches any player then-> if player has shield then this asteroid is destroyed. Otherwise do damage of the player.
    /// Modify velocity or force if collided with other asteroid?
    /// </summary>
    public class Asteroid : AIActor
    {
        int curBreakNum = 0;
        [SerializeField] int maxBreakNum = 2;
        [SerializeReference] [SerializeReferenceButton] IAIController AI_Con;
        public int CurrentBreakNumber { get { return curBreakNum; } set { curBreakNum = value; } }
        public int MaxBreakNumber { get { return maxBreakNum; } }
        [SerializeField] Asteroid partPrefab;
        [SerializeField] float breakableSideForce = 5f;
        [SerializeField] ForceMode breakableForceMode = ForceMode.Force;
        [SerializeField] int scoreToAddIfTotallyBroken = 10;
        Rigidbody rgd;
        PoolManager poolMan;

        public int ScoreToAddIfTotallyBroken { get { return scoreToAddIfTotallyBroken; } }
        public override IAIController AI_Controller { get => AI_Con; set => AI_Con = value; }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            rgd = GetComponent<Rigidbody>();
            poolMan = FindObjectOfType<PoolManager>();
        }

        public void BreakIt(out bool ShouldScore)
        {
            rgd.isKinematic = true;
            rgd.velocity = Vector3.zero;

            ShouldScore = false;
            curBreakNum++;
            if (curBreakNum >= maxBreakNum)
            {
                ShouldScore = true;
                return;
            }

            var dir = Vector3.Cross(Vector3.up, transform.forward);

            var curScale = _Transform.localScale;
            GameObject cloned1G = null;
            var cloned1Tr = poolMan.Clone(partPrefab.transform, out cloned1G);
            var cloned1 = cloned1Tr.GetComponent<Asteroid>();
            cloned1.transform.localScale = curScale * 0.5f;
            cloned1.CurrentBreakNumber = curBreakNum;
            var force1 = breakableSideForce * dir.normalized;
            force1.y = 0.0f;
            cloned1.rgd.isKinematic = false;
            cloned1.rgd.velocity = Vector3.zero;
            cloned1.rgd.AddForce(force1, breakableForceMode);

            GameObject cloned2G = null;
            var cloned2Tr = poolMan.Clone(partPrefab.transform, out cloned2G);
            var cloned2 = cloned2Tr.GetComponent<Asteroid>();
            cloned2.transform.localScale = curScale * 0.5f;
            cloned2.CurrentBreakNumber = curBreakNum;
            var force2 = breakableSideForce * -dir.normalized;
            force2.y = 0.0f;
            cloned2.rgd.isKinematic = false;
            cloned2.rgd.velocity = Vector3.zero;
            cloned2.rgd.AddForce(force2, breakableForceMode);
        }
    }
}