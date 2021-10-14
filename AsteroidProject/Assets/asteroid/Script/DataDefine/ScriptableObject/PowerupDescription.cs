using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AsteroidGame.Data;

namespace AsteroidGame.Data
{
    [CreateAssetMenu(fileName = "Powerup", menuName = "Asteroid Game/Create Powerup Description", order = 4)]
    public class PowerupDescription : ScriptableObject
    {
        [SerializeField] Sprite icon;
        [Header("Health Section")]
        [SerializeField] bool useHealth = false;
        [SerializeField] float addHealthAmount = 10f;

        [Header("Player Controller Section")]
        [SerializeField] bool usePlayerController = false;
        [SerializeField] SpaceShipDescription shipDescription;

        [Header("Weapon Section")]
        [SerializeField] bool useWeapon = true;
        [SerializeField] WeaponGainMode weaponUsageMode;
        [SerializeField] WeaponDescription weaponDescription;

        public bool UseHealth { get { return useHealth; } }
        public bool UsePlayerController { get { return usePlayerController; } }
        public bool UseWeapon { get { return useWeapon; } }

        public float AddHealthAmount { get { return addHealthAmount; } }
        public SpaceShipDescription ShipDescription { get { return shipDescription; } }
        public WeaponDescription WeaponDescription { get { return weaponDescription; } }
        public WeaponGainMode WeaponUsageMode { get { return weaponUsageMode; } }
        public Sprite Icon { get { return icon; } }
    }
}