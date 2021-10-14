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
        [SerializeField] int offensiveCount = 100;
        [SerializeField] bool isInfinite = true;
        [SerializeField] PlayerWeapon weaponPrefab;
        [SerializeField] float lifeTimeAfterStart = 10f;
        [SerializeField] int offensiveBurstCount = 3;

        public bool IsOffensive { get { return isOffensive; } }
        public int OffensiveCount { get { return offensiveCount; } }
        public bool IsInfinite { get { return isInfinite; } }
        public PlayerWeapon WeaponPrefab { get { return weaponPrefab; } }
        public float LifeTimeAfterStart { get { return lifeTimeAfterStart; } }
        public int OffensiveBurstCount { get { return offensiveBurstCount; } }
    }
}