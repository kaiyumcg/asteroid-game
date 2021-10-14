using AsteroidGame.Actor;
using AsteroidGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Data
{
    public enum WeaponGainMode { UseExclusively = 0, UseOtherWhenThisIsDone = 1, UseWithOther = 2 }
    public delegate bool WeaponValidityCondition();

    public class WeaponState
    {
        int amount;
        WeaponValidityCondition validityCondition;
        WeaponDescription weapon;
        float timer;
        bool active;
        public int Amount { get { return amount; } set { amount = value; } }
        public WeaponDescription Weapon { get { return weapon; } }

        public WeaponState(WeaponDescription weapon, WeaponValidityCondition validityCondition)
        {
            this.amount = weapon.Amount;
            this.active = false;
            this.weapon = weapon;
            this.validityCondition = validityCondition;
            this.timer = 0.0f;
        }

        public void UseWeapon(PoolManager poolMan, SpaceShip origin)
        {
            if (weapon.IsOffensive)
            {
                amount--;
            }

            PlayerWeapon weaponCloned = null;
            if (poolMan == null)
            {
                weaponCloned = Object.Instantiate(weapon.WeaponPrefab) as PlayerWeapon;
            }
            else
            {
                GameObject cl = null;
                poolMan.Clone(weapon.WeaponPrefab.transform, out cl);
                weaponCloned = cl.GetComponent<PlayerWeapon>();
            }

            weaponCloned.transform.position = origin.WeaponSpawnOrigin.position;
            weaponCloned.SetWeapon(weapon, this, origin);
        }

        public void UpdateState(float dt)
        {
            if (active == false) { return; }
            timer += dt;
        }

        public bool IsActivated { get { return active; } }
        public void Activate() { if (active) { return; } active = true; }

        public bool IsValid
        {
            get
            {
                var fromCondition = true;
                if (validityCondition != null)
                {
                    fromCondition = validityCondition.Invoke();
                }

                if (weapon.IsInfinite)
                {
                    return timer < weapon.LifeTimeAfterStart && fromCondition;
                }
                else
                {
                    return amount > 0 && fromCondition;
                }
            }
        }
    }
}