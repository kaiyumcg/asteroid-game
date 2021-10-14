using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

namespace AsteroidGame.Manager
{
    public class CollisionManager : GameSystem
    {
        protected override void InitSystem()
        {
            base.InitSystem();
            var layer = LayerMask.NameToLayer("weapon");
            Physics.IgnoreLayerCollision(layer, layer, true);
        }
    }
}