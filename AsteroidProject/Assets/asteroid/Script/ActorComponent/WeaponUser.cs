using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;

namespace AsteroidGame.Components
{
    public class WeaponState
    {
        int amount;

        public WeaponState(WeaponDescription weapon, WeaponValidityCondition validityCondition)
        {
            amount = 0;
        }

        public bool ShouldUse { get; }


    }

    public enum WeaponGainMode { UseExclusively = 0, UseOtherWhenThisIsDone = 1, UseWithOther = 2}
    public delegate bool WeaponValidityCondition();

    public class WeaponUser : GameplayComponent
    {
        [Header("Base Setting of Weapon Usage")]
        [Header("Weapon will be consumed one after another")]
        [SerializeField] List<WeaponDescription> baseWeapons;
        [SerializeField] bool shouldUseMultiDefense = false, shouldUseMultiOffence = false, removeInvalidWeaponsWheneverFound = false;

        //might give serialization support to linkedlist with custom editor coding and then use linkedlist later for 'weapons'
        List<WeaponState> weapons;
        protected override void AwakeComponent()
        {
            base.AwakeComponent();
            weapons = new List<WeaponState>();
            if (baseWeapons != null && baseWeapons.Count > 0)
            {
                for (int i = 0; i < baseWeapons.Count; i++)
                {
                    var wp = baseWeapons[i];
                    weapons.Add(new WeaponState(wp, null));
                }
            }
        }

        public void UseOffensive()
        {
            //fire an offensive from the weapon list. First come first serve. If found then do not fire any more
            //Start the first defensive

            if (shouldUseMultiOffence)
            {
                //fire all valid offence
                //for burst use coroutine
            }
            else
            {
                //fire the first valid
                //for burst use coroutine
            }

            //if 'removeInvalidWeaponsWheneverFound' is true then below
            //set to null for invalid
            //remove them
        }

        public void UseDefensive(WeaponState weaponState = null)
        {
            //start the first inactive defensive if no other defensive is in process depending upon bool flag or argument
        }

        //called by powerup system
        public void AddWeapon(WeaponDescription weapon,
            WeaponGainMode mode = WeaponGainMode.UseExclusively, WeaponValidityCondition validityCondition = null)
        {
            var wp = new WeaponState(weapon, validityCondition);
            if (weapons == null) { weapons = new List<WeaponState>(); }

            if (mode == WeaponGainMode.UseExclusively)
            {
                weapons.Add(wp);
            }
            else
            {
                if (mode == WeaponGainMode.UseOtherWhenThisIsDone)
                {
                    weapons.Insert(0, wp);
                }
                else if (mode == WeaponGainMode.UseWithOther)
                {
                    weapons.Add(wp);
                }
            }

            if (weapon.IsOffensive == false)
            {
                UseDefensive(wp);
            }
        }

        protected override void UpdateComponent(float dt, float fixedDt)
        {
            
        }

        protected override void UpdateComponentPhysics(float dt, float fixedDt)
        {
            
        }
    }
}