using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Events;

namespace AsteroidGame.Actor
{
    /// <summary>
    /// Uses the description file to construct spaceship actor features. 
    /// Has input handling and player control feature along with everything related to player
    /// </summary>
    public class SpaceShip : PlayerActor
    {
        [Header("SpaceShip Setting")]
        [SerializeReference] [SerializeReferenceButton] IPlayerController playerController;
        [SerializeField] InputAction shootInput, accelerationInput, turnInput;

        [SerializeField] UnityEvent onShoot, onStartAcceleration, onStopAcceleration, onStartTurn, onStopTurn;
        bool isAccelerating = false, isTurning = false;
        float turnValue = 0.0f;

        public UnityEvent OnShoot { get { return onShoot; } }
        public UnityEvent OnStartAcceleration { get { return onStartAcceleration; } }
        public UnityEvent OnStopAcceleration { get { return onStopAcceleration; } }
        public UnityEvent OnStartTurn { get { return onStartTurn; } }
        public UnityEvent OnStopTurn { get { return onStopTurn; } }
        public bool IsAccelerating { get { return isAccelerating; } }
        public bool IsTurning { get { return isTurning; } }
        public float TurnValue { get { return turnValue; } }

        public override IPlayerController PlayerController { get => playerController; set => playerController = value; }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            shootInput.performed += Shoot;

            accelerationInput.performed += AccelerationStart;
            accelerationInput.canceled += AccelerationStop;

            turnInput.performed += TurnStart;
            turnInput.canceled += TurnStop;
        }

        protected override void StartOrSpawnActor()
        {
            base.StartOrSpawnActor();
            shootInput.Enable();
            accelerationInput.Enable();
            turnInput.Enable();
        }

        protected override void OnDisableActor()
        {
            base.OnDisableActor();
            shootInput.Disable();
            accelerationInput.Disable();
            turnInput.Disable();
        }

        void TurnStart(InputAction.CallbackContext context)
        {
            turnValue = context.ReadValue<float>();
            isTurning = true;
            onStartTurn?.Invoke();
        }

        void TurnStop(InputAction.CallbackContext context)
        {
            turnValue = 0.0f;
            isTurning = false;
            onStopTurn?.Invoke();
        }

        void AccelerationStart(InputAction.CallbackContext context)
        {
            isAccelerating = true;
            onStartAcceleration?.Invoke();
        }

        void AccelerationStop(InputAction.CallbackContext context)
        {
            isAccelerating = false;
            onStopAcceleration?.Invoke();
        }

        void Shoot(InputAction.CallbackContext context)
        {
            onShoot?.Invoke();
        }
    }
}