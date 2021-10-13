using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using AsteroidGame.Actor;

namespace AsteroidGame.InterfaceImpl
{
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

        void IPlayerController.ControlInPhysicsUpdate()
        {

        }

        void IPlayerController.ControlInUpdate()
        {
            if (ship.IsTurning)
            {
                Debug.Log("turn amount: " + ship.TurnValue);
            }

            if (ship.IsAccelerating)
            {
                Debug.Log("go forward!");
            }
        }

        void IPlayerController.OnEndController(PlayerActor player)
        {

        }

        void IPlayerController.OnStartController(PlayerActor player)
        {
            _tr = player._Transform;
            playerActor = player;
            ship = (SpaceShip)player;

            if (ship == null)
            {
                throw new System.Exception("This player controller is only meant to be used for SpaceShip Actor! " +
                    "Incompatible controller error!");
            }

            ship.OnShoot.AddListener(Shoot);

            ship.OnStartTurn.AddListener(StartTurn);
            ship.OnStopTurn.AddListener(StopTurn);

            ship.OnStartAcceleration.AddListener(StartForward);
            ship.OnStopAcceleration.AddListener(StopForward);
        }

        void Shoot()
        {
            Debug.Log("shoot it!");
        }

        void StartTurn()
        {
            
        }

        void StopTurn()
        {
            
        }

        void StartForward()
        {
            
        }

        void StopForward()
        {

        }
    }
}