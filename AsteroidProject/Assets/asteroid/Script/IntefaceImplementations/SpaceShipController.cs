using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;

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

    void IPlayerController.ControlInPhysicsUpdate()
    {
        
    }

    void IPlayerController.ControlInUpdate()
    {
        
    }

    void IPlayerController.OnEndController(PlayerActor player)
    {
       
    }

    void IPlayerController.OnStartController(PlayerActor player)
    {
        _tr = player._Transform;
    }
}