using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Events;
using AsteroidGame.Components;

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
        [SerializeField] Transform weaponSpawnOrigin;
        bool isAccelerating = false, isTurning = false;
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
        public Transform WeaponSpawnOrigin { get { return weaponSpawnOrigin; } }

        List<WeaponUser> weaponHandlers;
        Rigidbody rgd;

        protected override void AwakeActor()
        {
            base.AwakeActor();
            weaponHandlers = GetGameplayComponents<WeaponUser>();
            if (weaponHandlers == null)
            {
                throw new System.Exception("SpaceShip actor must have 'Weapon User' Gameplay component(s) linked to it! ");
            }
            rgd = GetComponent<Rigidbody>();
        }

        public void ShootOffensive()
        {
            if (weaponHandlers != null && weaponHandlers.Count > 0)
            {
                for (int i = 0; i < weaponHandlers.Count; i++)
                {
                    var handler = weaponHandlers[i];
                    if (handler == null) { continue; }
                    handler.UseWeapon(Data.WeaponType.Offensive);
                }
            }
        }

        [SerializeField] float playAreaBackwardness = 5f;
        [SerializeField] LayerMask forwardRayMask, backwardRayMask;

        private void OnTriggerEnter(Collider other)
        {
            var boundary = other.transform.GetComponent<PlayAreaBoundaryTag>();
            if (boundary != null)
            {
                var ray_forward = new Ray(_Transform.position, _Transform.forward);
                var ray_backward = new Ray(_Transform.position, _Transform.forward * -1f);

                RaycastHit hit_forward, hit_backward;

                var isHit_forward = Physics.Raycast(ray_forward, out hit_forward, Mathf.Infinity, forwardRayMask);
                var isHit_backward = Physics.Raycast(ray_backward, out hit_backward, Mathf.Infinity, backwardRayMask);

                if (isHit_forward && isHit_backward)
                {
                    RaycastHit finalHit;
                    var isHit = Physics.Raycast(ray_backward, out finalHit, Mathf.Infinity, forwardRayMask);

                    if (isHit)
                    {
                        var vel = rgd.velocity;
                        var bHit_pts = finalHit.point + _Transform.forward * -playAreaBackwardness;
                        rgd.position = bHit_pts;
                        rgd.velocity = vel;
                    }
                }
            }
        }
    }
}