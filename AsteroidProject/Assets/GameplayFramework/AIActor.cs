using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// AI Actor class
/// </summary>
namespace GameplayFramework
{
    public abstract class AIActor : GameActor
    {
        public abstract IAIController AI_Controller { get; set; }
        protected virtual void OnDisableActor() { }

        private void OnDisable()
        {
            OnDisableActor();
            if (AI_Controller != null)
            {
                AI_Controller.OnEndController(this);
            }
        }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            if (AI_Controller != null)
            {
                AI_Controller.OnStartController(this);
            }
        }

        protected override void UpdateActor(float dt, float fixedDt)
        {
            if (AI_Controller != null)
            {
                AI_Controller.ControlInUpdate(dt, fixedDt);
            }
        }

        protected override void UpdateActorPhysics(float dt, float fixedDt)
        {
            if (AI_Controller != null)
            {
                AI_Controller.ControlInPhysicsUpdate(dt, fixedDt);
            }
        }
    }
}