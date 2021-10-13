using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameplayFramework
{
    public interface IAIController
    {
        Rigidbody AIRigidbody { get; }
        Rigidbody2D AIRigidbody2D { get; }
        Transform AITransform { get; }
        Animator AIAnimator { get; }
        NavMeshAgent AIAgent { get; }

        void ControlInUpdate();
        void ControlInPhysicsUpdate();
    }
}