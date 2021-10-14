using AsteroidGame.Actor;
using AsteroidGame.Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Data
{
    public enum WeaponGainMode { UseExclusively = 0, UseOtherWhenThisIsDone = 1, UseWithOther = 2, UseOnly = 3 }
    public delegate bool WeaponValidityCondition();

    public class WeaponState
    {
        bool def_used;
        int amount;
        WeaponValidityCondition validityCondition;
        WeaponDescription weapon;
        float timer;
        bool active;
        public int Amount { get { return amount; } }
        public WeaponDescription Weapon { get { return weapon; } }
        public void DepleteDefensiveAmount(int amount)
        {
            if (weapon.IsOffensive)
            {
                throw new System.Exception("This is offensive weapon, you can not deplete its amount from outside! Invalid operation.");
            }
            this.amount -= Mathf.Abs(amount);
        }

        public WeaponState(WeaponDescription weapon, WeaponValidityCondition validityCondition)
        {
            this.amount = weapon.Amount;
            this.active = false;
            this.weapon = weapon;
            this.validityCondition = validityCondition;
            this.timer = 0.0f;
            this.def_used = false;
        }

        public void UseWeapon(PoolManager poolMan, SpaceShip origin)
        {
            if (def_used) 
            {
                throw new System.Exception("You can not use a defensive weapon that has already being consumed! " +
                    "To add another defensive weapon, please consider adding a weapon through 'weaponuser' component's AddWeapon Method!");
            }

            if (def_used == false && weapon.IsOffensive == false)
            {
                def_used = true;
            }

            active = true;
            if (weapon.IsOffensive)
            {
                amount--;
            }

            Weapon weaponCloned = null;
            if (poolMan == null)
            {
                weaponCloned = Object.Instantiate(weapon.WeaponPrefab) as Weapon;
            }
            else
            {
                GameObject cl = null;
                poolMan.Clone(weapon.WeaponPrefab.transform, out cl);
                weaponCloned = cl.GetComponent<Weapon>();
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
                    if (weapon.WillEverDeplete)
                    {
                        return timer < weapon.LifeTimeAfterStart && fromCondition;
                    }
                    else
                    {
                        return fromCondition;
                    }
                }
                else
                {
                    return amount > 0 && fromCondition;
                }
            }
        }
    }
}