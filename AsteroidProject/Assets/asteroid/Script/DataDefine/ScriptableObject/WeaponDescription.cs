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
        [SerializeField] float speedIfProjectile = 5f;
        [SerializeField] Sprite weaponIcon;

        [Header("Burst Setting")]
        [SerializeField] bool burstAble = false;
        [SerializeField] int burstCount = 3;
        [SerializeField] float burstGapTime = 0.2f;

        [Header("Opponent Damage Setting")]
        [SerializeField] int damageToOpponentIfHit = 4;
        [SerializeField] bool oneHitKillOpponentIfHit = true;

        [Header("Self Damage Setting")]
        [SerializeField] int damageToSelfIfHit = 3;
        [SerializeField] bool oneHitKillSelfIfHit = true;

        public bool IsOffensive { get { return isOffensive; } }
        public int Amount { get { return amount; } }
        public bool IsInfinite { get { return isInfinite; } }
        public Weapon WeaponPrefab { get { return weaponPrefab; } }
        public float LifeTimeAfterStart { get { return lifeTimeAfterStart; } }
        public int BurstCount { get { return burstCount; } }
        public float BurstGapTime { get { return burstGapTime; } }
        public bool BurstAble { get { return burstAble; } }
        public int DamageToOpponentIfHit { get { return damageToOpponentIfHit; } }
        public float SpeedIfProjectile { get { return speedIfProjectile; } }
        public Sprite WeaponIcon { get { return weaponIcon; } }
        public int DamageToSelfIfHit { get { return damageToSelfIfHit; } }
        public bool WillEverDeplete { get { return !neverDeplete; } }
        public bool OneHitKillOpponentIfHit { get { return oneHitKillOpponentIfHit; } }
        public bool OneHitKillSelfIfHit { get { return oneHitKillSelfIfHit; } }
    }
}