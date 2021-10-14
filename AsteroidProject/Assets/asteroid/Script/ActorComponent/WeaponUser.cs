using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;
using AsteroidGame.Manager;
using AsteroidGame.Actor;

namespace AsteroidGame.Components
{
    public class WeaponUser : GameplayComponent
    {
        [Header("Base Setting of Weapon Usage")]
        [Header("Weapon will be consumed one after another")]
        [SerializeField] List<WeaponDescription> baseWeapons;
        [SerializeField] bool shouldUseMultiDefense = false, shouldUseMultiOffence = false, useBaseWhenWeaponDepleted = true;

        //might give serialization support to linkedlist with custom editor coding and then use linkedlist later for 'weapons'
        List<WeaponState> weapons;
        bool weaponDirty = false;
        PoolManager poolMan;
        SpaceShip ownerShip;

        protected override void AwakeComponent()
        {
            poolMan = FindObjectOfType<PoolManager>();
            ownerShip = (SpaceShip)Owner;
            base.AwakeComponent();
            LoadBases();
        }

        void LoadBases()
        {
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

        public void UseWeapon(WeaponType weaponType, WeaponState weaponState = null)
        {
            if (weapons != null && weapons.Count > 0)
            {
                if (weaponState != null)
                {
                    UseWeaponCore(weaponState, true);
                }
                else
                {
                    if (weaponType == WeaponType.Offensive)
                    {
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            var wp = weapons[i];
                            if (wp.Weapon.IsOffensive)
                            {
                                UseWeaponCore(wp, true);
                                if (wp.IsValid && shouldUseMultiOffence == false)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else if (weaponType == WeaponType.Defensive)
                    {
                        for (int i = 0; i < weapons.Count; i++)
                        {
                            var wp = weapons[i];
                            if (wp.Weapon.IsOffensive == false)
                            {
                                UseWeaponCore(wp, true);
                                if (wp.IsValid && shouldUseMultiDefense == false)
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            PostUsageCleanup();

            void PostUsageCleanup()
            {
                weaponDirty = true;
                if (weapons != null)
                {
                    weapons.RemoveAll((wp) => { return wp != null && wp.IsValid == false; });
                }

                if (weapons != null)
                {
                    weapons.RemoveAll((wp) => { return wp == null; });
                }

                if (weapons == null || weapons.Count == 0)
                {
                    weapons = new List<WeaponState>();
                }

                if (useBaseWhenWeaponDepleted && weapons.Count == 0)
                {
                    LoadBases();
                }
                weaponDirty = false;
            }

            IEnumerator DoBurst(WeaponState wp, int count, float gap)
            {
                var fCount = 0;
                while (true)
                {
                    if (fCount >= count)
                    {
                        yield break;
                    }
                    yield return new WaitForSeconds(gap);
                    UseWeaponCore(wp, false);
                    fCount++;
                }
            }

            void UseWeaponCore(WeaponState wp, bool useBurst)
            {
                if (wp == null) { return; }
                if (wp.IsActivated == false) { wp.Activate(); }
                if (wp.IsValid)
                {
                    var pMan = poolMan;
                    wp.UseWeapon(poolMan, ownerShip);

                    if (wp.Weapon.BurstAble && useBurst)
                    {
                        StartCoroutine(DoBurst(wp, wp.Weapon.BurstCount, wp.Weapon.BurstGapTime));
                    }
                }
            }
        }

        //called by powerup system
        public void AddWeapon(WeaponDescription weapon,
            WeaponGainMode mode = WeaponGainMode.UseExclusively, WeaponValidityCondition validityCondition = null)
        {
            weaponDirty = true;
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
                UseWeapon(WeaponType.Defensive, wp);
            }
            weaponDirty = false;
        }

        protected override void UpdateComponent(float dt, float fixedDt)
        {
            if (weaponDirty) { return; }
            if (weapons != null && weapons.Count > 0)
            {
                for (int i = 0; i < weapons.Count; i++)
                {
                    var weapon = weapons[i];
                    weapon.UpdateState(dt);
                }
            }
        }

        protected override void UpdateComponentPhysics(float dt, float fixedDt)
        {
            
        }
    }
}