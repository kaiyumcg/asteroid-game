using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AsteroidGame.Manager
{
    public abstract class GameSystem : MonoBehaviour
    {
        internal abstract void InitSystem();
        internal abstract IEnumerator InitSystemAsync();
        internal abstract void UpdateSystem();
    }
}