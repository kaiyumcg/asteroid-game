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
        [SerializeField] UnityEvent onShoot, onStartAcceleration, onStopAcceleration, onStartTurn, onStopTurn;
        [SerializeField] InputAction shootInput, accelerationInput, turnInput;
        public bool isAccelerating = false, isTurning = false;
        float turnValue = 0.0f;

        public UnityEvent OnShoot { get { return onShoot; } }
        public UnityEvent OnStartAcceleration { get { return onStartAcceleration; } }
        public UnityEvent OnStopAcceleration { get { return onStopAcceleration; } }
        public UnityEvent OnStartTurn { get { return onStartTurn; } }
        public UnityEvent OnStopTurn { get { return onStopTurn; } }
        public bool IsAccelerating { get { return isAccelerating; } set { isAccelerating = value; } }
        public bool IsTurning { get { return isTurning; }  set { isTurning = value; } }
        public float TurnValue { get { return turnValue; }  set { turnValue = value; } }

        public override IPlayerController PlayerController { get => playerController; set => playerController = value; }
        
        public InputAction ShootInput { get { return shootInput; } }
        public InputAction AccelerationInput { get { return accelerationInput; } }
        public InputAction TurnInput { get { return turnInput; } }


        public void ShootOffensive()
        {
            
        }
    }
}