using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;
using AsteroidGame.Actor;
using AsteroidGame.Components;
using AsteroidGame.Manager;
using AsteroidGame.InterfaceImpl;
using UnityEngine.UI;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Use the description file to have power up features. Adds weapon file stack of the player 
    /// </summary>
    public class Powerup : GameActor
    {
        [SerializeField] SpriteRenderer iconRenderer;
        [SerializeField] PowerupDescription powerupDescription;
        PoolManager poolMan;
        protected override void AwakeActor()
        {
            base.AwakeActor();
            poolMan = FindObjectOfType<PoolManager>();
            UpdateIcon();
        }

        void UpdateIcon()
        {
            if (powerupDescription != null && iconRenderer != null)
            {
                iconRenderer.sprite = powerupDescription.Icon;
            }
        }

        protected virtual WeaponValidityCondition GetCustomPowerupWeaponValidity()
        {
            return null;
        }

        private void OnTriggerEnter(Collider other)
        {
            var dest = other.GetComponent<DestructorTag>();
            if (dest != null)
            {
                OneHitKill();
                StopAllCoroutines();
                poolMan.Free(transform);
                return;
            }

            var ship = other.GetComponentInParent<SpaceShip>();
            if (ship != null)
            {
                //give powerup
                if (powerupDescription.UseWeapon)
                {
                    var weaponUser = ship.GetGameplayComponent<WeaponUser>();
                    var handle = weaponUser.AddWeapon(powerupDescription.WeaponDescription,
                        powerupDescription.WeaponUsageMode, GetCustomPowerupWeaponValidity());
                    if (handle.Weapon.IsOffensive == false)
                    {
                        handle.UseWeapon(poolMan, ship);
                    }
                }

                if (powerupDescription.UseHealth)
                {
                    ship.AddLife(powerupDescription.AddHealthAmount);
                }

                if (powerupDescription.UsePlayerController)
                {
                    var con = (SpaceShipController)ship.PlayerController;
                    if (con != null)
                    {
                        con.ShipDescription = powerupDescription.ShipDescription;
                    }
                }

                OneHitKill();
                StopAllCoroutines();
                poolMan.Free(transform);
            }
        }
    }
}
