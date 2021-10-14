using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Actor;
using UnityEngine.InputSystem;
using AsteroidGame.Data;
using AsteroidGame.Manager;

namespace AsteroidGame.InterfaceImpl
{
    /// <summary>
    /// Note:
    /// Controller class for player spaceship. Note that we could have placed the input actions in this class instead of Spaceship
    /// We place minimum amount of serializable fields in interface implimentation due to bug(s) in 'SerializeReference' attribute.
    /// Consider a tour from here: https://forum.unity.com/threads/serializereference-genericserializedreferenceinspectorui.813366/
    /// And here as well: https://forum.unity.com/threads/serializereference-attribute.678868/
    /// </summary>
    public class SpaceShipController : IPlayerController
    {
        Rigidbody rgd;
        Rigidbody2D rgd2D;
        Transform _tr;
        Animator anim;

        Rigidbody IPlayerController.PlayerRigidbody => rgd;
        Rigidbody2D IPlayerController.PlayerRigidbody2D => rgd2D;
        Transform IPlayerController.PlayerTransform => _tr;
        Animator IPlayerController.PlayerAnimator => anim;

        SpaceShip ship;
        PlayerActor playerActor;
        [SerializeField] SpaceShipDescription shipDescription;
        TaskManager taskMan;

        void IPlayerController.ControlInPhysicsUpdate(float dt, float fixedDt)
        {
            if (ship.IsAccelerating)
            {
                var fFactor = (shipDescription.MaxSpeed - rgd.velocity.magnitude) / shipDescription.MaxSpeed;
                var force = _tr.forward * fFactor * shipDescription.Acceleration;
                rgd.AddForce(force, ForceMode.Force);
            }
            else
            {
                taskMan.StartTask(GraduallyLowerSpeed(shipDescription.NoAccelerationDragTime));
            }
        }

        void IPlayerController.ControlInUpdate(float dt, float fixedDt)
        {
            if (ship.IsTurning)
            {
                _tr.Rotate(0f, shipDescription.TurnSpeed * dt * ship.TurnValue, 0f);
            }
        }

        IEnumerator GraduallyLowerSpeed(float withIn = 0.5f)
        {
            var dir = rgd.velocity.normalized;
            var speed = rgd.velocity.magnitude;

            var lowerSpeed = speed / withIn;
            while (true)
            {
                speed -= lowerSpeed * Time.deltaTime;
                if (speed <= 0.0f || ship.IsAccelerating)
                {
                    if (ship.IsAccelerating == false)
                    {
                        rgd.velocity = Vector3.zero;
                    }
                    
                    break;
                }
                var vel = dir * speed;
                rgd.velocity = vel;

                yield return null;
            }
        }

        void IPlayerController.OnEndController(PlayerActor player)
        {
            ship.ShootInput.Disable();
            ship.AccelerationInput.Disable();
            ship.TurnInput.Disable();
        }

        void IPlayerController.OnStartController(PlayerActor player)
        {
            taskMan = Object.FindObjectOfType<TaskManager>();
            _tr = player._Transform;
            rgd = player.GetComponent<Rigidbody>();
            playerActor = player;
            ship = (SpaceShip)player;

            if (ship == null)
            {
                throw new System.Exception("This player controller is only meant to be used for SpaceShip Actor! " +
                    "Incompatible controller error!");
            }

            ship.ShootInput.performed += Shoot;

            ship.AccelerationInput.performed += AccelerationStart;
            ship.AccelerationInput.canceled += AccelerationStop;

            ship.TurnInput.performed += TurnStart;
            ship.TurnInput.canceled += TurnStop;

            ship.ShootInput.Enable();
            ship.AccelerationInput.Enable();
            ship.TurnInput.Enable();
        }

        void TurnStart(InputAction.CallbackContext context)
        {
            ship.TurnValue = context.ReadValue<float>();
            ship.IsTurning = true;
            ship.OnStartTurn?.Invoke();
        }

        void TurnStop(InputAction.CallbackContext context)
        {
            ship.TurnValue = 0.0f;
            ship.IsTurning = false;
            ship.OnStopTurn?.Invoke();
        }

        void AccelerationStart(InputAction.CallbackContext context)
        {
            ship.IsAccelerating = true;
            ship.OnStartAcceleration?.Invoke();
        }

        void AccelerationStop(InputAction.CallbackContext context)
        {
            ship.IsAccelerating = false;
            ship.OnStopAcceleration?.Invoke();
        }

        void Shoot(InputAction.CallbackContext context)
        {
            ship.OnShoot?.Invoke();
            ship.ShootOffensive();
        }
    }
}