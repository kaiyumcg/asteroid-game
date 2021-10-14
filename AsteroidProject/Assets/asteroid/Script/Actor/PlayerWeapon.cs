using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;

namespace AsteroidGame.Actor
{
    

    //Shooter component is not necessary. Instead the component will be "WeaponUser".
    //At a time defensive and offensive weapon can be active. At a time only one offensive can be active
    //Powerups are actors that interact will ship, upon volume enter they set the currently active offensive and defensive weapon.
    //Offensive weapons are cloned with weapon scriptable object data.
    //Defensive weapon uses "ShipMesh" Script tag to find current ship and draw something defensive over it using the tagged gameobject.

    //Designer will set projectile prefab on weapon. Count of weapon. Defensive health protect amount

    //PlayArea for ship?
    //If one damage, respawn? No, we will use modern life bar contineuous UI. Defensive weapon shall have a protective bar with it
    //While it is active, we will ignore damage with asteroid and let it be destroyed with shield


    //Weapon is spawned. Now do its work i.e. collided with asterioid or destroyed or other etc
    public class PlayerWeapon : GameActor
    {
        [SerializeField] WeaponDescription weaponData;
        WeaponState state;
        SpaceShip origin;

        public void SetWeapon(WeaponDescription weaponData, WeaponState stateData, SpaceShip origin)
        {
            this.weaponData = weaponData;
            this.state = stateData;
            this.origin = origin;
            StartCoroutine(WeaponInitAsync());
        }

        IEnumerator WeaponInitAsync()
        {
            //if defensive then look for mesh tag and make child of this to it
            //and then compute until we hit an asteroid, if yes then reduce amount
            //when amount is less than zero or equal, we destroy or send weapon to pool manager

            //if however it is offensive then simply add force with parameter towards ship's forward direction
            //and then compute until we hit asteroid or destroyer tag. In both case, we simply destroy this actor through pool manager

            yield return null;
            
        }

        protected override void OnCleanupActor()
        {
            base.OnCleanupActor();
            StopAllCoroutines();
            weaponData = null;
            state = null;
            origin = null;
        }

        protected override void UpdateActor(float dt, float fixedDt)
        {
            //throw new System.NotImplementedException();
        }

        protected override void UpdateActorPhysics(float dt, float fixedDt)
        {
            //throw new System.NotImplementedException();
        }
    }
}