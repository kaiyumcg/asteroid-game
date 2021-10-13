using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Author: Md. Al Kaiyum(Rumman)
/// Email: kaiyumce06rumman@gmail.com
/// Gameplay Component Interface.
/// </summary>
namespace GameplayFramework
{
    public abstract class GameplayComponent : MonoBehaviour
    {
        protected internal virtual void OnStartOrSpawnActor() { }
        protected internal virtual void AwakeComponent() { }
        protected internal abstract void UpdateComponent();
        protected internal abstract void UpdateComponentPhysics();
        protected virtual void OnEditorUpdate() { }

        private void OnValidate()
        {
            OnEditorUpdate();
        }
    }
}