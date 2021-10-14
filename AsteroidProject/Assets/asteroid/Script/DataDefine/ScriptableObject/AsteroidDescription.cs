using AsteroidGame.Actor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Data
{
    [CreateAssetMenu(fileName = "Asteroid", menuName = "Asteroid Game/Create Asteroid Description", order = 2)]
    public class AsteroidDescription : ScriptableObject
    {
        [SerializeField] int maxBreakNum = 2;
        [SerializeField] Asteroid partPrefab;
        [SerializeField] float breakableSideForce = 5f;
        [SerializeField] ForceMode breakableForceMode = ForceMode.Force;
        [SerializeField] int scoreToAddIfTotallyBroken = 10;
        [SerializeField] bool destroyOnCollisionWithShip = true;
        [SerializeField] float damageToShipOnCollision = 10;
        [SerializeField] bool divideOnCollisionWithShip = true;

        public int MaxBreakNum { get { return maxBreakNum; } }
        public Asteroid PartPrefab { get { return partPrefab; } }
        public float BreakableSideForce { get { return breakableSideForce; } }
        public ForceMode BreakableForceMode { get { return breakableForceMode; } }
        public int ScoreToAddIfTotallyBroken { get { return scoreToAddIfTotallyBroken; } }
        public bool DestroyOnCollisionWithShip { get { return destroyOnCollisionWithShip; } }
        public float DamageToShipOnCollision { get { return damageToShipOnCollision; } }
        public bool DivideOnCollisionWithShip { get { return divideOnCollisionWithShip; } }
    }
}