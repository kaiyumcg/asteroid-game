using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace AsteroidGame.Data
{
    [CreateAssetMenu(fileName = "Spaceship", menuName = "Asteroid Game/Create Spaceship Description", order = 1)]
    public class SpaceShipDescription : ScriptableObject
    {
        [SerializeField] float turnSpeed = 10f;
        [SerializeField] float acceleration = 5f;
        [SerializeField] float maxSpeed = 20f;
        [SerializeField] float turnInputModifier = 1.0f;
        [SerializeField] float noAccelerationDragTime = 0.5f;

        public float TurnSpeed { get { return turnSpeed; } }
        public float Acceleration { get { return acceleration; } }
        public float TurnInputModifier { get { return turnInputModifier; } }
        public float MaxSpeed { get { return maxSpeed; } }
        public float NoAccelerationDragTime { get { return noAccelerationDragTime; } }
    }
}