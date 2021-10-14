using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameplayFramework;
using UnityEngine.AI;

namespace AsteroidGame.InterfaceImpl
{
    /// <summary>
    /// If we need(which we do not at this moment) any sophesticated AI for asteroid(perhaps alien hideout asteroid), 
    /// then we use this controller and assign it to any AIActor
    /// Right now it is left out for future
    /// </summary>
    public class AsteroidController : IAIController
    {
        Rigidbody rgd;
        Rigidbody2D rgd2D;
        Transform _tr;
        Animator anim;
        NavMeshAgent agent;

        Rigidbody IAIController.AIRigidbody => rgd;
        Rigidbody2D IAIController.AIRigidbody2D => rgd2D;
        Transform IAIController.AITransform => _tr;
        Animator IAIController.AIAnimator => anim;
        NavMeshAgent IAIController.AIAgent => agent;

        void IAIController.ControlInPhysicsUpdate(float dt, float fixedDt)
        {
            throw new System.NotImplementedException();
        }

        void IAIController.ControlInUpdate(float dt, float fixedDt)
        {
            throw new System.NotImplementedException();
        }

        void IAIController.OnEndController(AIActor ai)
        {
            throw new System.NotImplementedException();
        }

        void IAIController.OnStartController(AIActor ai)
        {
            throw new System.NotImplementedException();
        }
    }
}