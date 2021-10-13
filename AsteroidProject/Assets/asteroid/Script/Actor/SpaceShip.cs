using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Uses the description file to construct spaceship actor features. 
    /// Has input handling and player control feature along with everything related to player
    /// </summary>
    public class SpaceShip : PlayerActor
    {
        [SerializeReference] [SerializeReferenceButton] IPlayerController playerController;

        public override IPlayerController PlayerController { get => playerController; set => playerController = value; }
    }
}