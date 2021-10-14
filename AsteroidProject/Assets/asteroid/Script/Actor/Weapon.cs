using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Data;
using AsteroidGame.Manager;

namespace AsteroidGame.Actor
{
    //Weapon is spawned. Now do its work i.e. collided with asterioid or destroyed or other etc
    public class Weapon : GameActor
    {
        WeaponDescription weaponData;
        WeaponState state;
        SpaceShip origin;
        Rigidbody rgd;
        PoolManager poolMan;

        protected override void AwakeActor()
        {
            base.AwakeActor();
            rgd = GetComponent<Rigidbody>();
            poolMan = FindObjectOfType<PoolManager>();
        }

        public void SetWeapon(WeaponDescription weaponData, WeaponState stateData, SpaceShip origin)
        {
            _Transform.rotation = Quaternion.LookRotation(origin._Transform.forward);
            this.weaponData = weaponData;
            this.state = stateData;
            this.origin = origin;
            StartCoroutine(WeaponInitAsync());
        }

        IEnumerator WeaponInitAsync()
        {
            var fDir = origin._Transform.forward;
            if (weaponData.IsOffensive)
            {
                while (true)
                {
                    rgd.velocity = fDir.normalized * weaponData.SpeedIfProjectile;
                    yield return null;
                }
            }
            else
            {
                var t = origin.GetComponentInChildren<MeshTag>();
                _Transform.SetParent(t.transform, true);
                _Transform.localPosition = Vector3.zero;
                _Transform.localRotation = Quaternion.Euler(90f, 0.0f, 0.0f);
                _Transform.localScale = Vector3.one;
            }

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

        void DestroyWeapon()
        {
            state.ClearState();
            StopAllCoroutines();
            OneHitKill();
            poolMan.Free(_Transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            var dest = other.transform.GetComponentInParent<DestructorTag>();
            if (dest != null)
            {
                DestroyWeapon();
            }
            else
            {
                var ast = other.transform.GetComponentInParent<Asteroid>();
                if (ast != null)
                {
                    if (weaponData.IsOffensive)
                    {
                        //divide asteroid and set their initial small speed by a small force in two direction
                        var shouldScore = false;
                        ast.BreakIt(out shouldScore);
                        if (shouldScore)
                        {
                            origin.AddScore(ast.Description.ScoreToAddIfTotallyBroken);
                        }
                    }

                    if (weaponData.OneHitKillOpponentIfHit)
                    {
                        ast.OneHitKill();
                        poolMan.Free(ast._Transform);
                    }
                    else
                    {
                        ast.DoDamage(weaponData.DamageToOpponentIfHit);
                        if (ast.IsDead)
                        {
                            poolMan.Free(ast._Transform);
                        }
                    }

                    if (weaponData.OneHitKillSelfIfHit)
                    {
                        DestroyWeapon();
                    }
                    else
                    {
                        DoDamage(weaponData.DamageToOpponentIfHit);
                        if (IsDead)
                        {
                            DestroyWeapon();
                        }
                    }
                }
            }
        }
    }
}