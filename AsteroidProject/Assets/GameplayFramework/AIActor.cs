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
        [SerializeReference] [SerializeReferenceButton] IAIController AI_Con;
        public IAIController AI_Controller { get { return AI_Con; } set { AI_Con = value; } }
        protected virtual void OnDisableActor() { }

        private void OnDisable()
        {
            OnDisableActor();
            if (AI_Con != null)
            {
                AI_Con.OnEndController(this);
            }
        }

        protected override void AwakeActor()
        {
            base.AwakeActor();
            if (AI_Con != null)
            {
                AI_Con.OnStartController(this);
            }
        }

        protected override void UpdateActor()
        {
            if (AI_Con != null)
            {
                AI_Con.ControlInUpdate();
            }
        }

        protected override void UpdateActorPhysics()
        {
            if (AI_Con != null)
            {
                AI_Con.ControlInPhysicsUpdate();
            }
        }
    }
}