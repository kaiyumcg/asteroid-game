using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Actor;

namespace AsteroidGame.Manager
{
    public class AsteroidGameManager : GameManager
    {
        CutSceneManager cutMan;
        SpaceShip ship;

        protected override void AwakeGameManager()
        {
            base.AwakeGameManager();
            ship = FindObjectOfType<SpaceShip>();
            cutMan = FindObjectOfType<CutSceneManager>();
        }

        protected override bool WhenGameEnds()
        {
            return ship.IsDead;
        }

        protected override bool WhenGameStarts()
        {
            return cutMan.HasCutSceneEnded;
        }
    }
}