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
            var weapon_layer = LayerMask.NameToLayer("weapon");
            var ship_layer = LayerMask.NameToLayer("ship");
            var asteroid_layer = LayerMask.NameToLayer("asteroid");
            var playeArea_layer = LayerMask.NameToLayer("playArea");
            var powerup_layer = LayerMask.NameToLayer("powerup");

            Physics.IgnoreLayerCollision(powerup_layer, weapon_layer, true);
            Physics.IgnoreLayerCollision(powerup_layer, asteroid_layer, true);
            Physics.IgnoreLayerCollision(powerup_layer, playeArea_layer, true);
            Physics.IgnoreLayerCollision(powerup_layer, powerup_layer, true);

            Physics.IgnoreLayerCollision(playeArea_layer, weapon_layer, true);
            Physics.IgnoreLayerCollision(playeArea_layer, asteroid_layer, true);
            Physics.IgnoreLayerCollision(playeArea_layer, playeArea_layer, true);
            Physics.IgnoreLayerCollision(playeArea_layer, powerup_layer, true);

            Physics.IgnoreLayerCollision(weapon_layer, ship_layer, true);
            Physics.IgnoreLayerCollision(weapon_layer, weapon_layer, true);
        }
    }
}