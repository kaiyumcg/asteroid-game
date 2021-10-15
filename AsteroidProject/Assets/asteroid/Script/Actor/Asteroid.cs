using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Manager;
using AsteroidGame.Data;
using AsteroidGame.Components;

namespace AsteroidGame.Actor
{
    public class Asteroid : AIActor
    {
        int curBreakNum = 0;
        
        [SerializeReference] [SerializeReferenceButton] IAIController AI_Con;
        public int CurrentBreakNumber { get { return curBreakNum; } set { curBreakNum = value; } }
        [SerializeField] AsteroidDescription description;
        public AsteroidDescription Description { get { return description; } }

        Rigidbody rgd;
        PoolManager poolMan;
        public override IAIController AI_Controller { get => AI_Con; set => AI_Con = value; }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            rgd = GetComponent<Rigidbody>();
            poolMan = FindObjectOfType<PoolManager>();
        }

        public void BreakIt(out bool ShouldScore)
        {
            rgd.isKinematic = true;
            rgd.velocity = Vector3.zero;

            ShouldScore = false;
            curBreakNum++;
            if (curBreakNum >= description.MaxBreakNum)
            {
                ShouldScore = true;
                return;
            }

            var rnd = GetComponentInChildren<MeshRenderer>();
            var bDist = rnd.bounds.extents * 0.5f;
            var pos = _Transform.position;
            var pos1 = pos;
            pos1.x += bDist.x;
            pos1.z += bDist.z;

            var pos2 = pos;
            pos2.x -= bDist.x;
            pos2.z -= bDist.z;

            var dir = Vector3.Cross(Vector3.up, transform.forward);

            var curScale = _Transform.localScale;
            GameObject cloned1G = null;
            var cloned1Tr = poolMan.Clone(description.PartPrefab.transform, out cloned1G);
            var cloned1 = cloned1Tr.GetComponent<Asteroid>();
            cloned1.transform.position = pos1;
            cloned1.transform.localScale = curScale * 0.5f;
            cloned1.CurrentBreakNumber = curBreakNum;
            var force1 = description.BreakableSideForce * dir.normalized;
            force1.y = 0.0f;
            cloned1.rgd.isKinematic = false;
            cloned1.rgd.velocity = Vector3.zero;
            cloned1.rgd.AddForce(force1, description.BreakableForceMode);

            GameObject cloned2G = null;
            var cloned2Tr = poolMan.Clone(description.PartPrefab.transform, out cloned2G);
            var cloned2 = cloned2Tr.GetComponent<Asteroid>();
            cloned2.transform.position = pos2;
            cloned2.transform.localScale = curScale * 0.5f;
            cloned2.CurrentBreakNumber = curBreakNum;
            var force2 = description.BreakableSideForce * -dir.normalized;
            force2.y = 0.0f;
            cloned2.rgd.isKinematic = false;
            cloned2.rgd.velocity = Vector3.zero;
            cloned2.rgd.AddForce(force2, description.BreakableForceMode);
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
        }

        private void OnCollisionEnter(Collision collision)
        {
            var ship = collision.transform.GetComponentInParent<SpaceShip>();
            if (ship != null)
            {
                var weaponUser = ship.GetGameplayComponent<WeaponUser>();
                if (weaponUser != null)
                {
                    if (weaponUser.HasAnyDefensive())
                    {
                        return;
                    }
                }
                ship.DoDamage(description.DamageToShipOnCollision);
                if (description.DestroyOnCollisionWithShip && ship.IsDead == false)
                {
                    if (description.DivideOnCollisionWithShip)
                    {
                        bool shouldICare = false;
                        BreakIt(out shouldICare);
                    }

                    StopAllCoroutines();
                    OneHitKill();
                    poolMan.Free(_Transform);
                }
            }
        }
    }
}