using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public interface IPlayerController
    {
        Rigidbody PlayerRigidbody { get; }
        Rigidbody2D PlayerRigidbody2D { get; }
        Transform PlayerTransform { get; }
        Animator PlayerAnimator { get; }
        void ControlInUpdate();
        void ControlInPhysicsUpdate();
    }
}