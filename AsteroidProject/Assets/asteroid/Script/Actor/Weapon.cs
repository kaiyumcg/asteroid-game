using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;

namespace AsteroidGame.Actor
{
    //Weapon is spawned. Now do its work i.e. collided with asterioid or destroyed or other etc
    public class Weapon : GameActor
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
            //and then compute until we hit an asteroid, if yes then one hit kill of this one and divide the asteroid into two

            //if however it is offensive then simply add force with parameter towards ship's forward direction
            //and then compute until we hit asteroid or destroyer tag. In both case, we simply destroy this actor through pool manager
            //In case of asteroid, one hit kill current asteroid. Make division and compute scale from current bound. 
            //Check if we hit smallest part then update score
            //
            
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
    }
}