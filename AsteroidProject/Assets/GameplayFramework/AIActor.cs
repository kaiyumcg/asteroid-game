using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace GameplayFramework
{
    public abstract class AIActor : GameActor
    {
        [SerializeReference] [SerializeReferenceButton] IAIController AI_Con;
        public IAIController AI_Controller { get { return AI_Con; } set { AI_Con = value; } }

        protected override void UpdateActor()
        {
            AI_Con.ControlInUpdate();
        }

        protected override void UpdateActorPhysics()
        {
            AI_Con.ControlInPhysicsUpdate();
        }
    }
}