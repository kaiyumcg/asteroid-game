using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// After spawned, this moves into the given direction. 
    /// Get itself destroyed upon collision with asteroid or too far away from player
    /// </summary>
    public class Projectile : GameActor
    {
        protected override void UpdateActor()
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateActorPhysics()
        {
            throw new System.NotImplementedException();
        }
    }
}