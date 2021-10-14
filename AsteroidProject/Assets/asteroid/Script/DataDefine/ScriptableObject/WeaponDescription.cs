using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidGame.Actor;

namespace AsteroidGame.Data
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Asteroid Game/Create New Weapon", order = 3)]
    public class WeaponDescription : ScriptableObject
    {
        [SerializeField] bool isOffensive = true;
        [SerializeField] int amount = 100;
        [SerializeField] bool isInfinite = true, neverDeplete = false;
        [SerializeField] Weapon weaponPrefab;
        [SerializeField] float lifeTimeAfterStart = 10f;
        [SerializeField] bool burstAble = false;
        [SerializeField] int burstCount = 3;
        [SerializeField] float burstGapTime = 0.2f;
        [SerializeField] float damageToOpponentIfHit = 4f;
        [SerializeField] float damageToSelfIfHit = 3f;
        [SerializeField] float maxSpeedIfProjectile = 5f;
        [SerializeField] float shootForceIfProjectile = 2f;
        [SerializeField] Sprite weaponIcon;

        public bool IsOffensive { get { return isOffensive; } }
        public int Amount { get { return amount; } }
        public bool IsInfinite { get { return isInfinite; } }
        public Weapon WeaponPrefab { get { return weaponPrefab; } }
        public float LifeTimeAfterStart { get { return lifeTimeAfterStart; } }
        public int BurstCount { get { return burstCount; } }
        public float BurstGapTime { get { return burstGapTime; } }
        public bool BurstAble { get { return burstAble; } }
        public float DamageToOpponentIfHit { get { return damageToOpponentIfHit; } }
        public float MaxSpeedIfProjectile { get { return maxSpeedIfProjectile; } }
        public float ShootForceIfProjectile { get { return shootForceIfProjectile; } }
        public Sprite WeaponIcon { get { return weaponIcon; } }
        public float DamageToSelfIfHit { get { return damageToSelfIfHit; } }
        public bool WillEverDeplete { get { return !neverDeplete; } }
    }
}