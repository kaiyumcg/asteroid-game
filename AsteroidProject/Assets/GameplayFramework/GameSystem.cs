using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameplayFramework
{
    public abstract class GameSystem : MonoBehaviour
    {
        protected internal abstract void InitSystem();
        protected internal abstract IEnumerator InitSystemAsync();
        protected internal abstract void UpdateSystem();
    }
}