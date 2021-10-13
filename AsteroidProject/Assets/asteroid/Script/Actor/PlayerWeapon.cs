using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;

namespace AsteroidGame.Actor
{
    //Weapon should be scriptable object data that will be mutable upon gameplay start.
    //Meaning they will contain data that changes. So we clone then first before usage ensuring asset validity with gameplay.
    //We will destroy the clones of them on destroy.

    //Shooter component is not necessary. Instead the component will be "WeaponUser".
    //At a time defensive and offensive weapon can be active. At a time only one offensive can be active
    //Powerups are actors that interact will ship, upon volume enter they set the currently active offensive and defensive weapon.
    //Offensive weapons are cloned with weapon scriptable object data.
    //Defensive weapon uses "ShipMesh" Script tag to find current ship and draw something defensive over it using the tagged gameobject.

    //Designer will set projectile prefab on weapon. Count of weapon. Defensive health protect amount
    
    //PlayArea for ship?
    //If one damage, respawn? No, we will use modern life bar contineuous UI. Defensive weapon shall have a protective bar with it
    //While it is active, we will ignore damage with asteroid and let it be destroyed with shield
    /// <summary>
    /// Describes how Shooter component will use projectile actors when player gives input
    /// </summary>
    public class PlayerWeapon : GameActor
    {
        [Header("Weapon Setting")]
        [SerializeField] WeaponType weaponType;

        bool isActivated = false;
        public void UseWeapon()
        {
            if (isActivated == false) { return; }
        }

        public void ActivateWeapon()
        {
            isActivated = true;
        }

        public void DeactivateWeapon()
        {
            isActivated = false;
        }

        protected override void UpdateActor(float dt, float fixedDt)
        {
            throw new System.NotImplementedException();
        }

        protected override void UpdateActorPhysics(float dt, float fixedDt)
        {
            throw new System.NotImplementedException();
        }
    }
}