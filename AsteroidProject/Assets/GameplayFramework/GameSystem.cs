using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// Game System Interface
/// </summary>
namespace GameplayFramework
{
    public abstract class GameSystem : MonoBehaviour
    {
        protected internal abstract void InitSystem();
        protected internal abstract IEnumerator InitSystemAsync();
        protected internal abstract void UpdateSystem();
    }
}